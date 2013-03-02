namespace Praetorium.Configuration
{
    public interface IConfigReader
    {
        TSection GetSection<TSection>(string sectionName) where TSection : class;

        string GetSetting(string name);
    }
}
