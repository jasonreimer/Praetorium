using Praetorium.Properties;
#if SILVERLIGHT
using System;
#endif
#if !SILVERLIGHT
using System.Configuration;
#endif

namespace Praetorium.Configuration
{
    public static class ConfigReaderExtensions
    {
        public static TSection GetSection<TSection>(this IConfigReader configuration) where TSection : class
        {
            var sectionName = typeof(TSection).Name;
            var sectionNameAttribute = typeof(TSection).GetAttribute<DefaultSectionNameAttribute>(true);

            if (sectionNameAttribute != null)
            {
                sectionName = sectionNameAttribute.Name;
            }
            
            return configuration.GetSection<TSection>(sectionName);
        }

        public static string GetSetting(this IConfigReader configuration, string name, string defaultValue)
        {
            var value = configuration.GetSetting(name);

            return value.ReplaceNullOrWhiteSpace(defaultValue);
        }

        /// <summary>
        /// Gets the setting for the given <paramref name="settingName"/>. If the setting does not exist, an exception is thrown.
        /// </summary>
        /// <param name="container">The container instance.</param>
        /// <param name="settingName">The name of the setting.</param>
        /// <returns>
        /// The setting value.
        /// </returns>
        public static string GetSettingOrThrow(this IConfigReader configuration, string settingName)
        {
            Ensure.ArgumentNotNull(() => configuration);

            var setting = configuration.GetSetting(settingName);

            if (setting.IsNullOrWhiteSpace())
            {
                var message = string.Format(Resources.ConfigurationSettingMissingOrInvalid, settingName);
#if !SILVERLIGHT
                throw new ConfigurationErrorsException(message);
#else
                throw new Exception(message);
#endif
            }

            return setting;
        }
    }
}
