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

    public class OnlinerWordTest : OnlinerBaseTests<ushort>
    {
        protected override OnlinerBase<ushort> Onliner { get; set; }

        public override void Init()
        {
            Onliner = new OnlinerWord(new TestTwinObject(), $"readableTail", "symbolTail");
        }

        [Test()]
        public void ChangeEditedValueTest()
        {
            //-- Arrange
            var expected = ushort.MaxValue;

            //-- Act
            Onliner.Edit = expected;

            //-- Assert
            Assert.That(Onliner.GetAsync().Result, Is.EqualTo(expected));
            Assert.That(logs, Is.EqualTo($"Edit of {Onliner.Symbol};{Onliner.HumanReadable};0;{expected}"));
        }

        [Test()]
        public void ChangeShadow()
        {
            //-- Arrange
            var expected = ushort.MaxValue;

            //-- Act
            Onliner.Shadow = expected;

            //-- Assert
            Assert.That(Onliner.Shadow, Is.EqualTo(expected));
            Assert.That(logs, Is.EqualTo($"Shadow of {Onliner.Symbol};{Onliner.HumanReadable};0;{expected}"));
        }

        public void ValidateTightRangeTest()
        {
            //-- Arrange
            var min = OnlinerWord.MinValue;
            var max = OnlinerWord.MaxValue;
            var mid = (ushort)(OnlinerWord.MaxValue / 2);

            //-- Act  
            Assert.That(Onliner.Validator.Validate(mid, System.Globalization.CultureInfo.InvariantCulture).IsValid, Is.True);
            Assert.That(Onliner.Validator.Validate(min, System.Globalization.CultureInfo.InvariantCulture).IsValid, Is.True);
            Assert.That(Onliner.Validator.Validate(max, System.Globalization.CultureInfo.InvariantCulture).IsValid, Is.True);
        }

        [Test()]
        public void ValidateOverShootRangePresetTest()
        {
            Onliner.AttributeMinimum = (ushort)(OnlinerWord.MinValue + 1);
            Onliner.AttributeMaximum = (ushort)(OnlinerWord.MaxValue - 1);

            //-- Arrange
            var min = OnlinerWord.MinValue;
            var max = OnlinerWord.MaxValue;

            //-- Act  
            Assert.That(Onliner.Validator.Validate(min, System.Globalization.CultureInfo.InvariantCulture).IsValid, Is.False);
            Assert.That(Onliner.Validator.Validate(max, System.Globalization.CultureInfo.InvariantCulture).IsValid, Is.False);
        }

        [Test]
        public override void CanSetAsyncTest()
        {
            var expected = (ushort)(OnlinerWord.MaxValue / 15);
            Onliner.SetAsync(expected).Wait();

            Assert.That(Onliner.GetAsync().Result, Is.EqualTo(expected));
        }
    }
}