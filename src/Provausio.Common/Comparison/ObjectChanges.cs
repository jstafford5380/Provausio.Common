using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Provausio.Common.Comparison
{
    /// <inheritdoc />
    /// <summary>
    /// A collection of property changes.
    /// </summary>
    /// <seealso cref="!:System.Collections.ObjectModel.ReadOnlyCollection{DAS.Infrastructure.ObjectChange}" />
    public class ObjectChanges : ReadOnlyCollection<ObjectChange>
    {
        public DiffChangeType DiffChangeType { get; internal set; }

        public ObjectChanges(DiffChangeType type, IList<ObjectChange> list) : base(list)
        {
            DiffChangeType = type;
        }

        public ObjectChanges(IList<ObjectChange> list) : base(list)
        {
        }
    }
}