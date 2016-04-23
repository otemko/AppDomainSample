using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ActionAlgorithm.Action;
using ActionAlgorithm.ActionPlugins;
using ActionAlgorithm.MyAppDomain;
using PluginInterface;

namespace ActionAlgorithm
{
    public class AAlgorithm
    {
        public Queue<IAction> Queue { get; private set; }
        public IEnumerable<IActionPlugin> ActionPlugins { get; private set; }

        public AAlgorithm() : this(null, null)
        {
        }

        public AAlgorithm(Queue<IAction> queue, IEnumerable<IActionPlugin> listActionPlugins)
        {
            if (queue == null) throw new ArgumentNullException(nameof(queue));
            if (listActionPlugins == null) throw new ArgumentNullException(nameof(listActionPlugins));

            this.Queue = queue;
            this.ActionPlugins = listActionPlugins;
        }

        public Dictionary<string, double> Run()
        {
            IMyAppDomain appDomain = SingletonAppDomain.GetInstance();
            var result = new Dictionary<string, double>();

            while (Queue.Count != 0)
            {
                var currentAction = Queue.Dequeue();
                foreach (var actionPlugin in ActionPlugins)
                {
                    if (actionPlugin.Operation == currentAction.Operation)
                    {
                        var plugin = appDomain.GetObjectPlugin(actionPlugin.Assembly, actionPlugin.Class);
                        var attribute = (PluginAttribute) plugin.GetType().GetCustomAttribute(typeof(PluginAttribute));

                        if (attribute.AsynsOperation == true)
                        {
                            ThreadPool.QueueUserWorkItem(new WaitCallback((s) =>
                            {
                                var resultOperation = plugin.Operation(currentAction.Data);
                                result.Add(plugin.Name, resultOperation);
                            }));
                        }
                        else
                        {
                            var resultOperation = plugin.Operation(currentAction.Data);
                            result.Add(plugin.Name, resultOperation);
                        }
                    }
                }
            }

            return result;
        }
    }
}
