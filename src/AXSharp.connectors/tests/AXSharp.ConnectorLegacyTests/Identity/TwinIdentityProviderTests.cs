// AXSharp.ConnectorLegacyTests
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/ix-ax/axsharp/blob/master/notices.md

using NUnit.Framework;
using AXSharp.Connector.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using AXSharp.Connector.Localizations;
using AXSharp.Connector.ValueTypes;
using System.Globalization;

namespace AXSharp.Connector.Identity.Tests
{
    [TestFixture()]
    public class TwinIdentityProviderTests
    {
        [Test()]
        public async Task AddIdentityTest()
        {
            //-- Arrange
            var identityProvider = new TwinIdentityProvider(new DummyConnector());


            //-- Act
#pragma warning disable CS0618 // Type or member is obsolete
            identityProvider.AddIdentity(new IdentityTestObject() { Identity = new OnlinerULInt() { Cyclic = 1000, SetLastValue = 1000 }, AttributeName = "n1000", Symbol = "s1000" });
            identityProvider.AddIdentity(new IdentityTestObject() { Identity = new OnlinerULInt() { Cyclic = 2000, SetLastValue = 2000 }, AttributeName = "n2000", Symbol = "s2000" });
#pragma warning restore CS0618 // Type or member is obsolete
            await identityProvider.SortIdentitiesAsync();

            //-- Assert
            var actual = identityProvider.GetTwinByIdentity(1000);
            Assert.AreEqual("s1000", actual.Symbol);
            Assert.AreEqual("n1000", actual.AttributeName);

        }

        [Test()]
        public async Task GetTwinByIdentityTest()
        {
            //-- Arrange
            var identityProvider = new TwinIdentityProvider(new DummyConnector());

            //-- Act
#pragma warning disable CS0618 // Type or member is obsolete
            identityProvider.AddIdentity(new IdentityTestObject() { Identity = new OnlinerULInt() { Cyclic = 1000, SetLastValue = 1000 }, AttributeName = "n1000", Symbol = "s1000" });
            identityProvider.AddIdentity(new IdentityTestObject() { Identity = new OnlinerULInt() { Cyclic = 2000, SetLastValue = 2000 }, AttributeName = "n2000", Symbol = "s2000" });
#pragma warning restore CS0618 // Type or member is obsolete
            await identityProvider.SortIdentitiesAsync();

            //-- Assert
            var actual = identityProvider.GetTwinByIdentity(1000);
            Assert.AreEqual("s1000", actual.Symbol);
            Assert.AreEqual("n1000", actual.AttributeName);

            actual = identityProvider.GetTwinByIdentity(2000);
            Assert.AreEqual("s2000", actual.Symbol);
            Assert.AreEqual("n2000", actual.AttributeName);
        }

        [Test()]
        public async Task AddIdentityExistingTest()
        {
            //-- Arrange
            var identityProvider = new TwinIdentityProvider(new DummyConnector());

            //-- Act
#pragma warning disable CS0618 // Type or member is obsolete
            var ident = new IdentityTestObject() { Identity = new OnlinerULInt() { Cyclic = 1000, SetLastValue = 1000 }, AttributeName = "n1000", Symbol = "s1000" };
#pragma warning restore CS0618 // Type or member is obsolete
            identityProvider.AddIdentity(ident);
            identityProvider.AddIdentity(ident.MemberByIdentityObject);
            identityProvider.AddIdentity(ident.MemberByIdentityObjectNoAttribute);
            identityProvider.AddIdentity(ident.MemberByIdentityObjectNoAttributeDiffIdentity);
            
            await identityProvider.SortIdentitiesAsync();

            //-- Assert
            Assert.AreEqual(2, identityProvider.IdentitiesCount);

            var actual = identityProvider.GetTwinByIdentity(1000);            
            Assert.AreEqual("s1000", actual.Symbol);
            Assert.AreEqual("n1000", actual.AttributeName);


            actual = identityProvider.GetTwinByIdentity(255854);
            Assert.AreEqual(2, identityProvider.IdentitiesCount);
            Assert.AreEqual("x1000", actual.Symbol);
            Assert.AreEqual("x1000", actual.AttributeName);      
        }
    }

    public class IdentityTestObject : ITwinObject, ITwinIdentity
    {

        public IdentityTestObject()
        {
            memberByIdentityObject = new ByIdentityTestObject(this, nameof(MemberByIdentityObject));
            memberByIdentityObjectNoAttribute = new ByIdentityTestObject(this, nameof(MemberByIdentityObjectNoAttribute));
#pragma warning disable CS0618 // Type or member is obsolete
            memberByIdentityObjectNoAttributeDiffIdentity = new ByIdentityTestObject(this, nameof(MemberByIdentityObjectNoAttributeDiffIdentity)) { Identity = new OnlinerULInt() { Cyclic = 255854, SetLastValue = 255854 }, AttributeName = "x1000", Symbol = "x1000" }; ;
#pragma warning restore CS0618 // Type or member is obsolete
        }

        OnlinerULInt identity;
        public OnlinerULInt Identity
        {
            get
            {
                return identity;
            }
            set
            {
                identity = value;
                MemberByIdentityObject.Identity = value;
                MemberByIdentityObjectNoAttribute.Identity = value;                
            }
        }

        protected string SymbolTail { get; set; }

        public string AttributeName { get; set; }

        public string Symbol { get; set; }

        ByIdentityTestObject memberByIdentityObjectNoAttribute;
        ByIdentityTestObject memberByIdentityObjectNoAttributeDiffIdentity;
        ByIdentityTestObject memberByIdentityObject;
        [MemberByIdentity()]
        public ByIdentityTestObject MemberByIdentityObject
        {
            get
            {
                return memberByIdentityObject;
            }
            set
            {
                memberByIdentityObject = value;
            }
        }

        public ByIdentityTestObject MemberByIdentityObjectNoAttribute
        {
            get
            {
                return memberByIdentityObjectNoAttribute;
            }
            set
            {
                memberByIdentityObjectNoAttribute = value;
            }
        }

        public ByIdentityTestObject MemberByIdentityObjectNoAttributeDiffIdentity
        {
            get
            {
                return memberByIdentityObjectNoAttributeDiffIdentity;
            }
            set
            {
                memberByIdentityObjectNoAttributeDiffIdentity = value;
            }
        }

        public string HumanReadable => throw new NotImplementedException();

        public IEnumerable<ITwinObject> GetChildren()
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
            throw new NotImplementedException();
        }

        public string GetAttributeName(CultureInfo culture)
        {
            return this.Translate(this.AttributeName, culture);
        }

        public string GetHumanReadable(CultureInfo culture)
        {
            return this.Translate(this.HumanReadable, culture);
        }

        public Connector GetConnector()
        {
            throw new NotImplementedException();
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

        public object OnlineToPlain()
        {
            throw new NotImplementedException();
        }

        public void PlainToOnline(object plain)
        {
            throw new NotImplementedException();
        }

        public object ShadowToPlain()
        {
            throw new NotImplementedException();
        }

        public void PlainToShadow(object plain)
        {
            throw new NotImplementedException();
        }

        public ITwinObject GetParent()
        {
            return this;
        }

        public string GetSymbolTail()
        {
            return string.Empty;
        }

        public void Poll()
        {
            throw new NotImplementedException();
        }

        public Translator Interpreter { get; }

        public void AddKid(ITwinElement kid)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ITwinElement> GetKids()
        {
            throw new NotImplementedException();
        }
    }

    public class ByIdentityTestObject : ITwinObject, ITwinIdentity
    {

        public ByIdentityTestObject(ITwinObject parent, string symbolTail)
        {
            this.SymbolTail = symbolTail; 
            this.parent = parent;
        }
        OnlinerULInt identity;
        string SymbolTail;
        public OnlinerULInt Identity
        {
            get
            {
                return identity;
            }
            set
            {
                identity = value;
            }
        }
        

        public string AttributeName { get; set; }

        public string Symbol { get; set; }

        public string HumanReadable => throw new NotImplementedException();

        public string GetAttributeName(CultureInfo culture)
        {
            return this.Translate(this.AttributeName, culture);
        }

        public string GetHumanReadable(CultureInfo culture)
        {
            return this.Translate(this.HumanReadable, culture);
        }

        public IEnumerable<ITwinObject> GetChildren()
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
            throw new NotImplementedException();
        }

        public Connector GetConnector()
        {
            throw new NotImplementedException();
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

        public object OnlineToPlain()
        {
            throw new NotImplementedException();
        }

        public void PlainToOnline(object plain)
        {
            throw new NotImplementedException();
        }

        public object ShadowToPlain()
        {
            throw new NotImplementedException();
        }

        public void PlainToShadow(object plain)
        {
            throw new NotImplementedException();
        }

        ITwinObject parent;
        public ITwinObject GetParent()
        {
            return parent;
        }

        public string GetSymbolTail()
        {
            return this.SymbolTail;
        }

        public void Poll()
        {
            throw new NotImplementedException();
        }

        public Translator Interpreter { get; }

        public void AddKid(ITwinElement kid)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ITwinElement> GetKids()
        {
            throw new NotImplementedException();
        }
    }
}