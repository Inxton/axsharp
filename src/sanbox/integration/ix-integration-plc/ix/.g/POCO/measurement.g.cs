using System;
using AXSharp.Abstractions.Presentation;
using AXSharp.Connector;

namespace Pocos
{
    namespace MeasurementExample
    {
        public partial class Measurement : AXSharp.Connector.IPlain
        {
            public Measurement()
            {
            }

            [AXSharp.Connector.AddedPropertiesAttribute("AttributeName", "Minimum")]
            public Single Min { get; set; }

            [ReadOnly()]
            [AXSharp.Connector.AddedPropertiesAttribute("AttributeName", "Measured")]
            public Single Acquired { get; set; }

            [AXSharp.Connector.AddedPropertiesAttribute("AttributeName", "Maximum")]
            public Single Max { get; set; }

            [AXSharp.Connector.AddedPropertiesAttribute("AttributeName", "Measurement Result")]
            public Int16 Result { get; set; }
        }

        public partial class Measurements : AXSharp.Connector.IPlain
        {
            public Measurements()
            {
            }

            [Container(Layout.Stack)]
            [Group(GroupLayout.GroupBox)]
            [AXSharp.Connector.AddedPropertiesAttribute("AttributeName", "Stack panel")]
            public MeasurementExample.Measurement measurement_stack { get; set; } = new MeasurementExample.Measurement();
            [Container(Layout.Wrap)]
            [Group(GroupLayout.GroupBox)]
            [AXSharp.Connector.AddedPropertiesAttribute("AttributeName", "Wrap panel")]
            public MeasurementExample.Measurement measurement_wrap { get; set; } = new MeasurementExample.Measurement();
            [Container(Layout.UniformGrid)]
            [Group(GroupLayout.GroupBox)]
            [AXSharp.Connector.AddedPropertiesAttribute("AttributeName", "Grid")]
            public MeasurementExample.Measurement measurement_grid { get; set; } = new MeasurementExample.Measurement();
            [Container(Layout.Tabs)]
            [Group(GroupLayout.GroupBox)]
            [AXSharp.Connector.AddedPropertiesAttribute("AttributeName", "Tabs")]
            public MeasurementExample.Measurement measurement_tabs { get; set; } = new MeasurementExample.Measurement();
        }
    }
}