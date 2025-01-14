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
    using System.Reflection;
    using AXSharp.Connector.Tests;
    using AXSharp.Connector.ValueTypes;
    using AXSharp.Connector.ValueTypes.Online;
    using AXSharp.Connector.ValueTypes.Shadows;

    public class OnlinerCharTest : OnlinerBaseTests<char>
    {
        protected override OnlinerBase<char> Onliner { get; set; }


        public override void Init()
        {
            Onliner = new OnlinerChar(new TestTwinObject(), $"readableTail", "symbolTail");
        }

        [Test()]
        public void ChangeEditedValueTest()
        {
            //-- Act
            Onliner.Edit = 'w';

            //-- Assert
            Assert.That(Onliner.GetAsync().Result, Is.EqualTo('w'));
            Assert.That(logs, Is.EqualTo($"Edit of {Onliner.Symbol};{Onliner.HumanReadable};\0;w"));
        }

        [Test]
        public void ChangeValueOverOnlinerInterfaceTest()
        {
            var iOnliner = (IOnline<char>)this.Onliner;
            var iShadow = (IShadow<char>)this.Onliner;

            iShadow.Value = 's';
            iOnliner.Value = 'x';

            Assert.That(this.Onliner.Cyclic, Is.EqualTo('x'));
            Assert.That(this.Onliner.GetAsync().Result, Is.EqualTo('x'));
            Assert.That(this.Onliner.Shadow, Is.EqualTo('s'));
        }

        [Test]
        public void ChangeValueOverShadowInterfaceTest()
        {
            var iOnliner = (IOnline<char>)this.Onliner;
            var iShadow = (IShadow<char>)this.Onliner;

            iOnliner.Value = 'f';
            iShadow.Value = 'u';

            Assert.That(this.Onliner.Cyclic, Is.EqualTo('f'));
            Assert.That(this.Onliner.GetAsync().Result, Is.EqualTo('f'));
            Assert.That(this.Onliner.Shadow, Is.EqualTo('u'));
        }

        [Test()]
        public void ChangeShadow()
        {
            //-- Act
            Onliner.Shadow = 'g';

            //-- Assert
            Assert.That(Onliner.Shadow, Is.EqualTo('g'));
            Assert.That(logs, Is.EqualTo($"Shadow of {Onliner.Symbol};{Onliner.HumanReadable};\0;g"));
        }

        [Test()]
        public void ValidateInBuildRangeTest()
        {
            //-- Arrange
            var min = OnlinerChar.MinValue;
            var max = OnlinerChar.MaxValue;
            var mid = (char)(OnlinerChar.MaxValue / 2);
            //-- Act  
            Assert.That(Onliner.Validator.Validate(mid, System.Globalization.CultureInfo.InvariantCulture).IsValid, Is.True);
            Assert.That(Onliner.Validator.Validate(min, System.Globalization.CultureInfo.InvariantCulture).IsValid, Is.True);
            Assert.That(Onliner.Validator.Validate(max, System.Globalization.CultureInfo.InvariantCulture).IsValid, Is.True);
            //Assert.IsFalse(Onliner.Validator.Validate((byte)(max), System.Globalization.CultureInfo.InvariantCulture).IsValid);
            //Assert.IsFalse(Onliner.Validator.Validate((byte)(min), System.Globalization.CultureInfo.InvariantCulture).IsValid);
        }

        [Test()]
        public void ValidateInCustomRangeTest()
        {
            //-- Arrange
            Onliner.AttributeMaximum = 'y';
            Onliner.AttributeMinimum = 'a';

            var min = (char)Onliner.AttributeMinimum;
            var max = (char)Onliner.AttributeMaximum;
            var mid = 'u';
            //-- Act  
            Assert.That(Onliner.Validator.Validate(mid, System.Globalization.CultureInfo.InvariantCulture).IsValid, Is.True);
            Assert.That(Onliner.Validator.Validate(min, System.Globalization.CultureInfo.InvariantCulture).IsValid, Is.True);
            Assert.That(Onliner.Validator.Validate(max, System.Globalization.CultureInfo.InvariantCulture).IsValid, Is.True);
            Assert.That(Onliner.Validator.Validate((char)(max + 1), System.Globalization.CultureInfo.InvariantCulture).IsValid, Is.False);
            Assert.That(Onliner.Validator.Validate((char)(min - 1), System.Globalization.CultureInfo.InvariantCulture).IsValid, Is.False);
        }

        [Test()]
        public void ValidateOverShootRangeTest()
        {
            Onliner.AttributeMinimum = (char)(OnlinerChar.MinValue + 1);
            Onliner.AttributeMaximum = (char)(OnlinerChar.MaxValue - 1);

            //-- Arrange
            var min = OnlinerChar.MinValue;
            var max = OnlinerChar.MaxValue;


            //-- Act  
            Assert.That(Onliner.Validator.Validate(min, System.Globalization.CultureInfo.InvariantCulture).IsValid, Is.False);
            Assert.That(Onliner.Validator.Validate(max, System.Globalization.CultureInfo.InvariantCulture).IsValid, Is.False);
        }

        [Test]
        public override void CanSetAsyncTest()
        {
            Onliner.SetAsync((char)(100)).Wait();

            Assert.That(Onliner.GetAsync().Result, Is.EqualTo((char)(100)));
        }
    }
}