using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using Provausio.Common.Comparison;

namespace Provausio.Common.Ext
{
    public static class ObjectExt
    {
        [ExcludeFromCodeCoverage]
        public static T FindAttribute<T>(this object target)
            where T : Attribute
        {
            var targetType = target.GetType();
            var attribute = targetType.GetTypeInfo().GetCustomAttribute(typeof(T));
            return attribute as T;
        }

        [ExcludeFromCodeCoverage]
        public static IEnumerable<T> FindAttributes<T>(this object target)
        {
            var targetType = target.GetType();
            var attributes = targetType
                .GetTypeInfo()
                .GetCustomAttributes(typeof(T))
                .Cast<T>();

            return attributes;
        }

        public static ObjectChanges Compare<T>(this T oldObject, T newObject)
            where T : class, new()
        {
            return ObjectDiff.Compare(oldObject, newObject);
        }
    }
}
