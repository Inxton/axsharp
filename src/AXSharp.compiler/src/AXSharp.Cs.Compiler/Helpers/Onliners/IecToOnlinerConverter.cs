// AXSharp.Compiler.Cs
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/ix-ax/axsharp/blob/master/notices.md

using AX.ST.Semantic.Model.Declarations.Types;
using AX.ST.Syntax.Tree;
using AXSharp.Compiler.Cs.Exceptions;
using AXSharp.Connector.ValueTypes;

namespace AXSharp.Compiler.Cs.Helpers.Onliners;

internal static class IecToOnlinerConverter
{
    private static readonly IDictionary<string, Type> NonNullabePrimitives = new Dictionary<string, Type>
    {
        { "BIT", typeof(OnlinerBool) },
        { "BOOL", typeof(OnlinerBool) },
        { "BYTE", typeof(OnlinerByte) },
        { "DINT", typeof(OnlinerDInt) },
        { "DWORD", typeof(OnlinerDWord) },
        { "INT", typeof(OnlinerInt) },
        { "LINT", typeof(OnlinerLInt) },
        { "LREAL", typeof(OnlinerLReal) },
        { "LWORD", typeof(OnlinerLWord) },
        { "REAL", typeof(OnlinerReal) },
        { "SINT", typeof(OnlinerSInt) },
        { "UDINT", typeof(OnlinerUDInt) },
        { "UINT", typeof(OnlinerUInt) },
        { "ULINT", typeof(OnlinerULInt) },
        { "USINT", typeof(OnlinerUSInt) },
        { "WORD", typeof(OnlinerWord) },
        { "CHAR", typeof(OnlinerChar) },
        { "WCHAR", typeof(OnlinerWChar) }
    };


    private static readonly IDictionary<string, Type> NullabePrimitives = new Dictionary<string, Type>
    {
        { "WSTRING", typeof(OnlinerWString) },
        { "STRING", typeof(OnlinerString) },
        { "DATE", typeof(OnlinerDate) },
        { "DATE_AND_TIME", typeof(OnlinerDateTime) },
        { "DATE_TIME", typeof(OnlinerDateTime) },
        { "TIME_OF_DAY", typeof(OnlinerTimeOfDay) },
        { "TIME", typeof(OnlinerTime) },
        { "LTIME", typeof(OnlinerLTime) },
        { "LDATE_AND_TIME", typeof(OnlinerLDateTime) },
        { "LTIME_OF_DAY", typeof(OnlinerLTimeOfDay) },
        { "LDATE", typeof(OnlinerDate) },
    };

    public static bool IsNonNullablePrimitive(this IElementaryTypeSyntax type)
    {
        return NonNullabePrimitives.ContainsKey(type.TypeName);
    }

    public static bool IsNullablePrimitive(this IElementaryTypeSyntax type)
    {
        return NullabePrimitives.ContainsKey(type.TypeName);
    }

    public static string TransformType(this IElementaryTypeSyntax type)
    {
        var typeName = type.TypeName.ToUpperInvariant();
        if (NonNullabePrimitives.ContainsKey(typeName)) return NonNullabePrimitives[typeName].Name;

        if (NullabePrimitives.ContainsKey(typeName)) return NullabePrimitives[typeName].Name;

        throw new PrimitiveTypeNotRecognizedException($"Type {typeName} is not primitive type");
    }

    public static string TransformType(this IStringDeclarationSyntax type)
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

        throw new PrimitiveTypeNotRecognizedException($"Type {typeName} is not primitive type.");
    }

    public static string TransformType(this IStringTypeDeclaration type)
    {
        var typeName = type.NameWithoutCapacity.ToUpperInvariant();//type.Name.ToUpperInvariant();

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
}