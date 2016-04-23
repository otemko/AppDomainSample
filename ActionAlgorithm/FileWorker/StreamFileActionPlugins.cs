using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using ActionAlgorithm.ActionPlugins;

namespace ActionAlgorithm.FileWorker
{
    public class StreamFileActionPlugins : IFileWorker<IEnumerable<IActionPlugin>>
    {
        public string Path { get; set; }

        public StreamFileActionPlugins() : this(null)
        {
        }

        public StreamFileActionPlugins(string path)
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentException(nameof(path));
            this.Path = path;
        }

        public void Write(IEnumerable<IActionPlugin> actionPlugins)
        {
            using (var streamWriter = new StreamWriter(Path, true, System.Text.Encoding.Default))
            {
                foreach (var actionPlugin in actionPlugins)
                {
                    streamWriter.WriteLine($"[{actionPlugin.Operation}] = [{actionPlugin.Class},{actionPlugin.Assembly}]");
                }
            }
        }

        public IEnumerable<IActionPlugin> Read()
        {
            if (!File.Exists(Path))
                throw new FileNotFoundException();

            var actionPlugins = new List<IActionPlugin>();

            using (var streamReader = new StreamReader(Path, System.Text.Encoding.Default))
            {
                var pattern = @"\[(.*?)\]";
                var rgx = new Regex(pattern, RegexOptions.IgnoreCase);

                while (streamReader.Peek() > -1)
                {
                    string operation = null, _class= null, assembly = null;

                    foreach (Match match in rgx.Matches(streamReader.ReadLine()))
                    {
                        if (match.Value.Contains(",") == false)
                        {
                            operation = match.Value.Substring(match.Value.IndexOf("[") + 1, match.Value.IndexOf("]") - 1);
                        }
                        else
                        {
                            _class = match.Value.Split(',')[0];
                            _class = _class.Substring(_class.IndexOf("[") + 1);
                            assembly = match.Value.Split(',')[1];
                            assembly = assembly.Substring(0, assembly.IndexOf("]"));
                        }  
                    }
                    IActionPlugin actionPlugin = new ActionPlugin((TypeOperation)Enum.Parse(typeof(TypeOperation), operation), _class, assembly);

                    actionPlugins.Add(actionPlugin);
                }
            }
            return actionPlugins;
        }
    }
}