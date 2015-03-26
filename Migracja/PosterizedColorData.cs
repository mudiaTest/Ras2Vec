using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Migracja
{
    public class PosterizedColorData
    {
        public float gravity = 1;
        public Color garminColor;
        public Color rasterColor;

        public PosterizedColorData()
        {
            garminColor = new Color();
            rasterColor = new Color();
        }


        public float GetColorDistance(Color aColor)
        {
            float srcH = aColor.GetHue();
            float srcS = aColor.GetSaturation();
            float srcV = aColor.GetBrightness();

            float ratH = rasterColor.GetHue();
            float ratS = rasterColor.GetSaturation();
            float ratV = rasterColor.GetBrightness();

            float hueDist = Math.Min( Math.Abs(srcH-ratH), 
                                      (Math.Abs( (Math.Min(ratH, srcH)+360) - Math.Max(ratH, srcH) )) 
                                     )
                
                           /360*100;//procent
            float satDist = Math.Abs(srcS-ratS)*100;
            float valDist = Math.Abs(srcV-ratV)*100;

            float result = (hueDist*hueDist + satDist + valDist * valDist) / gravity;

            return result;
        }
    }
}
