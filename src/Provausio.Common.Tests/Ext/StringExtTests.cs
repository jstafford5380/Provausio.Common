using System;
using Provausio.Common.Ext;
using Xunit;

namespace Provausio.Common.Tests.Ext
{
    public class StringExtTests
    {
        [Theory]
        [InlineData("938475985")]
        [InlineData("987838388818")]
        [InlineData("81818995892829234929349191")]
        public void IsNumeric_NumericString_ReturnsTrue(string input)
        {
            // arrange

            // act
            var result = input.IsNumeric();

            // assert
            Assert.True(result);
        }

        [Theory]
        [InlineData("SDFD98DF9999FFFF")]
        [InlineData("987FFFjJSJ899")]
        [InlineData("982734982734987F")]
        public void IsNumeric_AlphaNumericString_ReturnsFalse(string input)
        {
            // arrange

            // act
            var result = input.IsNumeric();

            // assert
            Assert.False(result);
        }

        [Theory]
        [InlineData("LDFKJSLDKFLAKLALAL")]
        [InlineData("FDSDFJSDLFAAAA")]
        [InlineData("SDFJSDLFKJALKFJALSKDFJLJRLKSFLKJ")]
        public void IsNumeric_AlphaString_ReturnsFalse(string input)
        {
            // arrange

            // act
            var result = input.IsNumeric();

            // assert
            Assert.False(result);
        }

        [Fact]
        public void FindEnum_InvalidValue_Throws()
        {
            // arrange
            const string test = "foo";

            // act
            
            // assert
            Assert.Throws<ArgumentException>(() => test.FindEnum<StringSplitOptions>());
        }

        [Fact]
        public void FindEnum_IgnoreCase_LowerCaseValue_ReturnsExpectedEnum()
        {
            // arrange
            const string test = "none";
            var expected = StringSplitOptions.None;

            // act
            var result = test.FindEnum<StringSplitOptions>();

            // assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void FindEnum_MatchCase_LowerCaseValue_Throws()
        {
            // arrange
            const string test = "none";

            // act

            // assert
            Assert.Throws<ArgumentException>(() => test.FindEnum<StringSplitOptions>(false));
        }

        [Fact]
        public void TryFindEnum_NoMatch_ReturnsFalse()
        {
            // arrange
            const string test = "foobar";

            // act
            var t = test.TryFindEnum(out StringSplitOptions result);

            // assert
            Assert.False(t);
        }

        [Fact]
        public void TryFindEnum_NoMatch_ResultIsDefault()
        {
            // arrange
            const string test = "foobar";

            // act
            var t = test.TryFindEnum(out StringSplitOptions result);

            // assert
            Assert.Equal(default(StringSplitOptions), result);
        }

        [Fact]
        public void TryFindEnum_IsMatch_ReturnsTrue()
        {
            // arrange
            const string test = "none";

            // act
            var t = test.TryFindEnum(out StringSplitOptions result);

            // assert
            Assert.True(t);
        }

        [Fact]
        public void TryFindEnum_NoMatch_ResultIsCorrect()
        {
            // arrange
            const string test = "RemoveEmptyEntries";

            // act
            var t = test.TryFindEnum(out StringSplitOptions result);

            // assert
            Assert.Equal(StringSplitOptions.RemoveEmptyEntries, result);
        }

        [Fact]
        public void ToTitleCase_SingleWord_FirstIsUpper()
        {
            // arrange
            var input = "heLlO";

            // act
            var result = input.ToTitleCase();

            // assert
            Assert.Equal("Hello", result);
        }

        [Fact]
        public void ToTitleCase_MultipleWords_FirstOfEveryWordIsUpper()
        {
            // arrange
            var input = "hEllO thIs IS a tESt";

            // act
            var result = input.ToTitleCase();

            // assert
            Assert.Equal("Hello This Is A Test", result);
        }

        [Fact]
        public void Truncate_LessThanMax_DoesNotTruncate()
        {
            // arrange
            var input = "hello world";

            // act
            var result = input.Truncate(1000);

            // assert
            Assert.Equal(input, result);
        }

        [Fact]
        public void Truncate_MoreThanMax_WithEllipsis_LengthIncludesEllipsis()
        {
            // arrange
            var input = "hello world";

            // act
            var result = input.Truncate(3);

            // assert
            Assert.Equal(6, result.Length);
        }

        [Fact]
        public void Truncate_MoreThanMax_WithEllipsis_StringIsCorrect()
        {
            // arrange
            var input = "hello world";

            // act
            var result = input.Truncate(3);

            // assert
            Assert.Equal("hel...", result);
        }

        [Fact]
        public void Truncate_MoreThanMax_NoEllipsis_LengthIsCorrect()
        {
            // arrange
            var input = "hello world";

            // act
            var result = input.Truncate(3, false);

            // assert
            Assert.Equal(3, result.Length);
        }

        [Fact]
        public void Truncate_MoreThanMax_NoEllipsis_StringIsCorrect()
        {
            // arrange
            var input = "hello world";

            // act
            var result = input.Truncate(3, false);

            // assert
            Assert.Equal("hel", result);
        }

        [Fact]
        public void ToString_CharacterCollection_StringIsCorrect()
        {
            // arrange
            var input = new[] {'f', 'o', 'o'};

            // act
            var str = input.BuildString();

            // assert
            Assert.Equal("foo", str);
        }
    }
}
