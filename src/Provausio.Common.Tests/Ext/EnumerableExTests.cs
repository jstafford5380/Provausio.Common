using System.Collections;
using System.Collections.Generic;
using Provausio.Common.Ext;
using Xunit;

namespace Provausio.Common.Tests.Ext
{
    public class EnumerableExTests
    {
        [Fact]
        public void IsNullOrEmpty_IsNull_IsTrue()
        {
            // arrange
            IEnumerable source = null;

            // act
            var result = source.IsNullOrEmpty();

            // assert
            Assert.True(result);
        }

        [Fact]
        public void IsNullOrEmpty_IsEmpty_IsTrue()
        {
            // arrange
            var source = new List<string>();

            // act
            var result = source.IsNullOrEmpty();

            // assert
            Assert.True(result);
        }

        [Fact]
        public void IsNullOrEmpty_IsNotNullOrEmpty_IsFalse()
        {
            // arrange
            var source = new List<string> {"foo", "bar"};

            // act
            var result = source.IsNullOrEmpty();

            // assert
            Assert.False(result);

        }
    }
}
