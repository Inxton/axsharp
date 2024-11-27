namespace AXSharp.ConnectorTests.Localizations
{
    using AXSharp.Connector.Localizations;
    using System;
    using Xunit;
    using NSubstitute;
    using AXSharp.Connector;

    public class TranslatorTests
    {
        private Translator _testClass;

        public TranslatorTests()
        {
            _testClass = new Translator();
        }

        [Fact]
        public void CanCallTranslate()
        {
            // Arrange
            var originalString = "TestValue1598718209";
            var twin = Substitute.For<ITwinElement>();

            // Act
            var result = _testClass.Translate(originalString, twin);

            // Assert
            Assert.Equal(originalString, result);
        }

        [Fact]
        public void CannotCallTranslateWithNullTwin()
        {
            var actual = _testClass.Translate("TestValue824696765", default(ITwinElement));
            Assert.Equal("TestValue824696765", actual);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void CannotCallTranslateWithInvalidOriginalString(string value)
        {
            _testClass.Translate(value, Substitute.For<ITwinElement>());
        }

        [Fact]
        public void CanCallSetLocalizationResource()
        {
            // Arrange
            var resourceType = typeof(AXSharp.ConnectorTests.Localizations.Resources.Dictionary);

            // Act
            _testClass.SetLocalizationResource(resourceType);
        }        
    }
}