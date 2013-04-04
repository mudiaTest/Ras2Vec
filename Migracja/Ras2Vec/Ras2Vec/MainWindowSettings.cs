using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Xml.Serialization;
using System.IO;

namespace Ras2Vec
{
    [Serializable]
    public class MainWindowSettings
    {
        public String stSourceImagePath;
        public String leftXCoord;
        public String leftYCoord;
        public String rightXCoord;
        public String rightYCoord;
        public float dpScale;
        public int centerX;
        public int centerY;
        [NonSerialized]
        public String thisSettinsPath;
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
            thisSettinsPath = "";
        }

        public void Save(String aPath = "")
        {
            XmlSerializer xmlEngine = new XmlSerializer(typeof(MainWindowSettings));
            FileStream file;
            if (aPath != "") 
            {
                file = new FileStream(aPath, FileMode.OpenOrCreate);
                thisSettinsPath = aPath;
            }
            else
                file = new FileStream(thisSettinsPath, FileMode.OpenOrCreate);
            xmlEngine.Serialize(file, this);
            file.Close();
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
            this.thisSettinsPath = aPath;
        }
    }
}
