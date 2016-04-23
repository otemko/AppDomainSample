using System;

namespace PluginInterface
{
    public abstract class PluginBase : MarshalByRefObject, IPlugin
    {
        public abstract double Operation(double[] input);
        public string Name
        {

            get
            {
                var attrs = (PluginAttribute[])this.GetType().GetCustomAttributes(typeof(PluginAttribute), false);
                return $"Context domain:{AppDomain.CurrentDomain.FriendlyName}. Class name:{attrs[0].Name}";
            }
        }
    
    }
}
