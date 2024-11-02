using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WebCamLib;

namespace ImageProcessing1
{
    public partial class Form1 : Form
    {
        Bitmap loaded, processed, imageB, imageA, colorgreen, resultImage;
        Device[] mgaDevice;

        public Form1()
        {
            InitializeComponent();
        }

        private void dIPToolStripMenuItem_Click(object sender, EventArgs e)
        {
        
        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void mirrorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            processed = new Bitmap(loaded.Width, loaded.Height);
            Color pixel;
            for(int i = 0; i < loaded.Width; i++)
                for(int j = 0; j < loaded.Height; j++)
                {
                    pixel = loaded.GetPixel(i, j);
                    processed.SetPixel(i, j, pixel);
                }

            pictureBox2.Image = processed;
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.ShowDialog();
        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            processed.Save(saveFileDialog1.FileName);
        }

        private void greyScaleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            processed = new Bitmap(loaded.Width, loaded.Height);
            Color pixel;
            int average;
            for (int i = 0; i < loaded.Width; i++)
                for (int j = 0; j < loaded.Height; j++)
                {
                    pixel = loaded.GetPixel(i, j);
                    average = (int)(pixel.R + pixel.G + pixel.B) / 3;
                    Color grey = Color.FromArgb(average, average, average);
                    processed.SetPixel(i, j, grey);
                }

            pictureBox2.Image = processed;
        }

        private void invertToolStripMenuItem_Click(object sender, EventArgs e)
        {
            processed = new Bitmap(loaded.Width, loaded.Height);
            Color pixel;
           
            for (int i = 0; i < loaded.Width; i++)
                for (int j = 0; j < loaded.Height; j++)
                {
                    pixel = loaded.GetPixel(i, j);
                  
                    Color grey = Color.FromArgb(255-pixel.R,255-pixel.G,255-pixel.B);
                    processed.SetPixel(i, j, grey);
                }

            pictureBox2.Image = processed;
        }

        private void flipImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int width = loaded.Width;

            processed = new Bitmap(loaded.Width, loaded.Height);
            Color pixel;

            for (int i = 0; i < loaded.Width; i++)
                for (int j = 0; j < loaded.Height; j++)
                {
                    pixel = loaded.GetPixel(i, j);

                    
                    processed.SetPixel(width - 1 - i, j, pixel);
                }

            pictureBox2.Image = processed;


        }

        private void histogramToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BasicDIP.Histogram(ref loaded, ref processed);
            pictureBox2.Image = processed;
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            BasicDIP.Brightness(ref loaded, ref processed, trackBar1.Value);
            pictureBox2.Image = processed;
        }

        private void openFileDialog3_FileOk(object sender, CancelEventArgs e)
        {
            imageA = new Bitmap(openFileDialog3.FileName);
            pictureBox2.Image = imageA;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            openFileDialog3.ShowDialog();

        }

        private void videoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            mgaDevice = DeviceManager.GetAllDevices();

        }

        private void onToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mgaDevice[0].ShowWindow(pictureBox3);
        }

        private void offToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mgaDevice[0].Stop();
        }

        //public void subtract()
        //{

        //    resultImage = new Bitmap(imageA.Width, imageA.Height);
        //    Color mygreen = Color.FromArgb(0, 0, 255);
        //    int greygreen = (mygreen.R + mygreen.G + mygreen.B) / 3;
        //    int threshold = 10;


        //    for (int i = 0; i < imageB.Width; i++)
        //    {
        //        for (int j = 0; j < imageB.Height; j++)
        //        {
        //            Color pixel = imageB.GetPixel(i, j);
        //            Color backpixel = imageA.GetPixel(i, j);
        //            int grey = (pixel.R + pixel.G + pixel.B) / 3;
        //            int subtractvalue = Math.Abs(grey - greygreen);

        //            if (subtractvalue > threshold)
        //            {
        //                resultImage.SetPixel(i, j, pixel);
        //            }
        //            else
        //            {

        //                resultImage.SetPixel(i, j, backpixel);
        //            }






        //        }
        //    }
        //    pictureBox3.Image = resultImage;


        //}



        private void button3_Click(object sender, EventArgs e)
        {
            //subtract();
            resultImage = new Bitmap(imageA.Width, imageA.Height);
            
            BasicDIP.Subtract(ref imageA, ref imageB, ref resultImage);
            pictureBox3.Image = resultImage;


        }

        private void sepiaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            processed = new Bitmap(loaded.Width, loaded.Height);
            Color pixel;

            for (int i = 0; i < loaded.Width; i++)
                for (int j = 0; j < loaded.Height; j++)
                {


                    pixel = loaded.GetPixel(i, j);

                    

                    int tr = (int)(0.393 * pixel.R + 0.769 * pixel.G + 0.189 * pixel.B);
                    int tg = (int)(0.349 * pixel.R + 0.686 * pixel.G + 0.168 * pixel.B);
                    int tb = (int)(0.272 * pixel.R + 0.534 * pixel.G + 0.131 * pixel.B);

                    tr = Math.Min(255, tr);
                    tg = Math.Min(255, tg);
                    tb = Math.Min(255, tb);



                    Color sepia = Color.FromArgb(tr, tg, tb);
                    processed.SetPixel(i, j, sepia);
                }

            pictureBox2.Image = processed;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog2.ShowDialog();
        }

        private void openFileDialog2_FileOk(object sender, CancelEventArgs e)
        {
            imageB = new Bitmap(openFileDialog2.FileName);
            pictureBox1.Image = imageB;
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog(this);
            loaded = new Bitmap(openFileDialog1.FileName);
            pictureBox1.Image = loaded;
        }
    }
}
