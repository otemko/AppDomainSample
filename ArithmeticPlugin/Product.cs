using System.Linq;
using PluginInterface;

namespace ArithmeticPlugin
{
    [Plugin("Product")]
    public class Product: PluginBase
    {
        public override double Operation( double[] input)
        {
            return input.Aggregate(1.0,(acc,x)=>acc*=x);
        }
    }
}
