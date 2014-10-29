using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Reflection;

namespace Common.Utility.Configuration
{
    /// <summary>
    /// A configuration parser which resolves configuration class properties and other configuration related parsing.
    /// </summary>
    public static class DictionaryConfigurationParser
    {
        private const string GENERIC_CONFIG_ERROR = "Config Error: {0} [{1}] ";

        public static T Resolve<T>(string configSectionName) where T : class, new()
        {
            return Resolve<T>(null, configSectionName);
        }

        public static T Resolve<T>(string prefix, string configSectionName) where T : class, new()
        {
            var configSection = ConfigurationManager.GetSection(configSectionName);

            if (configSection == null)
                throw new ConfigurationErrorsException("Unable to find " + configSectionName + " in the configuration file");

            var type = typeof(T);
            var t = new T();
            var properties = type.GetProperties(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
            var errors = new List<string>();

            Hashtable configEntries = configSection as Hashtable;
            if (configEntries == null)
            {
                var nvc = ((NameValueCollection)configSection);
                configEntries = new Hashtable();
                foreach (var key in nvc.AllKeys)
                {
                    configEntries.Add(key, nvc[key]);
                }
            }

            foreach (var property in properties)
            {
                var attribs = property.GetCustomAttributes(typeof(ConfigurationEntryAttribute), true);

                if (attribs == null || attribs.Length == 0)
                    continue;

                var configKey = string.IsNullOrEmpty(prefix) ? property.Name : prefix + "_" + property.Name;
                var configEntryAttrib = (ConfigurationEntryAttribute)attribs[0];

                try
                {
                    ResolveConfigurationEntry(t, configKey, property, configEntryAttrib, configEntries);
                }
                catch (Exception ex)
                {
                    errors.Add(string.Format(GENERIC_CONFIG_ERROR + configEntryAttrib.ErrorMessage, configKey, ex.Message));
                }
            }

            if (errors.Count > 0)
                throw new ConfigurationErrorsException(string.Join(Environment.NewLine, errors.ToArray()));

            return t;
        }

        internal static void ResolveConfigurationEntry<T>(T t, string configKey, PropertyInfo property, ConfigurationEntryAttribute configEntryAttrib, Hashtable configEntries)
        {
            if (!configEntries.Contains(configKey))
            {
                // throw an exception required but no default value
                if (configEntryAttrib.IsRequired && configEntryAttrib.DefaultValue == null)
                    throw new ConfigurationErrorsException(configKey + " is not found in the configuration (Expected type: " + property.PropertyType + ")");

                // skip if not required and no default value and no custom load method specified
                if (configEntryAttrib.DefaultValue == null && string.IsNullOrEmpty(configEntryAttrib.CustomLoadMethod))
                {
                    return;
                }
                else if (configEntryAttrib.DefaultValue != null)
                {
                    // set the default value
                    property.SetValue(t, ConvertToType(configEntryAttrib.DefaultValue.ToString(), property.PropertyType, configEntryAttrib, true), null);
                }
            }
            else
            {
                // set the config entry value
                property.SetValue(t, ConvertToType(configEntries[configKey].ToString(), property.PropertyType, configEntryAttrib, false), null);
            }

            // invoke custom loading method
            if (!string.IsNullOrEmpty(configEntryAttrib.CustomLoadMethod))
            {
                try
                {
                    t.GetType().InvokeMember(configEntryAttrib.CustomLoadMethod, BindingFlags.InvokeMethod | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance, null, t, null);
                }
                catch (TargetInvocationException ex)
                {
                    var msg = ex.InnerException == null ? ex.Message : ex.InnerException.Message;
                    throw new TargetInvocationException(msg, ex);
                }
            }
        }

        private static object ConvertToType(string value, Type toType, ConfigurationEntryAttribute attrib, bool isDefaultValue)
        {
            if (toType == typeof(string))
            {
                if ((attrib != null && attrib.IsRequired) && (string.IsNullOrEmpty(value) || value.Trim().Length == 0) && !isDefaultValue)
                    throw new ConfigurationErrorsException("Value cannot be blank/empty");
                return value;
            }
            else if (toType == typeof(int))
            {
                int i;
                if (!int.TryParse(value, out i))
                    throw new ConfigurationErrorsException("Value must be numeric/number (Int32 type)");
                return i;
            }
            else if (toType == typeof(uint))
            {
                uint i;
                if (!uint.TryParse(value, out i))
                    throw new ConfigurationErrorsException("Value must be non-negative numeric/number (UInt32 type)");
                return i;
            }
            else if (toType == typeof(long))
            {
                long i;
                if (!long.TryParse(value, out i))
                    throw new ConfigurationErrorsException("Value must be numeric/number (Int64 type)");
                return i;
            }
            else if (toType == typeof(ulong))
            {
                ulong i;
                if (!ulong.TryParse(value, out i))
                    throw new ConfigurationErrorsException("Value must be non-negative numeric/number (UInt64 type)");
                return i;
            }
            else if (toType == typeof(short))
            {
                short i;
                if (!short.TryParse(value, out i))
                    throw new ConfigurationErrorsException("Value must be numeric/number (Int16 type)");
                return i;
            }
            else if (toType == typeof(ushort))
            {
                ushort i;
                if (!ushort.TryParse(value, out i))
                    throw new ConfigurationErrorsException("Value must be non-negative numeric/number (UInt16 type)");
                return i;
            }
            else if (toType == typeof(decimal))
            {
                decimal i;
                if (!decimal.TryParse(value, out i))
                    throw new ConfigurationErrorsException("Value must be numeric/number (Decimal type)");
                return i;
            }
            else if (toType == typeof(double))
            {
                double i;
                if (!double.TryParse(value, out i))
                    throw new ConfigurationErrorsException("Value must be numeric/number (Double type)");
                return i;
            }
            else if (toType == typeof(float))
            {
                float i;
                if (!float.TryParse(value, out i))
                    throw new ConfigurationErrorsException("Value must be numeric/number (Single type)");
                return i;
            }
            else if (toType == typeof(bool))
            {
                if (!new[] { "true", "yes", "1", "false", "no", "0" }.Any(x => StringInvariantCultureIgnoreCaseEquals(x, value)))
                    throw new ConfigurationErrorsException("Value must be a Boolean type. Case-insensitive allowed values: ([True = true, yes, 1], [False = false, no, 0])");

                return (StringInvariantCultureIgnoreCaseEquals(value, "yes") ||
                    StringInvariantCultureIgnoreCaseEquals(value, "true") ||
                    StringInvariantCultureIgnoreCaseEquals(value, "1"));
            }
            else if (toType.IsGenericType && (toType.GetGenericTypeDefinition() == typeof(List<>) || toType.GetGenericTypeDefinition() == typeof(IEnumerable<>)))
            {
                var parts = Split(value, attrib.Delimiter);
                Type genType = toType.GetGenericArguments()[0];
                var listType = typeof(List<>).MakeGenericType(new Type[] { genType });
                var list = Activator.CreateInstance(listType) as IList;

                foreach (var p in parts.Where(x => x.Trim().Length > 0))
                {
                    list.Add(ConvertToType(p, genType, null, false));
                }
                return list;
            }
            else if (toType.IsArray)
            {
                var parts = Split(value, attrib.Delimiter).Where(x => x.Trim().Length > 0).ToList();
                var array = Array.CreateInstance(toType.GetElementType(), parts.Count);

                for (var i = 0; i < parts.Count; i++)
                {
                    if (parts[i].Trim().Length > 0)
                        array.SetValue(ConvertToType(parts[i], toType.GetElementType(), null, false), i);
                }
                return array;
            }
            else if (typeof(ArrayList).IsAssignableFrom(toType))
            {
                var parts = Split(value, attrib.Delimiter);
                var arrayList = new ArrayList();

                foreach (var p in parts.Where(x => x.Trim().Length > 0))
                {
                    arrayList.Add(p);
                }
                return arrayList;
            }
            else if (toType.IsEnum)
            {
                // perform case-insensitive search
                var names = Enum.GetNames(toType);
                foreach (var name in names)
                {
                    if (StringInvariantCultureIgnoreCaseEquals(name, value))
                        return Enum.Parse(toType, name);
                }

                throw new ConfigurationErrorsException(value + " is not defined in " + toType + " enumeration. Valid values: " + string.Join(",", names) + ")");
            }
            else
                return value;
        }

        private static bool StringInvariantCultureIgnoreCaseEquals(string text1, string text2)
        {
            return string.Compare(text1, text2, StringComparison.InvariantCultureIgnoreCase) == 0;
        }

        private static string[] Split(string value, char delimiter)
        {
            if (value.Trim().Length == 0)
                throw new ConfigurationErrorsException("Value cannot be blank/empty");

            var parts = value.Split(delimiter);
            if (parts.Length == 0)
                throw new ConfigurationErrorsException("Unable to split '" + value + "' using " + delimiter);

            return parts;
        }
    }
}
