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
                double[] myHistogram = new double[256];
                double[] redcolor = new double[256];
                double[] cummulativeHis = new double[256];
                double[] myHistogram1 = new double[256];
                double[] myHistogram2 = new double[256];
                double[] myHistogram3 = new double[256];
                double[] myHistogram4 = new double[256];

                int imageSize = picture.Size.Width*picture.Size.Height;
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

                        var vung = GetVung(picture.Size.Width, picture.Size.Height, i, j);
                        switch (vung)
                        {
                            case 1:
                                myHistogram1[Temp]++;
                                break;
                            case 2:
                                myHistogram2[Temp]++;
                                break;
                            case 3:
                                myHistogram3[Temp]++;
                                break;
                            case 4:
                                myHistogram4[Temp]++;
                                break;
                        }
                    }
                for (int i = 0; i < 256; i++)
                {
                    var s = (myHistogram[i] / imageSize);
                    myHistogram[i] = s;
                    redcolor[i] = redcolor[i] / imageSize;

                    myHistogram1[i] = myHistogram1[i]*4 / imageSize;
                    myHistogram2[i] = myHistogram2[i] * 4 / imageSize;
                    myHistogram3[i] = myHistogram3[i] * 4 / imageSize;
                    myHistogram4[i] = myHistogram4[i] * 4 / imageSize;

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
                        Histogram1 = myHistogram1,
                        Histogram2 = myHistogram2,
                        Histogram3 = myHistogram3,
                        Histogram4 = myHistogram4
                    };
            }
            catch (Exception)
            {
              
            }
            return null;
        }
        private static int GetVung(int width, int height, int i, int j)
        {
            var ik = i <= (width/2);
            var jk = j <= (height / 2);
            if (ik & jk) return 1;
            if (ik==false & jk == false) return 4;
            if (ik & jk == false) return 3;
            return 2;
        }
         public static HistogramResult GetHistogramTB(Bitmap picture)
        {
            try
            {

                double[] myHistogram = new double[256];
                double[] redcolor = new double[256];
                double[] cummulativeHis = new double[256];
                double[] myHistogram1 = new double[256];
                double[] myHistogram2 = new double[256];
                double[] myHistogram3 = new double[256];
                double[] myHistogram4 = new double[256];
                int imageSize = picture.Width * picture.Height;

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
                        var vung = GetVung(picture.Size.Width,picture.Size.Height,i,j);
                        switch (vung)
                        {
                            case 1:
                                myHistogram1[Temp]++;
                                break;
                            case 2:
                                myHistogram2[Temp]++;
                                break;
                            case 3:
                                myHistogram3[Temp]++;
                                break;
                            case 4:
                                myHistogram4[Temp]++;
                                break;
                        }
                    }

                for (int i = 0; i < 256; i++)
                {
                    var s = (myHistogram[i] / imageSize);
                    myHistogram[i] = s;
                    redcolor[i] = redcolor[i] / imageSize;

                    myHistogram1[i] = myHistogram1[i] * 4 / imageSize;
                    myHistogram2[i] = myHistogram2[i] * 4 / imageSize;
                    myHistogram3[i] = myHistogram3[i] * 4 / imageSize;
                    myHistogram4[i] = myHistogram4[i] * 4 / imageSize;
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
                        Histogram1 = myHistogram1,
                        Histogram2 = myHistogram2,
                        Histogram3 = myHistogram3,
                        Histogram4 = myHistogram4
                    };
            }
            catch (Exception)
            {
              
            }
            return null;
        }

         public static double[] GetHistogramTangdan(System.Drawing.Bitmap picture)
        {
            double[] myHistogram = new double[256];

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
            double valuergb = 0;
            var acturalhis = new double[256];
            for (int i = 0; i < myHistogram.Length; i++)
            {
                acturalhis[i] = myHistogram[i] + valuergb;
                valuergb = acturalhis[i];
            }

            return acturalhis;
        }


        public static double GetDistance(double[] RedColor1, double[] RedColor2)
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
        public static double[] GetMatrix(double[] color1, double[] color2, double[] color3, bool iscolor1 = false)
        {
            double[] total = new double[256];
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
        public static double[] GetMatrix(double[] color1, double[] color2, double[] color3, double[] color4, double[] color5, double[] color6, double[] color7)
        {
            double[] total = new double[256];
            
                for (int i = 0; i < 256; i++)
                {
                    total[i] = 2 * color1[i] + color2[i] + color3[i] + 2*color4[i] + 3*color5[i] + 2*color6[i] + color7[i];
                }
            return total;
        }
        
    }
}
