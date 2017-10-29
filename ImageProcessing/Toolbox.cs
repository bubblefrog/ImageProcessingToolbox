using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessing
{
    public class Toolbox
    {
        public static byte Normalize(int input)
        {
            if (input < byte.MinValue)
                return byte.MinValue;
            if (input > byte.MaxValue)
                return byte.MaxValue;

            return (byte)input;
        }
        public static byte Normalize(float input)
        {
            return Normalize((int)input);
        }
    }
}
