using System;
using AXSharp.Connector;
using AXSharp.Connector.ValueTypes;
using System.Collections.Generic;
using AXSharp.Connector.Localizations;
using AXSharp.Abstractions.Presentation;

[AXSharp.Connector.SourceFileAttribute(@"test/enumStationStatus.st")]
public enum enumStationStatus
{
    Unknown,
    Available,
    UnAvailable
}