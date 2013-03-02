using System.Configuration;

namespace Praetorium.Configuration
{
    public class ConfigurationManagerReader : IConfigReader
    {
        public TSection GetSection<TSection>(string sectionName) where TSection : class
        {
            return ConfigurationManager.GetSection(sectionName) as TSection;
        }

        public string GetSetting(string name)
        {
            return ConfigurationManager.AppSettings[name];
        }
    }
}
