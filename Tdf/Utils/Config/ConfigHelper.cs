using System.Configuration;

namespace Tdf.Utils.Config
{
    public class ConfigHelper
    {
        public static string GetValue(string strName, string defvalue)
        {
            string val = ConfigurationManager.AppSettings[strName];
            if (string.IsNullOrEmpty(val))
            {
                val = defvalue;
            }
            return val;
        }
    }
}
