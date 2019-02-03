using System;

namespace Provausio.Common.DomainBase
{
    public interface IDomainEntity
    {
        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        Guid Id { get; }
    }

    public abstract class DomainEntity : IDomainEntity
    {
        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public Guid Id { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DomainEntity"/> class.
        /// </summary>
        protected DomainEntity()
        {
            Id = Guid.NewGuid();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DomainEntity"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <exception cref="System.ArgumentException">Default id value is invalid.</exception>
        protected DomainEntity(Guid id)
        {
            if (id == default(Guid))
                throw new ArgumentException("Default id value is invalid.", nameof(id));

            Id = id;
        }

        public override bool Equals(object obj)
        {
            var entity = obj as DomainEntity;
            return entity != null && Equals(entity);
        }

        public bool Equals(DomainEntity other)
        {
            return other.Id == Id;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
