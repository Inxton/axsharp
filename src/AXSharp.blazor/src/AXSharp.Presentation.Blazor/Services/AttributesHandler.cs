// AXSharp.Presentation.Blazor
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/ix-ax/axsharp/blob/master/notices.md

using System;
using System.Linq;
using System.Reflection;
using AXSharp.Connector;

namespace AXSharp.Presentation.Blazor.Services
{ 
    /// <summary>
    ///  Class containing methods to access property attributes from Twin objects.
    /// </summary>
    public class AttributesHandler
    {
        public EnumeratorDiscriminatorAttribute GetEnumeratorDiscriminatorAttribute(ITwinElement twinObject)
        {
            try
            {
                var propertyInfo = GetPropertyViaSymbol(twinObject);
                if (propertyInfo != null)
                {
                    var propertyAttribute = propertyInfo.GetCustomAttributes()
                        .ToList()
                        .Find(p => p.GetType() == typeof(EnumeratorDiscriminatorAttribute)) as EnumeratorDiscriminatorAttribute;

                    if (propertyAttribute != null)
                    {
                        return propertyAttribute;
                    }
                }

                var typeAttribute = twinObject.GetType()
                    .GetCustomAttributes(true)
                    .ToList()
                    .Find(p => p.GetType() == typeof(EnumeratorDiscriminatorAttribute)) as EnumeratorDiscriminatorAttribute;

                return typeAttribute;
            }
            catch (Exception)
            {
                //throw;
            }

            return null;
        }

        public RenderIgnoreAttribute GetIgnoreRenderingAttribute(ITwinElement twinObject)
        {
            if (twinObject == null) return null;
            try
            {
                var propertyInfo = GetPropertyViaSymbol(twinObject);
                if (propertyInfo != null)
                {
                    if (propertyInfo
                            .GetCustomAttributes().FirstOrDefault(p => p is RenderIgnoreAttribute) is RenderIgnoreAttribute propertyAttribute)
                    {
                        return propertyAttribute;
                    }
                }

                var typeAttribute = twinObject
                    .GetType()
                    .GetCustomAttributes(true)
                    .FirstOrDefault(p => p is RenderIgnoreAttribute) as RenderIgnoreAttribute;

                return typeAttribute;
            }
            catch (Exception)
            {
                //throw;
            }

            return null;
        }

        public RenderTemplateOverrideAttribute GetRenderTemplateOverrideAttribute(ITwinElement twinObject)
        {
            if (twinObject == null) return null;

            try
            {
                var propertyInfo = GetPropertyViaSymbol(twinObject);
                if (propertyInfo != null)
                {
                    if (propertyInfo
                            .GetCustomAttributes().FirstOrDefault(p => p is RenderTemplateOverrideAttribute) is RenderTemplateOverrideAttribute propertyAttribute)
                    {
                        return propertyAttribute;
                    }
                }

                var typeAttribute = twinObject
                    .GetType()
                    .GetCustomAttributes(true)
                    .FirstOrDefault(p => p is RenderTemplateOverrideAttribute) as RenderTemplateOverrideAttribute;

                return typeAttribute;
            }
            catch (Exception)
            {
                //throw;
            }

            return null;
        }

        public PropertyInfo GetPropertyViaSymbol(ITwinElement twinObject)
        {
            if (twinObject == null) return null;
            var propertyName = twinObject.GetSymbolTail();

            if (twinObject.Symbol == null)
                return null;

            if (twinObject.Symbol.EndsWith("]"))
            {
                propertyName = propertyName?.Substring(0, propertyName.IndexOf('[') - 1);
            }

            var propertyInfo = twinObject?.GetParent()?.GetType().GetProperty(propertyName);

            return propertyInfo;
        }
    }
}
