using System;

namespace HisogramResearch.Entity
{
    public class ImageFile : IDisposable,ICloneable
    {
        public ImageFile()
        {
            Color = new double[256];
        }
        public int Index { get; set; }
        public string FilePath { get; set; }
        public HistogramResult HistogramResult { get; set; }
        public double[] Color { get; set; }
        public void Dispose()
        {
            Color = null;
            if(HistogramResult !=null)
                HistogramResult.Dispose();
        }

        public object Clone()
        {
            return new ImageFile
                {
                    Index = Index,
                    FilePath= FilePath,
                    Color = Color,
                    HistogramResult = new HistogramResult
                        {
                            Histogram = HistogramResult.Histogram,
                            CumulativeHistogram = HistogramResult.CumulativeHistogram,
                            RedColor = HistogramResult.RedColor
                        }
                };
        }
    }
}
