using System.Linq;
using PluginInterface;

namespace StatisticPlugin
{
    // установка атрибута Plugin со свойствами Name и Complexity
    [Plugin("Mean")]
    public class Mean: PluginBase
    {
        public override double Operation(double[] input)
        {
            return input.Average();
        }
    }
}
