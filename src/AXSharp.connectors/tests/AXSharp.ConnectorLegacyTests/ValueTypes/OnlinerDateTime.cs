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

    public class OnlinerDateTimeTest : OnlinerBaseTests<DateTime>
    {
        protected override OnlinerBase<DateTime> Onliner { get; set; }


        public override void Init()
        {
            Onliner = new OnlinerDateTime(new TestTwinObject(), $"readableTail", "symbolTail");

        }

        [Test()]
        public void ChangeEditedValueTest()
        {
            //-- Arrange

            var expected = DateTime.Now;
            var original = new DateTime().ToString();

            //-- Act
            Onliner.Edit = expected;

            //-- Assert
            Assert.AreEqual(expected, Onliner.GetAsync().Result);
            Assert.AreEqual($"Edit of {Onliner.Symbol};{Onliner.HumanReadable};{original};{expected}", logs);

        }

        [Test()]
        public void ChangeShadow()
        {
            //-- Arrange

            var expected = DateTime.Now;
            var original = new DateTime().ToString();

            //-- Act
            Onliner.Shadow = expected;

            //-- Assert
            Assert.AreEqual(expected, Onliner.Shadow);
            Assert.AreEqual($"Shadow of {Onliner.Symbol};{Onliner.HumanReadable};{original};{expected}", logs);
        }

        [Test()]
        public void ValidateRangeTest()
        {
            //-- Arrange
            var min = OnlinerDateTime.MinValue;
            var max = OnlinerDateTime.MaxValue;
            var mid = DateTime.Now.Date;
            //-- Act  
            Assert.IsTrue(Onliner.Validator.Validate(mid, System.Globalization.CultureInfo.InvariantCulture).IsValid);
            Assert.IsTrue(Onliner.Validator.Validate(min, System.Globalization.CultureInfo.InvariantCulture).IsValid);
            Assert.IsTrue(Onliner.Validator.Validate(max, System.Globalization.CultureInfo.InvariantCulture).IsValid);
        }

        [Test()]
        public void ValidateOverShootRangeTest()
        {
            //-- Arrange
            var min = new DateTime(1969, 12, 31, 23, 59, 59, 100);
            var max = OnlinerDateTime.MaxValue.AddDays(1);

            
            //-- Act  
            Assert.IsFalse(Onliner.Validator.Validate(min, System.Globalization.CultureInfo.InvariantCulture).IsValid);
            Assert.IsFalse(Onliner.Validator.Validate(max, System.Globalization.CultureInfo.InvariantCulture).IsValid);
        }

        [Test()]
        public void ValidateOverShootRangePresetTest()
        {

            Onliner.AttributeMinimum = new DateTime(1970, 1, 2);
            Onliner.AttributeMaximum = new DateTime(2262, 4, 10);
            //-- Arrange
            var min = OnlinerDateTime.MinValue;
            var max = OnlinerDateTime.MaxValue;


            //-- Act  
            Assert.IsFalse(Onliner.Validator.Validate(min, System.Globalization.CultureInfo.InvariantCulture).IsValid);
            Assert.IsFalse(Onliner.Validator.Validate(max, System.Globalization.CultureInfo.InvariantCulture).IsValid);
        }

        [Test]
        public override void CanSetAsyncTest()
        {
            var expected = DateTime.Now;
            Onliner.SetAsync(expected).Wait();

            Assert.AreEqual(expected, Onliner.GetAsync().Result);
        }
    }
}