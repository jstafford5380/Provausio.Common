using System.Collections.Generic;
using Xunit;

namespace Provausio.Common.Tests
{
    public class WordCloudTests
    {
        [Fact]
        public void Ctor_SingleCount_ReturnsAllWordsSingleCount()
        {
            // arrange
            var input = new[] {"fooo", "baar", "baaz"};

            // act
            var cloud = new WordCloud(input);

            foreach (var entry in cloud)
            {
                Assert.Equal(1, entry.Value);
            }
        }

        [Fact]
        public void Ctor_MultipleCounts_ReturnsExpected()
        {
            // arrange
            var input = new[] {"foo", "foo", "bar", "baz", "baz"};

            // act
            var cloud = new WordCloud(new List<string>(), 2, input);

            // assert
            Assert.Equal(2, cloud["foo"]);
            Assert.Equal(2, cloud["baz"]);
            Assert.Equal(1, cloud["bar"]);
        }

        [Fact]
        public void Ctor_SingleCount_OmitsSmallwords()
        {
            // using default threshold of 3

            // arrange
            var input = new[] {"foo", "baar", "baaz"};

            // act
            var cloud = new WordCloud(input);

            // assert
            Assert.False(cloud.ContainsKey("foo"));
        }

        [Fact]
        public void Ctor_WithStopwordList_OmitsStopwords()
        {
            // arrange
            var input = new[] {"foo", "bar", "baz"};
            var stopwords = new[] {"foo", "bar"};
            
            // act
            var cloud = new WordCloud(stopwords, 2, input);

            // assert
            Assert.Equal(1, cloud.Count);
            Assert.True(cloud.ContainsKey("baz"));
        }
    }
}
