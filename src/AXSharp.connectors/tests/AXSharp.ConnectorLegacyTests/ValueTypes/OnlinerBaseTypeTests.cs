// AXSharp.ConnectorLegacyTests
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/ix-ax/axsharp/blob/master/notices.md

using System.Runtime.CompilerServices;
using NUnit.Framework;
using AXSharp.Connector;

namespace AXSharp.Connector.Onliners.Tests
{
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AXSharp.Connector.Tests;
    using AXSharp.Connector.ValueTypes;
    using AXSharp.Connector.ValueTypes.Online;
    using System.Globalization;

    [TestFixture()]    
    public abstract class OnlinerBaseTests<T>
    {
        protected string logs;
        private void LogShadowValue(ITwinPrimitive twinPrimitive, dynamic original, dynamic newValue)
        {
            //DateTime originalParsed = DateTime.Parse(original.ToString());
            //DateTime newValueParsed = DateTime.Parse(newValue.ToString());
            logs = $"Shadow of {twinPrimitive.Symbol};{twinPrimitive.HumanReadable};{original};{newValue}";
        }

        private void LogEditValue(ITwinPrimitive twinPrimitive, dynamic original, dynamic newValue)
        {

            //DateTime originalParsed = DateTime.Parse(original.ToString());
            //DateTime newValueParsed = DateTime.Parse(newValue.ToString()) ;
            logs = $"Edit of {twinPrimitive.Symbol};{twinPrimitive.HumanReadable};{original};{newValue}";
        }

        public OnlinerBaseTests()
        {
            
        }

        protected abstract OnlinerBase<T> Onliner { get; set; }


        [SetUp]
        public virtual void SetUpTest()
        {
            Init();
            Onliner.ShadowValueChange = LogShadowValue;
            Onliner.EditValueChange = LogEditValue;
        }
        

        public abstract void Init();

        [Test()]
        
        public void GetSymbolTailTest()
        {
            Assert.That(Onliner.GetSymbolTail(), Is.EqualTo("symbolTail"));
        }

        [Test()]
        public void GetParentTest()
        {
            Assert.That(Onliner.GetParent(), Is.EqualTo(typeof(ITwinObject)));
        }

        

        [Test()]
        public void SubscribeWithHandlerTest()
        {
            //-- Arrange

            var action = new ValueChangedEventHandlerDelegate((s, e) => Console.WriteLine());
            //-- Act Subscribe
            Onliner.Subscribe(action);

            //-- Assert
            Assert.That(Onliner.GetValueChangeEventSubscribers().Count(), Is.EqualTo(1));
        }

        [Test()]
        public void SubscribeTest()
        {
            //-- Act Subscribe
            Onliner.Subscribe();

            //-- Assert
            Assert.That(Onliner.IsSubscribed, Is.True);
        }

        [Test()]
        public void SubscribeMultipleTest()
        {
            //-- Arrange

            var action_a = new ValueChangedEventHandlerDelegate((s, e) => Console.WriteLine("a"));
            var action_b = new ValueChangedEventHandlerDelegate((s, e) => Console.WriteLine("b"));

            //-- Act 
            Onliner.Subscribe(action_a);
            Onliner.Subscribe(action_b);

            //-- Assert
            Assert.That(Onliner.GetValueChangeEventSubscribers().Count(), Is.EqualTo(2));

        }


        [Test()]
        public void UnSubscribeTest()
        {
            //-- Arrange

            var action_a = new ValueChangedEventHandlerDelegate((s, e) => Console.WriteLine("a"));
            var action_b = new ValueChangedEventHandlerDelegate((s, e) => Console.WriteLine("b"));

            //-- Act Subscribe
            Onliner.Subscribe(action_a);
            Onliner.Subscribe(action_b);

            //-- Assert
            Assert.That(Onliner.GetValueChangeEventSubscribers().Count(), Is.EqualTo(2));

            //-- Act UnSubscribe
            Onliner.UnSubscribe(action_a);
            Onliner.UnSubscribe(action_b);

            //-- Assert
            Assert.That(Onliner.GetValueChangeEventSubscribers().Count(), Is.EqualTo(0));
        }

        [Test]
        public virtual void CanSetAsyncTest()
        {
            Onliner.SetAsync((dynamic)(1)).Wait();

            Assert.That(Onliner.GetAsync().Result.ToString(), Is.EqualTo("1"));
        }

        [Test()]
        public void HasWriteAccessTestFalse()
        {
            
            //-- Assert
            Assert.That(Onliner.HasWriteAccess(), Is.True);

            //-- Arrange
            Onliner.MakeReadOnly(); // = AccessAttribute.ReadWriteAccess.Read;

            //-- Assert
            Assert.That(Onliner.HasWriteAccess(), Is.False);

            Onliner.GetParent().GetConnector().SuspendWriteProtection("Hoj morho vetvo mojho rodu, kto kramou rukou siahne na tvoju slobodu a co i dusu das v tom boji divokom vol nebyt ako byt otrokom!");

            Assert.That(Onliner.HasWriteAccess(), Is.True);

            Onliner.GetParent().GetConnector().ResumeWriteProtection();

            Assert.That(Onliner.HasWriteAccess(), Is.False);
        }

    }
}