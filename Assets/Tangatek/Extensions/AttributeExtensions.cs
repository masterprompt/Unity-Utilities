using System;
using System.Collections.Generic;
using System.Linq;

namespace Tangatek
{
    public static class AttributeExtensions
    {
        public static TValue GetAttributeValue<TAttribute, TValue>(this Type type, Func<TAttribute, TValue> valueSelector) where TAttribute : Attribute
        {
            var att = (TAttribute)type.GetCustomAttributes( typeof(TAttribute), true)
                .FirstOrDefault();
            if (att != null) return valueSelector(att);
            return default(TValue);
        }

        public static bool HasAttribute(this Type type, Type attribute)
        {
            return type.IsDefined(attribute, false);
        }

        public static bool IsTypeOf<T>(this Type type)
        {
            return typeof(T).IsAssignableFrom(type);
        }
        public static bool IsTypeOf(this Type type, Type target)
        {
            return target.IsAssignableFrom(type);
        }



        internal static IEnumerable<Type> WithAttribute(this IEnumerable<Type> types, Type attributeType)
        {
            return types.Where(type => type.HasAttribute(attributeType));
        }


    }
}
