using System;
using System.Globalization;
using Provausio.Common.Ext;
using Xunit;

namespace Provausio.Common.Tests.Ext
{
    public class DateTimeExTests
    {
        [Theory]
        [InlineData("1997-07-16T19:20+01:00")]
        [InlineData("1997-07-16T19:20-01:00")]
        [InlineData("1997-07-16T19:20+09:00")]
        [InlineData("1997-07-16T19:20")]
        [InlineData("1997-07-16T19:20:30.45+01:00")]
        public void ToDateTimeOffset_NotUtc_ConvertsToLocalOffset(string isoFormat)
        {
            // arrange
            var dt = DateTime.Parse(isoFormat);

            // act
            var dto = dt.ToDateTimeOffset();

            // assert
            Assert.Equal(TimeZoneInfo.Local.GetUtcOffset(dt), dto.Offset);
        }

        [Theory]
        [InlineData("1997-07-16T19:20", "-07:00")]
        [InlineData("1997-07-16T09:05", "01:00")]
        [InlineData("1997-07-16T13:10", "-04:00")]
        [InlineData("1997-07-16T13:10", "00:00")]
        public void ToDateTimeOffset_WithOffset_PreservesOffset(string isoDate, string offsetValue)
        {
            // arrange
            var dt = DateTime.Parse(
                isoDate, 
                CultureInfo.CurrentCulture, 
                DateTimeStyles.AssumeUniversal);
            var offset = TimeSpan.Parse(offsetValue);

            // act
            var dto = dt.ToDateTimeOffset(offset);

            // assert
            Assert.Equal(offset, dto.Offset);
        }

        [Theory]
        [InlineData("1997-07-16T19:20", "-07:00")]
        [InlineData("1997-07-16T09:05", "01:00")]
        [InlineData("1997-07-16T13:10", "-04:00")]
        [InlineData("1997-07-16T13:10", "00:00")]
        public void ToDateTimeOffsert_WithOffset_PreservesDateTimeValue(string isoDate, string offsetValue)
        {
            // arrange
            var dt = DateTime.Parse(
                isoDate,
                CultureInfo.CurrentCulture,
                DateTimeStyles.AssumeUniversal);
            var offset = TimeSpan.Parse(offsetValue);

            // act
            var dto = dt.ToDateTimeOffset(offset);

            // assert
            Assert.Equal(dto.DateTime, dt);
        }

        [Fact]
        public void StartOfThis_Second_ExpectedResult()
        {
            // arrange
            var now      = new DateTime(2017, 6, 12, 16, 3, 21, 500, DateTimeKind.Unspecified);
            var expected = new DateTime(2017, 6, 12, 16, 3, 21, 0, DateTimeKind.Unspecified);

            // act
            var result = now.StartOf(DateInterval.Second);

            // assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void StartOfThis_Minute_ExpectedResult()
        {
            // arrange
            var now = new DateTime(2017, 6, 12, 16, 3, 21, 500, DateTimeKind.Unspecified);
            var expected = new DateTime(2017, 6, 12, 16, 3, 0, 0, DateTimeKind.Unspecified);

            // act
            var result = now.StartOf(DateInterval.Minute);

            // assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void StartOfThis_Hour_ExpectedResult()
        {
            // arrange
            var now = new DateTime(2017, 6, 12, 16, 3, 21, 500, DateTimeKind.Unspecified);
            var expected = new DateTime(2017, 6, 12, 16, 0, 0, 0, DateTimeKind.Unspecified);

            // act
            var result = now.StartOf(DateInterval.Hour);

            // assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void StartOfThis_Day_ExpectedResult()
        {
            // arrange
            var now = new DateTime(2017, 6, 12, 16, 3, 21, 500, DateTimeKind.Unspecified);
            var expected = new DateTime(2017, 6, 12, 0, 0, 0, 0, DateTimeKind.Unspecified);

            // act
            var result = now.StartOf(DateInterval.Day);

            // assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void StartOfThis_Week_ExpectedResult()
        {
            // arrange
            var now = new DateTime(2017, 6, 12, 16, 3, 21, 500, DateTimeKind.Unspecified);
            var expected = new DateTime(2017, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified);

            // act
            var result = now.StartOf(DateInterval.Week);

            // assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void StartOfThis_Month_ExpectedResult()
        {
            // arrange
            var now = new DateTime(2017, 6, 12, 16, 3, 21, 500, DateTimeKind.Unspecified);
            var expected = new DateTime(2017, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified);

            // act
            var result = now.StartOf(DateInterval.Month);

            // assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void StartOfThis_Year_ExpectedResult()
        {
            // arrange
            var now = new DateTime(2017, 6, 12, 16, 3, 21, 500, DateTimeKind.Unspecified);
            var expected = new DateTime(2017, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified);

            // act
            var result = now.StartOf(DateInterval.Year);

            // assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Test()
        {
            var someRandomAssFebruaryDate = new DateTime(2017, 2, 23);
            var range = DateRange.FromStart(
                someRandomAssFebruaryDate.StartOf(DateInterval.Month), 
                DateInterval.Month, 1, false);
        }
    }
}
