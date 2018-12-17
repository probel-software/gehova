using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Probel.Gehova.Business.Helpers
{
    public static class DumpExtensions
    {
        #region Methods

        private static IEnumerable<PropertyInfo> GetProperties(object instance)
        {
            var properties = from p in instance.GetType().GetProperties()
                             orderby p.Name
                             select p;
            return properties;
        }

        public static string Dump<T>(this T instance)
        {
            var str = string.Empty;
            if (instance == null) { str = $"Instance is <NULL>"; }
            else
            {
                str += $"Dump of '{instance.GetType()}'" + Environment.NewLine;
                foreach (var p in GetProperties(instance))
                {
                    str += $"\t{p.Name}: "
                        + (p.GetValue(instance, null)?.ToString() ?? "<NULL>")
                        + Environment.NewLine;
                }
            }
            return str.Remove(str.Length - Environment.NewLine.Length, Environment.NewLine.Length);
        }

        public static string Dump<T>(this IEnumerable<T> collection)
        {
            var str = string.Empty;
            if (collection == null)
            {
                str = $"Collection is <NULL>";
            }
            else if (collection.Count() == 0)
            {
                str = $"Collection of type '{collection.GetType()}' is empty.";
            }
            else
            {
                str += $"Dumping Collectiono of '{collection.GetType()}'" + Environment.NewLine;
                var counter = 0;
                foreach (var item in collection)
                {
                    str += $"item {++counter,3}{Environment.NewLine}";
                    foreach (var p in GetProperties(item))
                    {
                        str += $"\t{p.Name}: "
                            + (p.GetValue(item, null)?.ToString() ?? "<NULL>")
                            + Environment.NewLine;
                    }
                    str += Environment.NewLine;
                }

            }
            return str;
        }

        #endregion Methods
    }
}