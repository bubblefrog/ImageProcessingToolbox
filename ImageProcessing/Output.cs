using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessing
{
    public class Output
    {
        List<INetworkNode> listeners;

        public ImageData Image { get; set; }

        public  INetworkNode Owner { get; set; }

        public Output(int width, int height, INetworkNode owner)
        {
            listeners = new List<INetworkNode>();
            Image = new ImageData(width, height);
            Owner = owner;
        }

        public void register(INetworkNode node)
        {
            if (!listeners.Contains(node))
                listeners.Add(node);

            node.RegisterInput(this);
        }
        public void unRegister (INetworkNode node)
        {
            if (listeners.Contains(node))
                listeners.Remove(node);
        }

       public void triggerUpdates()
        {
            foreach (INetworkNode item in listeners)
            {
                item.Update(this);
            }
        }
    }
}
