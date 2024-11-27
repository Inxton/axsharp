// AXSharp.Compiler.Cs
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/ix-ax/axsharp/blob/master/notices.md

using System.Text;
using AX.ST.Semantic.Model.Declarations;
using AX.ST.Syntax.Tree;
using AXSharp.Connector;

namespace AXSharp.Compiler.Cs.Helpers;

internal static class CsHelpers
{
    public static string Transform(this IAccessModifierSyntax syntax)
    {
        return $"{syntax.ModifierKeyword.Text.ToLower()} ";
    }

    public static string Transform(this AccessModifier semantics)
    {
        return $"{semantics.ToString().ToLower()} ";
    }

    public static string GetQualifiedName(this IDeclaration declaration)
    {
        return declaration.FullyQualifiedName;
    }

    public static string? n(this Type type)
    {
        return type.FullName;
    }

    public static string GetAttributeNameValue(this IDeclaration declaration, string memberName)
    {
        return declaration.GetPropertyValue("AttributeName", memberName);
    }

    public static string CreateGenericSwapperMethodToPlainer(string methodName, string pocoTypeName, bool isExtended)
    {
        var qualifier = isExtended ? "override" : "virtual";
        return $"public async {qualifier} Task<T> {methodName}<T>(){{\n return await (dynamic)this.{methodName}Async();\n}}";
    }

    public static string CreateGenericHasChangedMethodMethod(string methodName, string pocoTypeName, bool isExtended = false)
    {
        var qualifier = isExtended ? "override" : "virtual";
        var sb = new StringBuilder();
        sb.AppendLine("///<inheritdoc/>\r\n");
        sb.AppendLine($"public async {qualifier} Task<bool> {TwinObjectExtensions.HasChangedMethodName}<T>(T plain){{\n return await this.{methodName}((dynamic)plain);\n}}");
        return sb.ToString();
    }

    public static string CreateGenericSwapperMethodFromPlainer(string methodName, string pocoTypeName, bool isExtended)
    {
        var qualifier = isExtended ? "override" : "virtual";
        return $"public async {qualifier} Task {methodName}<T>(T plain){{\n await this.{methodName}Async((dynamic)plain);\n}}";
    }
}