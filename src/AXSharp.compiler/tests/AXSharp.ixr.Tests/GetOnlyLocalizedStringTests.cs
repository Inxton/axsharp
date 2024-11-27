using AXSharp.ixr_doc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AXSharp.ixr.Tests
{
    public class GetOnlyLocalizedStringTests
    {
        private readonly LocalizedStringWrapper _lw;

        public GetOnlyLocalizedStringTests()
        {
            _lw = new LocalizedStringWrapper();
        }

        [Fact]
        public void get_base_localized_string()
        {
            //arrange
            var localizedString = "<#localized#>";
            var expected = "<#localized#>";
            //act
            var result = _lw.TryToGetLocalizedStrings(localizedString);
            //assert
            Assert.Equal(expected, result.First());
        }

        [Fact]
        public void get_empty_localized_string()
        {
            //arrange
            var localizedString = "<##>";
            var expected = "<##>";
            //act
            var result = _lw.TryToGetLocalizedStrings(localizedString);
            //assert
            Assert.Equal(expected, result.First());
        }

        [Fact]
        public void get_empty_string()
        {
            //arrange
            var localizedString = "";
            //act
            var result = _lw.TryToGetLocalizedStrings(localizedString);
            //assert
            Assert.Null(result);
        }

        [Fact]
        public void get_no_localized_string()
        {
            //arrange
            var localizedString = "abc ";
            //act
            var result = _lw.TryToGetLocalizedStrings(localizedString);
            //assert
            Assert.Null(result);
        }

        [Fact]
        public void get_left_complex_localized_string()
        {
            //arrange
            var localizedString = "<#localized#>nolocalized";
            var expected = "<#localized#>";
            //act
            var result = _lw.TryToGetLocalizedStrings(localizedString);
            //assert
            Assert.Equal(expected, result.First());
        }

        [Fact]
        public void get_right_complex_localized_string()
        {
            //arrange
            var localizedString = "nolocalized <#localized#>";
            var expected = "<#localized#>";
            //act
            var result = _lw.TryToGetLocalizedStrings(localizedString);
            //assert
            Assert.Equal(expected, result.First());
        }

        [Fact]
        public void get_complex_localized_string()
        {
            //arrange
            var localizedString = "nolocalized<#localized#> nolocalized";
            var expected = "<#localized#>";
            //act
            var result = _lw.TryToGetLocalizedStrings(localizedString);
            //assert
            Assert.Equal(expected, result.First());
        }

        [Fact]
        public void get_two_localized_strings()
        {
            //arrange
            var localizedStrings = "<#localized1#><#localized2#>";
            var expected1 = "<#localized1#>";
            var expected2 = "<#localized2#>";
            //act
            var result = _lw.TryToGetLocalizedStrings(localizedStrings);
            //assert
            Assert.Equal(expected1, result.ElementAt(0));
            Assert.Equal(expected2, result.ElementAt(1));
        }

        [Fact]
        public void get_two_complex_localized_strings()
        {
            //arrange
            var localizedStrings = "nolocalized <#localized1#> no localized<#localized2#>";
            var expected1 = "<#localized1#>";
            var expected2 = "<#localized2#>";
            //act
            var result = _lw.TryToGetLocalizedStrings(localizedStrings);
            //assert
            Assert.Equal(expected1, result.ElementAt(0));
            Assert.Equal(expected2, result.ElementAt(1));
        }

        [Fact]
        public void get_three_localized_strings()
        {
            //arrange
            var localizedStrings = "<#localized1#><#localized2#><#localized3#>";
            var expected1 = "<#localized1#>";
            var expected2 = "<#localized2#>";
            var expected3 = "<#localized3#>";
            //act
            var result = _lw.TryToGetLocalizedStrings(localizedStrings);
            //assert
            Assert.Equal(expected1, result.ElementAt(0));
            Assert.Equal(expected2, result.ElementAt(1));
            Assert.Equal(expected3, result.ElementAt(2));
        }

        [Fact]
        public void get_three_complex_localized_strings()
        {
            //arrange
            var localizedStrings = " <#localized1#> no loc ali zed<#localized2#><#localized3#>no localized";
            var expected1 = "<#localized1#>";
            var expected2 = "<#localized2#>";
            var expected3 = "<#localized3#>";
            //act
            var result = _lw.TryToGetLocalizedStrings(localizedStrings);
            //assert
            Assert.Equal(expected1, result.ElementAt(0));
            Assert.Equal(expected2, result.ElementAt(1));
            Assert.Equal(expected3, result.ElementAt(2));
        }
    }
}
