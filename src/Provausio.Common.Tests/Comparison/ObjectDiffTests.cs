using System.Linq;
using System.Threading;
using Provausio.Common.Comparison;
using Provausio.Common.Ext;
using Xunit;

namespace Provausio.Common.Tests.Comparison
{
    public class ObjectDiffTests
    {
        [Fact]
        public void Compare_OneUpdate_ChangeDetected()
        {
            // arrange
            var obj1 = new TestObject {Prop1 = "foo", Prop2 = 11};
            var obj2 = new TestObject {Prop1 = "foo", Prop2 = 12};

            // act
            var diff = ObjectDiff.Compare(obj1, obj2);

            // assert
            var change = diff.SingleOrDefault();
            Assert.NotNull(change);
            Assert.Equal("Prop2", change.Name);
            Assert.Equal(11, change.PreviousValue);
            Assert.Equal(12, change.NewValue);
            Assert.Equal(DiffChangeType.ObjectUpdated, diff.DiffChangeType);
        }

        [Fact]
        public void Compare_NewObjectIsNull_ObjectDeleted()
        {
            // arrange
            var obj1 = new TestObject { Prop1 = "foo", Prop2 = 11 };

            // act
            var diff = ObjectDiff.Compare(obj1, null);

            // assert
            Assert.Equal(DiffChangeType.ObjectDeleted, diff.DiffChangeType);
        }

        [Fact]
        public void Compare_BothObjectNull_IsNoOp()
        {
            // arrange

            // act
            var diff = ObjectDiff.Compare<TestObject>(null, null);

            // assert
            Assert.Equal(DiffChangeType.NoOp, diff.DiffChangeType);
        }

        [Fact]
        public void Compare_NoUpdates_NoChangesDetected()
        {
            // arrange
            var obj1 = new TestObject { Prop1 = "foo", Prop2 = 11 };
            var obj2 = new TestObject { Prop1 = "foo", Prop2 = 11 };

            // act
            var diff = ObjectDiff.Compare(obj1, obj2);

            // assert
            Assert.Empty(diff);
        }

        [Fact]
        public void CompareExt_OneUpdate_ChangeDetected()
        {
            // arrange
            var obj1 = new TestObject { Prop1 = "foo", Prop2 = 11 };
            var obj2 = new TestObject { Prop1 = "foo", Prop2 = 12 };

            // act
            var diff = obj1.Compare(obj2);

            // assert
            var change = diff.SingleOrDefault();
            Assert.NotNull(change);
            Assert.Equal("Prop2", change.Name);
            Assert.Equal(11, change.PreviousValue);
            Assert.Equal(12, change.NewValue);
            Assert.Equal(DiffChangeType.ObjectUpdated, diff.DiffChangeType);
        }

        public class TestObject
        {
            public string Prop1 { get; set; }

            public int Prop2 { get; set; }
        }
    }
}
