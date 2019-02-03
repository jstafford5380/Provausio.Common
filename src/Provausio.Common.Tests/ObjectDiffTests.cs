using System.Linq;
using Provausio.Common.Comparison;
using Xunit;

namespace Provausio.Common.Tests
{
    public class ObjectDiffTests
    {
        [Fact]
        public void Compare_Prop1Null_OneChangeFound()
        {
            // arrange
            var o1 = new TestObject { Prop1 = null, Prop2 = 1 };
            var o2 = new TestObject { Prop1 = "bar", Prop2 = 1 };

            // act
            var changes = ObjectDiff.Compare(o1, o2);

            // assert
            Assert.Single(changes);
        }

        [Fact]
        public void Compare_Prop1Changed_OneChangeFound()
        {
            // arrange
            var o1 = new TestObject { Prop1 = "foo", Prop2 = 1 };
            var o2 = new TestObject { Prop1 = "bar", Prop2 = 1 };

            // act
            var changes = ObjectDiff.Compare(o1, o2);

            // assert
            Assert.Single(changes);
        }

        [Fact]
        public void Compare_Prop1Changed_IsUpdated()
        {
            // arrange
            var o1 = new TestObject { Prop1 = "foo", Prop2 = 1 };
            var o2 = new TestObject { Prop1 = "bar", Prop2 = 1 };

            // act
            var changes = ObjectDiff.Compare(o1, o2);

            // assert
            Assert.Equal(DiffChangeType.ObjectUpdated, changes.DiffChangeType);
        }

        [Fact]
        public void Compare_Prop1Changed_ChangesAreCorrect()
        {
            // arrange
            var o1 = new TestObject { Prop1 = "foo", Prop2 = 1 };
            var o2 = new TestObject { Prop1 = "bar", Prop2 = 1 };

            // act
            var changes = ObjectDiff.Compare(o1, o2);
            var change = changes.First();

            // assert
            Assert.Equal("foo", change.PreviousValue);
            Assert.Equal("bar", change.NewValue);
        }

        [Fact]
        public void Compare_Prop1Changed_ChangesTypeIsCorrect()
        {
            // arrange
            var o1 = new TestObject { Prop1 = "foo", Prop2 = 1 };
            var o2 = new TestObject { Prop1 = "bar", Prop2 = 1 };

            // act
            var changes = ObjectDiff.Compare(o1, o2);
            var change = changes.First();

            // assert
            Assert.Equal(typeof(string), change.Type);
        }

        [Fact]
        public void Compare_Prop1Changed_ChangedPropertyNameIsCorrect()
        {
            // arrange
            var o1 = new TestObject { Prop1 = "foo", Prop2 = 1 };
            var o2 = new TestObject { Prop1 = "bar", Prop2 = 1 };

            // act
            var changes = ObjectDiff.Compare(o1, o2);
            var change = changes.First();

            // assert
            Assert.Equal("Prop1", change.Name);
        }

        [Fact]
        public void Compare_Prop1and2Changed_TwoChangesFound()
        {
            // arrange
            var o1 = new TestObject { Prop1 = "foo", Prop2 = 1 };
            var o2 = new TestObject { Prop1 = "bar", Prop2 = 2 };

            // act
            var changes = ObjectDiff.Compare(o1, o2);

            // assert
            Assert.Equal(2, changes.Count);
        }

        [Fact]
        public void Compare_LeftSideNull_RightSideNotNull_ReturnsAllPropertiesChanged()
        {
            // arrange
            TestObject o1 = null;
            var o2 = new TestObject {Prop1 = "bar", Prop2 = 2};

            // act
            var changes = ObjectDiff.Compare(o1, o2);

            // assert
            Assert.Equal(2, changes.Count);
        }

        [Fact]
        public void Compare_LeftSideNull_RightSideNotNull_ReturnsCreated()
        {
            // arrange
            TestObject o1 = null;
            var o2 = new TestObject {Prop1 = "bar", Prop2 = 2};

            // act
            var changes = ObjectDiff.Compare(o1, o2);

            // assert
            Assert.Equal(DiffChangeType.ObjectCreated, changes.DiffChangeType);
        }

        [Fact]
        public void Compare_LeftSideNotNull_RightSideNull_ReturnsAllPropertiesChanged()
        {
            // arrange
            var o1 = new TestObject {Prop1 = "foo", Prop2 = 1};
            TestObject o2 = null;

            // act
            var changes = ObjectDiff.Compare(o1, o2);

            // assert
            Assert.Equal(2, changes.Count);
        }

        [Fact]
        public void Compare_LeftSideNotNull_RightSideNull_ReturnsDeleted()
        {
            // arrange
            var o1 = new TestObject {Prop1 = "foo", Prop2 = 1};
            TestObject o2 = null;

            // act
            var changes = ObjectDiff.Compare(o1, o2);

            // assert
            Assert.Equal(DiffChangeType.ObjectDeleted, changes.DiffChangeType);
        }

        [Fact]
        public void Compare_BothSidesEqual_ReturnsNoop()
        {
            // arrange
            var o1 = new TestObject {Prop1 = "foo", Prop2 = 1};
            var o2 = new TestObject {Prop1 = "foo", Prop2 = 1};

            // act
            var changes = ObjectDiff.Compare(o1, o2);

            // assert
            Assert.Equal(DiffChangeType.NoOp, changes.DiffChangeType);
        }

        private class TestObject
        {
            public string Prop1 { get; set; }

            public int Prop2 { get; set; }
        }
    }
}
