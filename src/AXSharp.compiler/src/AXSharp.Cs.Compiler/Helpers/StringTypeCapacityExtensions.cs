// AXSharp.Compiler.Cs
// Copyright (c) 2023 MTS spol. s r.o.,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/inxton/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/inxton/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/inxton/axsharp/blob/master/notices.md

using AX.ST.Semantic.Model.Declarations.Types;

namespace AXSharp.Compiler.Cs
{
    internal static class StringTypeCapacityExtensions
    {
        private const long DefaultCapacity = 254;

        /// <summary>
        /// Gets the declared capacity of a STRING/WSTRING declaration (STRING[n]),
        /// or the type's default capacity (254) when none is declared.
        /// </summary>
        public static long GetCapacityOrDefault(this IStringTypeDeclaration type)
        {
            var capacity = type.HasExplicitCapacity ? type.Capacity : type.DefaultTypeCapacity;
            return capacity > 0 ? capacity : DefaultCapacity;
        }
    }
}
