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

        //Wczytanie ostanio zapisanego stanu pracy(plik i ustawienia - tylko przygotowanie do pracy, ale nie jej wyniki)
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

        //przycisk Save w menu
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

        //przycisk Load w Menu
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

        //przycisk SaveAs w menu
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
        public float dpScaleVect;
        public int centerXVect;
        public int centerYVect;
        public int sliceWidthVect;
        public int sliceHeightVect;
        public float dpScaleCol;
        public int centerXCol;
        public int centerYCol;
        public int sliceWidthCol;
        public int sliceHeightCol;

        //[NonSerialized]
        public string thisSettingsPath;

        public MainWindowSettings()
        {
            leftXCoord = "";
            leftYCoord = "";
            rightXCoord = "";
            rightYCoord = "";
            stSourceImagePath = "";
            dpScaleVect = 1;
            centerXVect = 0;
            centerYVect = 0;
            dpScaleCol = 1;
            centerXCol = 0;
            centerYCol = 0;
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
            SettingsRegister reg = new SettingsRegister();
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
            this.dpScaleVect = tmp.dpScaleVect;
            this.centerXVect = tmp.centerXVect;
            this.centerYVect = tmp.centerYVect;
            this.dpScaleCol = tmp.dpScaleCol;
            this.centerXCol = tmp.centerXCol;
            this.centerYCol = tmp.centerYCol;
            this.thisSettingsPath = aPath;
            this.testOptions = tmp.testOptions;
            this.sliceWidthVect = tmp.sliceWidthVect;
            this.sliceHeightVect = tmp.sliceHeightVect;
            this.sliceWidthCol = tmp.sliceWidthCol;
            this.sliceHeightCol = tmp.sliceHeightCol;
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

        public bool SimplifyPhase1()
        {
            return StrExists("4");
        }

        public bool SimplifyPhase2()
        {
            return StrExists("5");
        }

        public bool SimplifyPhase3()
        {
            return StrExists("6");
        }
    }
}
