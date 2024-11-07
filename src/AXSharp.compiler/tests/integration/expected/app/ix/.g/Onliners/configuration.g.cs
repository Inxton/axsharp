using System;
using AXSharp.Connector;
using AXSharp.Connector.ValueTypes;
using System.Collections.Generic;
using AXSharp.Connector.Localizations;
using AXSharp.Abstractions.Presentation;

public partial class appTwinController : ITwinController
{
    public AXSharp.Connector.Connector Connector { get; }

    public lib1.MyClass lib1_MyClass { get; }

    public lib2.MyClass lib2_MyClass { get; }

    public appTwinController(AXSharp.Connector.ConnectorAdapter adapter, object[] parameters)
    {
        this.Connector = adapter.GetConnector(parameters);
        lib1_MyClass = new lib1.MyClass(this.Connector, "", "lib1_MyClass");
        lib2_MyClass = new lib2.MyClass(this.Connector, "", "lib2_MyClass");
    }

    public appTwinController(AXSharp.Connector.ConnectorAdapter adapter)
    {
        this.Connector = adapter.GetConnector(adapter.Parameters);
        lib1_MyClass = new lib1.MyClass(this.Connector, "", "lib1_MyClass");
        lib2_MyClass = new lib2.MyClass(this.Connector, "", "lib2_MyClass");
    }
}