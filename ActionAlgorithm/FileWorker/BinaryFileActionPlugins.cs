using System;
using System.Collections.Generic;
using System.IO;
using ActionAlgorithm.ActionPlugins;

namespace ActionAlgorithm.FileWorker
{
    public class BinaryFileActionPlugins: IFileWorker<IEnumerable<IActionPlugin>>
    {
        public string Path { get; set; }

        public BinaryFileActionPlugins() : this(null)
        {
        }

        public BinaryFileActionPlugins(string path)
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentException(nameof(path));
            this.Path = path;
        }

        public void Write(IEnumerable<IActionPlugin> actionPlugins)
        {
            using (var binaryWriter = new BinaryWriter(new FileStream(Path, FileMode.Append)))
            {
                foreach (var aPlugin in actionPlugins)
                {
                    binaryWriter.Write(aPlugin.Operation.ToString());
                    binaryWriter.Write(aPlugin.Class);
                    binaryWriter.Write(aPlugin.Assembly);
                }
            }
        }

        public IEnumerable<IActionPlugin> Read()
        {
            if (!File.Exists(Path))
                throw new FileNotFoundException();

            var actionPlugins = new List<IActionPlugin>();

            using (var binaryReader = new BinaryReader(new FileStream(Path, FileMode.Open)))
            {
                while (binaryReader.PeekChar() > -1)
                {
                    var operation = binaryReader.ReadString();
                    var _class = binaryReader.ReadString();
                    var assembly = binaryReader.ReadString();

                    IActionPlugin actionPlugin = new ActionPlugin((TypeOperation)Enum.Parse(typeof(TypeOperation), operation), _class, assembly);

                    actionPlugins.Add(actionPlugin);
                }
            }
            return actionPlugins;
        }
    }
}