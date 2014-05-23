using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using HisogramResearch.Entity;
using HisogramResearch.Utils;
using ZedGraph;

namespace HisogramResearch
{
    public partial class FrmMainFormFour : Form
    {
       // public Dictionary<string, HistogramResult> DicPicture = new Dictionary<string, HistogramResult>();
        private DistanceDental _distanceDental = new DistanceDental();
        private string FileDataName = "/DataFileFour.json";
        private string FileDistance = "/DataDistanceFour.json";
        private List<ImageFile> _directory = new List<ImageFile>();

        private List<ImageGrid> _listDisPlay = new List<ImageGrid>();


        public FrmMainFormFour()
        {
            InitializeComponent();
            progressBar1.Visible = false;
        }

        private void pictSource_DoubleClick(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            ofd.CheckFileExists = true;
            ofd.CheckPathExists = true;
            var result = ofd.ShowDialog();
            if (result == DialogResult.OK)
            {
                if (pictSource.Image != null)
                    pictSource.Image.Dispose();
                if (ofd.FileName.Length > 0)
                    pictSource.Image = Image.FromFile(ofd.FileName);

                LoadImageSelect();
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.Description = "Chọn thư viện ảnh";
            fbd.ShowNewFolderButton = false;
             
            DialogResult result = fbd.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                 textBox1.Text = fbd.SelectedPath;
            }
            
            //string[] files = Directory.GetFiles(fbd.SelectedPath);
        }
        private void Draw(ZedGraphControl control, PointPairList spl1, string nameGraph)
        {
            if (pictSource.Image == null) return;

           // var histogramResult = HistogramUtils.GetHistogramTB(new Bitmap(pictSource.Image));

          
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
            PointPairList spl3 = new PointPairList();
            for (int i = 0; i < histogramResult.Histogram.Length; i++)
            {
                spl1.Add((double)i, (double)histogramResult.Histogram[i]);
            }

            for (int i = 0; i < histogramResult.RedColor.Length; i++)
            {
                spl2.Add((double)i, (double)histogramResult.RedColor[i]);
            }
            for (int i = 0; i < histogramResult.CumulativeHistogram.Length; i++)
            {
                spl3.Add((double)i, (double)histogramResult.CumulativeHistogram[i]);
            }


            Draw(zedGraphControl1, spl1, "Histogram");
            Draw(zedGraphControl2, spl2, "Red Color");
            Draw(zedGraphControl3, spl3, "Cumulative Color");
        }

        private void LoadImageSelectRult(int index)
        {
            if (pictSource.Image == null) return;
            var histogramResult = _directory.Find(a => a.Index == index);
            if (histogramResult == null) return;
            //var histogramResult = HistogramUtils.GetHistogramTB(new Bitmap(pictureBoxDetail.Image));
            // GraphPane object holds one or more Curve objects (or plots)

            PointPairList spl1 = new PointPairList();
            PointPairList spl2 = new PointPairList();
            PointPairList spl3 = new PointPairList();
            for (int i = 0; i < histogramResult.HistogramResult.Histogram.Length; i++)
            {
                spl1.Add((double)i, (double)histogramResult.HistogramResult.Histogram[i]);
            }

            for (int i = 0; i < histogramResult.HistogramResult.RedColor.Length; i++)
            {
                spl2.Add((double)i, (double)histogramResult.HistogramResult.RedColor[i]);
            }
            for (int i = 0; i < histogramResult.HistogramResult.CumulativeHistogram.Length; i++)
            {
                spl3.Add((double)i, (double)histogramResult.HistogramResult.CumulativeHistogram[i]);
            }


            Draw(zedGraphControl4, spl1, "Histogram");
            Draw(zedGraphControl5, spl2, "Red Color");
            Draw(zedGraphControl6, spl3, "Cumulative Color");
        }

        private void btnPhanTich_Click(object sender, EventArgs e)
        {
            btnPhanTich.Enabled = false;
            LoadImage();
            GetOfffline();
            btnPhanTich.Enabled = true;
        }

        private void button3LoadImage_Click(object sender, EventArgs e)
        {
            button3LoadImage.Enabled = false;
            bool loatdata = false;
            var patheFolder = textBox1.Text + FileDistance;
            var patheFile = textBox1.Text + FileDataName;
            if (DirectionIO.IsExistFile(patheFolder))
            {
                var data = DirectionIO.ReadAllText(patheFolder);
                _distanceDental = JsonUtils.Deserialize<DistanceDental>(data);

            }
            else
                loatdata = true;

            if (DirectionIO.IsExistFile(patheFile))
            {
                var data = DirectionIO.ReadAllText(patheFile);
                _directory = JsonUtils.Deserialize<List<ImageFile>>(data);

            }
            else
                loatdata = true;
            if(loatdata)
            {
                btnPhanTich_Click(btnPhanTich,new EventArgs());
            }
            button3LoadImage.Enabled = true;
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
            if(string.IsNullOrWhiteSpace(textBox1.Text)) return;
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
                            Color = HistogramUtils.GetMatrix(histogramResult.Histogram, histogramResult.RedColor, histogramResult.CumulativeHistogram,
                            histogramResult.Histogram1, histogramResult.Histogram2, histogramResult.Histogram3, histogramResult.Histogram4)
                        });
                    index++;
                    imageList1.Images.Add(index.ToString(), Image.FromFile(file));
                }
                SetProgressBarNext();

                
            }
            
            var data = JsonUtils.Serialize(_directory);
            DirectionIO.WriteAllText(textBox1.Text + FileDataName, data);
        }
        // tinh toan offline
        private void GetOfffline()
        {
            if (_directory.Count > 2)
            {
                _distanceDental.ImageFile1 = _directory[0];
                _distanceDental.ImageFile2 = _directory[1];
            }
            _distanceDental.ListCount = _directory.Count;
            SetProgressBar(0, _directory.Count);
            foreach (var imageFile in _directory)
            {
                var distence1 = HistogramUtils.GetDistance(_distanceDental.ImageFile1.Color, imageFile.Color);
                _distanceDental.Distance1.Add(new IDistance
                    {
                        Distance = distence1,
                        Image = imageFile.Clone() as ImageFile,
                    });
                var distence2 =  HistogramUtils.GetDistance(_distanceDental.ImageFile2.Color, imageFile.Color);
                _distanceDental.Distance2.Add(new IDistance
                {
                    Distance = distence2,
                    Image = imageFile.Clone() as ImageFile,
                });
                SetProgressBarNext();
            }

            var data = JsonUtils.Serialize(_distanceDental);
            DirectionIO.WriteAllText(textBox1.Text + FileDistance, data);
        }
       

        /// <summary>
        /// xu ly online. tim kiem anh.
        /// loc ra danh sách cần xủ lý.
        /// </summary>
        /// <param name="ifind"></param>
        /// <returns></returns>
        private  void FindingImage(ImageFile ifind)
        {
            var distanceq1 = HistogramUtils.GetDistance(ifind.Color, _distanceDental.ImageFile1.Color);
            var distanceq2 = HistogramUtils.GetDistance(ifind.Color, _distanceDental.ImageFile2.Color);
            for (int i = 0; i < _distanceDental.ListCount; i++)
            {
                _distanceDental.Distanceq1[_distanceDental.Distance1[i].Image.Index] = Math.Abs(_distanceDental.Distance1[i].Distance - distanceq1);
                _distanceDental.Distanceq2[_distanceDental.Distance2[i].Image.Index] = Math.Abs(_distanceDental.Distance2[i].Distance - distanceq2);
            }
            foreach (var d in _distanceDental.Distanceq1)
            {
                var indexof = d.Key;
                if (_distanceDental.Distanceq2.ContainsKey(indexof))
                {
                    _distanceDental.Distancel[indexof] = Math.Max(d.Value, _distanceDental.Distanceq2[indexof]);
                }
                else
                {
                    throw new Exception(@"Loi khong tim thay khoang cach tuong ung ");
                }
            }
           
            foreach (var d in _distanceDental.Distancel)
            {

                var imagefile = _directory.Find(a => a.Index == d.Key);
                //imageList1.Images.Add(imagefile.FilePath,new Bitmap(imagefile.FilePath));
                if (imagefile != null)
                    _listDisPlay.Add(new ImageGrid
                        {
                            Distance = d.Value,
                            Image = Image.FromFile(imagefile.FilePath),
                            PathFile = imagefile.FilePath, //.Substring(imagefile.FilePath.LastIndexOf("\\"),imagefile.FilePath.Length - imagefile.FilePath.LastIndexOf("\\")),
                            indexId = imagefile.Index,
                        });

            }
        }

        private void btnTimKiemAnh_Click(object sender, EventArgs e)
        {
            
            _listDisPlay.Clear();
            if (pictSource.Image == null) return;
            btnTimKiemAnh.Enabled = false;
            var histogramResult = HistogramUtils.GetHistogramTB(new Bitmap(pictSource.Image));
            var imagefile = new ImageFile
                {
                    HistogramResult = histogramResult,
                    Color = HistogramUtils.GetMatrix(histogramResult.Histogram, histogramResult.RedColor, histogramResult.CumulativeHistogram,
                    histogramResult.Histogram1, histogramResult.Histogram2, histogramResult.Histogram3, histogramResult.Histogram4)
                };

             FindingImage(imagefile);
             if (_listDisPlay.Count > 0)
             {
                 if (cboKetQua.SelectedIndex == 0)
                 {
                     var listSource = new List<ImageGrid>(); 
                     foreach (var imageGrid in _listDisPlay)
                     {
                         if (imageGrid.Distance > 100) break;
                         listSource.Add(imageGrid);
                     }
                     listSource.Sort(delegate(ImageGrid grid, ImageGrid imageGrid)
                     { return grid.Distance.CompareTo(imageGrid.Distance); });
                     dataGridView1.DataSource = listSource;
                     dataGridView1.Refresh();
                     var count = listSource.Count;
                     lblKetQua.Text = "Kết Quả:" + count.ToString();
                 }
                 cboKetQua.SelectedIndex = 0;
               
             }
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

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            var couter = dataGridView1.SelectedRows.Count;
            if (couter > 0)
            {
                var index = dataGridView1.SelectedRows[0].Index;
                var filepath = dataGridView1.Rows[index].Cells[PathFile.Name].Value.ToString();
                pictureBoxDetail.Image = Image.FromFile(filepath);

                var myindex = dataGridView1.Rows[index].Cells[indexId.Name].Value;

                LoadImageSelectRult(Convert.ToInt32(myindex));
            }
        }
       // private List<ImageGrid> listSource = new List<ImageGrid>(); 
        private void cboKetQua_SelectedIndexChanged(object sender, EventArgs e)
        {
            var listSource = new List<ImageGrid>(); 
            if (cboKetQua.SelectedIndex == 0)
            {
                foreach (var imageGrid in _listDisPlay)
                {
                    if(imageGrid.Distance > 100) break;
                    listSource.Add(imageGrid);
                }
            }
            else
            {
                listSource.AddRange(_listDisPlay);
            }
            listSource.Sort(delegate(ImageGrid grid, ImageGrid imageGrid)
            { return grid.Distance.CompareTo(imageGrid.Distance); });
            dataGridView1.DataSource = listSource;
            dataGridView1.Refresh();

            var count = listSource.Count;
            lblKetQua.Text = "Kết Quả:" + count.ToString();
        }

       
       
    }
}
