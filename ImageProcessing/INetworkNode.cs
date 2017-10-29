using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessing
{
    public interface INetworkNode
    {
        void RegisterInput(Output input);
        Output GetOutput(string name = "out");
        void Update(Output output);
    }
}
