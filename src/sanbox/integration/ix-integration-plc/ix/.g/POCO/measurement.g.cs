using System;
using AXSharp.Abstractions.Presentation;

namespace Pocos
{
    namespace MeasurementExample
    {
        public partial class Measurement : AXSharp.Connector.IPlain
        {
            public Measurement()
            {
            }

            public Single Min { get; set; }

            public Single Acquired { get; set; }

            public Single Max { get; set; }

            public Int16 Result { get; set; }
        }

        public partial class Measurements : AXSharp.Connector.IPlain
        {
            public Measurements()
            {
            }

            public MeasurementExample.Measurement measurement_stack { get; set; } = new MeasurementExample.Measurement();
            public MeasurementExample.Measurement measurement_wrap { get; set; } = new MeasurementExample.Measurement();
            public MeasurementExample.Measurement measurement_grid { get; set; } = new MeasurementExample.Measurement();
            public MeasurementExample.Measurement measurement_tabs { get; set; } = new MeasurementExample.Measurement();
        }
    }
}