// AXSharp.Presentation.Blazor
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/ix-ax/axsharp/blob/master/notices.md

using System;
using System.Globalization;
using AXSharp.Connector;

namespace AXSharp.Presentation.Blazor
{
    /// <summary>
    ///  Enum to Integer converter.
    /// </summary>
    public class EnumToIntConverter
    {
        public EnumToIntConverter(EnumeratorDiscriminatorAttribute attribute)
        {
            propertyAttribute = attribute;
        }


        EnumeratorDiscriminatorAttribute propertyAttribute;

        /// <summary>
        /// Converts a value. 
        /// </summary>
        /// <returns>
        /// A converted value. If the method returns null, the valid null value is used.
        /// </returns>
        /// <param name="value">The value produced by the binding source.</param><param name="targetType">The type of the binding target property.</param><param name="parameter">The converter parameter to use.</param><param name="culture">The culture to use in the converter.</param>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return GetEnumValueString(value, propertyAttribute);
        }


        /// <summary>
        /// Converts a value. 
        /// </summary>
        /// <returns>
        /// A converted value. If the method returns null, the valid null value is used.
        /// </returns>
        /// <param name="value">The value that is produced by the binding target.</param><param name="targetType">The type to convert to.</param><param name="parameter">The converter parameter to use.</param><param name="culture">The culture to use in the converter.</param>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return GetEnumValue((string)value, propertyAttribute);
        }


        protected object GetEnumValue(string enumMember, EnumeratorDiscriminatorAttribute attr)
        {
            var retval = Enum.Parse(attr.EnumeratorType, enumMember);

            switch (attr.EnumeratorType.GetEnumUnderlyingType())
            {
                case Type a when a == typeof(sbyte):
                    return (sbyte)retval;
                case Type a when a == typeof(byte):
                    return (byte)retval;
                case Type a when a == typeof(short):
                    return (short)retval;
                case Type a when a == typeof(ushort):
                    return (ushort)retval;
                case Type a when a == typeof(int):
                    return (int)retval;
                case Type a when a == typeof(uint):
                    return (uint)retval;
                case Type a when a == typeof(long):
                    return (long)retval;
                case Type a when a == typeof(ulong):
                    return (ulong)retval;
                case Type a when a == typeof(char):
                    return (char)retval;
                case Type a when a == typeof(float):
                    return (float)retval;
                case Type a when a == typeof(double):
                    return (double)retval;
                case Type a when a == typeof(decimal):
                    return (decimal)retval;
                default:
                    return retval;
            }
        }

        protected string GetEnumValueString(object enumMember, EnumeratorDiscriminatorAttribute attr)
        {
            return Enum.GetName(attr.EnumeratorType, enumMember);
        }
    }
}

