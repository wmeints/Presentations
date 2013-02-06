using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecipeBrowser.Web.Helpers
{
    /// <summary>
    /// Extensions for the enumerable class
    /// </summary>
    public static class EnumerableExtensions
    {
        public static IEnumerable<T> Apply<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            foreach (T item in enumerable)
            {
                // Execute the action
                action(item);
            }

            return enumerable;
        }
    }
}