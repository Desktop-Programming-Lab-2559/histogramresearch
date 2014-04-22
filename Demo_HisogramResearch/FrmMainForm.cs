using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using HisogramResearch.Entity;
using HisogramResearch.Utils;
using ZedGraph;

namespace HisogramResearch
{
    public partial class FrmMainForm : Form
    {
        public Dictionary<string, HistogramResult> DicPicture = new Dictionary<string, HistogramResult>(); 
        public FrmMainForm()
        {
            InitializeComponent();
            progressBar1.Visible = false;
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

            LoadImageSelect();

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

        private void LoadImageSelect()
        {
            if (pictSource.Image == null) return;

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


            Draw(zedGraphControl1, spl1, "Histogram");
            Draw(zedGraphControl2, spl2, "Red Color");
        }

        private void button3LoadImage_Click(object sender, EventArgs e)
        {
            btnTimKiemAnh.Enabled = false;
            LoadImage();
            GetOfffline();
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

        private DistanceDental _distanceDental= new DistanceDental();
        private string FileDataName = "/DataFile.json";
        private string FileDistance = "/DataDistance.json";
        private List<ImageFile> _directory = new List<ImageFile>(); 
        private void LoadImage()
        {
            string[] files = Directory.GetFiles(textBox1.Text);
            _directory.Clear();
            int index = 0;
            SetProgressBar(0, files.Length);
            foreach (var file in files)
            {
                var histogramResult = HistogramUtils.GetHistogramTB(file);
                if (histogramResult != null)
                {
                    _directory.Add(new ImageFile
                        {
                            FilePath = file,
                            HistogramResult = histogramResult,
                            Index = index,
                            Color = HistogramUtils.GetMatrix(histogramResult.Histogram, histogramResult.RedColor, histogramResult.CumulativeHistogram)
                        });
                    index++;
                    //DicPicture[file] = histogramResult;
                }
                SetProgressBarNext();
            }

            var data = JsonUtils.Serialize(_directory);
            DirectionIO.WriteAllText(textBox1.Text + FileDataName, data);
        }

        private void GetOfffline()
        {
            if (_directory.Count > 2)
            {
                _distanceDental.ImageFile1 = _directory[0];
                _distanceDental.ImageFile2 = _directory[1];
            }
            SetProgressBar(0, _directory.Count);
            foreach (var imageFile in _directory)
            {
                _distanceDental.Distance1[imageFile.Index] = HistogramUtils.GetDistance(
                    _distanceDental.ImageFile1.Color, imageFile.Color);
                _distanceDental.Distance2[imageFile.Index] = HistogramUtils.GetDistance(
                   _distanceDental.ImageFile2.Color, imageFile.Color);
                SetProgressBarNext();
            }

            var data = JsonUtils.Serialize(_directory);
            DirectionIO.WriteAllText(textBox1.Text + FileDistance, data);
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
        private List<int> FindingImage(ImageFile ifind)
        {
            var distanceq1 = HistogramUtils.GetDistance(ifind.Color, _distanceDental.ImageFile1.Color);
            var distanceq2 = HistogramUtils.GetDistance(ifind.Color, _distanceDental.ImageFile2.Color);
            for (int i = 0; i < 256; i++)
            {
                _distanceDental.Distanceq1[i] = Math.Abs(_distanceDental.Distance1[i] - distanceq1);
                _distanceDental.Distanceq1[i] = Math.Abs(_distanceDental.Distance2[i] - distanceq2);
                _distanceDental.Distancel[i] = Math.Max(_distanceDental.Distanceq1[i], _distanceDental.Distanceq2[i]);
            }
        }

        private void btnTimKiemAnh_Click(object sender, EventArgs e)
        {

            if (pictSource.Image == null) return;
            btnTimKiemAnh.Enabled = false;
            var histogramResult = HistogramUtils.GetHistogramTB(new Bitmap(pictSource.Image));
            var imagefile = new ImageFile
                {
                    HistogramResult = histogramResult,
                    Color = HistogramUtils.GetMatrix(histogramResult.Histogram, histogramResult.RedColor, histogramResult.CumulativeHistogram)
                };
            var result = FindingImage(imagefile);
            if (result.Count > 0)
                pictureBox2.Image = Image.FromFile(result[0]);
            btnTimKiemAnh.Enabled = true;
        }


        #region progresbar
        private void SetProgressBar(int start, int end)
        {
            progressBar1.Minimum = start;
            progressBar1.Maximum = end;
            progressBar1.Value = start;
            progressBar1.Visible = true;
        }
        private void SetProgressBarNext()
        {
            progressBar1.Value ++;
            if (progressBar1.Value >= progressBar1.Maximum)
                progressBar1.Visible = false;
        }
        #endregion
    }
}
