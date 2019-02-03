using System;
using System.Collections.Generic;
using System.Linq;
using Provausio.Common.Ext;
using Xunit;

namespace Provausio.Common.Tests.Ext
{
    public class ListExtTests
    {
        [Fact]
        public void Replace_NoMatch_NothingWasChanged()
        {
            // arrange
            var targetList = new List<string>(){"foo", "bar"};

            // act
            var result = targetList.Replace("baz", "boing");

            // assert
            Assert.Equal(-1, result);
        }

        [Fact]
        public void Replace_OneMatch_OneChanged()
        {
            // arrange
            var targetList = new List<string>{"foo", "bar"};

            // act
            var index = targetList.Replace("foo", "bar");

            // assert
            Assert.Equal(0, index);
            Assert.Equal(2, targetList.Count(s => s.Equals("bar")));
        }

        [Fact]
        public void AddOrReplace_DoesntExist_AddsNew()
        {
            // arrange
            var targetList = new List<string> {"foo"};
            
            // act
            var index = targetList.AddOrReplace("bar", "baz");

            // assert
            Assert.Equal(-1, index);
            Assert.Equal(1, targetList.Count(s => s.Equals("baz")));
        }

        [Fact]
        public void AddOrReplace_NullEntry_AddsNewValue()
        {
            // arrange
            var targetList = new List<string> {"foo"};

            // act
            var index = targetList.AddOrReplace(null, "bar");

            // assert
            Assert.Equal(-1, index);
            Assert.Equal(2, targetList.Count);
            Assert.Equal(1, targetList.Count(s => s.Equals("bar")));
        }

        [Fact]
        public void AddOrReplace_DoesExist_ReplacesValue()
        {
            // arrange
            var targetList = new List<string> {"foo", "bar"};

            // act
            var index = targetList.AddOrReplace("foo", "baz");

            // assert
            Assert.Equal(0, index);
            Assert.Equal(1, targetList.Count(s => s.Equals("baz")));
        }

        [Fact]
        public void Replace_NullList_Throws()
        {
            // arrange
            List<string> targetList = null;

            // act

            // assert
            Assert.Throws<ArgumentNullException>(() => targetList.Replace("foo", "bar"));
        }

        [Fact]
        public void ReplaceAll_AllInstancesReplaced()
        {
            // arrange
            var targetList = new List<string> {"foo", "foo"};

            // act
            targetList.ReplaceAll("foo", "bar");

            // assert
            Assert.Equal(2, targetList.Count(s => s.Equals("bar")));
        }

        [Fact]
        public void ReplaceAll_NullList_Throws()
        {
            // arrange
            List<string> targetList = null;

            // act

            // assert
            Assert.Throws<ArgumentNullException>(() => targetList.ReplaceAll("foo", "bar"));
        }

        [Fact]
        public void Replace_IEnumerable_ReturnsExpectedList()
        {
            // arrange
            IEnumerable<string> targetList = new List<string>{"foo", "foo", "bar"};

            // act
            var result = targetList.Replace("foo", "bar");

            // assert
            Assert.Equal(3, result.Count(s => s.Equals("bar")));
        }

        [Fact]
        public void Replace_IEnumerable_NullList_Throws()
        {
            // arrange
            IEnumerable<string> targetList = null;

            // act

            // assert
            Assert.Throws<ArgumentNullException>(() => targetList.Replace("foo", "bar"));
        }
    }
}
