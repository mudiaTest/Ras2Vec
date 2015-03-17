using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.Linq;
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
                FillFlpColorsFromSettings(windowSettings);
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

        private void FillFlpColorsFromSettings(MainWindowSettings aSettings)
        {
            PosterizedColorData pcd;
            foreach (KeyValuePair<int, PosterizedColorData> pair in aSettings.dictColorData)
            {
                pcd = pair.Value;
                ColorPanel cp = new ColorPanel(pcd);
                flpColors.Controls.Add(cp);
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
        [NonSerialized]
        internal Dictionary<int, PosterizedColorData> dictColorData;

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
            dictColorData = new Dictionary<int, PosterizedColorData>();
        }

        public void Save(String aPath = "")
        {



            XElement mainElement = new XElement("MainWindowSettings",
                new XElement("leftXCoord", leftXCoord),
                new XElement("leftYCoord", leftYCoord),
                new XElement("rightXCoord", rightXCoord),
                new XElement("rightYCoord", rightYCoord),
                new XElement("stSourceImagePath", stSourceImagePath),
                new XElement("dpScaleVect", dpScaleVect.ToString()),
                new XElement("centerXVect", centerXVect.ToString()),
                new XElement("centerYVect", centerYVect.ToString()),
                new XElement("dpScaleCol", dpScaleCol.ToString()),
                new XElement("centerXCol", centerXCol.ToString()),
                new XElement("centerYCol", centerYCol.ToString()),
                new XElement("testOptions", testOptions),
                new XElement("sliceWidthVect", sliceWidthVect.ToString()),
                new XElement("sliceHeightVect", sliceHeightVect.ToString()),
                new XElement("sliceWidthCol", sliceWidthCol.ToString()),
                new XElement("sliceHeightCol", sliceHeightCol.ToString())
            );

            PosterizedColorData pcd;
            XElement postElement = new XElement("posterization","");
            foreach (KeyValuePair<int, PosterizedColorData> pair in dictColorData)
            {
                pcd = pair.Value;
                XElement colorElement = new XElement("color",
                    new XElement("gravity", pcd.gravity.ToString()),
                    new XElement("garminColorR", pcd.garminColor.R.ToString()),
                    new XElement("garminColorG", pcd.garminColor.G.ToString()),
                    new XElement("garminColorB", pcd.garminColor.B.ToString()),
                    new XElement("rasterColorR", pcd.rasterColor.R.ToString()),
                    new XElement("rasterColorG", pcd.rasterColor.G.ToString()),
                    new XElement("rasterColorB", pcd.rasterColor.B.ToString())
                );
                postElement.Add(colorElement);
            }
            mainElement.Add(postElement);
            XDocument xDoc = new XDocument( new XDeclaration("1.0", "UTF-16", null), mainElement);
            FileStream file;
            if (aPath != "") 
            {
                file = new FileStream(aPath, FileMode.OpenOrCreate);
                thisSettingsPath = aPath;
            }
            else
                file = new FileStream(thisSettingsPath, FileMode.Truncate);
            xDoc.Save(file);

           /* XmlSerializer xmlEngine = new XmlSerializer(typeof(MainWindowSettings));
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
            reg.SetLastSaveInfo(this);*/
        }

        public void Load(String aPath)
        {
           /* XmlSerializer xmlMainEngine = new XmlSerializer(typeof(MainWindowSettings));
            FileStream file = new FileStream(aPath, FileMode.Open);
            MainWindowSettings tmp = (MainWindowSettings)xmlMainEngine.Deserialize(file);
            file.Close();*/

            XElement settings = XElement.Load(aPath);

            // ... XNames.
            XName XleftXCoord = XName.Get("leftXCoord", "");
            XName XleftYCoord = XName.Get("leftYCoord", "");
            XName XrightXCoord = XName.Get("rightXCoord", "");
            XName XrightYCoord = XName.Get("rightYCoord", "");
            XName XstSourceImagePath = XName.Get("stSourceImagePath", "");
            XName XdpScaleVect = XName.Get("dpScaleVect", "");
            XName XcenterXVect = XName.Get("centerXVect", "");
            XName XcenterYVect = XName.Get("centerYVect", "");
            XName XdpScaleCol = XName.Get("dpScaleCol", "");
            XName XcenterXCol = XName.Get("centerXCol", "");
            XName XcenterYCol = XName.Get("centerYCol", "");
            XName XtestOptions = XName.Get("testOptions", "");
            XName XsliceWidthVect = XName.Get("sliceWidthVect", "");
            XName XsliceHeightVect = XName.Get("sliceHeightVect", "");
            XName XsliceWidthCol = XName.Get("sliceWidthCol", "");
            XName XsliceHeightCol = XName.Get("sliceHeightCol", "");

            XName XPosterization = XName.Get("posterization");
                XName XColor = XName.Get("color");
                    XName Xgravity = XName.Get("gravity");                    
                    XName XgarminColorR = XName.Get("garminColorR");
                    XName XgarminColorG = XName.Get("garminColorG");
                    XName XgarminColorB = XName.Get("garminColorB");
                    XName XrasterColorR = XName.Get("rasterColorR");
                    XName XrasterColorG = XName.Get("rasterColorG");
                    XName XrasterColorB = XName.Get("rasterColorB");

            // ... Loop over url elements.
            // ... Then access each loc element.
            //foreach (var urlElement in settings.Elements(url))
            //{
                //var locElement = urlElement.Element(loc);
                //Console.WriteLine(locElement.Value);
                //Console.WriteLine(urlElement.Value);

            this.leftXCoord = settings.Elements(XleftXCoord).First().Value;
            this.leftYCoord = settings.Elements(XleftYCoord).First().Value;
            this.rightXCoord = settings.Elements(XrightXCoord).First().Value;
            this.rightYCoord = settings.Elements(XrightYCoord).First().Value;
            this.stSourceImagePath = settings.Elements(XstSourceImagePath).First().Value;
            this.dpScaleVect = float.Parse(settings.Elements(XdpScaleVect).First().Value);
            this.centerXVect = int.Parse(settings.Elements(XcenterXVect).First().Value);
            this.centerYVect = int.Parse(settings.Elements(XcenterYVect).First().Value);
            this.dpScaleCol = float.Parse(settings.Elements(XdpScaleCol).First().Value);
            this.centerXCol = int.Parse(settings.Elements(XcenterXCol).First().Value);
            this.centerYCol = int.Parse(settings.Elements(XcenterYCol).First().Value);
            this.thisSettingsPath = aPath;
            this.testOptions = settings.Elements(XtestOptions).First().Value;
            this.sliceWidthVect = int.Parse(settings.Elements(XsliceWidthVect).First().Value);
            this.sliceHeightVect = int.Parse(settings.Elements(XsliceHeightVect).First().Value);
            this.sliceWidthCol = int.Parse(settings.Elements(XsliceWidthCol).First().Value);
            this.sliceHeightCol = int.Parse(settings.Elements(XsliceHeightCol).First().Value);

            var postElement = settings.Element(XPosterization);             
            foreach (var colorElement in postElement.Elements(XColor))
            {
                //if (colorElement.Element(XredMin) != null)
                //{ 
                    PosterizedColorData pcd = new PosterizedColorData();
                    pcd.gravity = int.Parse(colorElement.Element(Xgravity).Value);
                    pcd.garminColor = Color.FromArgb(255,
                                                        int.Parse(colorElement.Element(XgarminColorR).Value),
                                                        int.Parse(colorElement.Element(XgarminColorG).Value), 
                                                        int.Parse(colorElement.Element(XgarminColorB).Value)
                                                        );
                    pcd.rasterColor = Color.FromArgb(255,
                                                        int.Parse(colorElement.Element(XrasterColorR).Value),
                                                        int.Parse(colorElement.Element(XrasterColorG).Value),
                                                        int.Parse(colorElement.Element(XrasterColorB).Value)
                                                        );
                    this.dictColorData.Add(ExtDictionary.NextKey(this.dictColorData), pcd);
                //}
            }
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
