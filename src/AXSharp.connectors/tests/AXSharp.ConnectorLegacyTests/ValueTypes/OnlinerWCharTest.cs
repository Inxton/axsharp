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
    using System.Reflection;
    using AXSharp.Connector.Tests;
    using AXSharp.Connector.ValueTypes;
    using AXSharp.Connector.ValueTypes.Online;
    using AXSharp.Connector.ValueTypes.Shadows;

    public class OnlinerWCharTest : OnlinerBaseTests<char>
    {
        protected override OnlinerBase<char> Onliner { get; set; }


        public override void Init()
        {
            Onliner = new OnlinerWChar(new TestTwinObject(), $"readableTail", "symbolTail");
        }

        [Test()]
        public void ChangeEditedValueTest()
        {
            //-- Act
            Onliner.Edit = 'w';

            //-- Assert
            Assert.AreEqual('w', Onliner.GetAsync().Result);
            Assert.AreEqual($"Edit of {Onliner.Symbol};{Onliner.HumanReadable};\0;w", logs);
        }

        [Test]
        public void ChangeValueOverOnlinerInterfaceTest()
        {
            var iOnliner = (IOnline<char>)this.Onliner;
            var iShadow = (IShadow<char>)this.Onliner;

            iShadow.Value = 's';
            iOnliner.Value = 'x';

            Assert.AreEqual('x', this.Onliner.Cyclic);
            Assert.AreEqual('x', this.Onliner.GetAsync().Result);
            Assert.AreEqual('s', this.Onliner.Shadow);
        }
        
        [Test]
        public void ChangeValueOverShadowInterfaceTest()
        {
            var iOnliner = (IOnline<char>)this.Onliner;
            var iShadow = (IShadow<char>)this.Onliner;

            iOnliner.Value = 'f';
            iShadow.Value = 'u';

            Assert.AreEqual('f', this.Onliner.Cyclic);
            Assert.AreEqual('f', this.Onliner.GetAsync().Result);
            Assert.AreEqual('u', this.Onliner.Shadow);
        }

        [Test()]
        public void ChangeShadow()
        {
            //-- Act
            Onliner.Shadow = 'g';

            //-- Assert
            Assert.AreEqual('g', Onliner.Shadow);
            Assert.AreEqual($"Shadow of {Onliner.Symbol};{Onliner.HumanReadable};\0;g", logs);
        }

        [Test()]
        public void ValidateInBuildRangeTest()
        {
            //-- Arrange
            var min = OnlinerWChar.MinValue;
            var max = OnlinerWChar.MaxValue;
            var mid = (char)(OnlinerWChar.MaxValue / 2);
            //-- Act  
            Assert.IsTrue(Onliner.Validator.Validate(mid, System.Globalization.CultureInfo.InvariantCulture).IsValid);
            Assert.IsTrue(Onliner.Validator.Validate(min, System.Globalization.CultureInfo.InvariantCulture).IsValid);
            Assert.IsTrue(Onliner.Validator.Validate(max, System.Globalization.CultureInfo.InvariantCulture).IsValid);
            //Assert.IsFalse(Onliner.Validator.Validate((byte)(max), System.Globalization.CultureInfo.InvariantCulture).IsValid);
            //Assert.IsFalse(Onliner.Validator.Validate((byte)(min), System.Globalization.CultureInfo.InvariantCulture).IsValid);
        }

        [Test()]
        public void ValidateInCustomRangeTest()
        {
            //-- Arrange
            Onliner.AttributeMaximum = 'ž';
            Onliner.AttributeMinimum = 'ä';

            var min = (char)Onliner.AttributeMinimum;
            var max = (char)Onliner.AttributeMaximum;
            var mid = 'ú';
            //-- Act  
            Assert.IsTrue(Onliner.Validator.Validate(mid, System.Globalization.CultureInfo.InvariantCulture).IsValid);
            Assert.IsTrue(Onliner.Validator.Validate(min, System.Globalization.CultureInfo.InvariantCulture).IsValid);
            Assert.IsTrue(Onliner.Validator.Validate(max, System.Globalization.CultureInfo.InvariantCulture).IsValid);
            Assert.IsFalse(Onliner.Validator.Validate((char)(max + 1), System.Globalization.CultureInfo.InvariantCulture).IsValid);
            Assert.IsFalse(Onliner.Validator.Validate((char)(min - 1), System.Globalization.CultureInfo.InvariantCulture).IsValid);
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
            Assert.IsFalse(Onliner.Validator.Validate(min, System.Globalization.CultureInfo.InvariantCulture).IsValid);
            Assert.IsFalse(Onliner.Validator.Validate(max, System.Globalization.CultureInfo.InvariantCulture).IsValid);
        }

        [Test]
        public override void CanSetAsyncTest()
        {
            Onliner.SetAsync((char)(100)).Wait();

            Assert.AreEqual(((char)(100)), Onliner.GetAsync().Result);
        }
    }
}