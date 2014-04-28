using System.Collections.Generic;

namespace HisogramResearch.Entity
{
    public class DistanceDental
    {
        public DistanceDental()
        {
            Distance1 = new List<IDistance>();
            Distance2 = new List<IDistance>();

            Distanceq1 = new Dictionary<int, double>();
            Distanceq2 = new Dictionary<int, double>();


            Distancel = new Dictionary<int, double>();
        }
        public int ListCount { get; set; }
         public ImageFile ImageFile1 { get; set; }
         public ImageFile ImageFile2 { get; set; }

         public List<IDistance> Distance1 { get; set; }
         public List<IDistance> Distance2 { get; set; }

         public Dictionary<int, double> Distanceq1 { get; set; }
         public Dictionary<int, double> Distanceq2 { get; set; }

         public Dictionary<int, double> Distancel { get; set; }
    }
}
