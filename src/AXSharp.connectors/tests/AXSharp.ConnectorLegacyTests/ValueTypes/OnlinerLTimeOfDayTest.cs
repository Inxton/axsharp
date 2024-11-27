// AXSharp.ConnectorLegacyTests
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/ix-ax/axsharp/blob/master/notices.md

namespace AXSharp.Connector.Onliners.Tests
{
    using NUnit.Framework;
    using System;
    using System.Linq;
    using AXSharp.Connector.Tests;
    using AXSharp.Connector.ValueTypes;

    public class OnlinerLTimeOfDayTest : OnlinerBaseTests<TimeSpan>
    {
        protected override OnlinerBase<TimeSpan> Onliner { get; set; }


        public override void Init()
        {
            Onliner = new OnlinerLTimeOfDay(new TestTwinObject(), $"readableTail", "symbolTail");
        }

        [Test()]
        public void ChangeEditedValueTest()
        {
            //-- Arrange

            var expected = OnlinerLTimeOfDay.MaxValue;

            //-- Act
            Onliner.Edit = expected;

            //-- Assert
            Assert.AreEqual(expected, Onliner.GetAsync().Result);
            Assert.AreEqual($"Edit of {Onliner.Symbol};{Onliner.HumanReadable};00:00:00;{expected}", logs);

        }

        [Test()]
        public void ChangeShadow()
        {
            //-- Arrange

            var expected = OnlinerLTimeOfDay.MaxValue;

            //-- Act
            Onliner.Shadow = expected;

            //-- Assert
            Assert.AreEqual(expected, Onliner.Shadow);
            Assert.AreEqual($"Shadow of {Onliner.Symbol};{Onliner.HumanReadable};00:00:00;{expected}", logs);
        }

        [Test()]
        public void ValidateTightRangeTest()
        {
            //-- Arrange
            var min = OnlinerLTimeOfDay.MinValue;
            var max = OnlinerLTimeOfDay.MaxValue;
            var mid = (OnlinerLTimeOfDay.MaxValue / 2);

            //-- Act  
            Assert.True(Onliner.Validator.Validate(mid, System.Globalization.CultureInfo.InvariantCulture).IsValid);
            Assert.True(Onliner.Validator.Validate(min, System.Globalization.CultureInfo.InvariantCulture).IsValid);
            Assert.True(Onliner.Validator.Validate(max, System.Globalization.CultureInfo.InvariantCulture).IsValid);
        }

        [Test()]
        public void ValidateOverShootRangePresetTest()
        {
            Onliner.AttributeMinimum = TimeSpan.FromTicks(10);
            Onliner.AttributeMaximum = TimeSpan.FromHours(23);

            //-- Arrange
            var min = OnlinerLTimeOfDay.MinValue;
            var max = OnlinerLTimeOfDay.MaxValue;

            //-- Act  
            Assert.IsFalse(Onliner.Validator.Validate(min, System.Globalization.CultureInfo.InvariantCulture).IsValid);
            Assert.IsFalse(Onliner.Validator.Validate(max, System.Globalization.CultureInfo.InvariantCulture).IsValid);
        }

        [Test]
        public override void CanSetAsyncTest()
        {
            var expected = TimeSpan.FromHours(15);
            Onliner.SetAsync(expected).Wait();

            Assert.AreEqual(expected, Onliner.GetAsync().Result);
        }
    }
}