using System;

namespace HisogramResearch.Entity
{
    public class IDistance:IDisposable
    {

        public double Distance { get; set; }
        public ImageFile Image { get; set; }
        public ImageFile ImageCompare { get; set; }
        public void Dispose()
        {
            if (Image != null)
                Image.Dispose();
            if (ImageCompare != null)
                ImageCompare.Dispose();
        }
    }
}
