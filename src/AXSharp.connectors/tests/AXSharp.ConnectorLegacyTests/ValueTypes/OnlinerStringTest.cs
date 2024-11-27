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

    public class OnlinerStringTest : OnlinerBaseTests<string>
    {
        protected override OnlinerBase<string> Onliner { get; set; }


        public override void Init()
        {
            Onliner = new OnlinerString(new TestTwinObject(), $"readableTail", "symbolTail");
        }

        [Test()]
        public void ChangeEditedValueTest()
        {
            //-- Arrange

            var expected = "fasdft345tgrsfgsery";

            //-- Act
            Onliner.Edit = expected;

            //-- Assert
            Assert.AreEqual(expected, Onliner.GetAsync().Result);
            Assert.AreEqual($"Edit of {Onliner.Symbol};{Onliner.HumanReadable};;{expected}", logs);

        }

        [Test()]
        public void ChangeEditedValueWithInvalidCharactersTest()
        {
            //-- Arrange            
            var expected = "fasdft345tgrsfgsery";
            Onliner.Edit = expected;

            //-- Act
            Onliner.Edit = "43ojtopgwj05tu*SE*DF:5┘>1Ç.ÞYF"; ;

            //-- Assert
            Assert.AreEqual(expected, Onliner.GetAsync().Result);
            Assert.AreEqual($"Edit of {Onliner.Symbol};{Onliner.HumanReadable};;{expected}", logs);

        }

        [Test()]
        public void ChangeShadow()
        {
            //-- Arrange

            var expected = "fasdft345tgrsfgsery";

            //-- Act
            Onliner.Shadow = expected;

            //-- Assert
            Assert.AreEqual(expected, Onliner.Shadow);
            Assert.AreEqual($"Shadow of {Onliner.Symbol};{Onliner.HumanReadable};;{expected}", logs);
        }

        [Test]
        public override void CanSetAsyncTest()
        {
            var expected = "some string";
            Onliner.SetAsync(expected).Wait();

            Assert.AreEqual(expected, Onliner.GetAsync().Result);
        }
    }
}