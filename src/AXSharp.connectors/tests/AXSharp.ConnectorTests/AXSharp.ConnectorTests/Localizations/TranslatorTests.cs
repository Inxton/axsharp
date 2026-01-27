namespace AXSharp.ConnectorTests.Localizations
{
    using AXSharp.Connector.Localizations;
    using System;
    using System.Globalization;
    using Xunit;
    using NSubstitute;
    using AXSharp.Connector;
    using AXSharp.ConnectorTests.Localizations.Resources;

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

        [Fact]
        public void Translate_prefers_primary_application_resource_over_library_resource()
        {
            // Arrange
            Translator.SetPrimaryTranslatorResource(typeof(OverrideApplication));
            var translator = new Translator();
            translator.SetLocalizationResource(typeof(OverrideLibrary));
            var twin = Substitute.For<ITwinElement>();
            var originalString = "<#Override token#>";

            // Act
            var result = translator.Translate(originalString, twin, CultureInfo.InvariantCulture);

            // Assert
            Assert.Equal("APP", result);
        }

        [Fact]
        public void Translate_falls_back_to_library_resource_when_primary_does_not_have_key()
        {
            // Arrange
            Translator.SetPrimaryTranslatorResource(typeof(OverrideApplication));
            var translator = new Translator();
            translator.SetLocalizationResource(typeof(OverrideLibrary));
            var twin = Substitute.For<ITwinElement>();
            var originalString = "<#Library only#>";

            // Act
            var result = translator.Translate(originalString, twin, CultureInfo.InvariantCulture);

            // Assert
            Assert.Equal("LIB_ONLY", result);
        }

        [Fact]
        public void Translate_when_key_missing_returns_text_without_localization_tokens()
        {
            // Arrange
            Translator.SetPrimaryTranslatorResource(typeof(OverrideApplication));
            var translator = new Translator();
            translator.SetLocalizationResource(typeof(OverrideLibrary));
            var twin = Substitute.For<ITwinElement>();
            var originalString = "<#Does not exist#>";

            // Act
            var result = translator.Translate(originalString, twin, CultureInfo.InvariantCulture);

            // Assert
            Assert.Equal("Does not exist", result);
        }
    }
}