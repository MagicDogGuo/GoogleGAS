
using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
namespace UnkaEditor.IAttribute
{
    public static class AttributeReader
    {
        public static IEnumerable<MethodInfo> ReadMethods<T>(Type type) where T : Attribute
        {
            return type.GetMethods().Where(m => m.GetCustomAttributes(typeof(T), true).Count() > 0);
        }

        public static T GetMethodAttribute<T>(MethodInfo methodInfo) where T : Attribute
        {
            var list = methodInfo.GetCustomAttributes(true).Where(m => m.GetType() == typeof(T));
            int c = list.Count();
            var r = list.ElementAt(0);
            return c > 0 ? (T)r : null;
        }

    }
}