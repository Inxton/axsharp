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

    public class OnlinerRealTest : OnlinerBaseTests<float>
    {
        protected override OnlinerBase<float> Onliner { get; set; }


        public override void Init()
        {
            Onliner = new OnlinerReal(new TestTwinObject(), $"readableTail", "symbolTail");
        }

        [Test()]
        public void ChangeEditedValueTest()
        {
            //-- Arrange

            var expected = float.MaxValue;

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

            var expected = float.MaxValue;

            //-- Act
            Onliner.Shadow = expected;

            //-- Assert
            Assert.That(Onliner.Shadow, Is.EqualTo(expected));
            Assert.That(logs, Is.EqualTo($"Shadow of {Onliner.Symbol};{Onliner.HumanReadable};0;{expected}"));
        }

        [Test()]
        public void ValidateTightRangeTest()
        {
            //-- Arrange
            var min = OnlinerReal.MinValue;
            var max = OnlinerReal.MaxValue;
            var mid = (OnlinerReal.MaxValue / 2);

            //-- Act  
            Assert.That(Onliner.Validator.Validate(mid, System.Globalization.CultureInfo.InvariantCulture).IsValid, Is.True);
            Assert.That(Onliner.Validator.Validate(min, System.Globalization.CultureInfo.InvariantCulture).IsValid, Is.True);
            Assert.That(Onliner.Validator.Validate(max, System.Globalization.CultureInfo.InvariantCulture).IsValid, Is.True);
        }

        [Test()]
        public void ValidateOverShootRangePresetTest()
        {

            Onliner.AttributeMinimum = (OnlinerReal.MinValue + 0.402823466e+37f);
            Onliner.AttributeMaximum = (OnlinerReal.MaxValue - 0.402823466e+37f);
            //-- Arrange
            var min = OnlinerReal.MinValue;
            var max = OnlinerReal.MaxValue;

            //-- Act  
            Assert.That(Onliner.Validator.Validate(min, System.Globalization.CultureInfo.InvariantCulture).IsValid, Is.False);
            Assert.That(Onliner.Validator.Validate(max, System.Globalization.CultureInfo.InvariantCulture).IsValid, Is.False);
        }
    }
}