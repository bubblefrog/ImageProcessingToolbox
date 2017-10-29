using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessing
{
    public class SubSampler : INetworkNode
    {
        Dictionary<string, Output> outputs = new Dictionary<string, Output>();

        int Width { get; set; }
        int Height { get; set; }


        public SubSampler(int width = 2, int height = 2)
        {
            Width = width;
            Height = height;

            outputs.Add("out",new Output(width * 2, height * 2, this));

        }

        public Output GetOutput(string name = "out")
        {
            return outputs[name];
        }

        public void Update(Output output)
        {
            this.outputs["out"].Image.Width = output.Image.Width/Width;
            this.outputs["out"].Image.Height = output.Image.Height/ Height;

            for (int x = 0; x < this.outputs["out"].Image.Width; x++)
            {
                for (int y = 0; y < this.outputs["out"].Image.Height; y++)
                {
                    byte val = 0;

                    for (int i = 0; i < Width; i++)
                    {
                        for (int j = 0; j < Height; j++)
                        {
                           val = Math.Max (val,output.Image.GetPixel((x*Width) + i, (y*Height) + j));
                        }
                    }

                    this.outputs["out"].Image.Data[x, y] = val;
                }
            }

            foreach (Output o in outputs.Values)
            {
                o.triggerUpdates();
            }
        }

        public void RegisterInput(Output input)
        {
            
        }
    }
}
