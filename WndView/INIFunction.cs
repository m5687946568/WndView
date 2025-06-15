using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using Windows.Foundation.Metadata;
using Windows.Storage;

namespace WndView
{
    internal class INIFunction
    {
        [DllImport("kernel32")]
        public static extern long WritePrivateProfileString(string name, string key, string val, string filePath);

        [DllImport("kernel32")]
        public static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        public static readonly string ProfilePath =
            Path.Combine(ApplicationData.Current.LocalFolder.Path, "settings.ini");
            //Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "WndView", "settings.ini");

        public static string GPPS(string ClassName, string KeyName)
        {
            StringBuilder sb = new StringBuilder(255);
            GetPrivateProfileString(ClassName, KeyName, "", sb, 255, ProfilePath);
            return sb.ToString();
        }

        public static void WPPS(string ClassName, string KeyName, string KeyValue)
        {
            WritePrivateProfileString(ClassName, KeyName, KeyValue, ProfilePath);
        }



    }
}
