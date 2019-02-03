using System;
using Provausio.Common.Ext;
using Xunit;

namespace Provausio.Common.Tests.Ext
{
    public class DateTimeOffsetExtTests
    {
        [Fact]
        public void GetFirstMillisecond_CorrectDate()
        {
            // arrange
            var date = DateTimeOffset.Now;
            var expected = new DateTimeOffset(date.Year, date.Month, date.Day, 0, 0, 0, 0, date.Offset);

            // act
            var result = date.GetFirstMillisecond();

            // assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void GetLastMillisecond_CorrectDate()
        {
            // arrange
            var date = DateTimeOffset.Now;
            var expected = new DateTimeOffset(date.Year, date.Month, date.Day, 23, 59, 59, 999, date.Offset);

            // act
            var result = date.GetLastMillisecond();

            // assert
            Assert.Equal(expected, result);
            Assert.Equal(date.Offset, result.Offset);
        }

        [Fact]
        public void GetLastMillisecond_UtcDate_PreservesOffset()
        {
            // arrange
            var date = DateTimeOffset.UtcNow;
            
            // act
            var result = date.GetLastMillisecond();

            // assert
            Assert.Equal(date.Offset, result.Offset);
        }
    }
}
