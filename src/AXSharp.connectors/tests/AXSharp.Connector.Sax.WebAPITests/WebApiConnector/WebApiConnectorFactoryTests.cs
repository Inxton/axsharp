// AXSharp.Connector.S71500.WebAPITests
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/ix-ax/axsharp/blob/master/notices.md

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AXSharp.Connector.Localizations;
using AXSharp.Connector.S71500.WebApi;
using Xunit;

namespace AXSharp.Connector.S71500.WebAPITests
{
    public class WebApiConnectorFactoryTests
    {

        public class DummyParent : ITwinObject
        {
            public DummyParent(string symbol)
            {
                Symbol = symbol;
                AttributeName = $"RootParent";
                this.HumanReadable = Connector.CreateSymbol(AttributeName, symbol);
            }

            public string Symbol { get; }
            public string AttributeName { get; }
            public string GetAttributeName(CultureInfo culture)
            {
                return AttributeName;
            }

            public string GetHumanReadable(CultureInfo culture)
            {
                return HumanReadable;
            }

            public string HumanReadable { get; }

            private IList<ITwinPrimitive> _primitives = new List<ITwinPrimitive>();
            private IList<ITwinElement> _kids = new List<ITwinElement>();

            public ITwinObject GetParent()
            {
                throw new NotImplementedException();
            }

            public string GetSymbolTail()
            {
                throw new NotImplementedException();
            }

            public void Poll()
            {
                throw new NotImplementedException();
            }

            public Translator Interpreter { get; }

            public IEnumerable<ITwinObject> GetChildren()
            {
                throw new NotImplementedException();
            }

            public IEnumerable<ITwinElement> GetKids()
            {
                throw new NotImplementedException();
            }

            public IEnumerable<ITwinPrimitive> GetValueTags()
            {
                throw new NotImplementedException();
            }

            public void AddChild(ITwinObject twinObject)
            {
                throw new NotImplementedException();
            }

            public void AddValueTag(ITwinPrimitive twinPrimitive)
            {
                _primitives.Add(twinPrimitive);
            }

            public void AddKid(ITwinElement kid)
            {
                _kids.Add(kid);
            }

            public Connector GetConnector()
            {
                return new DummyConnector();
            }

            public Task<T> OnlineToPlain<T>()
            {
                throw new NotImplementedException();
            }

            public Task PlainToOnline<T>(T plain)
            {
                throw new NotImplementedException();
            }

            public Task<T> ShadowToPlain<T>()
            {
                throw new NotImplementedException();
            }

            public Task PlainToShadow<T>(T plain)
            {
                throw new NotImplementedException();
            }

            public Task<bool> AnyChangeAsync<T>(T plain)
            {
                throw new NotImplementedException();
            }
        }

        
        [Fact]
        public void should_create_new_instance_of_bool()
        {
            var factory = new WebApiConnectorFactory();
            var expectedSymbol = "father.son";
            var expectedHumanReadable = "RootParent.father.mother";
            var actual = factory.CreateBOOL(new DummyParent("father"), "mother", "son");

            Assert.True(actual is WebApiBool);
            Assert.Equal(expectedSymbol, actual.Symbol);
            Assert.Equal(expectedHumanReadable, actual.HumanReadable);
        }

        [Fact]
        public void should_create_new_instance_of_byte()
        {
            var factory = new WebApiConnectorFactory();
            var expectedSymbol = "father.son";
            var expectedHumanReadable = "RootParent.father.mother";
            var actual = factory.CreateBYTE(new DummyParent("father"), "mother", "son");

            Assert.True(actual is WebApiByte);
            Assert.Equal(expectedSymbol, actual.Symbol);
            Assert.Equal(expectedHumanReadable, actual.HumanReadable);
        }

        [Fact]
        public void should_create_new_instance_of_date()
        {
            var factory = new WebApiConnectorFactory();
            var expectedSymbol = "father.son";
            var expectedHumanReadable = "RootParent.father.mother";
            var actual = factory.CreateDATE(new DummyParent("father"), "mother", "son");

            Assert.True(actual is WebApiDate);
            Assert.Equal(expectedSymbol, actual.Symbol);
            Assert.Equal(expectedHumanReadable, actual.HumanReadable);
        }

        [Fact]
        public void should_create_new_instance_of_date_and_time()
        {
            var factory = new WebApiConnectorFactory();
            var expectedSymbol = "father.son";
            var expectedHumanReadable = "RootParent.father.mother";
            var actual = factory.CreateDATE_AND_TIME(new DummyParent("father"), "mother", "son");

            Assert.True(actual is WebApiDateTime);
            Assert.Equal(expectedSymbol, actual.Symbol);
            Assert.Equal(expectedHumanReadable, actual.HumanReadable);
        }

        [Fact]
        public void should_create_new_instance_of_dint()
        {
            var factory = new WebApiConnectorFactory();
            var expectedSymbol = "father.son";
            var expectedHumanReadable = "RootParent.father.mother";
            var actual = factory.CreateDINT(new DummyParent("father"), "mother", "son");

            Assert.True(actual is WebApiDInt);
            Assert.Equal(expectedSymbol, actual.Symbol);
            Assert.Equal(expectedHumanReadable, actual.HumanReadable);
        }

        [Fact]
        public void should_create_new_instance_of_dword()
        {
            var factory = new WebApiConnectorFactory();
            var expectedSymbol = "father.son";
            var expectedHumanReadable = "RootParent.father.mother";
            var actual = factory.CreateDWORD(new DummyParent("father"), "mother", "son");

            Assert.True(actual is WebApiDWord);
            Assert.Equal(expectedSymbol, actual.Symbol);
            Assert.Equal(expectedHumanReadable, actual.HumanReadable);
        }

        [Fact]
        public void should_create_new_instance_of_int()
        {
            var factory = new WebApiConnectorFactory();
            var expectedSymbol = "father.son";
            var expectedHumanReadable = "RootParent.father.mother";
            var actual = factory.CreateINT(new DummyParent("father"), "mother", "son");

            Assert.True(actual is WebApiInt);
            Assert.Equal(expectedSymbol, actual.Symbol);
            Assert.Equal(expectedHumanReadable, actual.HumanReadable);
        }

        [Fact]
        public void should_create_new_instance_of_lint()
        {
            var factory = new WebApiConnectorFactory();
            var expectedSymbol = "father.son";
            var expectedHumanReadable = "RootParent.father.mother";
            var actual = factory.CreateLINT(new DummyParent("father"), "mother", "son");

            Assert.True(actual is WebApiLInt);
            Assert.Equal(expectedSymbol, actual.Symbol);
            Assert.Equal(expectedHumanReadable, actual.HumanReadable);
        }

        [Fact]
        public void should_create_new_instance_of_lreal()
        {
            var factory = new WebApiConnectorFactory();
            var expectedSymbol = "father.son";
            var expectedHumanReadable = "RootParent.father.mother";
            var actual = factory.CreateLREAL(new DummyParent("father"), "mother", "son");

            Assert.True(actual is WebApiLReal);
            Assert.Equal(expectedSymbol, actual.Symbol);
            Assert.Equal(expectedHumanReadable, actual.HumanReadable);
        }

        [Fact]
        public void should_create_new_instance_of_ltime()
        {
            var factory = new WebApiConnectorFactory();
            var expectedSymbol = "father.son";
            var expectedHumanReadable = "RootParent.father.mother";
            var actual = factory.CreateLTIME(new DummyParent("father"), "mother", "son");

            Assert.True(actual is WebApiLTime);
            Assert.Equal(expectedSymbol, actual.Symbol);
            Assert.Equal(expectedHumanReadable, actual.HumanReadable);
        }

        [Fact]
        public void should_create_new_instance_of_lword()
        {
            var factory = new WebApiConnectorFactory();
            var expectedSymbol = "father.son";
            var expectedHumanReadable = "RootParent.father.mother";
            var actual = factory.CreateLWORD(new DummyParent("father"), "mother", "son");

            Assert.True(actual is WebApiLWord);
            Assert.Equal(expectedSymbol, actual.Symbol);
            Assert.Equal(expectedHumanReadable, actual.HumanReadable);
        }

        [Fact]
        public void should_create_new_instance_of_real()
        {
            var factory = new WebApiConnectorFactory();
            var expectedSymbol = "father.son";
            var expectedHumanReadable = "RootParent.father.mother";
            var actual = factory.CreateREAL(new DummyParent("father"), "mother", "son");

            Assert.True(actual is WebApiReal);
            Assert.Equal(expectedSymbol, actual.Symbol);
            Assert.Equal(expectedHumanReadable, actual.HumanReadable);
        }

        [Fact]
        public void should_create_new_instance_of_sint()
        {
            var factory = new WebApiConnectorFactory();
            var expectedSymbol = "father.son";
            var expectedHumanReadable = "RootParent.father.mother";
            var actual = factory.CreateSINT(new DummyParent("father"), "mother", "son");

            Assert.True(actual is WebApiSInt);
            Assert.Equal(expectedSymbol, actual.Symbol);
            Assert.Equal(expectedHumanReadable, actual.HumanReadable);
        }

        [Fact]
        public void should_create_new_instance_of_string()
        {
            var factory = new WebApiConnectorFactory();
            var expectedSymbol = "father.son";
            var expectedHumanReadable = "RootParent.father.mother";
            var actual = factory.CreateSTRING(new DummyParent("father"), "mother", "son");

            Assert.True(actual is WebApiString);
            Assert.Equal(expectedSymbol, actual.Symbol);
            Assert.Equal(expectedHumanReadable, actual.HumanReadable);
        }

        [Fact]
        public void should_create_new_instance_of_time()
        {
            var factory = new WebApiConnectorFactory();
            var expectedSymbol = "father.son";
            var expectedHumanReadable = "RootParent.father.mother";
            var actual = factory.CreateTIME(new DummyParent("father"), "mother", "son");

            Assert.True(actual is WebApiTime);
            Assert.Equal(expectedSymbol, actual.Symbol);
            Assert.Equal(expectedHumanReadable, actual.HumanReadable);
        }

        [Fact]
        public void should_create_new_instance_of_timeofday()
        {
            var factory = new WebApiConnectorFactory();
            var expectedSymbol = "father.son";
            var expectedHumanReadable = "RootParent.father.mother";
            var actual = factory.CreateTIME_OF_DAY(new DummyParent("father"), "mother", "son");

            Assert.True(actual is WebApiTimeOfDay);
            Assert.Equal(expectedSymbol, actual.Symbol);
            Assert.Equal(expectedHumanReadable, actual.HumanReadable);
        }

        [Fact]
        public void should_create_new_instance_of_udint()
        {
            var factory = new WebApiConnectorFactory();
            var expectedSymbol = "father.son";
            var expectedHumanReadable = "RootParent.father.mother";
            var actual = factory.CreateUDINT(new DummyParent("father"), "mother", "son");

            Assert.True(actual is WebApiUdInt);
            Assert.Equal(expectedSymbol, actual.Symbol);
            Assert.Equal(expectedHumanReadable, actual.HumanReadable);
        }

        [Fact]
        public void should_create_new_instance_of_uint()
        {
            var factory = new WebApiConnectorFactory();
            var expectedSymbol = "father.son";
            var expectedHumanReadable = "RootParent.father.mother";
            var actual = factory.CreateUINT(new DummyParent("father"), "mother", "son");

            Assert.True(actual is WebApiUInt);
            Assert.Equal(expectedSymbol, actual.Symbol);
            Assert.Equal(expectedHumanReadable, actual.HumanReadable);
        }

        [Fact]
        public void should_create_new_instance_of_ulint()
        {
            var factory = new WebApiConnectorFactory();
            var expectedSymbol = "father.son";
            var expectedHumanReadable = "RootParent.father.mother";
            var actual = factory.CreateULINT(new DummyParent("father"), "mother", "son");

            Assert.True(actual is WebApiULInt);
            Assert.Equal(expectedSymbol, actual.Symbol);
            Assert.Equal(expectedHumanReadable, actual.HumanReadable);
        }

        [Fact]
        public void should_create_new_instance_of_usint()
        {
            var factory = new WebApiConnectorFactory();
            var expectedSymbol = "father.son";
            var expectedHumanReadable = "RootParent.father.mother";
            var actual = factory.CreateUSINT(new DummyParent("father"), "mother", "son");

            Assert.True(actual is WebApiUSInt);
            Assert.Equal(expectedSymbol, actual.Symbol);
            Assert.Equal(expectedHumanReadable, actual.HumanReadable);
        }

        [Fact]
        public void should_create_new_instance_of_word()
        {
            var factory = new WebApiConnectorFactory();
            var expectedSymbol = "father.son";
            var expectedHumanReadable = "RootParent.father.mother";
            var actual = factory.CreateWORD(new DummyParent("father"), "mother", "son");

            Assert.True(actual is WebApiWord);
            Assert.Equal(expectedSymbol, actual.Symbol);
            Assert.Equal(expectedHumanReadable, actual.HumanReadable);
        }

        [Fact]
        public void should_create_new_instance_of_wstring()
        {
            var factory = new WebApiConnectorFactory();
            var expectedSymbol = "father.son";
            var expectedHumanReadable = "RootParent.father.mother";
            var actual = factory.CreateWSTRING(new DummyParent("father"), "mother", "son");

            Assert.True(actual is WebApiWString);
            Assert.Equal(expectedSymbol, actual.Symbol);
            Assert.Equal(expectedHumanReadable, actual.HumanReadable);
        }
    }
}