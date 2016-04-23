using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ActionAlgorithm;
using ActionAlgorithm.Action;
using ActionAlgorithm.ActionPlugins;
using ActionAlgorithm.FileWorker;
using ActionAlgorithm.MyAppDomain;

namespace DomainsDemo
{
    class Program
    {
        private static IEnumerable<IActionPlugin> GetActionPluginsFromFile(
            IFileWorker<IEnumerable<IActionPlugin>> typeFileWorker)
        {
            if (typeFileWorker == null) throw new ArgumentNullException(nameof(typeFileWorker));
            return typeFileWorker.Read();
        }

        private static Queue<IAction> GetQueueAction()
        {
            var queue = new Queue<IAction>();

            var action1 = new ActionAlgorithm.Action.Action(TypeOperation.Minimum, new double[] { 1, 2, 3, 4 });
            queue.Enqueue(action1);

            var action2 = new ActionAlgorithm.Action.Action(TypeOperation.Maximum, new double[] { 1, 2, 5, 4 });
            queue.Enqueue(action2);

            return queue;
        }

        private static void SetActionPluginsInFile(IFileWorker<IEnumerable<IActionPlugin>> typeFileWorker, 
            IEnumerable<IActionPlugin> actionPlugins)  
        {
            if (typeFileWorker == null) throw new ArgumentNullException(nameof(typeFileWorker));
            if (actionPlugins == null) throw new ArgumentNullException(nameof(actionPlugins));
            typeFileWorker.Write(actionPlugins);
        }

        private static IEnumerable<IActionPlugin> SetActionPluginsInFile()
        {
            var listActionPlugin = new List<IActionPlugin>
            {
                new ActionPlugin(TypeOperation.Product, "Product", "ArithmeticPlugin.dll"),
                new ActionPlugin(TypeOperation.Minimum, "Minimum", "StatisticPlugin.dll"),
                new ActionPlugin(TypeOperation.Maximum, "Maximum", "StatisticPlugin.dll")
            };
            return listActionPlugin;
        }

        static void Main(string[] args)
        {
            var path = @"file";

            var streamFile = new StreamFileActionPlugins($"{path}.txt");

            //var streamFile = new BinaryFileActionPlugins(path);

            //SetActionPluginsInFile(new StreamFileActionPlugins($"{path}.txt"), SetActionPluginsInFile());

            var actionAlgotihm = new AAlgorithm(GetQueueAction(), GetActionPluginsFromFile(streamFile));

            foreach (var d in actionAlgotihm.Run())
            {
                Console.WriteLine($"Name: {d.Key}, value: {d.Value}");
            }

            foreach (var assem in SingletonAppDomain.GetInstance().GetAssemblies())
                Console.WriteLine(assem.ToString());

            SingletonAppDomain.GetInstance().UnloadAppDomain();
        }    
    }
}
