// AXSharp.Compiler.Cs
// Copyright (c) 2023 MTS spol. s r.o.,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/inxton/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/inxton/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/inxton/axsharp/blob/master/notices.md

using AX.ST.Semantic.Model.Declarations.Types;
using AX.ST.Syntax.Tree;
using AXSharp.Compiler.Cs.Exceptions;

namespace AXSharp.Compiler.Cs.Helpers.Plain;

internal static class IecToClrConverter
{
    private static readonly IDictionary<string, Type> NonNullabePrimitives = new Dictionary<string, Type>
    {
        { "BIT", typeof(bool) },
        { "BOOL", typeof(bool) },
        { "BYTE", typeof(byte) },
        { "DINT", typeof(int) },
        { "DWORD", typeof(uint) },
        { "INT", typeof(short) },
        { "LINT", typeof(long) },
        { "LREAL", typeof(double) },
        { "LWORD", typeof(ulong) },
        { "REAL", typeof(float) },
        { "SINT", typeof(sbyte) },
        { "UDINT", typeof(uint) },
        { "UINT", typeof(ushort) },
        { "ULINT", typeof(ulong) },
        { "USINT", typeof(byte) },
        { "WORD", typeof(ushort) },
        { "CHAR", typeof(char) },
        { "WCHAR", typeof(char) }
    };


    private static readonly IDictionary<string, Type> NullabePrimitives = new Dictionary<string, Type>
    {
        { "WSTRING", typeof(string) },
        { "STRING", typeof(string) },        
        { "DATE", typeof(DateOnly) },
        { "LDATE", typeof(DateOnly) },
        { "DATE_AND_TIME", typeof(DateTime) },
        { "LDATE_AND_TIME", typeof(DateTime) },
        { "DATE_TIME", typeof(DateTime) },                                        
        { "TIME", typeof(TimeSpan) },
        { "LTIME", typeof(TimeSpan) },
        { "TIME_OF_DAY", typeof(TimeSpan) },
        { "LTIME_OF_DAY", typeof(TimeSpan) },
        { "TOD", typeof(TimeSpan) }
    };


    private static readonly IDictionary<string, Type> NullabeDateRelatedPrimitives = new Dictionary<string, Type>
    {        
        { "DATE", typeof(DateOnly) },
        { "LDATE", typeof(DateOnly) },
        { "DATE_AND_TIME", typeof(DateTime) },
        { "LDATE_AND_TIME", typeof(DateTime) },
        { "DATE_TIME", typeof(DateTime) },     
    };


    public static bool IsNonNullablePrimitive(this IElementaryTypeSyntax type)
    {
        return NonNullabePrimitives.ContainsKey(type.TypeName);
    }

    public static bool IsNonNullablePrimitive(this IScalarTypeDeclaration type)
    {
        return NonNullabePrimitives.ContainsKey(type.Name);
    }

    public static bool IsNullablePrimitive(this IElementaryTypeSyntax type)
    {
        return NullabePrimitives.ContainsKey(type.TypeName);
    }
    public static bool IsNullableDateRelatedPrimitive(this IScalarTypeDeclaration type)
    {
        return NullabeDateRelatedPrimitives.ContainsKey(type.Type.Name);
    }

    public static string CreateScalarInitializer(this IScalarTypeDeclaration scalar, string? targetPlatformMoniker)
    {
        if (targetPlatformMoniker == null)
        {
            throw new ArgumentNullException(nameof(targetPlatformMoniker), "Target platform moniker cannot be null.");
        }

        if (!scalar.IsNullableDateRelatedPrimitive() && scalar.IsNullablePrimitive())
        {
            return $" = default({scalar.TransformType()});\n";
        }

        if (scalar.IsNullableDateRelatedPrimitive())
        {

            // We need to provide differrent default values for date
            // related types based on target platform

            switch (targetPlatformMoniker.ToLower())
            {
                case "ax":
                    return scalar.CreateDefaultValueForAx();
                case "tia":
                    return scalar.CreateDefaultValueForTia();
            }           
        }

        return string.Empty;
    }

    private static string CreateDefaultValueForAx(this IScalarTypeDeclaration scalar)
    {
        switch (scalar.Name.Trim().ToUpper())
        {
            case "DATE":
                return " = new DateOnly(1970, 1, 1);\n";
            case "LDATE":
                return " = new DateOnly(1970, 1, 1);\n";
            case "DATE_AND_TIME":
                return " = new DateTime(1970, 1, 1);\n";
            case "LDATE_AND_TIME":
                return " = new DateTime(1970, 1, 1);\n";
            case "LDATE_TIME":
                return " = new DateTime(1970, 1, 1);\n";
            default:
                return " = new();";
        }
    }

    private static string CreateDefaultValueForTia(this IScalarTypeDeclaration scalar)
    {
        switch (scalar.Name.Trim().ToUpper())
        {
            case "DATE":
                return " = new DateOnly(1990, 1, 1);\n";
            case "LDATE":
                return " = new DateOnly(1990, 1, 1);\n";
            case "DATE_AND_TIME":
                return " = new DateTime(1990, 1, 1);\n";
            case "LDATE_AND_TIME":
                return " = new DateTime(1990, 1, 1);\n";
            case "LDATE_TIME":
                return " = new DateTime(1990, 1, 1);\n";
            default:
                return " = new();";
        }
    }

    private static bool IsNullablePrimitive(this IScalarTypeDeclaration type)
    {
        return NullabePrimitives.ContainsKey(type.Name);
    }    

    public static string TransformType(this IElementaryTypeSyntax type)
    {
        var typeName = type.TypeName.ToUpperInvariant();
        if (NonNullabePrimitives.ContainsKey(typeName)) return NonNullabePrimitives[typeName].Name;

        if (NullabePrimitives.ContainsKey(typeName)) return NullabePrimitives[typeName].Name;

        throw new PrimitiveTypeNotRecognizedException($"Type {typeName} is not primitive type");
    }

    public static string TransformType(this ITypeSyntax type)
    {
        var typeName = type.TypeName.ToUpperInvariant();
        if (NonNullabePrimitives.ContainsKey(typeName)) return NonNullabePrimitives[typeName].Name;

        if (NullabePrimitives.ContainsKey(typeName)) return NullabePrimitives[typeName].Name;

        throw new PrimitiveTypeNotRecognizedException($"Type {typeName} is not primitive type");
    }


    public static string TransformType(this IScalarTypeDeclaration type)
    {
        var typeName = type.Name.ToUpperInvariant();
        if (NonNullabePrimitives.ContainsKey(typeName)) return NonNullabePrimitives[typeName].Name;

        if (NullabePrimitives.ContainsKey(typeName)) return NullabePrimitives[typeName].Name;

        throw new PrimitiveTypeNotRecognizedException($"Type {typeName} is not primitive type");
    }

    public static string TransformType(this ITypeDeclaration type)
    {
        var typeName = type.Name.ToUpperInvariant();
        if (NonNullabePrimitives.ContainsKey(typeName)) return NonNullabePrimitives[typeName].Name;

        if (NullabePrimitives.ContainsKey(typeName)) return NullabePrimitives[typeName].Name;

        return type.FullyQualifiedName;
    }

    public static string TransformType(this IStringTypeDeclaration type)
    {
        return "string";
    }
}