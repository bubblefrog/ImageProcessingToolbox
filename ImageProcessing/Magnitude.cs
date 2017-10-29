using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessing
{
    public class Magnitude: INetworkNode
    {
        List<Output> inputs = new List<Output>();
        Output output;

        public Magnitude()
        {
            output=new Output(0, 0, this);
        }


        public Output GetOutput(string name = "out")
        {
            return output;
        }

        public void RegisterInput(Output input)
        {
            inputs.Add(input);
        }

        public void Update(Output output)
        {
            this.output.Image.Width = output.Image.Width;
            this.output.Image.Height = output.Image.Height;
            for (int x = 0; x < this.output.Image.Width; x++)
            {
                for (int y = 0; y < this.output.Image.Height; y++)
                {
                    double val = 0;
                    foreach (Output input in inputs)
                    {
                        val += Math.Pow(input.Image.GetPixel(x, y), 2);
                    }
                    this.output.Image.Data[x, y] = Toolbox.Normalize((float)Math.Sqrt(val));
                }
            }


                this.output.triggerUpdates();
            
        }
    }
}
