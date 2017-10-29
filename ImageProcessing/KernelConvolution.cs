using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessing
{
    public class KernelConvolution : INetworkNode
    {
        Output input, output;

        float[,] Kernel { get; set; }
        byte Size { get; set; }
        NormalizationMode normalizationMode;

        public KernelConvolution(byte size = 3, NormalizationMode normalizationMode = NormalizationMode.TotalKernelValue)
        {
            output = new Output(0, 0, this);
            this.normalizationMode = normalizationMode;
            Size = size;
            Kernel = new float[size, size];
        }
        public KernelConvolution(float[,] kernel, byte size = 3, NormalizationMode normalizationMode = NormalizationMode.TotalKernelValue)
        {
            this.normalizationMode = normalizationMode;
            output = new Output(0, 0, this);
            Size = size;
            Kernel = kernel;
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

            float totalVal =  0;

            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    totalVal += Kernel[i, j];
                }
            }


            for (int x = 0; x < this.output.Image.Width; x++)
            {
                for (int y = 0; y < this.output.Image.Height; y++)
                {
                    float sum = 0;
                    for (int i = -(Size - 1) / 2; i <= (Size-1)/2; i++)
                    {
                        for (int j = -(Size - 1) / 2; j <= (Size - 1) / 2; j++)
                        {
                             
                            sum += Kernel[i+ (Size - 1) / 2, j+ (Size - 1) / 2] *input.Image.GetPixel(x+i,y+j);
                        }
                    }
                    if (normalizationMode == NormalizationMode.TotalKernelValue)
                        this.output.Image.Data[x, y] = Toolbox.Normalize(sum/totalVal);
                    else if (normalizationMode == NormalizationMode.DivideByFour)
                        this.output.Image.Data[x, y] = Toolbox.Normalize(sum/8+127);
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
