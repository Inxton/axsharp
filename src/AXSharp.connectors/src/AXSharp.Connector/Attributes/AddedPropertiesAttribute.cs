using System;
using System.Collections.Generic;

namespace AXSharp.Connector;


[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property, AllowMultiple = true)]
public class AddedPropertiesAttribute : Attribute
{
    public string PropertyName { get; set; }
    public object PropertyValue { get; set; }
    
    public AddedPropertiesAttribute(string propertyName, object propertyValue)
    {
        PropertyName = propertyName;
        PropertyValue = propertyValue;
    }
}