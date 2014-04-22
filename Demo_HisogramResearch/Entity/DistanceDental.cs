namespace HisogramResearch.Entity
{
    public class DistanceDental
    {
        public DistanceDental()
        {
            Distance1 = new double[256];
            Distance2 = new double[256];

            Distanceq1 = new double[256];
            Distanceq2 = new double[256];


            Distancel = new double[256];
        }
         public ImageFile ImageFile1 { get; set; }
         public ImageFile ImageFile2 { get; set; }

         public double[] Distance1 { get; set; }
         public double[] Distance2 { get; set; }

         public double[] Distanceq1 { get; set; }
         public double[] Distanceq2 { get; set; }

         public double[] Distancel { get; set; }
    }
}
