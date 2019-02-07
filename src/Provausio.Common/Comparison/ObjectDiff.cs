using System.Collections.Generic;
using System.Reflection;

namespace Provausio.Common.Comparison
{
    /// <summary>
    /// Helper utility that compares two objects.
    /// </summary>
    public static class ObjectDiff
    {
        /// <summary>
        /// Compares public properties of the specified old object with the new object.
        /// </summary>
        /// <param name="oldObject">The old object.</param>
        /// <param name="newObject">The new object.</param>
        /// <returns></returns>
        public static ObjectChanges Compare<T>(T oldObject, T newObject)
            where T : class, new()
        {
            ObjectChanges changes;
            if (oldObject == null || newObject == null)
            {
                changes = oldObject == null
                    ? CompareObjects(new T(), newObject)
                    : CompareObjects(oldObject, new T());

                changes.DiffChangeType = oldObject == null 
                    ? DiffChangeType.ObjectCreated 
                    : DiffChangeType.ObjectDeleted;
            }
            else if(oldObject.AllPropertiesAreEqual(newObject))
            {
                changes = new ObjectChanges(DiffChangeType.NoOp, new List<ObjectChange>());
            }
            else
            {
                changes = CompareObjects(oldObject, newObject);
                changes.DiffChangeType = DiffChangeType.ObjectUpdated;
            }

            return changes;
        }

        private static bool AllPropertiesAreEqual<T>(this T oldObject, T newObject)
        {
            foreach (var prop in GetProperties<T>())
            {
                var oldProp = prop.GetValue(oldObject);
                var newProp = prop.GetValue(newObject);
                if (!Equals(oldProp, newProp))
                    return false;
            }

            return true;
        }

        private static ObjectChanges CompareObjects<T>(T oldObject, T newObject)
            where T : class, new()
        {
            var changes = new List<ObjectChange>();
            

            foreach (var property in GetProperties<T>())
            {
                var oldObj = property.GetValue(oldObject);
                var newObj = property.GetValue(newObject);

                if (Equals(oldObj, newObj)) continue;

                var change = new ObjectChange(
                    property.Name,
                    property.PropertyType,
                    property.GetValue(oldObject),
                    property.GetValue(newObject));

                changes.Add(change);
            }

            return new ObjectChanges(changes);
        }

        private static IEnumerable<PropertyInfo> GetProperties<T>()
        {
            var type = typeof(T);
            return typeof(T)
                .GetProperties(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public);
        }
    }
}
