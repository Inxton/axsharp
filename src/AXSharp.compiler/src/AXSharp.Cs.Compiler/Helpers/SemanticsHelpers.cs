// AXSharp.Compiler
// Copyright (c) 2023 MTS spol. s r.o.,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/inxton/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/inxton/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/inxton/axsharp/blob/master/notices.md

using AX.ST.Semantic;
using AX.ST.Semantic.Model.Declarations;
using AX.ST.Semantic.Model.Declarations.Types;
using AXSharp.Compiler.Core;
using AXSharp.Compiler.Cs.Exceptions;
using AXSharp.Connector;

namespace AXSharp.Compiler.Cs;

/// <summary>
///     Provides a series of helpers for semantics.
/// </summary>
public static class SemanticsHelpers
{
    /// <summary>
    ///     Determines whether the member is eligible for generation.
    /// </summary>
    /// <param name="field">Field declaration</param>
    /// <param name="sourceBuilder">Source builder</param>
    /// <param name="coBuilder">Lateral builder signature</param>
    /// <returns>True when the member is eligible for generation.</returns>
    public static bool IsMemberEligibleForTranspile(this IFieldDeclaration field, ISourceBuilder sourceBuilder, string coBuilder = "")
    {
        return field.AccessModifier == AccessModifier.Public 
               && field.IsEligibleForTranspile(sourceBuilder)
               && !IsToBeOmitted(field, sourceBuilder, coBuilder);
    }


    /// <summary>
    /// Finds type declaration.
    /// </summary>
    /// <param name="compilation">Compilation object</param>
    /// <param name="typeAccess">Required type</param>
    /// <returns>Required type if found.</returns>
    public static ITypeDeclaration FindTypeDeclaration(this Compilation compilation, AX.ST.Semantic.Model.ISemanticTypeAccess? typeAccess)
    {
        // This is to resolve fully qualified type name when the type cannot be determined propeprly form the semantic tree.
        // TODO: This workaround should be removed once we can properly use project dependencies in the stc.
        if (typeAccess == null)
        {
            return null;
        }

        var fullyQualified = compilation.GetSemanticTree().Types.DistinctBy(p => p.FullyQualifiedName).Where(p => p.FullyQualifiedName == typeAccess.Type.FullyQualifiedName).FirstOrDefault();

        if (fullyQualified != null)
        {
            return fullyQualified;
        }

        var candidates = compilation.GetSemanticTree().Types.DistinctBy(p => p.FullyQualifiedName).Where(p => p.Name == typeAccess.TypeSymbol.Name);
        if (candidates.Count() == 1)
        {
            return candidates.First();
        }

        if (candidates.Count() == 0)
        {
            Log.Logger.Warning($"Type '{typeAccess?.ToString()}' not found in the semantic tree.");
        }

        if (candidates.Count() > 1)
        {
           Log.Logger.Warning($"Multiple types found for '{typeAccess?.ToString()}' in the semantic tree. You may need to fully qualify the declaration.");
        }

        return null;
    }

    /// <summary>
    /// Determines fully qualified name of the declaration.
    /// </summary>
    /// <param name="declaration">Field declaration</param>
    /// <param name="compilation">Compilation object.</param>
    /// <returns>Fully qualified name of the declaration.</returns>
    public static string? DetermineFullyQualifiedName(this IFieldDeclaration declaration, Compilation compilation)
    {        
        return compilation.FindTypeDeclaration(declaration.TypeAccess)?.FullyQualifiedName;
    }

    /// <summary>
    /// Determines fully qualified name of the declaration.
    /// </summary>
    /// <param name="declaration">Variable declaration</param>
    /// <param name="compilation">Compilation object.</param>
    /// <returns>Fully qualified name of the declaration.</returns>
    public static string? DetermineFullyQualifiedName(this IVariableDeclaration declaration, Compilation compilation)
    {
        return compilation.FindTypeDeclaration(declaration.TypeAccess)?.FullyQualifiedName;
    }

    private static bool IsToBeOmitted(this IStorageDeclaration fieldDeclaration, ISourceBuilder sourceBuilder, string coBuilder)
    {

        var compilerOmitsAttribute = fieldDeclaration.Pragmas.FirstOrDefault(p =>
            p.Content.StartsWith("#ix-attr:[CompilerOmitsAttribute(") ||
            p.Content.StartsWith("#ix-attr:[CompilerOmits("));

        if (compilerOmitsAttribute == null)
        {
            return false;
        }

        try
        {
            var startParameters = compilerOmitsAttribute.Content.IndexOf('(');
            var parametersLength = compilerOmitsAttribute.Content.IndexOf(')') - startParameters - 1;

            if (startParameters >= 0 && parametersLength >= 0)
            {
                var parameters =
                    compilerOmitsAttribute.Content.Substring(startParameters + 1, parametersLength)
                        .Split(',').Select(p => p.Replace('"', ' ').Trim());

                var paramsArray = parameters as string[] ?? parameters.ToArray();
                return paramsArray.Any(p => p == sourceBuilder.BuilderType || p == coBuilder) || string.IsNullOrEmpty(paramsArray?.First());
            }
            else
            {
                // No parameters
                return true;
            }
        }
        catch (Exception e)
        {
            throw new FailedToParseCompilerOmittsPragma($"Failed to parse compiler omits pragma at: {compilerOmitsAttribute.Location.GetLineSpan()}", e);
        }
    }

    /// <summary>
    /// Determines whether the member or type is eligible for generation.
    /// </summary>
    /// <param name="fieldDeclaration"></param>
    /// <param name="sourceBuilder"></param>
    /// <returns>True when the type is eligible</returns>
    public static bool IsEligibleForTranspile(this IFieldDeclaration fieldDeclaration, ISourceBuilder sourceBuilder)
    {
        var type = fieldDeclaration.Type;
        var isEligible = !(type is IReferenceTypeDeclaration)
                &&
                fieldDeclaration.IsAvailableForComm(sourceBuilder)
                &&
                (type is IScalarTypeDeclaration ||
                 type is IStringTypeDeclaration ||
                 type is IStructuredTypeDeclaration ||
                 type is INamedValueTypeDeclaration ||
                 sourceBuilder.Compilation.FindTypeDeclaration(fieldDeclaration.TypeAccess) != null);

            if(!isEligible) 
            {
                Log.Logger.Debug($"Field '{fieldDeclaration.Name}' of type '{fieldDeclaration.Type.FullyQualifiedName}' is not eligible for transpile");
            }

        return isEligible;
    }

    /// <summary>
    /// Determines whether the member or type is eligible for generation.
    /// </summary>
    /// <param name="variableDeclaration"></param>
    /// <param name="sourceBuilder"></param>
    /// <returns>True when the type is eligible</returns>
    public static bool IsEligibleForTranspile(this IVariableDeclaration variableDeclaration, ISourceBuilder sourceBuilder)
    {
        var type = variableDeclaration.Type;
        var isEligible = !(type is IReferenceTypeDeclaration)
               &&
               variableDeclaration.IsAvailableForComm(sourceBuilder)
               &&
               (type is IScalarTypeDeclaration ||
                type is IStringTypeDeclaration ||
                type is IStructuredTypeDeclaration ||
                type is INamedValueTypeDeclaration ||
                sourceBuilder.Compilation.FindTypeDeclaration(variableDeclaration.TypeAccess) != null);

        if (!isEligible)
        {
            Log.Logger.Debug($"Variable '{variableDeclaration.Name}' of type '{variableDeclaration.Type.FullyQualifiedName}' is not eligible for transpile");
        }

        return isEligible;
    }


    /// <summary>
    /// Determines whether the member is eligible for generation.
    /// </summary>
    /// <param name="arrayTypeDeclaration"></param>
    /// <param name="sourceBuilder">Source builder</param>
    /// <returns></returns>
    public static bool IsEligibleForTranspile(this IArrayTypeDeclaration arrayTypeDeclaration, ISourceBuilder sourceBuilder)
    {
        var singleDimensionalArray = arrayTypeDeclaration.Dimensions.Count == 1;

        var isEligibleType = !(arrayTypeDeclaration.ElementTypeAccess.Type is IReferenceTypeDeclaration)
                             &&
                             arrayTypeDeclaration.IsAvailableForComm(sourceBuilder)
                             &&
                             (arrayTypeDeclaration.ElementTypeAccess.Type is IScalarTypeDeclaration ||
                              arrayTypeDeclaration.ElementTypeAccess.Type is IStringTypeDeclaration ||
                              arrayTypeDeclaration.ElementTypeAccess.Type is IStructuredTypeDeclaration ||
                              arrayTypeDeclaration.ElementTypeAccess.Type is INamedValueTypeDeclaration ||
                              sourceBuilder.Compilation.FindTypeDeclaration(arrayTypeDeclaration.ElementTypeAccess) != null);

        return isEligibleType && singleDimensionalArray;

    }


    /// <summary>
    ///     Determines whether the member is eligible for generation.
    /// </summary>
    /// <param name="variable">Variable declaration</param>
    /// <param name="sourceBuilder">Source builder</param>
    /// <param name="coBuilder">Co-builder signature (e.g. POCO, Onliner, etc.)</param>
    /// <returns>True when the member is eligible for generation.</returns>
    public static bool IsMemberEligibleForTranspile(this IVariableDeclaration variable, ISourceBuilder sourceBuilder, string coBuilder = "")
    {
        return variable.IsInGlobalMemory 
               && variable.IsEligibleForTranspile(sourceBuilder)
               && !IsToBeOmitted(variable, sourceBuilder, coBuilder); 
    }

    /// <summary>
    ///     Determines whether the member is eligible for generation.
    /// </summary>
    /// <param name="field">Field declaration</param>
    /// <param name="sourceBuilder">Source builder</param>
    /// <param name="coBuilder">Lateral builder</param>
    /// <returns>True when the member is eligible for generation.</returns>
    public static bool IsMemberEligibleForConstructor(this IFieldDeclaration field, ISourceBuilder sourceBuilder, string coBuilder = "")
    {
        return field.AccessModifier == AccessModifier.Public && field.IsMemberEligibleForTranspile(sourceBuilder, coBuilder);
    }

    /// <summary>
    ///     Determines whether the member is eligible for generation.
    /// </summary>
    /// <param name="variable">Variable declaration</param>
    /// <param name="sourceBuilder">Source builder</param>
    /// <param name="coBuilder">Lateral builder</param>
    /// <returns>True when the member is eligible for generation.</returns>
    public static bool IsMemberEligibleForConstructor(this IVariableDeclaration variable, ISourceBuilder sourceBuilder, string coBuilder = "")
    {
        return variable.IsMemberEligibleForTranspile(sourceBuilder, coBuilder);
    }

    private static bool IsAvailableForComm(this IDeclaration declaration, ISourceBuilder sourceBuilder)
    {
        if (sourceBuilder.CompilerOptions is { IgnoreS7Pragmas: true }) return true;
        var pragmaReadWrite = "S7.extern=ReadWrite".ToLower();
        var pragmaRead = "S7.extern=ReadOnly".ToLower();
        return declaration.Pragmas.Any(p =>
        {
            var prgma = p.Content.ToLower().Replace(" ", string.Empty, StringComparison.InvariantCulture);
            return (prgma == pragmaReadWrite || prgma == pragmaRead);
        }) || (sourceBuilder.TypeCommAccessibility == eCommAccessibility.ReadOnly || sourceBuilder.TypeCommAccessibility == eCommAccessibility.ReadWrite);
    }
    internal static bool IsAvailableReadOnlyForComm(this IDeclaration declaration, ISourceBuilder sourceBuilder)
    {
        if (sourceBuilder.CompilerOptions is { IgnoreS7Pragmas: true }) return false;

        var pargmaContent = "S7.extern=Read".ToLower();
        return declaration.Pragmas.Any(p =>
        {
            var prgma = p.Content.ToLower().Replace(" ", string.Empty, StringComparison.InvariantCulture);
            return (prgma == pargmaContent);
        });
    }

    private static bool IsAvailableReadWriteForComm(this IDeclaration declaration, ISourceBuilder sourceBuilder)
    {
        if (sourceBuilder.CompilerOptions is { IgnoreS7Pragmas: true }) return false;

        var pargmaContent = "S7.extern=ReadWrite".ToLower();
        return declaration.Pragmas.Any(p =>
        {
            var prgma = p.Content.ToLower().Replace(" ", string.Empty, StringComparison.InvariantCulture);
            return (prgma == pargmaContent);
        });
    }

    public static eCommAccessibility GetCommAccessibility(this IDeclaration declaration, ISourceBuilder sourceBuilder)
    {

        if (declaration.IsAvailableReadOnlyForComm(sourceBuilder))
        {
            return eCommAccessibility.ReadOnly;
        }

        if (declaration.IsAvailableReadWriteForComm(sourceBuilder))
        {
            return eCommAccessibility.ReadWrite;
        }

        return eCommAccessibility.None;
    }
    

    

    

    public static bool IsMemberEligibleForConstructor(this IArrayTypeDeclaration arrayTypeDeclaration, ISourceBuilder sourceBuilder)
    {
        return IsEligibleForTranspile(arrayTypeDeclaration, sourceBuilder);

    }

    /// <summary>
    ///     Create triple-slash documentation.
    /// </summary>
    /// <param name="docComment">Documentation comment</param>
    /// <param name="sourceBuilder">Source builder</param>
    /// <returns></returns>
    public static string AddDocumentationComment(this IDocComment docComment, ISourceBuilder sourceBuilder)
    {
        return docComment.Content;
    }
}