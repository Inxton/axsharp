// AXSharp.ConnectorLegacyTests
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/ix-ax/axsharp/blob/master/notices.md

using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AXSharp.Connector;
using AXSharp.Connector.ValueTypes;

namespace AXSharp.ConnectorTests
{
    [TestFixture()]
    public class ITwinObjectExtensionsTests
    {
        private string EditValueChanges = string.Empty;
        private string ShadowValueChanges = string.Empty;

        private void DetectEditValueChange(ITwinPrimitive twinPrimitive, dynamic original, dynamic newValue)
        {
            EditValueChanges = $"{EditValueChanges}+Edit {original} : {newValue}";
        }

        private void DetectShadowValueChange(ITwinPrimitive twinPrimitive, dynamic original, dynamic newValue)
        {
            ShadowValueChanges = $"{ShadowValueChanges}+Shadow {original} : {newValue}";
        }

        [Test()]
        public void SubscribeEditValueChangeTest()
        {
            //-- Arrange
            var a = new TestObject();

            //-- Act
            var valueTags = a.RetrievePrimitives();

            //-- Assert
            Assert.AreEqual(42, valueTags.Count());

            
            //-- Subscribe
            a.SubscribeEditValueChange(DetectEditValueChange);

            foreach (var tag in valueTags)
            {
                Assert.IsInstanceOf(typeof(OnlinerBase.ValueChangeDelegate), tag.EditValueChange, tag.Symbol);
            }

            //-- Make change
            a.Bool.Edit = true;
            a.String.Edit = "hdfahks dhfkahs";

            Assert.AreEqual("+Edit False : True+Edit  : hdfahks dhfkahs", EditValueChanges);
        }

        [Test()]
        public void SubscribeShadowValueChangeTest()
        {
            //-- Arrange
            var a = new TestObject();

            //-- Act
            var valueTags = a.RetrievePrimitives();

            //-- Assert
            Assert.AreEqual(42, valueTags.Count());


            //-- Subscribe
            a.SubscribeShadowValueChange(DetectShadowValueChange);

            foreach (var tag in valueTags)
            {
                Assert.IsInstanceOf(typeof(OnlinerBase.ValueChangeDelegate), tag.ShadowValueChange, tag.Symbol);
            }

            //-- Make change
            a.Bool.Shadow = true;
            a.String.Shadow = "hdfahks dhfkahs";

            Assert.AreEqual("+Shadow False : True+Shadow  : hdfahks dhfkahs", ShadowValueChanges);
        }


    }

    public class TestObject : Connector.Tests.TestTwinObject
    {
        public TestObject() : base()
        {
            Nested = new TestObjectNested1(this, nameof(Nested), nameof(Nested));
            this.AddChild(Nested);
            this.Bool = new OnlinerBool(this, nameof(Bool), nameof(Bool));
            this.Byte = new OnlinerByte(this, nameof(Byte), nameof(Byte));
            this.Date = new OnlinerDate(this, nameof(Date), nameof(Date));
            this.DInt = new OnlinerDInt(this, nameof(DInt), nameof(DInt));
            this.DWord = new OnlinerDWord(this, nameof(DWord), nameof(DWord));
            this.Int = new OnlinerInt(this, nameof(Int), nameof(Int));
            this.LInt = new OnlinerLInt(this, nameof(LInt), nameof(LInt));
            this.LReal = new OnlinerLReal(this, nameof(LReal), nameof(LReal));
            this.LTime = new OnlinerLTime(this, nameof(LTime), nameof(LTime));
            this.LWord = new OnlinerLWord(this, nameof(LWord), nameof(LWord));
            this.Real = new OnlinerReal(this, nameof(Real), nameof(Real));
            this.SInt = new OnlinerSInt(this, nameof(SInt), nameof(SInt));
            this.String = new OnlinerString(this, nameof(String), nameof(String));
            this.Time = new OnlinerTime(this, nameof(Time), nameof(Time));
            this.TimeOfDay = new OnlinerTimeOfDay(this, nameof(TimeOfDay), nameof(TimeOfDay));
            this.UDInt = new OnlinerUDInt(this, nameof(UDInt), nameof(UDInt));
            this.UInt = new OnlinerUInt(this, nameof(UInt), nameof(UInt));
            this.ULInt = new OnlinerULInt(this, nameof(ULInt), nameof(ULInt));
            this.USInt = new OnlinerUSInt(this, nameof(USInt), nameof(USInt));
            this.Word = new OnlinerWord(this, nameof(Word), nameof(Word));
            this.WString = new OnlinerWString(this, nameof(WString), nameof(WString));
        }

        public TestObjectNested1 Nested { get; private set; }
        public OnlinerBool Bool { get; private set; }
        public OnlinerByte Byte { get; private set; }
        public OnlinerDate Date { get; private set; }
        public OnlinerDInt DInt { get; private set; }
        public OnlinerDWord DWord { get; private set; }
        public OnlinerInt Int { get; private set; }
        public OnlinerLInt LInt { get; private set; }
        public OnlinerLReal LReal { get; private set; }
        public OnlinerLTime LTime { get; private set; }
        public OnlinerLWord LWord { get; private set; }
        public OnlinerReal Real { get; private set; }
        public OnlinerSInt SInt { get; private set; }
        public OnlinerString String { get; private set; }
        public OnlinerTime Time { get; private set; }
        public OnlinerTimeOfDay TimeOfDay { get; private set; }
        public OnlinerUDInt UDInt { get; private set; }
        public OnlinerUInt UInt { get; private set; }
        public OnlinerULInt ULInt { get; private set; }
        public OnlinerUSInt USInt { get; private set; }
        public OnlinerWord Word { get; private set; }
        public OnlinerWString WString { get; private set; } 
    }

    public class TestObjectNested1 : Connector.Tests.TestTwinObject
    {
        public TestObjectNested1(ITwinObject parent, string symbolTail, string readableTail) : base()
        {

            this.Parent = parent;
            this.Bool = new OnlinerBool(this, nameof(Bool), nameof(Bool));
            this.Byte = new OnlinerByte(this, nameof(Byte), nameof(Byte));
            this.Date = new OnlinerDate(this, nameof(Date), nameof(Date));
            this.DInt = new OnlinerDInt(this, nameof(DInt), nameof(DInt));
            this.DWord = new OnlinerDWord(this, nameof(DWord), nameof(DWord));
            this.Int = new OnlinerInt(this, nameof(Int), nameof(Int));
            this.LInt = new OnlinerLInt(this, nameof(LInt), nameof(LInt));
            this.LReal = new OnlinerLReal(this, nameof(LReal), nameof(LReal));
            this.LTime = new OnlinerLTime(this, nameof(LTime), nameof(LTime));
            this.LWord = new OnlinerLWord(this, nameof(LWord), nameof(LWord));
            this.Real = new OnlinerReal(this, nameof(Real), nameof(Real));
            this.SInt = new OnlinerSInt(this, nameof(SInt), nameof(SInt));
            this.String = new OnlinerString(this, nameof(String), nameof(String));
            this.Time = new OnlinerTime(this, nameof(Time), nameof(Time));
            this.TimeOfDay = new OnlinerTimeOfDay(this, nameof(TimeOfDay), nameof(TimeOfDay));
            this.UDInt = new OnlinerUDInt(this, nameof(UDInt), nameof(UDInt));
            this.UInt = new OnlinerUInt(this, nameof(UInt), nameof(UInt));
            this.ULInt = new OnlinerULInt(this, nameof(ULInt), nameof(ULInt));
            this.USInt = new OnlinerUSInt(this, nameof(USInt), nameof(USInt));
            this.Word = new OnlinerWord(this, nameof(Word), nameof(Word));
            this.WString = new OnlinerWString(this, nameof(WString), nameof(WString));
        }

        public OnlinerBool Bool { get; private set; }
        public OnlinerByte Byte { get; private set; }
        public OnlinerDate Date { get; private set; }
        public OnlinerDInt DInt { get; private set; }
        public OnlinerDWord DWord { get; private set; }
        public OnlinerInt Int { get; private set; }
        public OnlinerLInt LInt { get; private set; }
        public OnlinerLReal LReal { get; private set; }
        public OnlinerLTime LTime { get; private set; }
        public OnlinerLWord LWord { get; private set; }
        public OnlinerReal Real { get; private set; }
        public OnlinerSInt SInt { get; private set; }
        public OnlinerString String { get; private set; }
        public OnlinerTime Time { get; private set; }
        public OnlinerTimeOfDay TimeOfDay { get; private set; }
        public OnlinerUDInt UDInt { get; private set; }
        public OnlinerUInt UInt { get; private set; }
        public OnlinerULInt ULInt { get; private set; }
        public OnlinerUSInt USInt { get; private set; }
        public OnlinerWord Word { get; private set; }
        public OnlinerWString WString { get; private set; }
    }
}