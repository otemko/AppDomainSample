using System.Linq;
using PluginInterface;

namespace ArithmeticPlugin
{
    [Plugin("Sum")]
    public class Sum: PluginBase
    {
        public override double Operation(double[] input)
        {
            return input.Sum();
        }
    }
}
