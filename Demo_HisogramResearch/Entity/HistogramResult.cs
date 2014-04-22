namespace HisogramResearch.Entity
{
    public class HistogramResult
    {
        public long[] RedColor { get; set; }
        public long[] Histogram { get; set; }
        public long[] CumulativeHistogram { get; set; }


        public int CompareTo(HistogramResult compare)
        {
            for (int i = 0; i < Histogram.Length; i++)
            {
                if (Histogram[i] != compare.Histogram[i])
                    return 0;

            }
            return 1;
        }
    }
}
