using System;
using System.Collections.Generic;
using System.IO;
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
                while (streamReader.Peek() > -1)
                {
                    var line = streamReader.ReadLine();
                    //var operation = binaryReader.ReadString();
                    //var _class = binaryReader.ReadString();
                    //var assembly = binaryReader.ReadString();

                    //IActionPlugin book = new ActionPlugin((TypeOperation)Enum.Parse(typeof(TypeOperation), operation), _class, assembly);

                    //actionPlugins.Add(book);
                }
            }
            return actionPlugins;
        }
    }
}