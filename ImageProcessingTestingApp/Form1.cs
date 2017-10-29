using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ImageProcessing;
namespace ImageProcessingTestingApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ImageNode rootNode = new ImageNode(new Bitmap(pictureBox1.Image));

            Multiply mulr = new Multiply(1f / 3f);
            Multiply mulg = new Multiply(1f / 3f);
            Multiply mulb = new Multiply(1f / 3f);
            rootNode.GetOutput("r").register(mulr);
            rootNode.GetOutput("g").register(mulg);
            rootNode.GetOutput("b").register(mulb);

            Sum sum = new Sum();

            mulr.GetOutput().register(sum);
            mulg.GetOutput().register(sum);
            mulb.GetOutput().register(sum);

            SobelEdge sx = new SobelEdge();
            sum.GetOutput().register(sx);


            

            
            
            rootNode.Update(null);

            Bitmap bmp = new Bitmap(sx.GetOutput().Image.Width, sx.GetOutput().Image.Height);

            for (int x = 0; x < bmp.Width; x++)
            {
                for (int y = 0; y < bmp.Height; y++)
                {
                    Color c = Color.FromArgb(255, sx.GetOutput("out").Image.Data[x, y], sx.GetOutput("out").Image.Data[x, y], sx.GetOutput("out").Image.Data[x, y]);

                    bmp.SetPixel(x, y, c);
                }
            }

            pictureBox2.Image = bmp;
        }
    }
}
