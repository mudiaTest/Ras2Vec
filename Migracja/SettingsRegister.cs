using Microsoft.Win32;

namespace Migracja
{
    public class SettingsRegister
    {
        public void SetLastSaveInfo(MainWindowSettings settings)
        {
            RegistryKey regKey = Registry.CurrentUser;
            regKey.OpenSubKey(@"Software/R2V");
            if (regKey == null)
            {
                regKey.CreateSubKey(@"Software/R2V");
                regKey.OpenSubKey(@"Software/R2V");
            }
            regKey.SetValue("lastSave", settings.thisSettingsPath);
        }

        public string GetLastSaveInfo()
        {
            RegistryKey regKey = Registry.CurrentUser;
            regKey.OpenSubKey(@"Software/R2V");
            if (regKey.GetValue("lastSave") != null)
                return regKey.GetValue("lastSave").ToString();
            else
                return "";
        }
    }
}
