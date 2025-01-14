// AXSharp.ConnectorLegacyTests
// Copyright (c) 2023 MTS spol. s r.o.,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/inxton/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/inxton/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/inxton/axsharp/blob/master/notices.md

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

        [Test]
        public void ChangeEditedValueTest()
        {
            //-- Arrange
            var expected = DateTime.Now;
            var original = new DateTime().ToString();

            //-- Act
            Onliner.Edit = expected;

            //-- Assert
            Assert.That(Onliner.GetAsync().Result, Is.EqualTo(expected));
            Assert.That(logs, Is.EqualTo($"Edit of {Onliner.Symbol};{Onliner.HumanReadable};{original};{expected}"));
        }

        [Test]
        public void ChangeShadow()
        {
            //-- Arrange
            var expected = DateTime.Now;
            var original = new DateTime().ToString();

            //-- Act
            Onliner.Shadow = expected;

            //-- Assert
            Assert.That(Onliner.Shadow, Is.EqualTo(expected));
            Assert.That(logs, Is.EqualTo($"Shadow of {Onliner.Symbol};{Onliner.HumanReadable};{original};{expected}"));
        }

        [Test]
        public void ValidateRangeTest()
        {
            //-- Arrange
            var min = OnlinerDateTime.MinValue;
            var max = OnlinerDateTime.MaxValue;
            var mid = DateTime.Now.Date;

            //-- Act  
            Assert.That(Onliner.Validator.Validate(mid, System.Globalization.CultureInfo.InvariantCulture).IsValid, Is.True);
            Assert.That(Onliner.Validator.Validate(min, System.Globalization.CultureInfo.InvariantCulture).IsValid, Is.True);
            Assert.That(Onliner.Validator.Validate(max, System.Globalization.CultureInfo.InvariantCulture).IsValid, Is.True);
        }

        [Test]
        public void ValidateOverShootRangeTest()
        {
            //-- Arrange
            var min = new DateTime(1969, 12, 31, 23, 59, 59, 100);
            var max = OnlinerDateTime.MaxValue.AddDays(1);

            //-- Act  
            Assert.That(Onliner.Validator.Validate(min, System.Globalization.CultureInfo.InvariantCulture).IsValid, Is.False);
            Assert.That(Onliner.Validator.Validate(max, System.Globalization.CultureInfo.InvariantCulture).IsValid, Is.False);
        }

        [Test]
        public void ValidateOverShootRangePresetTest()
        {
            Onliner.AttributeMinimum = new DateTime(1970, 1, 2);
            Onliner.AttributeMaximum = new DateTime(2262, 4, 10);

            //-- Arrange
            var min = OnlinerDateTime.MinValue;
            var max = OnlinerDateTime.MaxValue;

            //-- Act  
            Assert.That(Onliner.Validator.Validate(min, System.Globalization.CultureInfo.InvariantCulture).IsValid, Is.False);
            Assert.That(Onliner.Validator.Validate(max, System.Globalization.CultureInfo.InvariantCulture).IsValid, Is.False);
        }

        [Test]
        public override void CanSetAsyncTest()
        {
            var expected = DateTime.Now;
            Onliner.SetAsync(expected).Wait();

            Assert.That(Onliner.GetAsync().Result, Is.EqualTo(expected));
        }
    }
}