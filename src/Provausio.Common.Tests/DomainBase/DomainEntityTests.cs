using System;
using Provausio.Common.DomainBase;
using Xunit;

namespace Provausio.Common.Tests.DomainBase
{
    public class DomainEntityTests
    {
        [Fact]
        public void Ctor_Default_AssignsId()
        {
            // arrange

            // act
            var ent = new FakeEntity();

            // assert
            Assert.NotEqual(default(Guid), ent.Id);
        }

        [Fact]
        public void Ctor_WithValidId_AssignsId()
        {
            // arrange
            var existingId = Guid.NewGuid();
            
            // act
            var ent = new FakeEntity(existingId);

            // assert
            Assert.Equal(existingId, ent.Id);
        }

        [Fact]
        public void Ctor_DefaultGuid_Throws()
        {
            // arrange
            var id = new Guid();

            // act

            // assert
            Assert.Throws<ArgumentException>(() => new FakeEntity(id));
        }

        [Fact]
        public void Equals_SameId_ReturnsTrue()
        {
            // arrange
            var id = Guid.NewGuid();
            var e1 = new FakeEntity(id);
            var e2 = new FakeEntity(id);

            // act
            
            // assert
            Assert.Equal(e1, e2);
        }

        [Fact]
        public void Equals_DifferentIds_ReturnsFalse()
        {
            // arrange
            var e1 = new FakeEntity();
            var e2 = new FakeEntity();

            // act

            // assert
            Assert.NotEqual(e1, e2);
        }

        [Fact]
        public void GetHashCode_SameId_SameCode()
        {
            // arrange
            var id = Guid.NewGuid();
            var e1 = new FakeEntity(id);
            var e2 = new FakeEntity(id);

            // act

            // assert
            Assert.Equal(e1.GetHashCode(), e2.GetHashCode());
        }

        [Fact]
        public void GetHashCode_DifferentIds_DifferentCode()
        {
            // arrange
            var e1 = new FakeEntity();
            var e2 = new FakeEntity();

            // act

            // assert
            Assert.NotEqual(e1.GetHashCode(), e2.GetHashCode());
        }

        private class FakeEntity : DomainEntity
        {
            public FakeEntity()
            {
            }

            public FakeEntity(Guid id) : base(id)
            {
            }
        }
    }
}
