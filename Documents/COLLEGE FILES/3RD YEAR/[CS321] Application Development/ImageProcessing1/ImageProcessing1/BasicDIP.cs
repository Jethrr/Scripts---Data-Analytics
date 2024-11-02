using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessing1
{
    static class BasicDIP
    {

        public static void Brightness(ref Bitmap a, ref Bitmap b, int value)
        {

            b = new Bitmap(a.Width, a.Height);
            for (int i = 0; i < a.Width; i++)
            {
                for (int j = 0; j < a.Height; j++)
                {
                    Color temp = a.GetPixel(i, j);
                    Color changed;
                    if (value > 0)
                    {
                        changed = Color.FromArgb(Math.Min(temp.R + value, 255), Math.Min(temp.G + value, 255), Math.Min(temp.G + value, 255));
                    } else
                    {
                        changed = Color.FromArgb(Math.Max(temp.R + value, 0), Math.Max(temp.G + value, 0), Math.Max(temp.G + value, 0));

                    }

                    b.SetPixel(i, j, changed);  




                }
            }


        }
       
        
      


        public static void Histogram(ref Bitmap a, ref Bitmap b)
        {
            Color sample;
            Color gray;
            Byte graydata;

            for (int i = 0; i < a.Width; i++)
            {
                for (int j = 0; j < a.Height; j++)
                {
                    sample = a.GetPixel(i, j);
                    graydata = (byte)((sample.R + sample.G + sample.B) / 3);
                    gray = Color.FromArgb(graydata, graydata, graydata);
                    a.SetPixel(i, j, gray);
                }
            }

            int[] histdata = new int[256];

            for (int i = 0; i < a.Width; i++)
            {
                for (int j = 0; j < a.Height; j++)
                {
                    sample = a.GetPixel(i, j);
                    histdata[sample.R]++;
                }
            }

            b = new Bitmap(256, 800);

            for (int i = 0; i < 256; i++)
            {
                for (int j = 0; j < 800; j++)
                {
                    b.SetPixel(i, j ,Color.White);
                }   
            }


            for (int i = 0; i < 256; i++)
            {
                for (int j = 0; j < Math.Min(histdata[i] / 5, b.Height - 1); j++)
                {
                    b.SetPixel(i, (b.Height - 1) - j, Color.Black);
                }
            }


        }

        public static void Subtract(ref Bitmap a, ref Bitmap b, ref Bitmap c) {
            Color mygreen = Color.FromArgb(0, 0,255);
            int greygreen = (mygreen.R + mygreen.G + mygreen.B) / 3;
            int threshold = 5;


            for (int i = 0; i < b.Width; i++)
            {
                for (int j = 0; j < b.Height; j++)
                {
                    Color pixel =  b.GetPixel(i, j);
                    Color backpixel = a.GetPixel(i, j);
                    int grey = (pixel.R + pixel.G + pixel.B) / 3;
                    int subtractvalue = Math.Abs(grey - greygreen);

                    if (subtractvalue > threshold)
                    {
                       c.SetPixel(i, j, pixel);
                    }
                    else
                    {
                       c.SetPixel(i, j, backpixel);
                    }

                    




                }
            }

            


        }
    }
}
