using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Imaging;
using NUnit.Framework;

namespace _17321_Blok1
{
    public partial class Form1 : Form
    {
        private Bitmap img;
        private string path;
        private List<Bitmap> Undo=new List<Bitmap>();
        private List<Bitmap> Redo = new List<Bitmap>();
        private int topU = -1;
        private int topR = -1;
        private bool win32 = false;
        private bool convo = false;
        private bool image = true;
        private int memory;

        private Color X, Y;
        private Point T;
        private int S;

        public Form1()
        {
            InitializeComponent();
            chart1.Series["Red"].Color = Color.Red;
            chart1.Series["Green"].Color = Color.Green;
            chart1.Series["Blue"].Color = Color.Blue;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if(this.img==null)
            { return; }
            else
            {
                pictureBox1.Image = this.img;
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var fileContent = string.Empty;
            var filePath = string.Empty;
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Title = "Open image";
                openFileDialog.Filter = "Image Files (*.bmp;*.jpg;*.png)|*.BMP;*.JPG;*.PNG";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    filePath = openFileDialog.FileName;
                    this.img = (Bitmap)Bitmap.FromFile(filePath);
                    this.path = filePath;
                    pictureBox1.Image = this.img;

                    this.ClearBuffers();
                }
            }
        }

        public void ClearBuffers()
        {
            this.Redo.Clear();
            this.topR = -1;
            this.Undo.Clear();
            this.topU = -1;
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("Izaberite sliku prvo");
                return;
            }
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.FileName = "Document"; 
            dlg.DefaultExt = ".jpg"; 
            dlg.Filter = "Image Files (*.bmp;*.jpg;*.png)|*.BMP;*.JPG;*.PNG"; 
            ImageFormat format = ImageFormat.Png;
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string ext = System.IO.Path.GetExtension(dlg.FileName);
                switch (ext)
                {
                    case ".bmp":
                        format = ImageFormat.Bmp;
                        break;
                    case ".jpg":
                        format = ImageFormat.Jpeg;
                        break;
                }
                this.img.Save(dlg.FileName, format);
            }
        }

        private void cMYToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("Izaberite sliku prvo");
                return;
            }

            Form2 dlg = new Form2();
            dlg.img = (Bitmap)this.img.Clone();
            dlg.path = this.path;
            dlg.ShowDialog();
            this.img = (Bitmap)dlg.img.Clone();
            this.path = dlg.path;
            this.pictureBox1.Image = this.img;
            this.ClearBuffers();
        }

        private void invertToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("Izaberite sliku prvo");
                return;
            }
            isBufferFull(this.img);
            this.Undo.Add((Bitmap)this.img.Clone());
            this.topU++;
            if (Picture.Invert(this.img))
            {
                pictureBox1.Image = this.img;
                this.Redo.Clear();
                this.topR = -1;
            }
        }

        private void gammaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("Izaberite sliku prvo");
                return;
            }
            GammaInput dlg = new GammaInput();
            dlg.red = dlg.green = dlg.blue = 1;

            if (DialogResult.OK == dlg.ShowDialog())
            {
                isBufferFull(this.img);
                this.Undo.Add((Bitmap)this.img.Clone());
                this.topU++;
                if (Picture.Gamma(this.img, dlg.red, dlg.green, dlg.blue))
                {
                    pictureBox1.Image = this.img;
                    this.Redo.Clear();
                    this.topR = -1;
                }
            }
        }

        private void embossLaplacianToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("Izaberite sliku prvo");
                return;
            }
            isBufferFull(this.img);
            this.Undo.Add((Bitmap)this.img.Clone());
            this.topU++;
            ConvMatrix m = new ConvMatrix();
            m.SetAll(0);
            m.TopMid = m.MidLeft = m.MidRight = m.BottomMid = -1;
            m.Pixel = 4;
            m.Offset = 127;

            if (Picture.embossLaplacian(this.img, m))
            {
                pictureBox1.Image = this.img;
                this.Redo.Clear();
                this.topR = -1;
            }
        }

        private void win32ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("Izaberite sliku prvo");
                return;
            }
            Win32 dlg = new Win32();
            if (DialogResult.OK == dlg.ShowDialog())
            {
                if (dlg.Option == "Win32" && this.win32 ==false)
                {
                    this.win32 = true;
                }
                else if (dlg.Option == "Win64" && this.win32 ==true)
                {
                    this.win32 = false;
                }
            }
        }
        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (topU >= 0)
            {
                this.Redo.Add(this.img);
                this.topR++;
                this.img = (Bitmap)this.Undo[this.topU].Clone();
                this.Undo.RemoveAt(this.topU);
                this.topU--;
            }
            this.pictureBox1.Image = this.img;
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (topR >= 0)
            {
                this.Undo.Add(this.img);
                this.topU++;
                this.img = (Bitmap)this.Redo[this.topR].Clone();
                this.Redo.RemoveAt(this.topR);
                this.topR--;
            }
            this.pictureBox1.Image = this.img; 
        }

        private void memorijaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Memory dlg = new Memory();
            dlg.Broj = this.memory;

            if (DialogResult.OK == dlg.ShowDialog())
            {
                this.memory = dlg.Broj;
                isBufferFull(null);
            }
        }

        private void isBufferFull(Bitmap m)
        {
            int all = 0;
            int counter = 0;
            int sizeOfP = 0;
            if (m != null)
                sizeOfP = (int)(Picture.Memory_of_picture(m, this.path) / (1024 * 1024));
            for (int i = this.topU; i >= 0; i--) //otpozadi dok se ne dodje do el koji krsi velicinu memorije
            {
                int size = (int)(Picture.Memory_of_picture(this.Undo[i],this.path) / (1024 * 1024)); // bytes/1024*1024=mb
                all = all + size;
                if (all + sizeOfP > this.memory)
                {
                    break;
                }
                else counter++;
            }
            if(counter<this.Undo.Count)
            {
                int num = this.topU - counter + 1;
                for(int i=num; i<=this.topU;i++)
                {
                    this.Undo[i - num] = (Bitmap)this.Undo[i].Clone(); //pomeranje
                }
                ClearBuffer(counter);
                this.topU = counter;
            }
        }
        private void ClearBuffer(int counter)
        {
            for(int i=counter;i<=this.topU;i++)
            {
                this.Undo[counter] = null;
            }
        }

        private void konvoluToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("Izaberite sliku prvo");
                return;
            }
            Convolution dlg = new Convolution();
            if (DialogResult.OK == dlg.ShowDialog())
            {
                if (dlg.Option == "Da")
                    this.convo = true;
                else this.convo = false;
            }
        }

        private void randomJitterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("Izaberite sliku prvo");
                return;
            }
            Parametar dlg = new Parametar();
            dlg.nValue = 5;

            if (DialogResult.OK == dlg.ShowDialog())
            {
                isBufferFull(this.img);
                this.Undo.Add((Bitmap)this.img.Clone());
                this.topU++;
                if (Picture.RandomJitter(this.img, (short)dlg.nValue))
                {
                    pictureBox1.Image = this.img;
                    this.Redo.Clear();
                    this.topR = -1;
                }
            }
        }

        private void changeMatrixToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("Izaberite sliku prvo");
                return;
            }
            Form2 dlg = new Form2();
            dlg.img = (Bitmap)this.img.Clone();
            dlg.path = this.path;
            dlg.matrix = true;
            dlg.ShowDialog();
            this.img = dlg.img;
            this.path = dlg.path;
            this.pictureBox1.Image = this.img;
            ClearBuffers();
        }

        private void edgeDetectDifferenceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("Izaberite sliku prvo");
                return;
            }
            Parametar dlg = new Parametar();
            dlg.nValue = 0;

            if (DialogResult.OK == dlg.ShowDialog())
            {
                isBufferFull(this.img);
                this.Undo.Add((Bitmap)this.img.Clone());
                this.topU++;
                if (Picture.EdgeDetectDifference(this.img, (byte)dlg.nValue))
                {
                    pictureBox1.Image = this.img;
                    this.Redo.Clear();
                    this.topR = -1;
                }
            }
        }

        public void ChangeToStandard()
        {
            this.chart1.Visible = false;
            this.pictureBox1.Visible = true;
            this.image = true;
            this.pictureBox1.Image = img;
        }
        public void ChangeToChart()
        {
            this.pictureBox1.Visible = false;
            this.chart1.Visible = true;
            this.image = false;
            this.PrikazChart();
        }

        private void PrikazChart()
        {
            chart1.Series["Red"].Points.Clear();
            chart1.Series["Green"].Points.Clear();
            chart1.Series["Blue"].Points.Clear();
            List<int> red = new List<int>(256), green = new List<int>(256), blue = new List<int>(256);
            for(int i=0;i<256;i++)
            {
                red.Add(0);
                green.Add(0);
                blue.Add(0);
            }
            for (int i = 0; i < this.img.Height; i++)
            {
                for (int j = 0; j < this.img.Width; j++)
                {
                    Color pixel = this.img.GetPixel(j, i);
                    red[pixel.R]++;
                    green[pixel.G]++;
                    blue[pixel.B]++;
                }
            }
            for (int i = 0; i < 256; i++)
            {
                chart1.Series["Red"].Points.AddY(red[i]);
                chart1.Series["Green"].Points.AddY(green[i]);
                chart1.Series["Blue"].Points.AddY(blue[i]);
            }
        }

  
        private void stuckiDitheringToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("Izaberite sliku prvo");
                return;
            }
            isBufferFull(this.img);
            this.Undo.Add((Bitmap)this.img.Clone());
            this.topU++;
            StuckiDithering stucki = new StuckiDithering();
            Size size = this.img.Size;
            Color[] pixelData = Picture.GetPixelsFrom24BitArgbImage(this.img);

            this.img=Picture.ProcessPixels(pixelData, size, stucki, true);
            pictureBox1.Image = this.img;
            this.Redo.Clear();
            this.topR = -1;
        }

        private void changeVisualsToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (this.image == false)
            {
                ChangeToStandard();
            }
            else if (this.image == true)
            {
                ChangeToChart();
            }
        }

        private void orderedDitheringToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("Izaberite sliku prvo");
                return;
            }
            isBufferFull(this.img);
            this.Undo.Add((Bitmap)this.img.Clone());
            this.topU++;
            bool fastMode = true;

            DitherCalculateAndSave(new OrderedDithering(Picture.TrueColorToWebSafeColor, fastMode), this.img);

            pictureBox1.Image = this.img;
            this.Redo.Clear();
            this.topR = -1;
        }

        public void DitherCalculateAndSave(DitheringBase method, Bitmap input)
        {
            Bitmap dithered = method.DoDithering(input);
            this.img = (Bitmap)dithered.Clone();
        }

        private void crossdomainColorizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("Izaberite sliku prvo");
                return;
            }
            HueSaturation dlg = new HueSaturation();

            if (DialogResult.OK == dlg.ShowDialog())
            {
                isBufferFull(this.img);
                this.Undo.Add((Bitmap)this.img.Clone());
                this.topU++;
                if (Picture.CrossDomainColorize(this.img, dlg.Hue, dlg.Saturation))
                {
                    pictureBox1.Image = this.img;
                    this.Redo.Clear();
                    this.topR = -1;
                }
            }
        }

        private void grayScaleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("Izaberite sliku prvo");
                return;
            }

            Form2 dlg = new Form2();
            dlg.img = (Bitmap)this.img.Clone();
            dlg.path = this.path;
            dlg.grayscale = true;
            dlg.ShowDialog();
            this.img = (Bitmap)dlg.img.Clone();
            this.path = dlg.path;
            this.pictureBox1.Image = this.img;
            this.ClearBuffers();
        }

        private void simpleColorizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("Izaberite sliku prvo");
                return;
            }
            isBufferFull(this.img);
            this.Undo.Add((Bitmap)this.img.Clone());
            this.topU++;
            if (Picture.SimpleColorize(this.img))
            {
                pictureBox1.Image = this.img;
                this.Redo.Clear();
                this.topR = -1;
            }
        }

        private void simpleColorizeWithPictureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap pom = new Bitmap(this.path), convert;
            isBufferFull(this.img);
            this.Undo.Add((Bitmap)this.img.Clone());
            this.topU++;
            var fileContent = string.Empty;
            var filePath = string.Empty;
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Title = "Open image";
                openFileDialog.Filter = "Image Files (*.bmp;*.jpg;*.png)|*.BMP;*.JPG;*.PNG";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    filePath = openFileDialog.FileName;
                    pom = (Bitmap)Bitmap.FromFile(filePath);
                    this.Redo.Clear();
                    this.topR = -1;
                }
            }
            convert = (Bitmap)pom.Clone();
            if (Picture.GrayScaleMaxMin(convert))
            {
                Picture.SimpleColorizeWithPicture(this.img, pom, convert);
            }
            this.pictureBox1.Image = this.img;
        }

        private void loadToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Compressed Files(.lzw)|*.lzw;";
            openFileDialog.InitialDirectory = @"C:\\";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                FileStream selectedFile = new FileStream(openFileDialog.FileName, FileMode.Open, FileAccess.Read);
                string path = Compression.Decompress(selectedFile, openFileDialog.FileName);
                var fileInfo = new FileInfo(path);
                byte[] image = File.ReadAllBytes(fileInfo.FullName);
                //byte[] image = Sampling.UpSampling(fileInfo.FullName);

                this.img = Picture.ByteArrayToBitmap(image);
                this.pictureBox1.Image = this.img;
                this.ClearBuffers();
            }
        }

        private void saveToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("Izaberite sliku prvo");
                return;
            }
            Form2 dlg = new Form2();
            dlg.img = (Bitmap)this.img.Clone();
            dlg.path = this.path;
            dlg.sampling = true;
            dlg.ShowDialog();
            this.img = dlg.img;
            this.path = dlg.path;
            this.pictureBox1.Image = this.img;
            ClearBuffers();
        }

        private void kuwaharaFilterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("Izaberite sliku prvo");
                return;
            }
            isBufferFull(this.img);
            this.Undo.Add((Bitmap)this.img.Clone());
            this.topU++;
            this.img = Picture.Kuwahara(this.img, 5);
            pictureBox1.Image = this.img;
            this.Redo.Clear();
            this.topR = -1;
        }

        private void colorUniformityToolStripMenuItem_Click(object sender, EventArgs e)
        {
            colorPicker();
        }

        private void colorPicker()
        {
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("Izaberite sliku prvo");
                return;
            }
            isBufferFull(this.img);
            this.Undo.Add((Bitmap)this.img.Clone());
            this.topU++;
            ColorDialog dlg = new ColorDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                X = dlg.Color;
            }
            this.Redo.Clear();
            this.topR = -1;
        }

        private PointF stretched(Point p0, PictureBox pb)
        {
            if (pb.Image == null) return PointF.Empty;

            float scaleX = 1f * pb.Image.Width / pb.ClientSize.Width;
            float scaleY = 1f * pb.Image.Height / pb.ClientSize.Height;

            return new PointF(p0.X * scaleX, p0.Y * scaleY);
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            T = Point.Round(stretched(e.Location, pictureBox1));
            Y = ((Bitmap)pictureBox1.Image).GetPixel(T.X, T.Y);
            
            if(X!=null || X!=Color.Empty)
            {
                MessageBox.Show("Uzeti vrednost granice srazmerno s bojom da ne bi doslo do stack overflow");
                Parametar dlg = new Parametar();
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    S = dlg.nValue;

                    ((Bitmap)pictureBox1.Image).SetPixel(T.X, T.Y, X);
                    this.Recursion(T);
                    this.pictureBox1.Image = this.img;
                }
            }
        }

        private void Recursion(Point T) 
        {
            try
            {
                if (T.X < 0 || T.Y < 0 || T.X >= pictureBox1.Image.Width || T.Y >= pictureBox1.Image.Height)
                    return;
                Point p = new Point(T.X - 1, T.Y - 1);
                 if (Picture.Sim(((Bitmap)pictureBox1.Image).GetPixel(p.X,p.Y),Y) <= S)
                    {
                        ((Bitmap)pictureBox1.Image).SetPixel(p.X,p.Y, X);
                        Recursion(p);
                    }
                if (Picture.Sim(((Bitmap)pictureBox1.Image).GetPixel(T.X - 1, T.Y + 1), Y) <= S)
                    { 
                        ((Bitmap)pictureBox1.Image).SetPixel(T.X - 1, T.Y + 1, X);
                        p = new Point(T.X - 1, T.Y + 1);
                        Recursion(p);
                    }
                if (Picture.Sim(((Bitmap)pictureBox1.Image).GetPixel(T.X + 1, T.Y - 1), Y) <= S)
                    {
                        ((Bitmap)pictureBox1.Image).SetPixel(T.X + 1, T.Y - 1, X);
                        p = new Point(T.X + 1, T.Y - 1);
                        Recursion(p);
                    }
                if (Picture.Sim(((Bitmap)pictureBox1.Image).GetPixel(T.X + 1, T.Y + 1), Y) <= S)
                    {
                        ((Bitmap)pictureBox1.Image).SetPixel(T.X + 1, T.Y + 1, X);
                        p = new Point(T.X + 1, T.Y + 1);
                        Recursion(p);
                    }
            }
            catch (ArgumentOutOfRangeException)
            {
                return;
            }
            catch (Exception)
            {
                return;
            }
        }
    }
}
