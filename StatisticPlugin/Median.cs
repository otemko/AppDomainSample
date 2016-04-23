using System;
using PluginInterface;

namespace StatisticPlugin
{
    /// <summary>
    /// Операция плагина Median должна быть выполнена в асинхронном режиме (как пример)
    /// </summary>
    [Plugin("Median", Complexity.Quadratic, true)]
    public class Median : PluginBase
    {
        public override double Operation(double[] input)
        {
            //Note:
            Array.Sort(input);
            var len = input.Length;
            if (len % 2 == 0)
                return (input[len / 2 - 1] + input[len / 2]) / 2;
            return input[len / 2];
        }
    }
}
