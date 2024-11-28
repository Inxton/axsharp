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

    public class OnlinerBoolTest : OnlinerBaseTests<bool>
    {
        protected override OnlinerBase<bool> Onliner { get; set; }


        public override void Init()
        {
            Onliner = new OnlinerBool(new TestTwinObject(), $"readableTail", "symbolTail");

        }

        [Test()]
        public void ChangeEditedValueTest()
        {
            //-- Act
            Onliner.Edit = true;

            //-- Assert
            Assert.That(Onliner.GetAsync().Result, Is.EqualTo(true));
            Assert.That(logs, Is.EqualTo($"Edit of {Onliner.Symbol};{Onliner.HumanReadable};False;True"));

        }

        [Test()]
        public void ChangeShadow()
        {
            //-- Act
            Onliner.Shadow = true;

            //-- Assert
            Assert.That(Onliner.Shadow, Is.EqualTo(true));
            Assert.That(logs, Is.EqualTo($"Shadow of {Onliner.Symbol};{Onliner.HumanReadable};False;True"));
        }

        [Test]
        public override void CanSetAsyncTest()
        {
            Onliner.SetAsync(false).Wait();
            Assert.That(Onliner.GetAsync().Result, Is.EqualTo(false));

            var expected = true;
            Onliner.SetAsync(expected).Wait();

            Assert.That(Onliner.GetAsync().Result, Is.EqualTo(expected));
        }
    }
}