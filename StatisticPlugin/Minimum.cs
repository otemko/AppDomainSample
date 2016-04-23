using System.Linq;
using PluginInterface;

namespace StatisticPlugin
{
    [Plugin("Minimum")]
    public class Minimum: PluginBase
    {
        public override double Operation(double[] input)
        {
            return input.Min();
        }
    }
}
