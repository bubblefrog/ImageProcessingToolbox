using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessing
{
    public class ImageData
    {
        private int _width, _height;
        public int Width { get { return _width; } set { _width = value; Data = new byte[_width, _height]; } }
        public int Height { get { return _height; } set { _height = value; Data = new byte[_width, _height]; } }

        public byte[,] Data { get; set; }

        public byte GetPixel(int x, int y, OverflowType oType = OverflowType.Ignore)
        {
            if (oType == OverflowType.Ignore)
            {
                if (x < 0 || y < 0)
                {
                    return 0;
                }else if (x>= Width || y >= Height)
                {
                    return 0;
                }
                else
                {
                    return Data[x, y];
                }
            }
            else if (oType == OverflowType.EdgeExtend)
            {

            }
            return 0;
        }



        public ImageData(int width, int height)
        {
            Width = width;
            Height = height;
            Data = new byte[width,height];
        }


        public enum OverflowType
        {
            Ignore,
            EdgeExtend,
            ImageWrap
        }

    }
}
