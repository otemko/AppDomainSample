using System.Collections.Generic;
using ActionAlgorithm.ActionPlugins;

namespace ActionAlgorithm.FileWorker
{
    public interface IFileWorker<T>
    {
        string Path { get; set; }
        void Write(T actionPlugins);
        T Read();
    }
}