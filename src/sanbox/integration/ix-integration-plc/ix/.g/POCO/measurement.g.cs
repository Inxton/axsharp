using System;
using AXSharp.Abstractions.Presentation;
using AXSharp.Connector;

namespace Pocos
{
    namespace MeasurementExample
    {
        [AXSharp.Connector.SourceFileAttribute(@"src/sanbox/integration/ix-integration-plc/src/measurement.st")]
        public partial class Measurement : AXSharp.Connector.IPlain
        {
            public Measurement()
            {
            }

            [AXSharp.Connector.AddedPropertiesAttribute("AttributeName", @"Minimum")]
            public Single Min { get; set; }

            [AXSharp.Connector.AddedPropertiesAttribute("AttributeName", @"Measured")]
            public Single Acquired { get; set; }

            [AXSharp.Connector.AddedPropertiesAttribute("AttributeName", @"Maximum")]
            public Single Max { get; set; }

            [AXSharp.Connector.AddedPropertiesAttribute("AttributeName", @"Measurement Result")]
            [AXSharp.Connector.EnumeratorDiscriminatorAttribute(typeof(global::MeasurementExample.Result))]
            public Int16 Result { get; set; }
        }

        [AXSharp.Connector.SourceFileAttribute(@"src/sanbox/integration/ix-integration-plc/src/measurement.st")]
        public partial class Measurements : AXSharp.Connector.IPlain
        {
            public Measurements()
            {
            }

            [AXSharp.Connector.AddedPropertiesAttribute("AttributeName", @"Stack panel")]
            public MeasurementExample.Measurement measurement_stack { get; set; } = new MeasurementExample.Measurement();

            [AXSharp.Connector.AddedPropertiesAttribute("AttributeName", @"Wrap panel")]
            public MeasurementExample.Measurement measurement_wrap { get; set; } = new MeasurementExample.Measurement();

            [AXSharp.Connector.AddedPropertiesAttribute("AttributeName", @"Grid")]
            public MeasurementExample.Measurement measurement_grid { get; set; } = new MeasurementExample.Measurement();

            [AXSharp.Connector.AddedPropertiesAttribute("AttributeName", @"Tabs")]
            public MeasurementExample.Measurement measurement_tabs { get; set; } = new MeasurementExample.Measurement();
        }
    }
}