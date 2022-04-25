using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.IO;
using SixLabors.ImageSharp.ColorSpaces;

namespace _17321_Blok1
{
    public class Picture
    {
        private static Color[] colors = new Color[256];

        public struct Rgb
        {
            private byte _r;
            private byte _g;
            private byte _b;
            public Rgb(byte r, byte g, byte b)
            {
                this._r = r;
                this._g = g;
                this._b = b;
            }

            public byte R
            {
                get { return this._r; }
                set { this._r = value; }
            }

            public byte G
            {
                get { return this._g; }
                set { this._g = value; }
            }

            public byte B
            {
                get { return this._b; }
                set { this._b = value; }
            }

            public bool Equals(Rgb rgb)
            {
                return (this.R == rgb.R) && (this.G == rgb.G) && (this.B == rgb.B);
            }
        }

        public struct Hsv
        {
            private double _h;
            private double _s;
            private double _v;

            public Hsv(double h, double s, double v)
            {
                this._h = h;
                this._s = s;
                this._v = v;
            }

            public double H
            {
                get { return this._h; }
                set { this._h = value; }
            }

            public double S
            {
                get { return this._s; }
                set { this._s = value; }
            }

            public double V
            {
                get { return this._v; }
                set { this._v = value; }
            }

            public bool Equals(Hsv hsv)
            {
                return (this.H == hsv.H) && (this.S == hsv.S) && (this.V == hsv.V);
            }
        }
        public static void convertToCMYK(Bitmap slika)
        {
            for (int i = 0; i < slika.Width; i++)
            {
                for (int j = 0; j < slika.Height; j++)
                {
                    System.Drawing.Color clr = slika.GetPixel(i, j);
                    byte b = clr.B;
                    byte g = clr.G;
                    byte r = clr.R;
                    double modifiedR, modifiedG, modifiedB, c, m, y, k;

                    modifiedR = r / 255.0;
                    modifiedG = g / 255.0;
                    modifiedB = b / 255.0;

                    k = 1 - new List<double>() { modifiedR, modifiedG, modifiedB }.Max();
                    if (k == 1)
                    {
                        c = m = y = 0;
                    }
                    else
                    {
                        c = (1 - modifiedR - k) / (1 - k);
                        m = (1 - modifiedG - k) / (1 - k);
                        y = (1 - modifiedB - k) / (1 - k);
                    }

                    slika.SetPixel(i, j, System.Drawing.Color.FromArgb((byte)(Math.Round(k) * 100),(byte)(Math.Round(c) * 100), (byte)(Math.Round(m) * 100), (byte)(Math.Round(y) * 100)));
                }
            }
        }
        public static bool Invert(Bitmap slika)
        {
            for (int i = 0; i < slika.Width; i++)
            {
                for (int j = 0; j < slika.Height; j++)
                {
                    System.Drawing.Color c = slika.GetPixel(i, j);
                    slika.SetPixel(i, j, System.Drawing.Color.FromArgb(255 - c.R, 255 - c.G, 255 - c.B));
                }
            }
            return true;
        }
        public static bool Gamma(Bitmap slika, double red, double green, double blue)
        {
            if (red < .2 || red > 5) return false;
            if (green < .2 || green > 5) return false;
            if (blue < .2 || blue > 5) return false;

            byte[] redGamma = new byte[256];
            byte[] greenGamma = new byte[256];
            byte[] blueGamma = new byte[256];

            for (int i = 0; i < 256; ++i)
            {
                redGamma[i] = (byte)Math.Min(255, (int)((255.0 * Math.Pow(i / 255.0, 1.0 / red)) + 0.5));
                greenGamma[i] = (byte)Math.Min(255, (int)((255.0 * Math.Pow(i / 255.0, 1.0 / green)) + 0.5));
                blueGamma[i] = (byte)Math.Min(255, (int)((255.0 * Math.Pow(i / 255.0, 1.0 / blue)) + 0.5));
            }

            BitmapData bmData = slika.LockBits(new Rectangle(0, 0, slika.Width, slika.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            int stride = bmData.Stride;
            System.IntPtr Scan0 = bmData.Scan0;

            unsafe
            {
                byte* p = (byte*)(void*)Scan0;

                int nOffset = stride - slika.Width * 3;

                for (int y = 0; y < slika.Height; ++y)
                {
                    for (int x = 0; x < slika.Width; ++x)
                    {
                        p[2] = redGamma[p[2]];
                        p[1] = greenGamma[p[1]];
                        p[0] = blueGamma[p[0]];

                        p += 3;
                    }
                    p += nOffset;
                }
            }
            slika.UnlockBits(bmData);
            return true;
        }
        public static bool embossLaplacian(Bitmap b, ConvMatrix m)
        {
            if (0 == m.Factor) return false;

            Bitmap bSrc = (Bitmap)b.Clone();

            BitmapData bmData = b.LockBits(new Rectangle(0, 0, b.Width, b.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            BitmapData bmSrc = bSrc.LockBits(new Rectangle(0, 0, bSrc.Width, bSrc.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            int stride = bmData.Stride;
            int stride2 = stride * 2;
            System.IntPtr Scan0 = bmData.Scan0;
            System.IntPtr SrcScan0 = bmSrc.Scan0;

            unsafe
            {
                byte* p = (byte*)(void*)Scan0;
                byte* pSrc = (byte*)(void*)SrcScan0;

                int nOffset = stride - b.Width * 3;
                int nWidth = b.Width - 2;
                int nHeight = b.Height - 2;

                int nPixel;

                for (int y = 0; y < nHeight; ++y)
                {
                    for (int x = 0; x < nWidth; ++x)
                    {
                        nPixel = ((((pSrc[2] * m.TopLeft) + (pSrc[5] * m.TopMid) + (pSrc[8] * m.TopRight) +
                            (pSrc[2 + stride] * m.MidLeft) + (pSrc[5 + stride] * m.Pixel) + (pSrc[8 + stride] * m.MidRight) +
                            (pSrc[2 + stride2] * m.BottomLeft) + (pSrc[5 + stride2] * m.BottomMid) + (pSrc[8 + stride2] * m.BottomRight)) / m.Factor) + m.Offset);

                        if (nPixel < 0) nPixel = 0;
                        if (nPixel > 255) nPixel = 255;

                        p[5 + stride] = (byte)nPixel;

                        nPixel = ((((pSrc[1] * m.TopLeft) + (pSrc[4] * m.TopMid) + (pSrc[7] * m.TopRight) +
                            (pSrc[1 + stride] * m.MidLeft) + (pSrc[4 + stride] * m.Pixel) + (pSrc[7 + stride] * m.MidRight) +
                            (pSrc[1 + stride2] * m.BottomLeft) + (pSrc[4 + stride2] * m.BottomMid) + (pSrc[7 + stride2] * m.BottomRight)) / m.Factor) + m.Offset);

                        if (nPixel < 0) nPixel = 0;
                        if (nPixel > 255) nPixel = 255;

                        p[4 + stride] = (byte)nPixel;

                        nPixel = ((((pSrc[0] * m.TopLeft) + (pSrc[3] * m.TopMid) + (pSrc[6] * m.TopRight) +
                            (pSrc[0 + stride] * m.MidLeft) + (pSrc[3 + stride] * m.Pixel) + (pSrc[6 + stride] * m.MidRight) +
                            (pSrc[0 + stride2] * m.BottomLeft) + (pSrc[3 + stride2] * m.BottomMid) + (pSrc[6 + stride2] * m.BottomRight)) / m.Factor) + m.Offset);

                        if (nPixel < 0) nPixel = 0;
                        if (nPixel > 255) nPixel = 255;

                        p[3 + stride] = (byte)nPixel;

                        p += 3;
                        pSrc += 3;
                    }
                    p += nOffset;
                    pSrc += nOffset;
                }
            }

            b.UnlockBits(bmData);
            bSrc.UnlockBits(bmSrc);

            return true;
        }

        public static bool embossLaplacian2(Bitmap slika, ConvMatrix m)
        {
            if (0 == m.Factor)
                return false;
            Bitmap bSrc = (Bitmap)slika.Clone();
            BitmapData bmData = slika.LockBits(new Rectangle(0, 0, slika.Width, slika.Height),
                                ImageLockMode.ReadWrite,
                                PixelFormat.Format24bppRgb);
            BitmapData bmSrc = bSrc.LockBits(new Rectangle(0, 0, bSrc.Width, bSrc.Height),
                               ImageLockMode.ReadWrite,
                               PixelFormat.Format24bppRgb);
            int stride = bmData.Stride;
            int stride2 = stride * 2;

            System.IntPtr Scan0 = bmData.Scan0;
            System.IntPtr SrcScan0 = bmSrc.Scan0;

            unsafe
            {
                byte* p = (byte*)(void*)Scan0;
                byte* pSrc = (byte*)(void*)SrcScan0;
                int nOffset = stride - slika.Width * 3;
                int nWidth = slika.Width - 2;
                int nHeight = slika.Height - 2;

                int nPixel;

                for (int y = 0; y < nHeight; ++y)
                {
                    for (int x = 0; x < nWidth; ++x)
                    {
                        nPixel = ((((pSrc[2] * m.TopLeft) +
                            (pSrc[5] * m.TopMid) +
                            (pSrc[8] * m.TopRight) +
                            (pSrc[2 + stride] * m.MidLeft) +
                            (pSrc[5 + stride] * m.Pixel) +
                            (pSrc[8 + stride] * m.MidRight) +
                            (pSrc[2 + stride2] * m.BottomLeft) +
                            (pSrc[5 + stride2] * m.BottomMid) +
                            (pSrc[8 + stride2] * m.BottomRight))
                            / m.Factor) + m.Offset);

                        if (nPixel < 0) nPixel = 0;
                        if (nPixel > 255) nPixel = 255;
                        p[5 + stride] = (byte)nPixel;

                        nPixel = ((((pSrc[1] * m.TopLeft) +
                            (pSrc[4] * m.TopMid) +
                            (pSrc[7] * m.TopRight) +
                            (pSrc[1 + stride] * m.MidLeft) +
                            (pSrc[4 + stride] * m.Pixel) +
                            (pSrc[7 + stride] * m.MidRight) +
                            (pSrc[1 + stride2] * m.BottomLeft) +
                            (pSrc[4 + stride2] * m.BottomMid) +
                            (pSrc[7 + stride2] * m.BottomRight))
                            / m.Factor) + m.Offset);

                        if (nPixel < 0) nPixel = 0;
                        if (nPixel > 255) nPixel = 255;
                        p[4 + stride] = (byte)nPixel;

                        nPixel = ((((pSrc[0] * m.TopLeft) +
                                       (pSrc[3] * m.TopMid) +
                                       (pSrc[6] * m.TopRight) +
                                       (pSrc[0 + stride] * m.MidLeft) +
                                       (pSrc[3 + stride] * m.Pixel) +
                                       (pSrc[6 + stride] * m.MidRight) +
                                       (pSrc[0 + stride2] * m.BottomLeft) +
                                       (pSrc[3 + stride2] * m.BottomMid) +
                                       (pSrc[6 + stride2] * m.BottomRight))
                            / m.Factor) + m.Offset);

                        if (nPixel < 0) nPixel = 0;
                        if (nPixel > 255) nPixel = 255;
                        p[3 + stride] = (byte)nPixel;

                        p += 3;
                        pSrc += 3;
                    }

                    p += nOffset;
                    pSrc += nOffset;
                }
            }
            slika.UnlockBits(bmData);
            bSrc.UnlockBits(bmSrc);
            return true;
        }

        public static long Memory_of_picture(Bitmap m, string path)
        {
            long jpegByteSize;
            using (var ms = new MemoryStream((int)(new System.IO.FileInfo(path)).Length))
            {
                m.Save(ms, ImageFormat.Jpeg);
                jpegByteSize = (int)ms.Length;
            }
            return jpegByteSize;
        }

        public static bool EdgeDetectDifference(Bitmap b, byte nThreshold)
        {
            Bitmap b2 = (Bitmap)b.Clone();

            BitmapData bmData = b.LockBits(new Rectangle(0, 0, b.Width, b.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            BitmapData bmData2 = b2.LockBits(new Rectangle(0, 0, b.Width, b.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            int stride = bmData.Stride;
            System.IntPtr Scan0 = bmData.Scan0;
            System.IntPtr Scan02 = bmData2.Scan0;

            unsafe
            {
                byte* p = (byte*)(void*)Scan0;
                byte* p2 = (byte*)(void*)Scan02;

                int nOffset = stride - b.Width * 3;
                int nWidth = b.Width * 3;

                int nPixel = 0, nPixelMax = 0;

                p += stride;
                p2 += stride;

                for (int y = 1; y < b.Height - 1; ++y)
                {
                    p += 3;
                    p2 += 3;

                    for (int x = 3; x < nWidth - 3; ++x)
                    {
                        nPixelMax = Math.Abs((p2 - stride + 3)[0] - (p2 + stride - 3)[0]);
                        nPixel = Math.Abs((p2 + stride + 3)[0] - (p2 - stride - 3)[0]);
                        if (nPixel > nPixelMax) nPixelMax = nPixel;

                        nPixel = Math.Abs((p2 - stride)[0] - (p2 + stride)[0]);
                        if (nPixel > nPixelMax) nPixelMax = nPixel;

                        nPixel = Math.Abs((p2 + 3)[0] - (p2 - 3)[0]);
                        if (nPixel > nPixelMax) nPixelMax = nPixel;

                        if (nPixelMax < nThreshold) nPixelMax = 0;

                        p[0] = (byte)nPixelMax;

                        ++p;
                        ++p2;
                    }

                    p += 3 + nOffset;
                    p2 += 3 + nOffset;
                }
            }

            b.UnlockBits(bmData);
            b2.UnlockBits(bmData2);

            return true;
        }

        public static bool RandomJitter(Bitmap b, short nDegree)
        {
            Point[,] ptRandJitter = new Point[b.Width, b.Height];

            int nWidth = b.Width;
            int nHeight = b.Height;

            int newX, newY;

            short nHalf = (short)Math.Floor((double)nDegree / 2);
            Random rnd = new Random();

            for (int x = 0; x < nWidth; ++x)
                for (int y = 0; y < nHeight; ++y)
                {
                    newX = rnd.Next(nDegree) - nHalf;

                    if (x + newX > 0 && x + newX < nWidth)
                        ptRandJitter[x, y].X = newX;
                    else
                        ptRandJitter[x, y].X = 0;

                    newY = rnd.Next(nDegree) - nHalf;

                    if (y + newY > 0 && y + newY < nWidth)
                        ptRandJitter[x, y].Y = newY;
                    else
                        ptRandJitter[x, y].Y = 0;
                }
            OffsetFilter(b, ptRandJitter);

            return true;
        }

        public static bool OffsetFilter(Bitmap b, Point[,] offset)
        {
            Bitmap bSrc = (Bitmap)b.Clone();

            BitmapData bmData = b.LockBits(new Rectangle(0, 0, b.Width, b.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            BitmapData bmSrc = bSrc.LockBits(new Rectangle(0, 0, bSrc.Width, bSrc.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            int scanline = bmData.Stride;

            System.IntPtr Scan0 = bmData.Scan0;
            System.IntPtr SrcScan0 = bmSrc.Scan0;

            unsafe
            {
                byte* p = (byte*)(void*)Scan0;
                byte* pSrc = (byte*)(void*)SrcScan0;

                int nOffset = bmData.Stride - b.Width * 3;
                int nWidth = b.Width;
                int nHeight = b.Height;

                int xOffset, yOffset;

                for (int y = 0; y < nHeight; ++y)
                {
                    for (int x = 0; x < nWidth; ++x)
                    {
                        xOffset = offset[x, y].X;
                        yOffset = offset[x, y].Y;

                        if (y + yOffset >= 0 && y + yOffset < nHeight && x + xOffset >= 0 && x + xOffset < nWidth)
                        {
                            p[0] = pSrc[((y + yOffset) * scanline) + ((x + xOffset) * 3)];
                            p[1] = pSrc[((y + yOffset) * scanline) + ((x + xOffset) * 3) + 1];
                            p[2] = pSrc[((y + yOffset) * scanline) + ((x + xOffset) * 3) + 2];
                        }
                        p += 3;
                    }
                    p += nOffset;
                }
            }
            b.UnlockBits(bmData);
            bSrc.UnlockBits(bmSrc);

            return true;
        }
        public static Color[] GetPixelsFrom24BitArgbImage(Bitmap b)
        {
            int width = b.Width;
            int height = b.Height; ;

            Color[] results = new Color[width * height];

            Bitmap bSrc = (Bitmap)b.Clone();
            BitmapData bmData = b.LockBits(new Rectangle(0, 0, b.Width, b.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            BitmapData bmSrc = bSrc.LockBits(new Rectangle(0, 0, bSrc.Width, bSrc.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            int scanline = bmData.Stride;

            System.IntPtr Scan0 = bmData.Scan0;
            System.IntPtr SrcScan0 = bmSrc.Scan0;

            unsafe
            {
                byte* p = (byte*)(void*)Scan0;
                byte* pSrc = (byte*)(void*)SrcScan0;

                int nOffset = bmData.Stride - b.Width * 3;

                for (int y = 0; y < height; ++y)
                {
                    for (int x = 0; x < width; ++x)
                    {
                        results[y * width + x] = Color.FromArgb(p[0], p[1], p[2]);
                        p += 3;
                    }
                    p += nOffset;
                }
            }

            b.UnlockBits(bmData);
            bSrc.UnlockBits(bmSrc);

            return results;
        }

        public static Bitmap ProcessPixels(Color[] pixelData, Size size, StuckiDithering sierra, bool monochrome)
        {
            Color black = Color.FromArgb(0, 0, 0);
            Color white = Color.FromArgb(255, 255, 255);
            int trashold = 127;

            for (int row = 0; row < size.Height; row++)
            {
                for (int col = 0; col < size.Width; col++)
                {
                    int index;
                    Color current;
                    Color transformed;

                    index = row * size.Width + col;
                    current = pixelData[index];

                    if (monochrome)
                    {
                        byte gray;
                        gray = (byte)(0.299 * current.R + 0.587 * current.G + 0.114 * current.B);
                        transformed = gray < trashold ? black : white;
                        pixelData[index] = transformed;
                    }
                    else
                    {
                        transformed = current;
                    }

                    sierra.DoDiffuse(pixelData, current, transformed, col, row, size.Width, size.Height);
                }
            }

            return ToBitmap(pixelData, size);
        }

        public static Bitmap ToBitmap(Color[] data, Size size)
        {
            int height;
            int width;

            Bitmap result;

            result = new Bitmap(size.Width, size.Height, PixelFormat.Format24bppRgb);
            width = result.Width;
            height = result.Height;

            for (int y = 0; y < height; ++y)
            {
                for (int x = 0; x < width; ++x)
                {
                    Color rgb = data[y * width + x];
                    Color color = Color.FromArgb(rgb.R, rgb.G, rgb.B);
                    result.SetPixel(x, y, color);
                }
            }
            return result;
        }

        public static Color TrueColorToWebSafeColor(Color inputColor)
        {
            Color returnColor = Color.FromArgb((byte)Math.Round(inputColor.R / 51.0) * 51,
                                                (byte)Math.Round(inputColor.G / 51.0) * 51,
                                                (byte)Math.Round(inputColor.B / 51.0) * 51);
            return returnColor;
        }

        public static bool GrayScaleAritmeticki2(Bitmap b)
        {
            BitmapData bmData = b.LockBits(new Rectangle(0, 0, b.Width, b.Height), ImageLockMode.ReadWrite, b.PixelFormat);// PixelFormat.Format24bppRgb);

            int stride = bmData.Stride;
            System.IntPtr Scan0 = bmData.Scan0;

            unsafe
            {
                byte* p = (byte*)(void*)Scan0;

                int nOffset = stride - b.Width * 3;

                byte red, green, blue;

                for (int y = 0; y < b.Height; ++y)
                {
                    for (int x = 0; x < b.Width; ++x)
                    {
                        blue = p[0];
                        green = p[1];
                        red = p[2];

                        p[0] = p[1] = p[2] = (byte)((red + green + blue) / 3);

                        p += 3;
                    }
                    p += nOffset;
                }
            }
            b.UnlockBits(bmData);
            return true;
        }
        public static bool GrayScaleAritmeticki(Bitmap slika)
        {
            for (int i = 0; i < slika.Width; i++)
            {
                for (int j = 0; j < slika.Height; j++)
                {
                    System.Drawing.Color clr = slika.GetPixel(i, j);
                    byte b = clr.B;
                    byte g = clr.G;
                    byte r = clr.R;
                    byte gray= (byte)((r + g + b) / 3);

                    slika.SetPixel(i, j, System.Drawing.Color.FromArgb((byte)gray, (byte)gray, (byte)gray));
                }
            }
            return true;
        }
        public static bool GrayScaleMax(Bitmap slika)
        {
            for (int i = 0; i < slika.Width; i++)
            {
                for (int j = 0; j < slika.Height; j++)
                {
                    System.Drawing.Color clr = slika.GetPixel(i, j);
                    byte b = clr.B;
                    byte g = clr.G;
                    byte r = clr.R;
                    byte gray = (byte)Math.Max(r, Math.Max(g, b));

                    slika.SetPixel(i, j, System.Drawing.Color.FromArgb((byte)gray, (byte)gray, (byte)gray));
                }
            }
            return true;
        }

        public static bool GrayScaleMaxMin(Bitmap slika)
        {
            for (int i = 0; i < slika.Width; i++)
            {
                for (int j = 0; j < slika.Height; j++)
                {
                    System.Drawing.Color clr = slika.GetPixel(i, j);
                    byte b = clr.B;
                    byte g = clr.G;
                    byte r = clr.R;
                    byte gray = (byte)((Math.Max(r, Math.Max(g, b)) + Math.Min(r, Math.Min(g, b))) / 2);

                    slika.SetPixel(i, j, System.Drawing.Color.FromArgb((byte)gray, (byte)gray, (byte)gray));
                }
            }
            return true;
        }

        public static bool changeHistogram(Bitmap C, int minC, int maxC, Bitmap M, int minM, int maxM, Bitmap Y, int minY, int maxY,Bitmap img, Bitmap key)
        {
            int mostMinC = 0, mostMaxC = 0, mostMinM = 0, mostMaxM = 0, mostMinY = 0, mostMaxY = 0;
            int indexMinC = 0, indexMaxC = 0, indexMinM = 0, indexMaxM = 0, indexMinY = 0, indexMaxY = 0;
            List<int> cyan = new List<int>(255), magenta = new List<int>(255), yellow = new List<int>(255);
            for (int i = 0; i < 256; i++)
            {
                cyan.Add(0);
                magenta.Add(0);
                yellow.Add(0);
            }
            //posto su iste sirine i visine moze kroz jednu
            for (int i = 0; i < C.Height; i++)
            {
                for (int j = 0; j < C.Width; j++)
                {
                    Color pixel = C.GetPixel(j, i);
                    if (pixel.G >= minC && pixel.G <= maxC)
                        cyan[pixel.G]++;
                    Color pixelM = M.GetPixel(j, i);
                    if (pixelM.R >= minM && pixelM.R <= maxM)
                        magenta[pixelM.R]++;
                    Color pixelY = Y.GetPixel(j, i);
                    if (pixelY.G >= minY && pixelY.G <= maxY)
                        yellow[pixelY.G]++;
                }
            }
            for (int i = 0; i < 256; i++)
            {
                if (i < 128)
                {
                    if (mostMinC <= cyan[i])
                    {
                        mostMinC = cyan[i];
                        indexMinC = i;
                    }
                    if (mostMinM <= magenta[i])
                    {
                        mostMinM = magenta[i];
                        indexMinM = i;
                    }
                    if (mostMinY <= yellow[i])
                    {
                        mostMinY = yellow[i];
                        indexMinY = i;
                    }
                }
                else
                {
                    if (mostMaxC <= cyan[i])
                    {
                        mostMaxC = cyan[i];
                        indexMaxC = i;
                    }
                    if (mostMaxM <= magenta[i])
                    {
                        mostMaxM = magenta[i];
                        indexMaxM = i;
                    }
                    if (mostMaxY <= yellow[i])
                    {
                        mostMaxY = yellow[i];
                        indexMaxY = i;
                    }
                }
            }
            for (int i = 0; i < C.Height; i++)
            {
                for (int j = 0; j < C.Width; j++)
                {
                    Color pixel = C.GetPixel(j, i);
                    if (pixel.G < minC)
                        C.SetPixel(j, i, Color.FromArgb(0, (int)indexMinC, (int)indexMinC));
                    else if (pixel.G > maxC)
                         C.SetPixel(j, i, Color.FromArgb(0, (int)indexMaxC, (int)indexMaxC));
                    Color pixelM = M.GetPixel(j, i);
                    if (pixelM.R < minM)
                        M.SetPixel(j, i, Color.FromArgb((int)indexMinM, 0, (int)indexMinM));
                    else if (pixelM.R > maxM)
                        M.SetPixel(j, i, Color.FromArgb((int)indexMaxM, 0, (int)indexMaxM));
                    Color pixelY = Y.GetPixel(j, i);
                    if (pixelY.G < minY)
                        Y.SetPixel(j, i, Color.FromArgb((int)indexMinY, (int)indexMinY, 0));
                    else if (pixelY.G > maxY)
                        Y.SetPixel(j, i, Color.FromArgb((int)indexMaxY, (int)indexMaxY, 0));
                }
            }

            ConvertToRGB(img,C,M,Y,key);
            return true;
        }

        public static void ConvertToRGB(Bitmap img, Bitmap c, Bitmap m, Bitmap y, Bitmap key)
        {
            //Bitmap key = (Bitmap)img.Clone();
            //convertToCMYK(key);
            float R, G, B;
            for (int i = 0; i < img.Height; i++)
            {
                for (int j = 0; j < img.Width; j++)
                {
                    float C = c.GetPixel(j, i).G;
                    float M = m.GetPixel(j, i).R;
                    float Y = y.GetPixel(j, i).G;
                    float K = key.GetPixel(j, i).R;

                    R= 255 * ((1 - (C / 255 )) * (1 - (K / 255 )));
                    G = 255 * ((1 - (M / 255) ) * (1 - (K/ 255 )));
                    B = 255 * ((1 - (Y / 255) ) * (1 -( K/ 255 )));

                    img.SetPixel(j, i, Color.FromArgb((int)R, (int)G, (int)B));
                }
            }
        }
        public static bool CrossDomainColorize(Bitmap img, double hue, double saturation)
        {
            int newH = (int)(hue + 1) * 360 / 6;
            int newS;
            if (saturation != 101)
            {
                newS = (int)(saturation * 100);
            }
            else
            {
                newS = (int)saturation;
            }
            int iWidth = img.Width;
            int iHeight = img.Height;
            unsafe
            {
                int R, G, B;
                for (int x = 0; x < iWidth; x++)
                {
                    for (int y = 0; y < iHeight; y++)
                    {
                        R = img.GetPixel(x, y).R;
                        G = img.GetPixel(x, y).G;
                        B = img.GetPixel(x, y).B;

                        Hsv valueHSV = RGBToHSV(new Rgb((byte)R, (byte)G, (byte)B));

                        Rgb valueRGB = new Rgb();

                        if (saturation == 101)
                        {
                            valueRGB = HSVToRGB(new Hsv(newH, valueHSV.S, valueHSV.V));
                        }
                        else if (saturation != 101)
                        {
                            valueRGB = HSVToRGB(new Hsv(newH, newS, valueHSV.V));
                        }
                        img.SetPixel(x, y, Color.FromArgb(valueRGB.R, valueRGB.G, valueRGB.B));
                    }
                }
            }
            return true;
        }
        public static Hsv RGBToHSV(Rgb rgb)
        {
            double delta, min;
            double h = 0, s, v;

            min = Math.Min(Math.Min(rgb.R, rgb.G), rgb.B);
            v = Math.Max(Math.Max(rgb.R, rgb.G), rgb.B);
            delta = v - min;

            if (v == 0.0)
                s = 0;
            else
                s = delta / v;

            if (s == 0)
                h = 0.0;

            else
            {
                if (rgb.R == v)
                    h = (rgb.G - rgb.B) / delta;
                else if (rgb.G == v)
                    h = 2 + (rgb.B - rgb.R) / delta;
                else if (rgb.B == v)
                    h = 4 + (rgb.R - rgb.G) / delta;

                h *= 60;

                if (h < 0.0)
                    h = h + 360;
            }

            return new Hsv(h, s, (v / 255));
        }
        public static Rgb HSVToRGB(Hsv hsv)
        {
            double r = 0, g = 0, b = 0;

            if (hsv.S == 0)
            {
                r = hsv.V;
                g = hsv.V;
                b = hsv.V;
            }
            else
            {
                int i;
                double f, p, q, t;

                if (hsv.H == 360)
                    hsv.H = 0;
                else
                    hsv.H = hsv.H / 60;

                i = (int)Math.Truncate(hsv.H);
                f = hsv.H - i;

                p = hsv.V * (1.0 - hsv.S);
                q = hsv.V * (1.0 - (hsv.S * f));
                t = hsv.V * (1.0 - (hsv.S * (1.0 - f)));

                switch (i)
                {
                    case 0:
                        r = hsv.V;
                        g = t;
                        b = p;
                        break;

                    case 1:
                        r = q;
                        g = hsv.V;
                        b = p;
                        break;

                    case 2:
                        r = p;
                        g = hsv.V;
                        b = t;
                        break;

                    case 3:
                        r = p;
                        g = q;
                        b = hsv.V;
                        break;

                    case 4:
                        r = t;
                        g = p;
                        b = hsv.V;
                        break;

                    default:
                        r = hsv.V;
                        g = p;
                        b = q;
                        break;
                }
            }
            return new Rgb((byte)(r * 255), (byte)(g * 255), (byte)(b * 255));
        }

        public static bool SimpleColorize(Bitmap b)
        {
            GetColorDiagram(b);
            for (int i = 0; i < b.Width; i++)
            {
                for (int j = 0; j < b.Height; j++)
                {
                    int gray = b.GetPixel(i, j).R;
                    b.SetPixel(i, j, colors[gray]);
                }
            }
            return true;
        }

        public static void GetColorDiagram(Bitmap img)
        {
            int r, g, b;
            for (int x = 0; x < img.Width; x++)
            {
                for (int y = 0; y < img.Height; y++)
                {
                    r = (int)(img.GetPixel(x, y).R*0.299);
                    g = (int)(img.GetPixel(x, y).G*0.587);
                    b = (int)(img.GetPixel(x, y).B*0.114);
                    colors[img.GetPixel(x, y).R] = System.Drawing.Color.FromArgb(r, g, b);
                }
            }
        }

        public static void SimpleColorizeWithPicture(Bitmap img, Bitmap pom, Bitmap convert)
        {
            Color[] colormap=new Color[256];
            int R, G, B, gray;
            for (int x = 0; x < pom.Width; x++)
            {
                for (int y = 0; y < pom.Height; y++)
                {
                    R = pom.GetPixel(x, y).R;
                    G = pom.GetPixel(x, y).G;
                    B = pom.GetPixel(x, y).B;
                    gray = convert.GetPixel(x, y).B;
                    colormap[gray] = System.Drawing.Color.FromArgb(R, G, B);
                }
            }
            for (int x = 0; x < img.Width; x++)
            {
                for (int y = 0; y < img.Height; y++)
                {
                    gray = img.GetPixel(x, y).B;
                    img.SetPixel(x, y, colormap[gray]);
                }
            }
        }

        public static Bitmap Kuwahara(Bitmap Image, int Size)
        {
            Bitmap TempBitmap = Image;
            Bitmap NewBitmap = new System.Drawing.Bitmap(TempBitmap.Width, TempBitmap.Height);
            Graphics NewGraphics = System.Drawing.Graphics.FromImage(NewBitmap);
            NewGraphics.DrawImage(TempBitmap, new System.Drawing.Rectangle(0, 0, TempBitmap.Width, TempBitmap.Height), new System.Drawing.Rectangle(0, 0, TempBitmap.Width, TempBitmap.Height), System.Drawing.GraphicsUnit.Pixel);
            NewGraphics.Dispose();
            Random TempRandom = new Random();
            int[] ApetureMinX = { -(Size / 2), 0, -(Size / 2), 0 };
            int[] ApetureMaxX = { 0, (Size / 2), 0, (Size / 2) };
            int[] ApetureMinY = { -(Size / 2), -(Size / 2), 0, 0 };
            int[] ApetureMaxY = { 0, 0, (Size / 2), (Size / 2) };
            for (int x = 0; x < NewBitmap.Width; ++x)
            {
                for (int y = 0; y < NewBitmap.Height; ++y)
                {
                    int[] RValues = { 0, 0, 0, 0 };
                    int[] GValues = { 0, 0, 0, 0 };
                    int[] BValues = { 0, 0, 0, 0 };
                    int[] NumPixels = { 0, 0, 0, 0 };
                    int[] MaxRValue = { 0, 0, 0, 0 };
                    int[] MaxGValue = { 0, 0, 0, 0 };
                    int[] MaxBValue = { 0, 0, 0, 0 };
                    int[] MinRValue = { 255, 255, 255, 255 };
                    int[] MinGValue = { 255, 255, 255, 255 };
                    int[] MinBValue = { 255, 255, 255, 255 };
                    for (int i = 0; i < 4; ++i)
                    {
                        for (int x2 = ApetureMinX[i]; x2 < ApetureMaxX[i]; ++x2)
                        {
                            int TempX = x + x2;
                            if (TempX >= 0 && TempX < NewBitmap.Width)
                            {
                                for (int y2 = ApetureMinY[i]; y2 < ApetureMaxY[i]; ++y2)
                                {
                                    int TempY = y + y2;
                                    if (TempY >= 0 && TempY < NewBitmap.Height)
                                    {
                                        Color TempColor = TempBitmap.GetPixel(TempX, TempY);
                                        RValues[i] += TempColor.R;
                                        GValues[i] += TempColor.G;
                                        BValues[i] += TempColor.B;
                                        if (TempColor.R > MaxRValue[i])
                                        {
                                            MaxRValue[i] = TempColor.R;
                                        }
                                        else if (TempColor.R < MinRValue[i])
                                        {
                                            MinRValue[i] = TempColor.R;
                                        }

                                        if (TempColor.G > MaxGValue[i])
                                        {
                                            MaxGValue[i] = TempColor.G;
                                        }
                                        else if (TempColor.G < MinGValue[i])
                                        {
                                            MinGValue[i] = TempColor.G;
                                        }

                                        if (TempColor.B > MaxBValue[i])
                                        {
                                            MaxBValue[i] = TempColor.B;
                                        }
                                        else if (TempColor.B < MinBValue[i])
                                        {
                                            MinBValue[i] = TempColor.B;
                                        }
                                        ++NumPixels[i];
                                    }
                                }
                            }
                        }
                    }
                    int j = 0;
                    int MinDifference = 10000;
                    for (int i = 0; i < 4; ++i)
                    {
                        int CurrentDifference = (MaxRValue[i] - MinRValue[i]) + (MaxGValue[i] - MinGValue[i]) + (MaxBValue[i] - MinBValue[i]);
                        if (CurrentDifference < MinDifference && NumPixels[i] > 0)
                        {
                            j = i;
                            MinDifference = CurrentDifference;
                        }
                    }

                    Color MeanPixel = Color.FromArgb(RValues[j] / NumPixels[j],
                        GValues[j] / NumPixels[j],
                        BValues[j] / NumPixels[j]);
                    NewBitmap.SetPixel(x, y, MeanPixel);
                }
            }
            return NewBitmap;
        }

        public static int Sim(Color A,Color B)
        {
            int pom ;
            pom = (int)Math.Sqrt(Math.Pow(((A.R-B.R) + (A.G-B.G) + (A.B-B.B)),2));
            return pom;
        }

        public static byte[] BitmapToByteArray(Bitmap bitmap)
        {
            BitmapData bmpdata = null;
            try
            {
                bmpdata = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadOnly, bitmap.PixelFormat);
                int numbytes = bmpdata.Stride * bitmap.Height;
                byte[] bytedata = new byte[numbytes];
                IntPtr ptr = bmpdata.Scan0;

                Marshal.Copy(ptr, bytedata, 0, numbytes);

                return bytedata;
            }
            finally
            {
                if (bmpdata != null)
                    bitmap.UnlockBits(bmpdata);
            }
        }

        public static Bitmap ByteArrayToBitmap(byte[] bytes)
        {
            Bitmap bmp;
            using (var ms = new MemoryStream(bytes))
            {
                bmp = new Bitmap(ms);
            }
            return bmp;
        }
    }
}

