using System;
using System.Threading.Tasks;
using Xunit;

namespace Provausio.Common.Tests
{
    public class TimestampTests
    {
        [Fact]
        public void FromMilliseconds_AsLong_ParsesCorrectly()
        {
            // arrange
            var stamp = Timestamp.UtcNowMilliseconds();

            // act
            var dateStamp = Timestamp.FromMilliseconds(stamp);

            // assert
            Assert.Equal(DateTimeOffset.FromUnixTimeMilliseconds(stamp), dateStamp);
        }

        [Fact]
        public void FromMilliseconds_AsSTring_ParsesCorrectly()
        {
            // arrange
            var stamp = Timestamp.UtcNowMilliseconds();
            var asString = stamp.ToString();

            // act
            var dateStamp = Timestamp.FromMilliseconds(asString);

            // assert
            Assert.Equal(DateTimeOffset.FromUnixTimeMilliseconds(stamp), dateStamp);
        }

        [Fact]
        public void AreEqual_Strings_AreEqual()
        {
            // arrange
            var stamp = Timestamp.UtcNowMilliseconds();
            var asString = stamp.ToString();

            // act
            var areEqual = Timestamp.AreEqual(asString, asString);

            // assert
            Assert.True(areEqual);
        }

        [Fact]
        public async Task AreEqual_Strings_AreNotEqual()
        {
            // arrange
            var stamp1 = Timestamp.UtcNowMilliseconds().ToString();
            await Task.Delay(1500);
            var stamp2 = Timestamp.UtcNowMilliseconds().ToString();

            // act
            var areEqual = Timestamp.AreEqual(stamp1, stamp2);

            // assert
            Assert.False(areEqual);
        }
    }
}
