using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessing
{
    public class ImageNode : INetworkNode
    {
       Dictionary<string, Output> outputs = new Dictionary<string, Output>();
        Bitmap _image;
        Bitmap Image
        {
            get { return _image; }
            set
            {
                _image = value;
                this.Update(null);
            }
        }

        public ImageNode(Bitmap image)
        {
            outputs.Add("r", new Output(image.Width, image.Height, this));
            outputs.Add("g", new Output(image.Width, image.Height, this));
            outputs.Add("b", new Output(image.Width, image.Height, this));
            _image = image;

        }



        public Output GetOutput(string name)
        {
            return outputs[name];
        }

        public void Update(Output output)
        {


            for (int x = 0; x < _image.Width; x++)
            {
                for (int y = 0; y < _image.Height; y++)
                {
                    Color c = _image.GetPixel(x, y);


                    outputs["r"].Image.Data[x, y] = c.R;
                    outputs["g"].Image.Data[x, y] = c.G;
                    outputs["b"].Image.Data[x, y] = c.B;

                }
            }

            foreach (Output o in outputs.Values)
            {
                o.triggerUpdates();
            }
        }

        public void RegisterInput(Output input)
        {
            throw new NotImplementedException();
        }
    }
}
