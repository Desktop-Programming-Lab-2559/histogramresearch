namespace HisogramResearch.Entity
{
    public class ImageFile
    {
        public ImageFile()
        {
            Color= new long[256];
        }
        public int Index { get; set; }
        public string FilePath { get; set; }
        public HistogramResult HistogramResult { get; set; }
        public long[] Color { get; set; }
    }
}
