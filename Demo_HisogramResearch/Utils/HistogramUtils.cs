using System;
using System.Drawing;
using HisogramResearch.Entity;

namespace HisogramResearch.Utils
{
    public class HistogramUtils
    {
        public static HistogramResult GetHistogramTB(string file)
        {
            try
            {
                if (!DirectionIO.IsFileImage(file)) return null;
                var picture = new Bitmap(file);
                long[] myHistogram = new long[256];
                long[] redcolor = new long[256];
                long[] cummulativeHis = new long[256];

                for (int i = 0; i < picture.Size.Width; i++)
                    for (int j = 0; j < picture.Size.Height; j++)
                    {
                        System.Drawing.Color c = picture.GetPixel(i, j);

                        long Temp = 0;
                        Temp += c.R;
                        Temp += c.G;
                        Temp += c.B;

                        Temp = (int)Temp / 3;
                        myHistogram[Temp]++;

                        redcolor[c.R]++;
                    }
                cummulativeHis[0] = myHistogram[0];
                for (int i = 1; i < 256; i++)
                {
                    cummulativeHis[i] = cummulativeHis[i - 1] + myHistogram[i];
                }
                return new HistogramResult
                    {
                        Histogram = myHistogram,
                        RedColor = redcolor,
                        CumulativeHistogram = cummulativeHis,
                    };
            }
            catch (Exception)
            {
              
            }
            return null;
        }

         public static HistogramResult GetHistogramTB(Bitmap picture)
        {
            try
            {
                
                long[] myHistogram = new long[256];
                long[] redcolor = new long[256];
                long[] cummulativeHis = new long[256];

                for (int i = 0; i < picture.Size.Width; i++)
                    for (int j = 0; j < picture.Size.Height; j++)
                    {
                        System.Drawing.Color c = picture.GetPixel(i, j);

                        long Temp = 0;
                        Temp += c.R;
                        Temp += c.G;
                        Temp += c.B;

                        Temp = (int)Temp / 3;
                        myHistogram[Temp]++;

                        redcolor[c.R]++;
                    }
                cummulativeHis[0] = myHistogram[0];
                for (int i = 1; i < 256; i++)
                {
                    cummulativeHis[i] = cummulativeHis[i - 1] + myHistogram[i];
                }
                return new HistogramResult
                    {
                        Histogram = myHistogram,
                        RedColor = redcolor,
                        CumulativeHistogram = cummulativeHis,
                    };
            }
            catch (Exception)
            {
              
            }
            return null;
        }
       
        public static long[] GetHistogramTangdan(System.Drawing.Bitmap picture)
        {
            long[] myHistogram = new long[256];

            for (int i = 0; i < picture.Size.Width; i++)
                for (int j = 0; j < picture.Size.Height; j++)
                {
                    System.Drawing.Color c = picture.GetPixel(i, j);

                    long Temp = 0;
                    Temp += c.R;
                    Temp += c.G;
                    Temp += c.B;

                    Temp = (int)Temp / 3;
                    myHistogram[Temp]++;
                }
            long valuergb = 0;
            var acturalhis = new long[256];
            for (int i = 0; i < myHistogram.Length; i++)
            {
                acturalhis[i] = myHistogram[i] + valuergb;
                valuergb = acturalhis[i];
            }

            return acturalhis;
        }


        public static double GetDistance(long[] RedColor1, long[] RedColor2)
        {
            double total = 0;
            for (int i = 0; i < 256; i++)
            {
                var d = Math.Abs(RedColor1[i] - RedColor2[i]);
                total += Math.Pow(d, 2);
            }
            return Math.Sqrt(total);
        }
        /// <summary>
        /// 2x+y+z
        /// </summary>
        /// <param name="color1"></param>
        /// <param name="color2"></param>
        /// <param name="color3"></param>
        /// <returns></returns>
        public static long[] GetMatrix(long[] color1, long[] color2, long[] color3, bool iscolor1 = false)
        {
            long[] total = new long[256];
            if (iscolor1)
            
                for (int i = 0; i < 256; i++)
                {
                    total[i] =  color1[i];
                }
            else 
                for (int i = 0; i < 256; i++)
                {
                    total[i] = 2 * color1[i] + color2[i] + color3[i];
                }
            return total;
        }
        
    }
}
