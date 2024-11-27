// AXSharp.ConnectorTests
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/ix-ax/axsharp/blob/master/notices.md

using System.Xml.XPath;

namespace AXSharp.ConnectorTests
{
    using AXSharp.Connector;
    using System;
    using Xunit;
    using NSubstitute;

    public class DummyConnectorFactoryTests
    {
        private DummyConnectorFactory _testClass;

        public DummyConnectorFactoryTests()
        {
            _testClass = new DummyConnectorFactory();
        }

        [Fact]
        public void CanCallCreateConnector()
        {
            // Arrange
            var parameters = new[] { new object(), new object(), new object() };

            // Act
            var result = _testClass.CreateConnector(parameters);

            // Assert

            Assert.IsType<DummyConnector>(result);
        }

       

        [Fact]
        public void CanCallCreateBOOL()
        {
            // Arrange
            var parent = Substitute.For<ITwinObject>();
          
            var readableTail = "TestValue1756423506";
            var symbolTail = "TestValue683630235";

            var parentsSymbol = "parent_symbol";
            var parentsReadable = "ParentAttributeName";

            parent.Symbol.Returns(parentsSymbol);
            parent.AttributeName.Returns(parentsReadable);
            parent.HumanReadable.Returns(parentsReadable);

            // Act
            var result = _testClass.CreateBOOL(parent, readableTail, symbolTail);
            

            // Assert
            Assert.Equal(symbolTail, result.AttributeName); // returns symbolTail if attribute name null or empty.
            Assert.Equal(symbolTail, result.GetSymbolTail());

            Assert.Equal($"{parentsReadable}.{readableTail}", result.HumanReadable);
            Assert.Equal($"{parentsSymbol}.{symbolTail}", result.Symbol);
        }

        [Fact]
        public void CannotCallCreateBOOLWithNullParent()
        {
            Assert.Throws<ArgumentNullException>(() => _testClass.CreateBOOL(default(ITwinObject), "TestValue89646055", "TestValue1734561492"));
        }

        

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void CannotCallCreateBOOLWithInvalidSymbolTail(string value)
        {
            Assert.Throws<ArgumentNullException>(() => _testClass.CreateBOOL(Substitute.For<ITwinObject>(), "TestValue1438640864", value));
        }

        [Fact]
        public void CanCallCreateBYTE()
        {
            // Arrange
            var parent = Substitute.For<ITwinObject>();
            var readableTail = "TestValue671345412";
            var symbolTail = "TestValue59129990";

            var parentsSymbol = "parent_symbol";
            var parentsReadable = "ParentAttributeName";

            parent.Symbol.Returns(parentsSymbol);
            parent.AttributeName.Returns(parentsReadable);
            parent.HumanReadable.Returns(parentsReadable);

            // Act
            var result = _testClass.CreateBYTE(parent, readableTail, symbolTail);


            // Assert
            Assert.Equal(symbolTail, result.AttributeName); // returns symbolTail if attribute name null or empty.
            Assert.Equal(symbolTail, result.GetSymbolTail());

            Assert.Equal($"{parentsReadable}.{readableTail}", result.HumanReadable);
            Assert.Equal($"{parentsSymbol}.{symbolTail}", result.Symbol);
        }

        [Fact]
        public void CannotCallCreateBYTEWithNullParent()
        {
            Assert.Throws<ArgumentNullException>(() => _testClass.CreateBYTE(default(ITwinObject), "TestValue571254927", "TestValue553180355"));
        }

       

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void CannotCallCreateBYTEWithInvalidSymbolTail(string value)
        {
            Assert.Throws<ArgumentNullException>(() => _testClass.CreateBYTE(Substitute.For<ITwinObject>(), "TestValue2110586976", value));
        }

        [Fact]
        public void CanCallCreateDINT()
        {
            // Arrange
            var parent = Substitute.For<ITwinObject>();
            var readableTail = "TestValue1773137761";
            var symbolTail = "TestValue399632439";

            var parentsSymbol = "parent_symbol";
            var parentsReadable = "ParentAttributeName";

            parent.Symbol.Returns(parentsSymbol);
            parent.AttributeName.Returns(parentsReadable);
            parent.HumanReadable.Returns(parentsReadable);

            // Act
            var result = _testClass.CreateDINT(parent, readableTail, symbolTail);


            // Assert
            Assert.Equal(symbolTail, result.AttributeName); // returns symbolTail if attribute name null or empty.
            Assert.Equal(symbolTail, result.GetSymbolTail());

            Assert.Equal($"{parentsReadable}.{readableTail}", result.HumanReadable);
            Assert.Equal($"{parentsSymbol}.{symbolTail}", result.Symbol);
        }

        [Fact]
        public void CannotCallCreateDINTWithNullParent()
        {
            Assert.Throws<ArgumentNullException>(() => _testClass.CreateDINT(default(ITwinObject), "TestValue2006164208", "TestValue671464114"));
        }

        

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void CannotCallCreateDINTWithInvalidSymbolTail(string value)
        {
            Assert.Throws<ArgumentNullException>(() => _testClass.CreateDINT(Substitute.For<ITwinObject>(), "TestValue478875647", value));
        }

        [Fact]
        public void CanCallCreateDWORD()
        {
            // Arrange
            var parent = Substitute.For<ITwinObject>();
            var readableTail = "TestValue1957356104";
            var symbolTail = "TestValue1725266900";

            var parentsSymbol = "parent_symbol";
            var parentsReadable = "ParentAttributeName";

            parent.Symbol.Returns(parentsSymbol);
            parent.AttributeName.Returns(parentsReadable);
            parent.HumanReadable.Returns(parentsReadable);

            // Act
            var result = _testClass.CreateDWORD(parent, readableTail, symbolTail);


            // Assert
            Assert.Equal(symbolTail, result.AttributeName); // returns symbolTail if attribute name null or empty.
            Assert.Equal(symbolTail, result.GetSymbolTail());

            Assert.Equal($"{parentsReadable}.{readableTail}", result.HumanReadable);
            Assert.Equal($"{parentsSymbol}.{symbolTail}", result.Symbol);
        }

        [Fact]
        public void CannotCallCreateDWORDWithNullParent()
        {
            Assert.Throws<ArgumentNullException>(() => _testClass.CreateDWORD(default(ITwinObject), "TestValue64503484", "TestValue444590246"));
        }

        

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void CannotCallCreateDWORDWithInvalidSymbolTail(string value)
        {
            Assert.Throws<ArgumentNullException>(() => _testClass.CreateDWORD(Substitute.For<ITwinObject>(), "TestValue1351990217", value));
        }

        [Fact]
        public void CanCallCreateINT()
        {
            // Arrange
            var parent = Substitute.For<ITwinObject>();
            var readableTail = "TestValue1536783018";
            var symbolTail = "TestValue660774839";

            var parentsSymbol = "parent_symbol";
            var parentsReadable = "ParentAttributeName";

            parent.Symbol.Returns(parentsSymbol);
            parent.AttributeName.Returns(parentsReadable);
            parent.HumanReadable.Returns(parentsReadable);

            // Act
            var result = _testClass.CreateINT(parent, readableTail, symbolTail);


            // Assert
            Assert.Equal(symbolTail, result.AttributeName); // returns symbolTail if attribute name null or empty.
            Assert.Equal(symbolTail, result.GetSymbolTail());

            Assert.Equal($"{parentsReadable}.{readableTail}", result.HumanReadable);
            Assert.Equal($"{parentsSymbol}.{symbolTail}", result.Symbol);
        }

        [Fact]
        public void CannotCallCreateINTWithNullParent()
        {
            Assert.Throws<ArgumentNullException>(() => _testClass.CreateINT(default(ITwinObject), "TestValue881299633", "TestValue996738949"));
        }

        

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void CannotCallCreateINTWithInvalidSymbolTail(string value)
        {
            Assert.Throws<ArgumentNullException>(() => _testClass.CreateINT(Substitute.For<ITwinObject>(), "TestValue988516190", value));
        }

        [Fact]
        public void CanCallCreateLINT()
        {
            // Arrange
            var parent = Substitute.For<ITwinObject>();
            var readableTail = "TestValue1104545562";
            var symbolTail = "TestValue1369406484";

            var parentsSymbol = "parent_symbol";
            var parentsReadable = "ParentAttributeName";

            parent.Symbol.Returns(parentsSymbol);
            parent.AttributeName.Returns(parentsReadable);
            parent.HumanReadable.Returns(parentsReadable);

            // Act
            var result = _testClass.CreateLINT(parent, readableTail, symbolTail);


            // Assert
            Assert.Equal(symbolTail, result.AttributeName); // returns symbolTail if attribute name null or empty.
            Assert.Equal(symbolTail, result.GetSymbolTail());

            Assert.Equal($"{parentsReadable}.{readableTail}", result.HumanReadable);
            Assert.Equal($"{parentsSymbol}.{symbolTail}", result.Symbol);
        }

        [Fact]
        public void CannotCallCreateLINTWithNullParent()
        {
            Assert.Throws<ArgumentNullException>(() => _testClass.CreateLINT(default(ITwinObject), "TestValue383543523", "TestValue2138249354"));
        }

        

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void CannotCallCreateLINTWithInvalidSymbolTail(string value)
        {
            Assert.Throws<ArgumentNullException>(() => _testClass.CreateLINT(Substitute.For<ITwinObject>(), "TestValue1060004553", value));
        }

        [Fact]
        public void CanCallCreateLREAL()
        {
            // Arrange
            var parent = Substitute.For<ITwinObject>();
            var readableTail = "TestValue1046053404";
            var symbolTail = "TestValue281030175";

            var parentsSymbol = "parent_symbol";
            var parentsReadable = "ParentAttributeName";

            parent.Symbol.Returns(parentsSymbol);
            parent.AttributeName.Returns(parentsReadable);
            parent.HumanReadable.Returns(parentsReadable);

            // Act
            var result = _testClass.CreateLREAL(parent, readableTail, symbolTail);


            // Assert
            Assert.Equal(symbolTail, result.AttributeName); // returns symbolTail if attribute name null or empty.
            Assert.Equal(symbolTail, result.GetSymbolTail());

            Assert.Equal($"{parentsReadable}.{readableTail}", result.HumanReadable);
            Assert.Equal($"{parentsSymbol}.{symbolTail}", result.Symbol);
        }

        [Fact]
        public void CannotCallCreateLREALWithNullParent()
        {
            Assert.Throws<ArgumentNullException>(() => _testClass.CreateLREAL(default(ITwinObject), "TestValue1977914012", "TestValue162205035"));
        }

       

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void CannotCallCreateLREALWithInvalidSymbolTail(string value)
        {
            Assert.Throws<ArgumentNullException>(() => _testClass.CreateLREAL(Substitute.For<ITwinObject>(), "TestValue1602287525", value));
        }

        [Fact]
        public void CanCallCreateLTIME()
        {
            // Arrange
            var parent = Substitute.For<ITwinObject>();
            var readableTail = "TestValue851068072";
            var symbolTail = "TestValue749575129";

            var parentsSymbol = "parent_symbol";
            var parentsReadable = "ParentAttributeName";

            parent.Symbol.Returns(parentsSymbol);
            parent.AttributeName.Returns(parentsReadable);
            parent.HumanReadable.Returns(parentsReadable);

            // Act
            var result = _testClass.CreateLTIME(parent, readableTail, symbolTail);


            // Assert
            Assert.Equal(symbolTail, result.AttributeName); // returns symbolTail if attribute name null or empty.
            Assert.Equal(symbolTail, result.GetSymbolTail());

            Assert.Equal($"{parentsReadable}.{readableTail}", result.HumanReadable);
            Assert.Equal($"{parentsSymbol}.{symbolTail}", result.Symbol);
        }

        [Fact]
        public void CannotCallCreateLTIMEWithNullParent()
        {
            Assert.Throws<ArgumentNullException>(() => _testClass.CreateLTIME(default(ITwinObject), "TestValue1653602937", "TestValue2136419569"));
        }

       

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void CannotCallCreateLTIMEWithInvalidSymbolTail(string value)
        {
            Assert.Throws<ArgumentNullException>(() => _testClass.CreateLTIME(Substitute.For<ITwinObject>(), "TestValue1716702914", value));
        }

        [Fact]
        public void CanCallCreateLWORD()
        {
            // Arrange
            var parent = Substitute.For<ITwinObject>();
            var readableTail = "TestValue155070310";
            var symbolTail = "TestValue1605219842";

            var parentsSymbol = "parent_symbol";
            var parentsReadable = "ParentAttributeName";

            parent.Symbol.Returns(parentsSymbol);
            parent.AttributeName.Returns(parentsReadable);
            parent.HumanReadable.Returns(parentsReadable);

            // Act
            var result = _testClass.CreateLWORD(parent, readableTail, symbolTail);


            // Assert
            Assert.Equal(symbolTail, result.AttributeName); // returns symbolTail if attribute name null or empty.
            Assert.Equal(symbolTail, result.GetSymbolTail());

            Assert.Equal($"{parentsReadable}.{readableTail}", result.HumanReadable);
            Assert.Equal($"{parentsSymbol}.{symbolTail}", result.Symbol);
        }

        [Fact]
        public void CannotCallCreateLWORDWithNullParent()
        {
            Assert.Throws<ArgumentNullException>(() => _testClass.CreateLWORD(default(ITwinObject), "TestValue383318307", "TestValue2080268970"));
        }

        

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void CannotCallCreateLWORDWithInvalidSymbolTail(string value)
        {
            Assert.Throws<ArgumentNullException>(() => _testClass.CreateLWORD(Substitute.For<ITwinObject>(), "TestValue349713619", value));
        }

        [Fact]
        public void CanCallCreateREAL()
        {
            // Arrange
            var parent = Substitute.For<ITwinObject>();
            var readableTail = "TestValue994463928";
            var symbolTail = "TestValue1311833260";

            var parentsSymbol = "parent_symbol";
            var parentsReadable = "ParentAttributeName";

            parent.Symbol.Returns(parentsSymbol);
            parent.AttributeName.Returns(parentsReadable);
            parent.HumanReadable.Returns(parentsReadable);

            // Act
            var result = _testClass.CreateREAL(parent, readableTail, symbolTail);


            // Assert
            Assert.Equal(symbolTail, result.AttributeName); // returns symbolTail if attribute name null or empty.
            Assert.Equal(symbolTail, result.GetSymbolTail());

            Assert.Equal($"{parentsReadable}.{readableTail}", result.HumanReadable);
            Assert.Equal($"{parentsSymbol}.{symbolTail}", result.Symbol);
        }

        [Fact]
        public void CannotCallCreateREALWithNullParent()
        {
            Assert.Throws<ArgumentNullException>(() => _testClass.CreateREAL(default(ITwinObject), "TestValue996459445", "TestValue885139485"));
        }

       

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void CannotCallCreateREALWithInvalidSymbolTail(string value)
        {
            Assert.Throws<ArgumentNullException>(() => _testClass.CreateREAL(Substitute.For<ITwinObject>(), "TestValue1949397246", value));
        }

        [Fact]
        public void CanCallCreateSINT()
        {
            // Arrange
            var parent = Substitute.For<ITwinObject>();
            var readableTail = "TestValue557341231";
            var symbolTail = "TestValue1822090110";

            var parentsSymbol = "parent_symbol";
            var parentsReadable = "ParentAttributeName";

            parent.Symbol.Returns(parentsSymbol);
            parent.AttributeName.Returns(parentsReadable);
            parent.HumanReadable.Returns(parentsReadable);

            // Act
            var result = _testClass.CreateSINT(parent, readableTail, symbolTail);


            // Assert
            Assert.Equal(symbolTail, result.AttributeName); // returns symbolTail if attribute name null or empty.
            Assert.Equal(symbolTail, result.GetSymbolTail());

            Assert.Equal($"{parentsReadable}.{readableTail}", result.HumanReadable);
            Assert.Equal($"{parentsSymbol}.{symbolTail}", result.Symbol);
        }

        [Fact]
        public void CannotCallCreateSINTWithNullParent()
        {
            Assert.Throws<ArgumentNullException>(() => _testClass.CreateSINT(default(ITwinObject), "TestValue167066688", "TestValue1730222384"));
        }

        

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void CannotCallCreateSINTWithInvalidSymbolTail(string value)
        {
            Assert.Throws<ArgumentNullException>(() => _testClass.CreateSINT(Substitute.For<ITwinObject>(), "TestValue1515437503", value));
        }

        [Fact]
        public void CanCallCreateSTRING()
        {
            // Arrange
            var parent = Substitute.For<ITwinObject>();
            var readableTail = "TestValue1727043453";
            var symbolTail = "TestValue1782372054";

            var parentsSymbol = "parent_symbol";
            var parentsReadable = "ParentAttributeName";

            parent.Symbol.Returns(parentsSymbol);
            parent.AttributeName.Returns(parentsReadable);
            parent.HumanReadable.Returns(parentsReadable);

            // Act
            var result = _testClass.CreateSTRING(parent, readableTail, symbolTail);


            // Assert
            Assert.Equal(symbolTail, result.AttributeName); // returns symbolTail if attribute name null or empty.
            Assert.Equal(symbolTail, result.GetSymbolTail());

            Assert.Equal($"{parentsReadable}.{readableTail}", result.HumanReadable);
            Assert.Equal($"{parentsSymbol}.{symbolTail}", result.Symbol);
        }

        [Fact]
        public void CannotCallCreateSTRINGWithNullParent()
        {
            Assert.Throws<ArgumentNullException>(() => _testClass.CreateSTRING(default(ITwinObject), "TestValue1768011106", "TestValue946159655"));
        }

       

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void CannotCallCreateSTRINGWithInvalidSymbolTail(string value)
        {
            Assert.Throws<ArgumentNullException>(() => _testClass.CreateSTRING(Substitute.For<ITwinObject>(), "TestValue1844330356", value));
        }

        [Fact]
        public void CanCallCreateTIME()
        {
            // Arrange
            var parent = Substitute.For<ITwinObject>();
            var readableTail = "TestValue648445282";
            var symbolTail = "TestValue1795151069";

            var parentsSymbol = "parent_symbol";
            var parentsReadable = "ParentAttributeName";

            parent.Symbol.Returns(parentsSymbol);
            parent.AttributeName.Returns(parentsReadable);
            parent.HumanReadable.Returns(parentsReadable);

            // Act
            var result = _testClass.CreateTIME(parent, readableTail, symbolTail);


            // Assert
            Assert.Equal(symbolTail, result.AttributeName); // returns symbolTail if attribute name null or empty.
            Assert.Equal(symbolTail, result.GetSymbolTail());

            Assert.Equal($"{parentsReadable}.{readableTail}", result.HumanReadable);
            Assert.Equal($"{parentsSymbol}.{symbolTail}", result.Symbol);
        }

        [Fact]
        public void CannotCallCreateTIMEWithNullParent()
        {
            Assert.Throws<ArgumentNullException>(() => _testClass.CreateTIME(default(ITwinObject), "TestValue193947616", "TestValue609699606"));
        }

       

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void CannotCallCreateTIMEWithInvalidSymbolTail(string value)
        {
            Assert.Throws<ArgumentNullException>(() => _testClass.CreateTIME(Substitute.For<ITwinObject>(), "TestValue1085079308", value));
        }

        [Fact]
        public void CanCallCreateDATE_AND_TIME()
        {
            // Arrange
            var parent = Substitute.For<ITwinObject>();
            var readableTail = "TestValue1845870927";
            var symbolTail = "TestValue1547847096";
            var parentsSymbol = "parent_symbol";
            var parentsReadable = "ParentAttributeName";

            parent.Symbol.Returns(parentsSymbol);
            parent.AttributeName.Returns(parentsReadable);
            parent.HumanReadable.Returns(parentsReadable);

            // Act
            var result = _testClass.CreateDATE_AND_TIME(parent, readableTail, symbolTail);


            // Assert
            Assert.Equal(symbolTail, result.AttributeName); // returns symbolTail if attribute name null or empty.
            Assert.Equal(symbolTail, result.GetSymbolTail());

            Assert.Equal($"{parentsReadable}.{readableTail}", result.HumanReadable);
            Assert.Equal($"{parentsSymbol}.{symbolTail}", result.Symbol);
        }

        [Fact]
        public void CannotCallCreateDATE_AND_TIMEWithNullParent()
        {
            Assert.Throws<ArgumentNullException>(() => _testClass.CreateDATE_AND_TIME(default(ITwinObject), "TestValue105468830", "TestValue1312080366"));
        }

        

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void CannotCallCreateDATE_AND_TIMEWithInvalidSymbolTail(string value)
        {
            Assert.Throws<ArgumentNullException>(() => _testClass.CreateDATE_AND_TIME(Substitute.For<ITwinObject>(), "TestValue434327107", value));
        }

        [Fact]
        public void CanCallCreateDATE()
        {
            // Arrange
            var parent = Substitute.For<ITwinObject>();
            var readableTail = "TestValue605197883";
            var symbolTail = "TestValue1171760239";

            var parentsSymbol = "parent_symbol";
            var parentsReadable = "ParentAttributeName";

            parent.Symbol.Returns(parentsSymbol);
            parent.AttributeName.Returns(parentsReadable);
            parent.HumanReadable.Returns(parentsReadable);

            // Act
            var result = _testClass.CreateDATE(parent, readableTail, symbolTail);


            // Assert
            Assert.Equal(symbolTail, result.AttributeName); // returns symbolTail if attribute name null or empty.
            Assert.Equal(symbolTail, result.GetSymbolTail());

            Assert.Equal($"{parentsReadable}.{readableTail}", result.HumanReadable);
            Assert.Equal($"{parentsSymbol}.{symbolTail}", result.Symbol);
        }

        [Fact]
        public void CannotCallCreateDATEWithNullParent()
        {
            Assert.Throws<ArgumentNullException>(() => _testClass.CreateDATE(default(ITwinObject), "TestValue1830428512", "TestValue33829904"));
        }

        

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void CannotCallCreateDATEWithInvalidSymbolTail(string value)
        {
            Assert.Throws<ArgumentNullException>(() => _testClass.CreateDATE(Substitute.For<ITwinObject>(), "TestValue1614755367", value));
        }

        [Fact]
        public void CanCallCreateTIME_OF_DAY()
        {
            // Arrange
            var parent = Substitute.For<ITwinObject>();
            var readableTail = "TestValue63545108";
            var symbolTail = "TestValue160913919";

            var parentsSymbol = "parent_symbol";
            var parentsReadable = "ParentAttributeName";

            parent.Symbol.Returns(parentsSymbol);
            parent.AttributeName.Returns(parentsReadable);
            parent.HumanReadable.Returns(parentsReadable);

            // Act
            var result = _testClass.CreateTIME_OF_DAY(parent, readableTail, symbolTail);


            // Assert
            Assert.Equal(symbolTail, result.AttributeName); // returns symbolTail if attribute name null or empty.
            Assert.Equal(symbolTail, result.GetSymbolTail());

            Assert.Equal($"{parentsReadable}.{readableTail}", result.HumanReadable);
            Assert.Equal($"{parentsSymbol}.{symbolTail}", result.Symbol);
        }

        [Fact]
        public void CannotCallCreateTIME_OF_DAYWithNullParent()
        {
            Assert.Throws<ArgumentNullException>(() => _testClass.CreateTIME_OF_DAY(default(ITwinObject), "TestValue83251701", "TestValue28516766"));
        }

        

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void CannotCallCreateTIME_OF_DAYWithInvalidSymbolTail(string value)
        {
            Assert.Throws<ArgumentNullException>(() => _testClass.CreateTIME_OF_DAY(Substitute.For<ITwinObject>(), "TestValue1856712821", value));
        }

        [Fact]
        public void CanCallCreateUDINT()
        {
            // Arrange
            var parent = Substitute.For<ITwinObject>();
            var readableTail = "TestValue1435220837";
            var symbolTail = "TestValue1268329335";

            var parentsSymbol = "parent_symbol";
            var parentsReadable = "ParentAttributeName";

            parent.Symbol.Returns(parentsSymbol);
            parent.AttributeName.Returns(parentsReadable);
            parent.HumanReadable.Returns(parentsReadable);

            // Act
            var result = _testClass.CreateUDINT(parent, readableTail, symbolTail);


            // Assert
            Assert.Equal(symbolTail, result.AttributeName); // returns symbolTail if attribute name null or empty.
            Assert.Equal(symbolTail, result.GetSymbolTail());

            Assert.Equal($"{parentsReadable}.{readableTail}", result.HumanReadable);
            Assert.Equal($"{parentsSymbol}.{symbolTail}", result.Symbol);
        }

        [Fact]
        public void CannotCallCreateUDINTWithNullParent()
        {
            Assert.Throws<ArgumentNullException>(() => _testClass.CreateUDINT(default(ITwinObject), "TestValue1300940336", "TestValue138165434"));
        }

        

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void CannotCallCreateUDINTWithInvalidSymbolTail(string value)
        {
            Assert.Throws<ArgumentNullException>(() => _testClass.CreateUDINT(Substitute.For<ITwinObject>(), "TestValue920417602", value));
        }

        [Fact]
        public void CanCallCreateUINT()
        {
            // Arrange
            var parent = Substitute.For<ITwinObject>();
            var readableTail = "TestValue2096175455";
            var symbolTail = "TestValue1356394302";

            var parentsSymbol = "parent_symbol";
            var parentsReadable = "ParentAttributeName";

            parent.Symbol.Returns(parentsSymbol);
            parent.AttributeName.Returns(parentsReadable);
            parent.HumanReadable.Returns(parentsReadable);

            // Act
            var result = _testClass.CreateUINT(parent, readableTail, symbolTail);


            // Assert
            Assert.Equal(symbolTail, result.AttributeName); // returns symbolTail if attribute name null or empty.
            Assert.Equal(symbolTail, result.GetSymbolTail());

            Assert.Equal($"{parentsReadable}.{readableTail}", result.HumanReadable);
            Assert.Equal($"{parentsSymbol}.{symbolTail}", result.Symbol);
        }

        [Fact]
        public void CannotCallCreateUINTWithNullParent()
        {
            Assert.Throws<ArgumentNullException>(() => _testClass.CreateUINT(default(ITwinObject), "TestValue1979809132", "TestValue686471598"));
        }

        

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void CannotCallCreateUINTWithInvalidSymbolTail(string value)
        {
            Assert.Throws<ArgumentNullException>(() => _testClass.CreateUINT(Substitute.For<ITwinObject>(), "TestValue2038794197", value));
        }

        [Fact]
        public void CanCallCreateULINT()
        {
            // Arrange
            var parent = Substitute.For<ITwinObject>();
            var readableTail = "TestValue155766003";
            var symbolTail = "TestValue384764322";

            var parentsSymbol = "parent_symbol";
            var parentsReadable = "ParentAttributeName";

            parent.Symbol.Returns(parentsSymbol);
            parent.AttributeName.Returns(parentsReadable);
            parent.HumanReadable.Returns(parentsReadable);

            // Act
            var result = _testClass.CreateULINT(parent, readableTail, symbolTail);


            // Assert
            Assert.Equal(symbolTail, result.AttributeName); // returns symbolTail if attribute name null or empty.
            Assert.Equal(symbolTail, result.GetSymbolTail());

            Assert.Equal($"{parentsReadable}.{readableTail}", result.HumanReadable);
            Assert.Equal($"{parentsSymbol}.{symbolTail}", result.Symbol);
        }

        [Fact]
        public void CannotCallCreateULINTWithNullParent()
        {
            Assert.Throws<ArgumentNullException>(() => _testClass.CreateULINT(default(ITwinObject), "TestValue1718311086", "TestValue2058863784"));
        }

        

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void CannotCallCreateULINTWithInvalidSymbolTail(string value)
        {
            Assert.Throws<ArgumentNullException>(() => _testClass.CreateULINT(Substitute.For<ITwinObject>(), "TestValue797415025", value));
        }

        [Fact]
        public void CanCallCreateUSINT()
        {
            // Arrange
            var parent = Substitute.For<ITwinObject>();
            var readableTail = "TestValue1843928416";
            var symbolTail = "TestValue1392744512";

            var parentsSymbol = "parent_symbol";
            var parentsReadable = "ParentAttributeName";

            parent.Symbol.Returns(parentsSymbol);
            parent.AttributeName.Returns(parentsReadable);
            parent.HumanReadable.Returns(parentsReadable);

            // Act
            var result = _testClass.CreateUSINT(parent, readableTail, symbolTail);


            // Assert
            Assert.Equal(symbolTail, result.AttributeName); // returns symbolTail if attribute name null or empty.
            Assert.Equal(symbolTail, result.GetSymbolTail());

            Assert.Equal($"{parentsReadable}.{readableTail}", result.HumanReadable);
            Assert.Equal($"{parentsSymbol}.{symbolTail}", result.Symbol);
        }

        [Fact]
        public void CannotCallCreateUSINTWithNullParent()
        {
            Assert.Throws<ArgumentNullException>(() => _testClass.CreateUSINT(default(ITwinObject), "TestValue980421471", "TestValue1880223228"));
        }

       

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void CannotCallCreateUSINTWithInvalidSymbolTail(string value)
        {
            Assert.Throws<ArgumentNullException>(() => _testClass.CreateUSINT(Substitute.For<ITwinObject>(), "TestValue424358201", value));
        }

        [Fact]
        public void CanCallCreateWORD()
        {
            // Arrange
            var parent = Substitute.For<ITwinObject>();
            var readableTail = "TestValue1832492638";
            var symbolTail = "TestValue1693213549";

            var parentsSymbol = "parent_symbol";
            var parentsReadable = "ParentAttributeName";

            parent.Symbol.Returns(parentsSymbol);
            parent.AttributeName.Returns(parentsReadable);
            parent.HumanReadable.Returns(parentsReadable);

            // Act
            var result = _testClass.CreateWORD(parent, readableTail, symbolTail);


            // Assert
            Assert.Equal(symbolTail, result.AttributeName); // returns symbolTail if attribute name null or empty.
            Assert.Equal(symbolTail, result.GetSymbolTail());

            Assert.Equal($"{parentsReadable}.{readableTail}", result.HumanReadable);
            Assert.Equal($"{parentsSymbol}.{symbolTail}", result.Symbol);
        }

        [Fact]
        public void CannotCallCreateWORDWithNullParent()
        {
            Assert.Throws<ArgumentNullException>(() => _testClass.CreateWORD(default(ITwinObject), "TestValue638586628", "TestValue153255739"));
        }

        

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void CannotCallCreateWORDWithInvalidSymbolTail(string value)
        {
            Assert.Throws<ArgumentNullException>(() => _testClass.CreateWORD(Substitute.For<ITwinObject>(), "TestValue1611980438", value));
        }

        [Fact]
        public void CanCallCreateWSTRING()
        {
            // Arrange
            var parent = Substitute.For<ITwinObject>();
            var readableTail = "TestValue1761078655";
            var symbolTail = "TestValue619928516";

            var parentsSymbol = "parent_symbol";
            var parentsReadable = "ParentAttributeName";

            parent.Symbol.Returns(parentsSymbol);
            parent.AttributeName.Returns(parentsReadable);
            parent.HumanReadable.Returns(parentsReadable);

            // Act
            var result = _testClass.CreateWSTRING(parent, readableTail, symbolTail);


            // Assert
            Assert.Equal(symbolTail, result.AttributeName); // returns symbolTail if attribute name null or empty.
            Assert.Equal(symbolTail, result.GetSymbolTail());

            Assert.Equal($"{parentsReadable}.{readableTail}", result.HumanReadable);
            Assert.Equal($"{parentsSymbol}.{symbolTail}", result.Symbol);
        }

        [Fact]
        public void CannotCallCreateWSTRINGWithNullParent()
        {
            Assert.Throws<ArgumentNullException>(() => _testClass.CreateWSTRING(default(ITwinObject), "TestValue42803618", "TestValue484718442"));
        }

        

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void CannotCallCreateWSTRINGWithInvalidSymbolTail(string value)
        {
            Assert.Throws<ArgumentNullException>(() => _testClass.CreateWSTRING(Substitute.For<ITwinObject>(), "TestValue472676486", value));
        }
    }
}