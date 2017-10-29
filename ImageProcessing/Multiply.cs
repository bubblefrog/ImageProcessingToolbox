using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessing
{
    public class Multiply : INetworkNode
    {
        Output input;
        Dictionary<string, Output> outputs = new Dictionary<string, Output>();

        float Factor { get; set; }

        public Multiply(float factor)
        {
            Factor = factor;
            outputs.Add("out", new Output(0, 0, this));
        }

        public Output GetOutput(string name = "out")
        {
            return outputs[name];
        }

        public void Update(Output output)
        {
            this.outputs["out"].Image.Width = output.Image.Width;
            this.outputs["out"].Image.Height = output.Image.Height;

            for (int x = 0; x < this.outputs["out"].Image.Width; x++)
            {
                for (int y = 0; y < this.outputs["out"].Image.Height; y++)
                {
                    outputs["out"].Image.Data[x, y] = Toolbox.Normalize(Factor * output.Image.Data[x, y]);
                }
            }

            foreach (Output o in outputs.Values)
            {
                o.triggerUpdates();
            }
        }

        public void RegisterInput(Output input)
        {
            this.input = input;
        }
    }
}
