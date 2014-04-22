namespace Demo_HisogramResearch
{
    public class HistogramUtils
    {
        public static HistogramResult GetHistogramTB(System.Drawing.Bitmap picture)
        {
            long[] myHistogram = new long[256];
            long[] redcolor = new long[256];
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

            return new HistogramResult
                {
                    Histogram = myHistogram,
                    RedColor = redcolor,
                };
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
        
    }
}
