using System.Globalization;
using AXSharp.Connector.Localizations;



namespace AXSharp.ConnectorTests.Localizations
{
    using System;
    using Xunit;
    using NSubstitute;
    using AXSharp.Connector;

    public static class TranslatorExtensionTests
    {
        [Fact]
        public static void Translate_no_localization_should_return_the_same()
        {
            // Arrange
            var twin = Substitute.For<ITwinElement>();
            var originalString = "TestValue1961414016";

            // Act
            var result = twin.Translate(originalString, CultureInfo.CurrentCulture);

            // Assert
            Assert.Equal(originalString, result);
        }


        [Fact]
        public static void Translate_localized_string_keep_default_translation()
        {
            // Arrange
            var twin = Substitute.For<ITwinElement>();
            var interpreter = new Translator();
            interpreter.SetLocalizationResource(typeof(AXSharp.ConnectorTests.Localizations.Resources.Dictionary));
            twin.Interpreter.Returns(interpreter);
            var originalString = "<#In the middle of the night#>";

            // Act
            var result = twin.Translate(originalString, CultureInfo.CurrentCulture);

            // Assert
            Assert.Equal(originalString.CleanUpLocalizationTokens(), result);
        }

        [Fact]
        public static void Translate_localized_string_translate_to_sk()
        {
            // Arrange
            var twin = Substitute.For<ITwinElement>();
            var interpreter = new Translator();
            interpreter.SetLocalizationResource(typeof(AXSharp.ConnectorTests.Localizations.Resources.Dictionary));
            twin.Interpreter.Returns(interpreter);
            var originalString = "<#In the middle of the night#>";
            var expected = "Uprostred noci";
            var culture = new CultureInfo("sk-SK");
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;
            // Act
            var result = twin.Translate(originalString, CultureInfo.CurrentCulture);

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public static void Translate_partially_localized_string_translate_to_sk()
        {
            // Arrange
            var twin = Substitute.For<ITwinElement>();
            var interpreter = new Translator();
            interpreter.SetLocalizationResource(typeof(AXSharp.ConnectorTests.Localizations.Resources.Dictionary));
            twin.Interpreter.Returns(interpreter);
            var originalString = "(A4)<#In the middle of the night#> 1.5";
            var expected = "(A4)Uprostred noci 1.5";
            var culture = new CultureInfo("sk-SK");
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;
            // Act
            var result = twin.Translate(originalString, CultureInfo.CurrentCulture);

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public static void Translate_partially_localized_inexisting_culture()
        {
            // Arrange
            var twin = Substitute.For<ITwinElement>();
            var interpreter = new Translator();
            interpreter.SetLocalizationResource(typeof(AXSharp.ConnectorTests.Localizations.Resources.Dictionary));
            twin.Interpreter.Returns(interpreter);
            var originalString = "(A4)<#In the middle of the night#> 1.5";
            var expected = "(A4)Uprostred noci 1.5";
            var culture = new CultureInfo("cn-CN");
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;
            // Act
            var result = twin.Translate(originalString, CultureInfo.CurrentCulture);

            // Assert
            Assert.Equal(originalString.CleanUpLocalizationTokens(), result);
        }

        [Fact]
        public static void Translate_partially_localized_inexisting_translation()
        {
            // Arrange
            var twin = Substitute.For<ITwinElement>();
            var interpreter = new Translator();
            interpreter.SetLocalizationResource(typeof(AXSharp.ConnectorTests.Localizations.Resources.Dictionary));
            twin.Interpreter.Returns(interpreter);
            var originalString = "(A4)<#In the middle of the night does not exist#> 1.5";
          
            var culture = new CultureInfo("sk-SK");
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;
            // Act
            var result = twin.Translate(originalString, CultureInfo.CurrentCulture);

            // Assert
            Assert.Equal(originalString.CleanUpLocalizationTokens(), result);
        }
    }
}