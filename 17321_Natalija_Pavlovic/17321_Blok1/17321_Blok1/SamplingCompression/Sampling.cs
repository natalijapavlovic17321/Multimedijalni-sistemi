using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _17321_Blok1
{
    class Sampling
    {
        public static byte[] Downsampling(Bitmap bitmap, string izbor)
        {
            MemoryStream stream = new MemoryStream();
            bitmap.Save(stream, ImageFormat.Bmp);
            byte[] bytes = stream.ToArray();

            int header = bytes[10] + 256 * (bytes[11] + 256 * (bytes[12] + 256 * bytes[13]));
            byte[] outbyte = bytes;

            byte[] pomocni = new byte[256];
            for (int i = header; i < outbyte.Length; i += 256)
            {
                Array.Copy(outbyte, i, pomocni, 0, 256);
                byte[] outpom;
                if (izbor=="1")
                    outpom = DownsamplingMY(pomocni);
                else if(izbor=="2")
                    outpom = DownsamplingCY(pomocni);
                else outpom = DownsamplingCM(pomocni);
                Array.Copy(outpom, 0, outbyte, i, 256);
            }
            //bitmap =(Bitmap)Picture.ByteArrayToBitmap(outbyte).Clone();

            return outbyte;
        }

        public static byte[] DownsamplingCY(byte[] inbytes)
        {
            byte[] outbyte = new byte[inbytes.Count()];

            // M i A-K
            for (int i = 0; i < 256; i += 4)
            {
                if (i < inbytes.Count())
                {
                    outbyte[i + 1] = inbytes[i + 1];
                    outbyte[i + 3] = inbytes[i + 3];
                }
            }

            // C i Y 
            for (int i = 0; i < 256; i += 8) // na po dva piksela
            {
                if (i == 32 || i == 96 || i == 160 || i == 224) // preskace svaki 2. red 
                    i = i + 32;                                 // jer se racuna zajedno s 1.
                if (i < inbytes.Count())
                {
                    byte[] pom = new byte[4];
                    pom[0] = inbytes[i + 2];
                    pom[1] = inbytes[i + 4 + 2];
                    pom[2] = inbytes[i + 32 + 2];
                    pom[3] = inbytes[i + 32 + 4 + 2];
                    byte outs = AverageByte(pom);
                    outbyte[i + 2] = outs;

                    pom[0] = inbytes[i];
                    pom[1] = inbytes[i + 4];
                    pom[2] = inbytes[i + 32];
                    pom[3] = inbytes[i + 32 + 4];
                    outs = AverageByte(pom);
                    outbyte[i] = outs;
                }
            }

            return outbyte;
        }

        public static byte[] DownsamplingMY(byte[] inbytes)
        {
            byte[] outbyte = new byte[inbytes.Count()];

            // M i A-K
            for (int i = 0; i < 256; i += 4)
            {
                if (i < inbytes.Count())
                {
                    outbyte[i + 2] = inbytes[i + 2];
                    outbyte[i + 3] = inbytes[i + 3];
                }
            }

            // M i Y
            for (int i = 0; i < 256; i += 8) // na po dva piksela
            {
                if (i == 32 || i == 96 || i == 160 || i == 224) // preskace svaki 2. red 
                    i = i + 32;                                 // jer se racuna zajedno s 1.
                if (i < inbytes.Count())
                {
                    byte[] pom = new byte[4];
                    pom[0] = inbytes[i];
                    pom[1] = inbytes[i + 4];
                    pom[2] = inbytes[i + 32];
                    pom[3] = inbytes[i + 32 + 4];
                    byte outs = AverageByte(pom);
                    outbyte[i] = outs;

                    pom[0] = inbytes[i + 1];
                    pom[1] = inbytes[i + 4 + 1];
                    pom[2] = inbytes[i + 32 + 1];
                    pom[3] = inbytes[i + 32 + 4 + 1];
                    outs = AverageByte(pom);
                    outbyte[i + 1] = outs;
                }
            }
            return outbyte;
        }

        public static byte[] DownsamplingCM(byte[] inbytes)
        {
            byte[] outbyte = new byte[inbytes.Count()];

            // C i A-K 
            for (int i = 0; i < 256; i += 4)
            {
                if (i < inbytes.Count())
                {
                    outbyte[i] = inbytes[i];
                    outbyte[i + 3] = inbytes[i + 3];
                }
            }

            // M i C 
            for (int i = 0; i < 256; i += 8) // na po dva piksela
            {
                if (i == 32 || i == 96 || i == 160 || i == 224) // preskace svaki 2. red 
                    i = i + 32;                                 // jer se racuna zajedno s 1.
                if (i < inbytes.Count())
                {
                    byte[] pom = new byte[4];
                    pom[0] = inbytes[i + 2];
                    pom[1] = inbytes[i + 4 + 2];
                    pom[2] = inbytes[i + 32 + 2];
                    pom[3] = inbytes[i + 32 + 4 + 2];
                    byte outs = AverageByte(pom);
                    outbyte[i + 2] = outs;

                    pom[0] = inbytes[i + 1];
                    pom[1] = inbytes[i + 4 + 1];
                    pom[2] = inbytes[i + 32 + 1];
                    pom[3] = inbytes[i + 32 + 4 + 1];
                    outs = AverageByte(pom);
                    outbyte[i + 1] = outs;
                }
            }

            return outbyte;
        }

        public static byte AverageByte(byte[] bytes)
        {
            byte output;
            int average = 0;
            for (int i = 0; i < 4; i++)
            {
                average = (int)bytes[i];
            }
            average = (int)(average / 4);
            output = (byte)average;
            return output;
        }

        public static byte[] UpSamplingBlock(byte[] inbytes)
        {
            byte[] outbyte = new byte[inbytes.Count()];

            for (int i = 0; i < 256; i += 4)
            {
                if (i < inbytes.Count())
                {
                    outbyte[i] = inbytes[i];
                }
            }

            for (int i = 0; i < 256; i += 8) // na po dva piksela
            {
                if (i == 32 || i == 96 || i == 160 || i == 224) // preskace svaki 2. red 
                    i = i + 32;                                 // jer se racuna zajedno s 1.
                if (i < inbytes.Count())
                {
                     
                    outbyte[i + 2]=inbytes[i + 2];
                    outbyte[i + 4 + 2]=inbytes[i + 2];
                    outbyte[i + 32 + 2]=inbytes[i + 2];
                    outbyte[i + 32 + 4 + 2] = inbytes[i + 2];

                    outbyte[i + 1] = inbytes[i + 1];
                    outbyte[i + 4 + 1] = inbytes[i + 1];
                    outbyte[i + 32 + 1] = inbytes[i + 1];
                    outbyte[i + 32 + 4 + 1] = inbytes[i + 1];

                    outbyte[i ] = inbytes[i];
                    outbyte[i + 4] = inbytes[i];
                    outbyte[i + 32] = inbytes[i];
                    outbyte[i + 32 + 4] = inbytes[i];
                }
            }
            return outbyte;
        }

        public static byte[] UpSampling(string fileName)
        {
            byte[] slika = File.ReadAllBytes(fileName);

            int header = slika[10] + 256 * (slika[11] + 256 * (slika[12] + 256 * slika[13]));

            byte[] outbyte = new byte[slika.Length];
            for (int i = 0; i < header; i++)
            {
                outbyte[i] = slika[i];
            }

            byte[] pomocni = new byte[256];
            for (int i = header; i < slika.Length; i += 256)
            {
                Array.Copy(slika, i, pomocni, 0, 256);
                byte[] outpom = UpSamplingBlock(pomocni);
                Array.Copy(outpom, 0, outbyte, i, 256);
            }
            return outbyte;
        }
    }
}
