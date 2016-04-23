using System.Linq;
using PluginInterface;

namespace StatisticPlugin
{
    [Plugin("Maximum")]
    public class Maximum: PluginBase
    {
        public override double Operation(double[] input)
        {
            return input.Max();
        }
    }
}
