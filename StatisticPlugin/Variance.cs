using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PluginInterface;

namespace StatisticPlugin
{
    [Plugin("Variance",true)]
    public class Variance: PluginBase
    {
        public override double Operation(double[] input)
        {
            var sumSquares = input.Sum(t => t * t);
            var mean = new Mean().Operation(input);
            return (sumSquares - input.Length * mean * mean) / (input.Length - 1);
        }
    }
}
