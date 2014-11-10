using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Common.Utility.Extensions
{
    public static class IDictionaryExtensions
    {
        public static bool TryGetValue<T>(this IDictionary self, string key, out T value)
        {
            value = default(T);
            var v = self[key];

            if (v != null)
            {
                value = (T)v;
                return true;
            }

            return false;
        }

        /// <summary>
        /// Gets the value for the specified key
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static T GetValue<T>(this IDictionary self, object key)
        {
            var v = self[key];
            return v != null ? (T)v : default(T);
        }

        /// <summary>
        /// Check that all keys are present in the IDictionary
        /// </summary>
        /// <param name="self"></param>
        /// <param name="keys"></param>
        /// <returns></returns>
        public static bool ContainAllKeys(this IDictionary self, object[] keys)
        {
            return keys.All(x => self.Contains(x));
        }

        /// <summary>
        /// Check that all keys are present in the IDictionary
        /// </summary>
        /// <param name="self"></param>
        /// <param name="keys"></param>
        /// <returns></returns>
        public static bool ContainAnyKey(this IDictionary self, object[] keys)
        {
            return keys.Any(x => self.Contains(x));
        }

        /// <summary>
        /// Tries to remove the item from the IDictionary if it exists
        /// </summary>
        /// <param name="self"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool TryRemove(this IDictionary self, object key)
        {
            if (self.Contains(key))
            {
                self.Remove(key);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Adds the items if it doesn't exist, otherwise, the item's value will be updated
        /// </summary>
        /// <param name="self"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void AddOrUpdate(this IDictionary self, object key, object value)
        {
            if (self.Contains(key))
            {
                self[key] = value;
            }
            else
            {
                self.Add(key, value);
            }
        }

        /// <summary>
        /// Creates a shalow copy of the dictionary
        /// </summary>
        /// <typeparam name="K"></typeparam>
        /// <typeparam name="V"></typeparam>
        /// <param name="self"></param>
        /// <param name="target"></param>
        public static void CopyTo<K, V>(this IDictionary<K, V> self, IDictionary<K, V> target)
        {
            foreach (var kvp in self)
            {
                target.Add(kvp.Key, kvp.Value);
            }
        }

        /// <summary>
        /// Create a shallow or deep-copy of the dictionary. Provide the copyFunc to enable deep-copy.
        /// </summary>
        /// <typeparam name="K"></typeparam>
        /// <typeparam name="V"></typeparam>
        /// <param name="self"></param>
        /// <param name="target"></param>
        /// <param name="copyFunc"></param>
        public static void CopyTo<K, V>(this IDictionary<K, V> self, IDictionary<K, V> target, Func<V, V> copyFunc)
        {
            foreach (var kvp in self)
            {
                target.Add(kvp.Key, copyFunc(kvp.Value));
            }
        }
    }
}
