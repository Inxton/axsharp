// AXSharp.ConnectorLegacyTests
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/ix-ax/axsharp/blob/master/notices.md

using NUnit.Framework;
using AXSharp.Connector;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AXSharp.Connector.Localizations;

namespace AXSharp.Connector.Tests
{
    [TestFixture()]
    public class StringInterpolatorTests
    {
        [Test()]
        public void InterpolateTest()
        {
            //-- Arrange
            var interpolatedObject = new InterpolationTestObject();
            var expected = "This is a InterpolatedValue string of InterpolationTestObject";

            //-- Act
            var actual = AXSharp.Connector.StringInterpolator.Interpolate("This is a |[AttributeInterpolated]| string of |[AttributeObjectType]|", interpolatedObject);

            //-- Assert
            Assert.AreEqual(expected, actual);
        }

        [Test()]
        public void InterpolateNestedMembersTest()
        {           
            //-- Arrange
            var interpolatedObject = new InterpolationTestObject();
            var expected = "This is a InterpolatedValue string of InterpolationTestObject First level Second level";
                            

            //-- Act
            var actual = AXSharp.Connector.StringInterpolator.Interpolate("This is a |[AttributeInterpolated]| string of |[AttributeObjectType]| |[Nested.AttributeFirstLevel]| |[Nested.NestedLevel2.AttributeSecondLevel]|", interpolatedObject);

            Console.WriteLine(expected);
            Console.WriteLine(actual);

            //-- Assert
            Assert.AreEqual(expected, actual);
        }

        [Test()]
        public void InterpolateAncestorTest()
        {
            //-- Arrange
            var interpolatedObject = new InterpolationTestObject();
            var expected = "This is a InterpolatedValue string of InterpolationTestObject First level Second level Second level";


            //-- Act
            var actual = AXSharp.Connector.StringInterpolator.Interpolate("This is a |[[2]AttributeInterpolated]| string of |[[2]AttributeObjectType]| |[[1]AttributeFirstLevel]| |[AttributeSecondLevel]| |[[2]Nested.NestedLevel2.AttributeSecondLevel]|", interpolatedObject.Nested.NestedLevel2);

            Console.WriteLine(expected);
            Console.WriteLine(actual);

            //-- Assert
            Assert.AreEqual(expected, actual);
        }

        [Test()]
        public void MalformedInterpolationTest()
        {
            //-- Arrange
            var interpolatedObject = new InterpolationTestObject();   


            //-- Act
            var actual = AXSharp.Connector.StringInterpolator.Interpolate("This is a |[2]]AttributeInterpolated]|", interpolatedObject);
          
            Console.WriteLine(actual);           
        }

        [Test()]
        public void OutOfRangeParentTest()
        {
            //-- Arrange
            var interpolatedObject = new InterpolationTestObject();
            var expected = "This is a [30]AttributeInterpolated";

            //-- Act
            var actual = AXSharp.Connector.StringInterpolator.Interpolate("This is a |[[30]AttributeInterpolated]|", interpolatedObject.Nested.NestedLevel2);

            Console.WriteLine(actual);

            Assert.AreEqual(expected, actual);
        }

    }




    public class NestedLevel1InterpolationTest : ITwinObject
    {

        public NestedLevel1InterpolationTest(ITwinObject parent)
        {
            this._parent = parent;
            NestedLevel2 = new NestedLevel2InterpolationTest(this);
        }

        public NestedLevel2InterpolationTest NestedLevel2 { get; set; }

        public string AttributeFirstLevel => "First level";

        public string Symbol => throw new NotImplementedException();

        public string AttributeName => throw new NotImplementedException();
        public string GetAttributeName(CultureInfo culture)
        {
           return this.Translate(this.AttributeName, culture);
        }

        public string GetHumanReadable(CultureInfo culture)
        {
           return this.Translate(this.HumanReadable, culture);
        }

        public string HumanReadable => throw new NotImplementedException();

        public void AddChild(ITwinObject twinObject)
        {
            throw new NotImplementedException();
        }

        public void AddValueTag(ITwinPrimitive twinPrimitive)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ITwinObject> GetChildren()
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

        private readonly ITwinObject _parent;

        public ITwinObject GetParent()
        {
            return _parent;
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

        public IEnumerable<ITwinPrimitive> GetValueTags()
        {
            throw new NotImplementedException();
        }

        public void AddKid(ITwinElement kid)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ITwinElement> GetKids()
        {
            throw new NotImplementedException();
        }
    }

    public class NestedLevel2InterpolationTest : ITwinObject
    {

        public NestedLevel2InterpolationTest(ITwinObject parent)
        {
            this._parent = parent;
        }

        public string AttributeSecondLevel => "Second level";


        public string Symbol => throw new NotImplementedException();

        public string AttributeName => throw new NotImplementedException();

        public string HumanReadable => throw new NotImplementedException();

        public string GetAttributeName(CultureInfo culture)
        {
            return this.Translate(this.AttributeName, culture);
        }

        public string GetHumanReadable(CultureInfo culture)
        {
            return this.Translate(this.HumanReadable, culture);
        }

        public void AddChild(ITwinObject twinObject)
        {
            throw new NotImplementedException();
        }

        public void AddValueTag(ITwinPrimitive twinPrimitive)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ITwinObject> GetChildren()
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

        private readonly ITwinObject _parent;

        public ITwinObject GetParent()
        {
            return _parent;
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

        public IEnumerable<ITwinPrimitive> GetValueTags()
        {
            throw new NotImplementedException();
        }

        public void AddKid(ITwinElement kid)
        {
            
        }

        public IEnumerable<ITwinElement> GetKids()
        {
            throw new NotImplementedException();
        }
    }

    public class InterpolationTestObject : ITwinObject
    {

        public InterpolationTestObject()
        {
            Nested = new NestedLevel1InterpolationTest(this);
        }

        public NestedLevel1InterpolationTest Nested { get; set; }

        public string AttributeInterpolated => "InterpolatedValue";

        public string AttributeObjectType => this.GetType().Name;

        public string Symbol => throw new NotImplementedException();

        public string AttributeName => throw new NotImplementedException();

        public string HumanReadable => throw new NotImplementedException();

        private ITwinObject _parent;

        public ITwinObject GetParent()
        {
            return _parent;
        }

        public string GetAttributeName(CultureInfo culture)
        {
            return this.Translate(this.AttributeName, culture);
        }

        public string GetHumanReadable(CultureInfo culture)
        {
            return this.Translate(this.HumanReadable, culture);
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

        public void AddKid(ITwinElement kid)
        {
           
        }

        public IEnumerable<ITwinElement> GetKids()
        {
            throw new NotImplementedException();
        }
    }
}