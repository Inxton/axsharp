// AXSharp.Compiler.Cs
// Copyright (c) 2023 MTS spol. s r.o.,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/inxton/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/inxton/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/inxton/axsharp/blob/master/notices.md

using System.Text;
using AX.ST.Semantic.Model.Declarations;
using AX.ST.Semantic.Model.Declarations.Types;
using AX.ST.Semantic.Pragmas;
using AXSharp.Compiler.Core;
using AXSharp.Compiler.Cs.Pragmas.PragmaParser;
using AXSharp.Connector;

namespace AXSharp.Compiler.Cs;

/// <summary>
///     Provides series of methods for working with IX pragmas.
/// </summary>
public static class PragmaExtensions
{
    public const string PRAGMA_ATTRIBUTE_SIGNATURE = "#ix-attr:";
    public const string PRAGMA_DECLARE_PROPERTY_SIGNATURE = "#ix-prop:";
    public const string PRAGMA_PROPERTY_SET_SIGNATURE = "#ix-set:";
    public const string PRAGMA_PROPERTY_GENERIC_ATTRIBUTES = "#ix-generic:";

    private static readonly int pragma_attribute_signature_length = PRAGMA_ATTRIBUTE_SIGNATURE.Length;
    private static readonly int pragma_declare_property_signature_length = PRAGMA_DECLARE_PROPERTY_SIGNATURE.Length;
    private static readonly int pragma_property_set_signature_length = PRAGMA_PROPERTY_SET_SIGNATURE.Length;

    /// <summary>
    ///     Produces clr attributes from list of ix pragmas.
    /// </summary>
    /// <param name="pragmas">Pragmas</param>
    /// <returns>Attribute syntax from given ix pragmas.</returns>
    public static string AddAttributes(this IEnumerable<IPragma> pragmas)
    {
        return string.Join("\r\n",
            pragmas.Where(p => p.Content.StartsWith(PRAGMA_ATTRIBUTE_SIGNATURE))
                .Select(p => Pragmas.PragmaParser.PragmaCompiler.Compile(p).Product));
    }

    public static string AddedPropertiesAsAttributes(this IEnumerable<IPragma> pragmas)
    {
        var properties =  pragmas.Where(p => 
                p.Content.StartsWith(PRAGMA_PROPERTY_SET_SIGNATURE))
            .Select(p => Pragmas.PragmaParser.PragmaCompiler.Compile(p).Property);


        var valueTuples = properties as (string PropertyName, string InitValue)[] ?? properties.ToArray();
        if (valueTuples.Count() > 0)
        {
            var sb = new StringBuilder();
            
            foreach (var property in valueTuples)
            {
                sb.AppendLine($"[AXSharp.Connector.AddedPropertiesAttribute(\"{property.PropertyName}\", {property.InitValue})]\n");
            }

            return sb.ToString();
        }
        
        return string.Empty;
    }
    
    /// <summary>
    ///     Produces property from list of ix pragmas declared on type declaration.
    /// </summary>
    /// <param name="typeDeclaration">Type declaration</param>
    /// <returns>Property syntax from given ix pragmas.</returns>
    public static string DeclareProperties(this ITypeDeclaration typeDeclaration)
    {
        return string.Join("\r\n",
            typeDeclaration.Pragmas.Where(p => p.Content.StartsWith(PRAGMA_DECLARE_PROPERTY_SIGNATURE))
                .Select(p => Pragmas.PragmaParser.PragmaCompiler.Compile(p, typeDeclaration).Product));
    }

    /// <summary>
    ///     Produces property from list of ix pragmas declared in configuration declaration.
    /// </summary>
    /// <param name="configDeclaration">Type declaration</param>
    /// <returns>Property syntax from given ix pragmas.</returns>
    public static string DeclareProperties(this IConfigurationDeclaration configDeclaration)
    {
        return string.Join("\r\n",
            configDeclaration.Pragmas.Where(p => p.Content.StartsWith(PRAGMA_DECLARE_PROPERTY_SIGNATURE))
                .Select(p => Pragmas.PragmaParser.PragmaCompiler.Compile(p, configDeclaration).Product));
    }

    /// <summary>
    ///     Produces statement to set a value of a property in constructor.
    /// </summary>
    /// <param name="fieldDeclaration">Field declaration</param>
    /// <returns>Statement setting property to given value.</returns>
    public static string SetProperties(this IStorageDeclaration fieldDeclaration)
    {
        return string.Join("\r\n",
            fieldDeclaration.Pragmas.Where(p => p.Content.StartsWith(PRAGMA_PROPERTY_SET_SIGNATURE)).Select(p => Pragmas.PragmaParser.PragmaCompiler.Compile(p, fieldDeclaration).Product));
    }
    
    public static VisitorProduct GetGenericAttributes(this ITypeDeclaration typeDeclaration)
    {
        return typeDeclaration.Pragmas
            .Where(p => p.Content.StartsWith(PRAGMA_PROPERTY_GENERIC_ATTRIBUTES))
                .Select(p => Pragmas.PragmaParser.PragmaCompiler.Compile(p, typeDeclaration)).FirstOrDefault();
    }

    public static IEnumerable<VisitorProduct> GetGenericAttributes(this IFieldDeclaration typeDeclaration)
    {
        return typeDeclaration.Pragmas
            .Where(p => p.Content.StartsWith(PRAGMA_PROPERTY_GENERIC_ATTRIBUTES))
            .Select(p => Pragmas.PragmaParser.PragmaCompiler.Compile(p, typeDeclaration));
    }

    /// <summary>
    ///     Produces statement to set a value of a property in constructor.
    /// </summary>
    /// <param name="variableDeclaration">Variable declaration</param>
    /// <returns>Statement setting property to given value.</returns>
    public static string SetProperties(this IVariableDeclaration variableDeclaration)
    {
        return string.Join("\r\n",
            variableDeclaration.Pragmas.Where(p => p.Content.StartsWith(PRAGMA_PROPERTY_SET_SIGNATURE))
                .Select(p => Pragmas.PragmaParser.PragmaCompiler.Compile(p, variableDeclaration).Product));
    }

    /// <summary>
    ///     Produces statement to set a value of a property in constructor.
    /// </summary>
    /// <param name="typeDeclaration">Type declaration</param>
    /// <returns>Statement setting property to given value.</returns>
    public static string SetProperties(this ITypeDeclaration typeDeclaration)
    {
        return string.Join("\r\n",
            typeDeclaration.Pragmas.Where(p => p.Content.StartsWith(PRAGMA_PROPERTY_SET_SIGNATURE))
                .Select(p => Pragmas.PragmaParser.PragmaCompiler.Compile(p).Product));
    }

    /// <summary>
    ///     Gets a value of a property declared with set value pragma.
    ///     If a property with given name is not found member name is returned instead.
    /// </summary>
    /// <param name="declaration">Declaration</param>
    /// <param name="propertyName">Property name</param>
    /// <param name="memberName">Member name</param>
    /// <returns></returns>
    public static string GetPropertyValue(this IDeclaration declaration, string propertyName, string memberName = "")
    {
        try
        {
            // This is only to check for malformed pragmas, if there is any, exception will be thrown and caught in catch block and empty string will be returned.
            string.Join("\r\n",
                declaration.Pragmas.Where(p => p.Content.StartsWith(PRAGMA_PROPERTY_SET_SIGNATURE))
                    .Select(p => Pragmas.PragmaParser.PragmaCompiler.Compile(p).Product));

            var propertyValue = declaration.Pragmas.FirstOrDefault(p =>
                    p.Content.Replace(" ", string.Empty).StartsWith($"{PRAGMA_PROPERTY_SET_SIGNATURE}{propertyName}"))
                ?.Content.Split('=');

        if (propertyValue is { Length: > 0 }) return propertyValue[1].Replace("\"", string.Empty).Trim();
            
            return memberName;
        }
        catch (MalformedPragmaException ex)
        {
           // swallow
        }
        
        return string.Empty;
    }

    /// <summary>
    ///     Produces statement to annotate the member based on attributes.
    /// </summary>
    /// <param name="declaration">Declaration</param>
    /// <param name="sourceBuilder">Source builder.</param>
    /// <returns>Annotation statements</returns>
    public static string AddAnnotations(this IDeclaration declaration, ISourceBuilder sourceBuilder)
    {
        var sb = new StringBuilder();
        foreach (var attribute in
                 declaration.Pragmas.Where(p => p.Content.StartsWith(PRAGMA_ATTRIBUTE_SIGNATURE))
                     .Select(p => p.Content.Substring(pragma_attribute_signature_length,
                         p.Content.Length - pragma_attribute_signature_length)))
        {
            switch (attribute)
            {
                case "[ReadOnceAttribute()]":
                case "[ReadOnce()]":
                
                    sb.AppendLine($"{declaration.Name}.MakeReadOnce();");
                    break;
                case "[ReadOnlyAttribute()]":
                case "[ReadOnly()]":

                    sb.AppendLine($"{declaration.Name}.MakeReadOnly();");
                    break;
            }

            if (declaration.IsAvailableReadOnlyForComm(sourceBuilder))
            {
                sb.AppendLine($"{declaration.Name}.MakeReadOnly();");
            }
        }
            

        return sb.ToString();
    }
}
