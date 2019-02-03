using System;

namespace Provausio.Common.Comparison
{
    public class ObjectChange
    {
        /// <summary>
        /// Gets the name of the property that was changed.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; }

        /// <summary>
        /// Gets the type of the property that was changed.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public Type Type { get; }

        /// <summary>
        /// Gets the previous value of the changed property.
        /// </summary>
        /// <value>
        /// The previous value.
        /// </value>
        public object PreviousValue { get; }

        /// <summary>
        /// Gets the previous value of the changed property.
        /// </summary>
        /// <value>
        /// The new value.
        /// </value>
        public object NewValue { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ObjectChange"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="type">The type.</param>
        /// <param name="previousValue">The previous value.</param>
        /// <param name="newValue">The new value.</param>
        public ObjectChange(string name, Type type, object previousValue, object newValue)
        {
            Name = name;
            Type = type;
            PreviousValue = previousValue;
            NewValue = newValue;
        }
    }
}