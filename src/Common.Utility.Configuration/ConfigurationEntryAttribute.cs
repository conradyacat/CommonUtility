using System;

namespace Common.Utility.Configuration
{
    /// <summary>
    /// Attribute to mark a configuration class property that it has a corresponding configuration file entry with the same name.
    /// Example: [prefix]_[propertyname]
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public sealed class ConfigurationEntryAttribute : Attribute
    {
        public ConfigurationEntryAttribute()
        {
            IsRequired = true;
        }

        /// <summary>
        /// Gets or sets the method name of the custom parsing to be executed after a property is loaded.
        /// </summary>
        public string CustomLoadMethod { get; set; }

        /// <summary>
        /// Gets or sets the value whether the property is required/mandatory. The default value is true.
        /// </summary>
        public bool IsRequired { get; set; }

        /// <summary>
        /// Gets or sets the default value of the property.
        /// </summary>
        public object DefaultValue { get; set; }

        /// <summary>
        /// Gets or sets the delimiter to use to split the values. This is applicable only if the property type is a collection.
        /// </summary>
        public char Delimiter { get; set; }

        /// <summary>
        /// Gets or sets the additional error message in case parsing fails for the configuration entry
        /// </summary>
        public string ErrorMessage { get; set; }
    }
}
