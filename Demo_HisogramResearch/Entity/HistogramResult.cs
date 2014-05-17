using System;

namespace HisogramResearch.Entity
{
    public class HistogramResult: IDisposable
    {
        public double[] RedColor { get; set; }
        public double[] Histogram { get; set; }
        public double[] CumulativeHistogram { get; set; }

        public double[] Histogram1 { get; set; }
        public double[] Histogram2 { get; set; }
        public double[] Histogram3 { get; set; }
        public double[] Histogram4 { get; set; }

        public int CompareTo(HistogramResult compare)
        {
            for (int i = 0; i < Histogram.Length; i++)
            {
                if (Histogram[i] != compare.Histogram[i])
                    return 0;

            }
            return 1;
        }

        public void Dispose()
        {
                RedColor = null;
                Histogram = null;
                CumulativeHistogram = null;

                Histogram1 = null;
                Histogram2 = null;
                Histogram3 = null;
                Histogram4 = null;
        }
    }
}
