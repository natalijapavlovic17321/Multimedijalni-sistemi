using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _17321_Blok1
{
    public partial class Form2 : Form
    {
        public Bitmap img;
        public Bitmap C;
        public Bitmap M;
        public Bitmap Y;
        public Bitmap K;
        public byte[] c;
        public byte[] m;
        public byte[] y;
        public Bitmap CMYK;
        public string path;
        private List<Bitmap> Redo = new List<Bitmap>();
        private List<Bitmap> Undo = new List<Bitmap>();
        private int topU = -1;
        private int topR = -1;
        private bool win32 = false;
        private bool convo = false;
        public bool matrix=false;
        public bool CMY = true;
        public bool sampling = false;
        public bool grayscale = false;

        private int memory=10; //10mb

        public Form2()
        {
            InitializeComponent();
            chart1.Series["Red"].Color = Color.Red;
            chart1.Series["Green"].Color = Color.Green;
            chart1.Series["Blue"].Color = Color.Blue;
            chart2.Series["Cyan"].Color = Color.Cyan;
            chart3.Series["Magenta"].Color = Color.Magenta;
            chart4.Series["Yellow"].Color = Color.Yellow;
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
            this.PrikazKanalskihSlika();
        }

        public void ClearBuffers()
        {
            this.Redo.Clear();
            this.topR = -1;
            this.Undo.Clear();
            this.topU = -1;
        }

        public void PrikazKanalskihSlika()
        {
            Bitmap C= (Bitmap)this.img.Clone(), M= (Bitmap)this.img.Clone(), Y= (Bitmap)this.img.Clone(), K = (Bitmap)this.img.Clone();
            Bitmap CMYK1 = (Bitmap)this.img.Clone();
            float vc, vm, vy, vk;
            Color c, d;
            int x, y;
            for (y = 0; y < this.img.Height; y++)
            {
                for (x = 0; x < this.img.Width; x++)
                {
                    c = this.img.GetPixel(x, y);
                    vc = 255.0f - c.R;
                    vm = 255.0f - c.G;
                    vy = 255.0f - c.B;
                    if ((vc == 255.0f) && (vm == 255.0f) && (vy == 255.0f))
                    {
                        vc = 0.0f;
                        vm = 0.0f;
                        vy = 0.0f;
                        vk = 255.0f;
                    }
                    else
                    {
                        if (vc < vm )
                        {
                            vk = vc;
                        }
                        else
                        {
                            vk = vm;
                        }
                        if (vk > vy)
                        {
                            vk = vy;
                        }
                        vc = 255.0f * (vc - vk) / (255.0f - vk);
                        vm = 255.0f * (vm - vk) / (255.0f - vk);
                        vy = 255.0f * (vy - vk) / (255.0f - vk);
                    }
                    d = Color.FromArgb(0, (int)vc, (int)vc);
                    C.SetPixel(x, y, d);
                    d = Color.FromArgb((int)vm, 0, (int)vm);
                    M.SetPixel(x, y, d);
                    d = Color.FromArgb((int)vy, (int)vy, 0);
                    Y.SetPixel(x, y, d);
                    d = Color.FromArgb((int)vk, (int)vk, (int)vk);
                    K.SetPixel(x, y, d);
                    d = Color.FromArgb((int)vk, (int)vc, (int)vm, (int)vy);
                    CMYK1.SetPixel(x, y, d);
                }
            }
            if (C != null && M != null && Y != null)
            {
                this.pictureBox2.Image = C;
                this.C = (Bitmap)C.Clone();
                this.pictureBox3.Image = M;
                this.M = (Bitmap)M.Clone();
                this.pictureBox4.Image = Y;
                this.Y = (Bitmap)Y.Clone();
                this.K = (Bitmap)K.Clone();
                this.CMYK = (Bitmap)CMYK1.Clone();
            }
        }

        public void PrikazChart()
        {
            chart1.Series["Red"].Points.Clear();
            chart1.Series["Green"].Points.Clear();
            chart1.Series["Blue"].Points.Clear();
            chart2.Series["Cyan"].Points.Clear();
            chart3.Series["Magenta"].Points.Clear();
            chart4.Series["Yellow"].Points.Clear();
            List<int> red = new List<int>(256), green = new List<int>(256), blue = new List<int>(256);
            List<int> cyan = new List<int>(256), magenta = new List<int>(256), yellow = new List<int>(256);
            for (int i = 0; i < 256; i++)
            {
                red.Add(0);
                green.Add(0);
                blue.Add(0);
                cyan.Add(0);
                magenta.Add(0);
                yellow.Add(0);
            }
            for (int i = 0; i < this.img.Height; i++)
            {
                for (int j = 0; j < this.img.Width; j++)
                {
                    Color pixel = this.img.GetPixel(j, i);
                    red[pixel.R]++;
                    green[pixel.G]++;
                    blue[pixel.B]++;
                    Color pixelC = this.C.GetPixel(j, i);
                    cyan[pixelC.G]++;
                    Color pixelM = this.M.GetPixel(j, i);
                    magenta[pixelM.R]++;
                    Color pixelY = this.Y.GetPixel(j, i);
                    yellow[pixelY.G]++;
                }
            }
            for (int i = 0; i < 256; i++)
            {
                chart1.Series["Red"].Points.AddY(red[i]);
                chart1.Series["Green"].Points.AddY(green[i]);
                chart1.Series["Blue"].Points.AddY(blue[i]);
                chart2.Series["Cyan"].Points.AddY(cyan[i]);
                chart3.Series["Magenta"].Points.AddY(magenta[i]);
                chart4.Series["Yellow"].Points.AddY(yellow[i]);
            }
        }

        public void PrikazMatricnihSlika()
        {
            Emboss3x3 e3 = new Emboss3x3();
            pictureBox2.Image = CT.ConvolutionFilter<Emboss3x3>(this.img, e3);
            Emboss5x5 e5 = new Emboss5x5();
            pictureBox3.Image=CT.ConvolutionFilter<Emboss5x5>(this.img, e5);
            Emboss7x7 e7 = new Emboss7x7();
            pictureBox4.Image = CT.ConvolutionFilter<Emboss7x7>(this.img, e7);
            this.Redo.Clear();
            this.topR = -1;
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

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            if (this.img != null)
            {
                this.pictureBox1.Image = this.img;
                if (matrix == false && grayscale ==false && sampling==false)
                    this.PrikazKanalskihSlika();
                else if(grayscale==false && sampling==false)
                {
                    this.PrikazMatricnihSlika();
                    this.matrix = false;
                }
                else if(sampling==false)
                {
                    this.PrikazGrayScale();
                    this.grayscale = false;
                }
                else
                {
                    this.PrikazKanalskihSlika();
                    this.PrikazDownsampling();
                    this.button1.Visible = true;
                    this.sampling = false;
                }
            }
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
            this.PrikazKanalskihSlika();
        }

        private void gammaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(pictureBox1.Image == null)
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
                this.PrikazKanalskihSlika();
            }
        }
        private void isBufferFull(Bitmap m)
        {
            int all = 0;
            int counter = 0;
            int sizeOfP = 0;
            if (m != null)
                sizeOfP = (int)(Picture.Memory_of_picture(m,this.path) / (1024 * 1024));
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
            if (counter < this.Undo.Count)
            {
                int num = this.topU - counter + 1;
                for (int i = num; i <= this.topU; i++)
                {
                    this.Undo[i - num] = (Bitmap)this.Undo[i].Clone(); //pomeranje
                }
                ClearBuffer(counter);
                this.topU = counter;
            }
        }
        private void ClearBuffer(int counter)
        {
            for (int i = counter; i <= this.topU; i++)
            {
                this.Undo[counter] = null;
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
            m.SetAll(-1);
            m.TopMid = m.MidLeft = m.MidRight = m.BottomMid = 0;
            m.Pixel = 4;
            m.Offset = 127;

            if (Picture.embossLaplacian(this.img, m))
            {
                pictureBox1.Image = this.img;
                this.Redo.Clear();
                this.topR = -1;
            }
            this.PrikazKanalskihSlika();
        }

        private void memorijaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Memory dlg = new Memory
            {
                Broj = this.memory
            };

            if (DialogResult.OK == dlg.ShowDialog())
            {
                this.memory = dlg.Broj;
                isBufferFull(null);
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
            this.PrikazKanalskihSlika();
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
            this.PrikazKanalskihSlika();
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
                if (dlg.Option == "Win32" && this.win32==false)
                {
                    this.win32 = true;
                    System.Diagnostics.Process.Start(Path.GetDirectoryName(Application.ExecutablePath)
                                     + @"\Your32BitApplicationLauncher.exe");
                    MessageBox.Show("32");
                }
                else if (dlg.Option == "Win64" && this.win32 ==true)
                {
                    this.win32 = false;
                    System.Diagnostics.Process.Start(Path.GetDirectoryName(Application.ExecutablePath)
                                     + @"\Your64BitApplicationLauncher.exe");
                    MessageBox.Show("64");
                }
            }
        }

        private void konvoluToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("Izaberite sliku prvo");
                return;
            }
            Win32 dlg = new Win32();
            if (DialogResult.OK == dlg.ShowDialog())
            {
                if (dlg.Option == "Da")
                    this.convo = true;
                else this.convo = false;
            }
        }

        private void randomJitteToolStripMenuItem_Click(object sender, EventArgs e)
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
            this.PrikazKanalskihSlika();
        }

        private void changeMatrixToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.PrikazMatricnihSlika();
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
            this.PrikazKanalskihSlika();
        }

        public void ChangeToCMY()
        {
            this.chart1.Visible = false;
            this.chart2.Visible = false;
            this.chart3.Visible = false;
            this.chart4.Visible = false;
            this.pictureBox1.Visible = true;
            this.pictureBox2.Visible = true;
            this.pictureBox3.Visible = true;
            this.pictureBox4.Visible = true;
            this.CMY = true;
            this.PrikazKanalskihSlika();
        }
        public void ChangeToChart()
        {
            this.pictureBox1.Visible = false;
            this.pictureBox2.Visible = false;
            this.pictureBox3.Visible = false;
            this.pictureBox4.Visible = false;
            this.chart1.Visible = true;
            this.chart2.Visible = true;
            this.chart3.Visible = true;
            this.chart4.Visible = true;
            this.CMY = false;
            this.PrikazChart();
        }

        private void changeVisualsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.CMY == false)
            {
                ChangeToCMY();
            }
            else if (this.CMY == true)
            {
               ChangeToChart();
            }
        }

        private void changeHistogramToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("Izaberite sliku prvo");
                return;
            }
            if (this.CMY == true)
            {
                MessageBox.Show("Izaberite chart prikaz prvo");
                return;
            }
            Histogram dlg = new Histogram();

            if (DialogResult.OK == dlg.ShowDialog())
            {
                isBufferFull(this.img);
                this.Undo.Add((Bitmap)this.img.Clone());
                this.topU++;
                if (Picture.changeHistogram(C, dlg.minC,dlg.maxC, M, dlg.minM, dlg.maxM, Y, dlg.minY, dlg.maxY, this.img, K))
                {
                    pictureBox1.Image = this.img;
                    this.Redo.Clear();
                    this.topR = -1;
                }
            }
            this.PrikazChart();
        }

        private void grayscaleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("Izaberite sliku prvo");
                return;
            }
            isBufferFull(this.img);
            this.Undo.Add((Bitmap)this.img.Clone());
            this.topU++;
            this.PrikazGrayScale();
            this.Redo.Clear();
            this.topR = -1;
            this.PrikazChart();
        }

        private void PrikazGrayScale()
        {
            Bitmap g1 = new Bitmap((Bitmap)this.img.Clone()), g2 = new Bitmap((Bitmap)this.img.Clone()), g3 = new Bitmap((Bitmap)this.img.Clone());
            Picture.GrayScaleAritmeticki(g1);
            this.pictureBox2.Image = g1;
            Picture.GrayScaleMax(g2);
            this.pictureBox3.Image = g2;
            Picture.GrayScaleMaxMin(g3);
            this.pictureBox4.Image = g3;
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

            this.img = Picture.ProcessPixels(pixelData, size, stucki, true);
            pictureBox1.Image = this.img;
            this.Redo.Clear();
            this.topR = -1;

            this.PrikazKanalskihSlika();
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
            string filenameBase = "dither";

            DitherCalculateAndSave(new OrderedDithering(Picture.TrueColorToWebSafeColor, fastMode), this.img, filenameBase);

            pictureBox1.Image = this.img;
            this.Redo.Clear();
            this.topR = -1;
            this.PrikazKanalskihSlika();
        }

        public void DitherCalculateAndSave(DitheringBase method, Bitmap input, string filenameWithoutExtension)
        {
            Bitmap dithered = method.DoDithering(input);
            this.img = dithered;
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
            this.PrikazKanalskihSlika();
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
            this.PrikazKanalskihSlika();
        }

        private void simpleColorizeWithPictureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap pom=new Bitmap(""),convert;
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
            if(Picture.GrayScaleMaxMin(convert))
            {
                Picture.SimpleColorizeWithPicture(this.img, pom, convert);
            }

            this.pictureBox1.Image = this.img;
            this.PrikazKanalskihSlika();
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
                byte[]image= File.ReadAllBytes(fileInfo.FullName);
                //byte[] image = Sampling.UpSampling(fileInfo.FullName);

                this.img = Picture.ByteArrayToBitmap(image);
                this.pictureBox1.Image = this.img;
                this.PrikazKanalskihSlika();
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
            isBufferFull(this.img);
            this.Undo.Add((Bitmap)this.img.Clone());
            this.topU++;
            this.button1.Visible = true;
            this.PrikazDownsampling();
            this.Redo.Clear();
            this.topR = -1;
        }

        public void PrikazDownsampling()
        {
            Bitmap C1 = (Bitmap)this.img.Clone(), M1 = (Bitmap)this.img.Clone(), Y1 = (Bitmap)this.img.Clone();

            this.c = Sampling.Downsampling(CMYK, "3");
            this.m = Sampling.Downsampling(CMYK, "1");
            this.y = Sampling.Downsampling(CMYK, "2");
            C1 = Picture.ByteArrayToBitmap(c);
            M1 = Picture.ByteArrayToBitmap(m);
            Y1 = Picture.ByteArrayToBitmap(y);
            //pictureBox1.Image = CMYK;
            if (C != null && M != null && Y != null)
            {
                this.pictureBox2.Image = C1;
                this.C = (Bitmap)C1.Clone();
                this.pictureBox3.Image = M1;
                this.M = (Bitmap)M1.Clone();
                this.pictureBox4.Image = Y1;
                this.Y = (Bitmap)Y1.Clone();
            }
        }

        private void kuwaharaFiltetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("Izaberite sliku prvo");
                return;
            }
            isBufferFull(this.img);
            this.Undo.Add((Bitmap)this.img.Clone());
            this.topU++;
            this.img = Picture.Kuwahara(this.img,5);
            pictureBox1.Image = this.img;
            this.Redo.Clear();
            this.topR = -1;
            this.PrikazKanalskihSlika();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DownSampling dlg = new DownSampling();

            if (DialogResult.OK == dlg.ShowDialog())
            {
                string option = dlg.Option;
                if(option=="1")
                {
                    this.CompressAndSave(this.c);
                }
                else if(option=="2")
                {
                    this.CompressAndSave(this.m);
                }
                else
                {
                    this.CompressAndSave(this.y);
                }
                this.button1.Visible = false;
            }
        }

        private void CompressAndSave(byte[] m)
        {
            SaveFileDialog dlg= new SaveFileDialog();
            dlg.FileName = "Document";
            dlg.DefaultExt = ".jpg";
            dlg.Filter = "Image Files (*.bmp;*.jpg;*.png)|*.BMP;*.JPG;*.PNG";
            ImageFormat format = ImageFormat.Png;
            if (dlg.ShowDialog() == DialogResult.OK)
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
                File.WriteAllBytes(dlg.FileName, m);
                FileStream selectedFile = new FileStream(dlg.FileName, FileMode.Open, FileAccess.ReadWrite);
                string path = Compression.Compress(selectedFile, dlg.FileName, dlg.FileName);
            }
        }

        private void colorUniformityToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Izaberite opciju color Uniformity na formi 1 za optimalan rezultat");
            this.Close();
            //zbog vece slike;
        }

        private void cMYToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.PrikazKanalskihSlika();
        }
    }
}
