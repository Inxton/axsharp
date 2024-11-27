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

    public class OnlinerByteTest : OnlinerBaseTests<byte>
    {
        protected override OnlinerBase<byte> Onliner { get; set; }


        public override void Init()
        {
            Onliner = new OnlinerByte(new TestTwinObject(), $"readableTail", "symbolTail");
        }

        [Test()]
        public void ChangeEditedValueTest()
        {
            //-- Act
            Onliner.Edit = 150;

            //-- Assert
            Assert.AreEqual(150, Onliner.GetAsync().Result);
            Assert.AreEqual($"Edit of {Onliner.Symbol};{Onliner.HumanReadable};0;150", logs);
        }

        [Test]
        public void ChangeValueOverOnlinerInterfaceTest()
        {
            var iOnliner = (IOnline<byte>)this.Onliner;
            var iShadow = (IShadow<byte>)this.Onliner;

            iShadow.Value = 55;
            iOnliner.Value = 25;

            Assert.AreEqual(25, this.Onliner.Cyclic);
            Assert.AreEqual(25, this.Onliner.GetAsync().Result);
            Assert.AreEqual(55, this.Onliner.Shadow);
        }
        
        [Test]
        public void ChangeValueOverShadowInterfaceTest()
        {
            var iOnliner = (IOnline<byte>)this.Onliner;
            var iShadow = (IShadow<byte>)this.Onliner;

            iOnliner.Value = 111;
            iShadow.Value = 158;

            Assert.AreEqual(111, this.Onliner.Cyclic);
            Assert.AreEqual(111, this.Onliner.GetAsync().Result);
            Assert.AreEqual(158, this.Onliner.Shadow);
        }

        [Test()]
        public void ChangeShadow()
        {
            //-- Act
            Onliner.Shadow = 140;

            //-- Assert
            Assert.AreEqual(140, Onliner.Shadow);
            Assert.AreEqual($"Shadow of {Onliner.Symbol};{Onliner.HumanReadable};0;140", logs);
        }

        [Test()]
        public void ValidateInBuildRangeTest()
        {
            //-- Arrange
            var min = OnlinerByte.MinValue;
            var max = OnlinerByte.MaxValue;
            var mid = (byte)(OnlinerByte.MaxValue / 2);
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
            Onliner.AttributeMaximum = 100;
            Onliner.AttributeMinimum = 50;

            var min = (byte)Onliner.AttributeMinimum;
            var max = (byte)Onliner.AttributeMaximum;
            var mid = (byte)(max / 2);
            //-- Act  
            Assert.IsTrue(Onliner.Validator.Validate(mid, System.Globalization.CultureInfo.InvariantCulture).IsValid);
            Assert.IsTrue(Onliner.Validator.Validate(min, System.Globalization.CultureInfo.InvariantCulture).IsValid);
            Assert.IsTrue(Onliner.Validator.Validate(max, System.Globalization.CultureInfo.InvariantCulture).IsValid);
            Assert.IsFalse(Onliner.Validator.Validate((byte)(max + 1), System.Globalization.CultureInfo.InvariantCulture).IsValid);
            Assert.IsFalse(Onliner.Validator.Validate((byte)(min - 1), System.Globalization.CultureInfo.InvariantCulture).IsValid);
        }

        [Test()]
        public void ValidateOverShootRangeTest()
        {
            Onliner.AttributeMinimum = (byte)(OnlinerByte.MinValue + 1);
            Onliner.AttributeMaximum = (byte)(OnlinerByte.MaxValue - 1);

            //-- Arrange
            var min = OnlinerByte.MinValue;
            var max = OnlinerByte.MaxValue;


            //-- Act  
            Assert.IsFalse(Onliner.Validator.Validate(min, System.Globalization.CultureInfo.InvariantCulture).IsValid);
            Assert.IsFalse(Onliner.Validator.Validate(max, System.Globalization.CultureInfo.InvariantCulture).IsValid);
        }

        [Test]
        public override void CanSetAsyncTest()
        {
            Onliner.SetAsync((byte)(100)).Wait();

            Assert.AreEqual(100, Onliner.GetAsync().Result);
        }
    }
}