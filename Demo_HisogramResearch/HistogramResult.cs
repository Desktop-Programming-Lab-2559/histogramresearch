using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo_HisogramResearch
{
    public class HistogramResult
    {
        public long[] RedColor { get; set; }
        public long[] Histogram { get; set; }


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
