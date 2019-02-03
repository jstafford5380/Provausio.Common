using System;
using Provausio.Common.Ext;
using Xunit;

namespace Provausio.Common.Tests
{
    public class DateRangeTests
    {
        private readonly DateTimeOffset _startDate = new DateTimeOffset(2017, 1, 1, 1, 0, 0, TimeSpan.Zero);

        [Fact]
        public void Ctor_FullRange_AreFirstAndLastMillisecond()
        {
            // arrange
            var expectedStart = new DateTimeOffset(2017, 1, 1, 0, 0, 0, 0, TimeSpan.Zero);
            var expectedEnd = expectedStart.Date.AddDays(1).AddMilliseconds(-1).ToDateTimeOffset(TimeSpan.Zero);
            var date = new DateTimeOffset(2017, 1, 1, 13, 1, 24, 765, TimeSpan.Zero);

            // act
            var range = new DateRange(date, date, true);

            Assert.Equal(expectedStart, range.StartDate);
            Assert.Equal(expectedEnd, range.EndDate);
        }

        [Theory]
        [InlineData(10)]
        [InlineData(59)]
        [InlineData(31)]
        [InlineData(45)]
        public void FromStart_Second_ExpectedRange(int count)
        {
            // arrange
            var expected = new DateRange(
                _startDate, 
                _startDate.AddSeconds(count), false);

            // act
            var range = DateRange.FromStart(_startDate, DateInterval.Second, count, false);

            // assert
            Assert.True(expected == range);
        }

        [Theory]
        [InlineData(10)]
        [InlineData(59)]
        [InlineData(31)]
        [InlineData(45)]
        public void FromStart_Minutes_ExpectedRange(int count)
        {
            // arrange
            var expected = new DateRange(
                _startDate,
                _startDate.AddMinutes(count), false);

            // act
            var range = DateRange.FromStart(_startDate, DateInterval.Minute, count, false);

            // assert
            Assert.True(expected == range);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(6)]
        [InlineData(13)]
        [InlineData(25)]
        public void FromStart_Hours_ExpectedRange(int count)
        {
            // arrange
            var expected = new DateRange(
                _startDate,
                _startDate.AddHours(count), false);

            // act
            var range = DateRange.FromStart(_startDate, DateInterval.Hour, count, false);

            // assert
            Assert.True(expected == range);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(5)]
        [InlineData(8)]
        public void FromStart_Days_ExpectedRange(int count)
        {
            // arrange
            var expected = new DateRange(
                _startDate,
                _startDate.AddDays(count), false);

            // act
            var range = DateRange.FromStart(_startDate, DateInterval.Day, count, false);

            // assert
            Assert.True(expected == range);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(20)]
        [InlineData(59)]
        public void FromStart_Weeks_ExpectedRange(int count)
        {
            // arrange
            var expected = new DateRange(
                _startDate,
                _startDate.AddDays(count*7), false);

            // act
            var range = DateRange.FromStart(_startDate, DateInterval.Week, count, false);

            // assert
            Assert.True(expected == range);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(20)]
        [InlineData(59)]
        public void FromStart_Months_ExpectedRange(int count)
        {
            // arrange
            var expected = new DateRange(
                _startDate,
                _startDate.AddMonths(count), false);

            // act
            var range = DateRange.FromStart(_startDate, DateInterval.Month, count, false);

            // assert
            Assert.True(expected == range);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(5)]
        [InlineData(10)]
        [InlineData(11)]
        public void FromStart_Years_ExpectedRange(int count)
        {
            // arrange
            var expected = new DateRange(
                _startDate,
                _startDate.AddYears(count), false);

            // act
            var range = DateRange.FromStart(_startDate, DateInterval.Year, count, false);

            // assert
            Assert.True(expected == range);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(5)]
        [InlineData(8)]
        public void FromEnd_Days_ExpectedRange(int count)
        {
            // arrange
            var interval = new DateIntervalValue(DateInterval.Day, count);
            var expected = new DateRange(
                _startDate.AddDays(-count),
                _startDate, false);

            // act
            var range = DateRange.FromEnd(
                _startDate, 
                interval.Interval,
                interval.Value, false);

            // assert
            Assert.True(expected == range);
        }

        [Theory]
        [InlineData(3)]
        [InlineData(56)]
        [InlineData(31)]
        [InlineData(99)]
        public void EqualsOperator_SameRange_IsEqual(int count)
        {
            // arrange
            var left = new DateRange(
                _startDate,
                _startDate.AddDays(count), false);

            var right = new DateRange(
                _startDate,
                _startDate.AddDays(count), false);

            // act
            
            // assert
            Assert.True(left == right);
            Assert.True(right == left);
        }

        [Theory]
        [InlineData(3)]
        [InlineData(56)]
        [InlineData(31)]
        [InlineData(99)]
        public void EqualsOperator_DifferentRange_IsNotEqual(int count)
        {
            // arrange
            var left = new DateRange(
                _startDate,
                _startDate.AddDays(count), false);

            var right = new DateRange(
                _startDate,
                _startDate.AddDays(count*2), false);

            // act

            // assert
            Assert.True(left != right);
            Assert.True(right != left);
        }

        [Theory]
        [InlineData(3)]
        [InlineData(56)]
        [InlineData(31)]
        [InlineData(99)]
        public void Equals_SameRangeValues_IsEqual(int count)
        {
            // arrange
            var left = new DateRange(
                _startDate,
                _startDate.AddDays(count), false);

            var right = new DateRange(
                _startDate,
                _startDate.AddDays(count), false);

            // act

            // assert
            Assert.Equal(left, right);
            Assert.Equal(right, left);
        }

        [Theory]
        [InlineData(3)]
        [InlineData(56)]
        [InlineData(31)]
        [InlineData(99)]
        public void Equals_SameRangeValues_HashIsEqual(int count)
        {
            // arrange
            var left = new DateRange(
                _startDate,
                _startDate.AddDays(count), false);

            var right = new DateRange(
                _startDate,
                _startDate.AddDays(count), false);

            // act

            // assert
            Assert.Equal(left.GetHashCode(), right.GetHashCode());
            Assert.Equal(right.GetHashCode(), left.GetHashCode());
        }

        [Theory]
        [InlineData(3)]
        [InlineData(56)]
        [InlineData(31)]
        [InlineData(99)]
        public void Equals_DifferentRangeValues_IsNotEqual(int count)
        {
            // arrange
            var left = new DateRange(
                _startDate,
                _startDate.AddDays(count), false);

            var right = new DateRange(
                _startDate,
                _startDate.AddDays(count*3), false);

            // act

            // assert
            Assert.NotEqual(left, right);
            Assert.NotEqual(right, left);
        }

        [Theory]
        [InlineData(3)]
        [InlineData(56)]
        [InlineData(31)]
        [InlineData(99)]
        public void Equals_DifferentRangeValues_HashIsNotEqual(int count)
        {
            // arrange
            var left = new DateRange(
                _startDate,
                _startDate.AddDays(count), false);

            var right = new DateRange(
                _startDate,
                _startDate.AddDays(count * 3), false);

            // act

            // assert
            Assert.NotEqual(left.GetHashCode(), right.GetHashCode());
            Assert.NotEqual(right.GetHashCode(), left.GetHashCode());
        }

        [Fact]
        public void Addition_ExpectedValue()
        {
            // arrange
            var range = DateRange.FromStart(_startDate, DateInterval.Day, 1, true);
            var operand = new DateIntervalValue(DateInterval.Week, 2);
            var expectedEndDate = range.EndDate.AddDays(2 * 7);
            var expectedRange = new DateRange(range.StartDate, expectedEndDate, true);

            // act
            var result = range + operand;

            // assert
            Assert.Equal(expectedRange, result);
        }

        [Fact]
        public void Subtraction_ExpectedValue()
        {
            // arrange
            var range = DateRange.FromStart(_startDate, DateInterval.Week, 1, true);
            var expectedResult = DateRange.FromStart(_startDate, DateInterval.Week, 0, true);
            var operand = new DateIntervalValue(DateInterval.Week, 1);

            // act
            var result = range - operand;

            // assert
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void Subtraction_EndLessThanStart_CoversOneDay()
        {
            // arrange
            var range = DateRange.FromStart(_startDate, DateInterval.Day, 5, true);
            var expectedResult = new DateRange(range.StartDate, range.StartDate, true);
            var operand = new DateIntervalValue(DateInterval.Week, 1);

            // act
            var result = range - operand;

            // assert
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void FromRelative_Second_ExpectedRange()
        {
            // arrange
            var today = new DateTimeOffset(2017, 4, 22, 1, 2, 3, TimeSpan.Zero);
            var expectedRange = new DateRange(
                new DateTimeOffset(2017, 4, 22, 1, 2, 3, TimeSpan.Zero),
                new DateTimeOffset(2017, 4, 22, 1, 2, 3, 999, TimeSpan.Zero), false);

            // act
            var range = DateRange.FromRelative(DateInterval.Second, today);

            // assert
            Assert.Equal(expectedRange, range);
        }

        [Fact]
        public void FromRelative_Minute_ExpectedRange()
        {
            // arrange
            var today = new DateTimeOffset(2017, 4, 22, 1, 2, 3, TimeSpan.Zero);
            var expectedRange = new DateRange(
                new DateTimeOffset(2017, 4, 22, 1, 2, 0, TimeSpan.Zero),
                new DateTimeOffset(2017, 4, 22, 1, 2, 59, 999, TimeSpan.Zero), false);

            // act
            var range = DateRange.FromRelative(DateInterval.Minute, today);

            // assert
            Assert.Equal(expectedRange, range);
        }

        [Fact]
        public void FromRelative_Hour_ExpectedRange()
        {
            // arrange
            var today = new DateTimeOffset(2017, 4, 22, 1, 2, 3, TimeSpan.Zero);
            var expectedRange = new DateRange(
                new DateTimeOffset(2017, 4, 22, 1, 0, 0, TimeSpan.Zero),
                new DateTimeOffset(2017, 4, 22, 1, 59, 59, 999, TimeSpan.Zero), false);

            // act
            var range = DateRange.FromRelative(DateInterval.Hour, today);

            // assert
            Assert.Equal(expectedRange, range);
        }

        [Fact]
        public void FromRelative_Day_ExpectedRange()
        {
            // arrange
            var today = new DateTimeOffset(2017, 4, 22, 1, 2, 3, TimeSpan.Zero);
            var expectedRange = new DateRange(
                new DateTimeOffset(2017, 4, 22, 0, 0, 0, TimeSpan.Zero),
                new DateTimeOffset(2017, 4, 22, 23, 59, 59, 999, TimeSpan.Zero), false);

            // act
            var range = DateRange.FromRelative(DateInterval.Day, today);

            // assert
            Assert.Equal(expectedRange, range);
        }

        [Fact]
        public void FromRelative_Week_ExpectedRange()
        {
            // arrange

            // this sample week range is from 4/16/2017 - 4/22/2017
            var today = new DateTimeOffset(2017, 4, 22, 1, 2, 3, TimeSpan.Zero);
            var expectedRange = new DateRange(
                new DateTimeOffset(2017, 4, 16, 0, 0, 0, TimeSpan.Zero),
                new DateTimeOffset(2017, 4, 22, 23, 59, 59, 999, TimeSpan.Zero), false);

            // act
            var range = DateRange.FromRelative(DateInterval.Week, today);

            // assert
            Assert.Equal(expectedRange, range);
        }

        [Fact]
        public void FromRelative_Month_ExpectedRange()
        {
            // arrange
            var today = new DateTimeOffset(2017, 4, 22, 1, 2, 3, TimeSpan.Zero);
            var expectedRange = new DateRange(
                new DateTimeOffset(2017, 4, 1, 0, 0, 0, TimeSpan.Zero),
                new DateTimeOffset(2017, 4, 30, 23, 59, 59, 999, TimeSpan.Zero), false);

            // act
            var range = DateRange.FromRelative(DateInterval.Month, today);

            // assert
            Assert.Equal(expectedRange, range);
        }

        [Fact]
        public void FromRelative_Year_ExpectedRange()
        {
            // arrange
            var today = new DateTimeOffset(2017, 4, 22, 1, 2, 3, TimeSpan.Zero);
            var expectedRange = new DateRange(
                new DateTimeOffset(2017, 1, 1, 0, 0, 0, TimeSpan.Zero),
                new DateTimeOffset(2017, 12, 31, 23, 59, 59, 999, TimeSpan.Zero), false);

            // act
            var range = DateRange.FromRelative(DateInterval.Year, today);

            // assert
            Assert.Equal(expectedRange, range);
        }
    }
}
