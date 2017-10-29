using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessing
{
    public class SobelEdge : INetworkNode
    {
        Output input, output;

        byte Size = 3;
        public SobelEdge()
        {
            output = new Output(0, 0, this);
           
        }
        public Output GetOutput(string name = "out")
        {
            return output;
        }

        public void RegisterInput(Output input)
        {
            this.input = input;
        }

        public void Update(Output output)
        {
            this.output.Image.Width = input.Image.Width;
            this.output.Image.Height = input.Image.Height;


            for (int x = 0; x < this.output.Image.Width; x++)
            {
                for (int y = 0; y < this.output.Image.Height; y++)
                {
                    double sumx = 0;
                    double sumy = 0;
                    for (int i = -(Size - 1) / 2; i <= (Size-1)/2; i++)
                    {
                        for (int j = -(Size - 1) / 2; j <= (Size - 1) / 2; j++)
                        {
                            sumx += Sx[i+ (Size - 1) / 2, j+ (Size - 1) / 2] *input.Image.GetPixel(x+i,y+j);
                            sumy += Sy[i + (Size - 1) / 2, j + (Size - 1) / 2] * input.Image.GetPixel(x + i, y + j);
                        }
                    }

                    this.output.Image.Data[x, y] = Toolbox.Normalize((float)Math.Sqrt(Math.Pow(sumx/2, 2) + Math.Pow(sumy/2, 2)));
                    
                }
            }

            this.output.triggerUpdates();
        }



        public static float[,] MeanBlur = { { 1f, 1f, 1f }, { 1f, 1f, 1f }, { 1f, 1f, 1f } };
        public static float[,] GausBlur = { { 1f, 2f, 1f }, { 2f, 4f, 2f }, { 1f, 2f, 1f } };

        public static float[,] Sx = { { -1f, 0f, 1f }, 
                                      { -2f, 0f, 2f }, 
                                      { -1f, 0f, 1f } };
        public static float[,] Sy = { { -1f, -2f, -1f }, 
                                      { 0f, 0f, 0f }, 
                                      { 1f, 2f, 1f } };



        public enum NormalizationMode
        {
            TotalKernelValue,
            DivideByFour
        }
    }
}
