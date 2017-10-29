using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessing
{
    public class Sum : INetworkNode
    {
        List<Output> inputs = new List<Output>();
        Dictionary<string, Output> outputs = new Dictionary<string, Output>();
        byte bias;
        public Sum(byte bias=0)
        {
            outputs.Add("out", new Output(0, 0, this));
            this.bias = bias;
        }


        public Output GetOutput(string name = "out")
        {
            return outputs[name];
        }

        public void RegisterInput(Output input)
        {
            inputs.Add(input);
        }

        public void Update(Output output)
        {
            this.outputs["out"].Image.Width = output.Image.Width;
            this.outputs["out"].Image.Height = output.Image.Height;

            foreach (Output input in inputs)
            {
                for (int x = 0; x < this.outputs["out"].Image.Width; x++)
                {
                    for (int y = 0; y < this.outputs["out"].Image.Height; y++)
                    {
                        this.outputs["out"].Image.Data[x, y] = Toolbox.Normalize(this.outputs["out"].Image.Data[x, y] +  input.Image.GetPixel(x,y) + bias);
                    }
                }
            }

            foreach (Output o in outputs.Values)
            {
                o.triggerUpdates();
            }
        }
    }
}
