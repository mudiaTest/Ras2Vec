using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Xml.Serialization;
using System.IO;
using Microsoft.Win32;
using System.Windows.Forms;

namespace Migracja
{
    public partial class MainWindow : Form
    {
        private void RefreshLastSaveButton()
        {
            if (lastSavePath != "")
            {
                loadLastSaveToolStripMenuItem.Visible = true;
                loadLastSaveToolStripMenuItem.Text = "Load " + lastSavePath;
            }
            else
                loadLastSaveToolStripMenuItem.Visible = false;
        }
        private void LoadLastSave()
        {
            if (File.Exists(lastSavePath))
            { 
                windowSettings.Load(lastSavePath);
                if (File.Exists(windowSettings.stSourceImagePath))
                    PrepareSourceImage(windowSettings.stSourceImagePath);
                SettingsToScr(windowSettings);
            }
        }
        private void MenuSave(object sender, EventArgs e)
        {
            if (windowSettings.thisSettingsPath != "")
            {
                windowSettings.Save();
                lastSavePath = windowSettings.thisSettingsPath;
            }
            else
                saveAsToolStripMenuItem_Click(sender, e);
            RefreshLastSaveButton();
        }
        private void MenuLoad(object sender, EventArgs e)
        {
            DialogResult result = loadDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                windowSettings.Load(loadDialog.FileName);
                PrepareSourceImage(windowSettings.stSourceImagePath);
                SettingsToScr(windowSettings);
            }


        }
        private void MenuSaveAs(object sender, EventArgs e)
        {
            DialogResult result = saveDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                windowSettings.Save(saveDialog.FileName);
            }
        }
    }

    [Serializable]
    public class MainWindowSettings
    {
        public String stSourceImagePath;
        public String leftXCoord;
        public String leftYCoord;
        public String rightXCoord;
        public String rightYCoord;
        public String testOptions;
        public float dpScale;
        public int centerX;
        public int centerY;
        public int sliceWidth;
        public int sliceHeight;

        //[NonSerialized]
        public string thisSettingsPath;

        public MainWindowSettings()
        {
            leftXCoord = "";
            leftYCoord = "";
            rightXCoord = "";
            rightYCoord = "";
            stSourceImagePath = "";
            dpScale = 0;
            centerX = 0;
            centerY = 0;
            thisSettingsPath = "";
            testOptions = "";
        }

        public void Save(String aPath = "")
        {
            XmlSerializer xmlEngine = new XmlSerializer(typeof(MainWindowSettings));
            FileStream file;
            if (aPath != "") 
            {
                file = new FileStream(aPath, FileMode.OpenOrCreate);
                thisSettingsPath = aPath;
            }
            else
                file = new FileStream(thisSettingsPath, FileMode.Truncate);
            xmlEngine.Serialize(file, this);
            file.Close();
            //zapamiętanie ostatniego save'u w rejestrze
            MainWindowRegister reg = new MainWindowRegister();
            reg.SetLastSaveInfo(this);
        }

        public void Load(String aPath)
        {
            XmlSerializer xmlEngine = new XmlSerializer(typeof(MainWindowSettings));
            FileStream file = new FileStream(aPath, FileMode.Open);
            MainWindowSettings tmp = (MainWindowSettings)xmlEngine.Deserialize(file);
            file.Close();
            this.leftXCoord = tmp.leftXCoord;
            this.leftYCoord = tmp.leftYCoord;
            this.rightXCoord = tmp.rightXCoord;
            this.rightYCoord = tmp.rightYCoord;
            this.stSourceImagePath = tmp.stSourceImagePath;
            this.dpScale = tmp.dpScale;
            this.centerX = tmp.centerX;
            this.centerY = tmp.centerY;
            this.thisSettingsPath = aPath;
            this.testOptions = tmp.testOptions;
            this.sliceWidth = tmp.sliceWidth;
            this.sliceHeight = tmp.sliceHeight;
        }

        public string[] GetCheckegTestOptionsList()
        {
            return testOptions.Split(',');
        }

        private bool StrExists(string aText)
        {
            int idx = Array.FindIndex(
                GetCheckegTestOptionsList(),
                delegate(string s) { return s.Equals(aText); }
                );
            return idx != -1;
        }

        public bool Polygons()
        {
            return StrExists("0");
        }

        public bool Edges()
        {
            return StrExists("1");
        }

        public bool TestColor()
        {
            return StrExists("2");
        }

        public bool ShowSimplifiedEdge()
        {
            return StrExists("3");
        }
    }

    class MainWindowRegister
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
