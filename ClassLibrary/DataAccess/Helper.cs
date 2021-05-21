using System.Configuration;

namespace ClassLibrary.DataAccess
{
    public static class Helper
    {
        public static string CnnValue(string name)
        {
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }
    }
}
