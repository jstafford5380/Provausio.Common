namespace Provausio.Common.Comparison
{
    public enum DiffChangeType
    {
        /// <summary>
        /// The value was not explicitly set.
        /// </summary>
        Unspecified,
        /// <summary>
        /// Both objects were the same.
        /// </summary>
        NoOp,
        /// <summary>
        /// The old object was null and the new object was not.
        /// </summary>
        ObjectCreated,
        /// <summary>
        /// The old object was not null but the new object is null.
        /// </summary>
        ObjectDeleted,
        /// <summary>
        /// Both objects were not null.
        /// </summary>
        ObjectUpdated
    }
}