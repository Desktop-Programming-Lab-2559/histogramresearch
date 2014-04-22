using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using ZedGraph;

namespace Demo_HisogramResearch
{
    public partial class FrmMainForm : Form
    {
        public Dictionary<string, HistogramResult> DicPicture = new Dictionary<string, HistogramResult>(); 
        public FrmMainForm()
        {
            InitializeComponent();
        }

        private void pictSource_DoubleClick(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            ofd.CheckFileExists = true;
            ofd.CheckPathExists = true;

            ofd.ShowDialog();

            if (pictSource.Image != null)
                pictSource.Image.Dispose();
            if (ofd.FileName.Length > 0)
                pictSource.Image = Image.FromFile(ofd.FileName);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            DialogResult result = fbd.ShowDialog();

            //string[] files = Directory.GetFiles(fbd.SelectedPath);


            textBox1.Text = fbd.SelectedPath;
        }
        private void Draw(ZedGraphControl control, PointPairList spl1, string nameGraph)
        {
            if (pictSource.Image == null) return;

            var histogramResult = HistogramUtils.GetHistogramTB(new Bitmap(pictSource.Image));

          
            control.GraphPane.CurveList.Clear();

            // GraphPane object holds one or more Curve objects (or plots)
            GraphPane myPane = control.GraphPane;

            // Add cruves to myPane object

            BarItem myCurve1 = myPane.AddBar(nameGraph, spl1, Color.Black);
           
            myPane.Chart.Fill = new Fill(Color.White,
              Color.FromArgb(255, 255, 255), 45.0F);

            myPane.XAxis.Scale.Min = 0;
            myPane.XAxis.Scale.Max = 255;
            myPane.XAxis.Scale.MinorStep = 1;
            myPane.XAxis.Scale.MajorStep = 1;

            myPane.Title.Text = "";

            //I add all three functions just to be sure it refeshes the plot.   
            control.AxisChange();
            control.Invalidate();
            control.Refresh();
        }
        private void button3LoadImage_Click(object sender, EventArgs e)
        {
            if(pictSource.Image == null) return;

            var histogramResult = HistogramUtils.GetHistogramTB(new Bitmap(pictSource.Image));

            // GraphPane object holds one or more Curve objects (or plots)
           
            PointPairList spl1 = new PointPairList();
            PointPairList spl2 = new PointPairList();
            for (int i = 0; i < histogramResult.Histogram.Length; i++)
            {
                spl1.Add((double)i, (double)histogramResult.Histogram[i]);
            }

            for (int i = 0; i < histogramResult.RedColor.Length; i++)
            {
                spl2.Add((double)i, (double)histogramResult.RedColor[i]);
            }


            Draw(zedGraphControl1,spl1,"Histogram");
            Draw(zedGraphControl2,spl2,"Red Color");

            btnTimKiemAnh.Enabled = false;
             
            LoadImage();
            btnTimKiemAnh.Enabled = true;
        }
        public void CreateChart()
        {
            GraphPane myPane = zedGraphControl1.GraphPane;

            // Set the title and axis labels
            myPane.Title.Text = "Vertical Bars with Value Labels Above Each Bar";
            myPane.XAxis.Title.Text = "Position Number";
            myPane.YAxis.Title.Text = "Some Random Thing";

            PointPairList list = new PointPairList();
            PointPairList list2 = new PointPairList();
            PointPairList list3 = new PointPairList();
            Random rand = new Random();

            // Generate random data for three curves
            for (int i = 0; i < 5; i++)
            {
                double x = (double)i;
                double y = rand.NextDouble() * 1000;
                double y2 = rand.NextDouble() * 1000;
                double y3 = rand.NextDouble() * 1000;
                list.Add(x, y);
                list2.Add(x, y2);
                list3.Add(x, y3);
            }

            // create the curves
            BarItem myCurve = myPane.AddBar("curve 1", list, Color.Blue);
            BarItem myCurve2 = myPane.AddBar("curve 2", list2, Color.Red);
            BarItem myCurve3 = myPane.AddBar("curve 3", list3, Color.Green);

            // Fill the axis background with a color gradient
            myPane.Chart.Fill = new Fill(Color.White,
               Color.FromArgb(255, 255, 166), 45.0F);

        
            // expand the range of the Y axis slightly to accommodate the labels
            myPane.YAxis.Scale.Max += myPane.YAxis.Scale.MajorStep;

            // Create TextObj's to provide labels for each bar
            BarItem.CreateBarLabels(myPane, false, "f0");

        }
        private void LoadImage()
        {
            string[] files = Directory.GetFiles(textBox1.Text);

            foreach (var file in files)
            {
                try
                {
                    var histogramResult = HistogramUtils.GetHistogramTB(new Bitmap(file));
                    DicPicture[file] = histogramResult;
                }
                catch (Exception)
                {}
            }
        }
        private List<string> FindingImage(HistogramResult ifind)
        {
            var list = new List<string>();
            foreach (var histogram in DicPicture)
            {
                 if(histogram.Value.CompareTo(ifind) == 1)
                     list.Add(histogram.Key);
            }
            return list;
        }

        private void btnTimKiemAnh_Click(object sender, EventArgs e)
        {

            if (pictSource.Image == null) return;
            btnTimKiemAnh.Enabled = false;
            var histogramResult = HistogramUtils.GetHistogramTB(new Bitmap(pictSource.Image));
            var result = FindingImage(histogramResult);
            if (result.Count > 0)
                pictureBox2.Image = Image.FromFile(result[0]);
            btnTimKiemAnh.Enabled = true;
        }
    }
}
