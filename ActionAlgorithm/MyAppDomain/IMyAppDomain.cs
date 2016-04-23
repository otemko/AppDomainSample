using System;
using System.Reflection;
using PluginInterface;

namespace ActionAlgorithm.MyAppDomain
{
    public interface IMyAppDomain
    {
        AppDomain MAppDomain { get;}
        Assembly[] GetAssemblies();
        IPlugin GetObjectPlugin(string assemblyFullName, string s1);
        void UnloadAppDomain();
    }
}