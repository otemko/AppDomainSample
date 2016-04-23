using System;
using System.Reflection;
using PluginInterface;

namespace ActionAlgorithm.MyAppDomain
{
    [Serializable]
    public sealed class SingletonAppDomain: IMyAppDomain
    {
        private static SingletonAppDomain instance;
        
        public AppDomain MAppDomain { get; private set; }

        static SingletonAppDomain()
        {

        }

        private SingletonAppDomain()
        {
            MAppDomain = AppDomain.CreateDomain("pluginsDomain");
            AppDomain.CurrentDomain.ReflectionOnlyAssemblyResolve += CurrentDomain_ReflectionOnlyAssemblyResolve; 
        }
        
        
        private Assembly CurrentDomain_ReflectionOnlyAssemblyResolve(object sender, ResolveEventArgs args)
        {
            return Assembly.ReflectionOnlyLoad(args.Name);
        }

        public static SingletonAppDomain GetInstance()
        {
            return instance ?? (instance = new SingletonAppDomain());
        }

        public Assembly[] GetAssemblies()
        {
            return MAppDomain.GetAssemblies();
        }

        public IPlugin GetObjectPlugin(string assemblyFullName, string _class)
        {
            if (assemblyFullName == null) throw new ArgumentNullException(nameof(assemblyFullName));

            var refAsm = Assembly.ReflectionOnlyLoadFrom(assemblyFullName);
            if (refAsm == null) return null;

            var pluginType = refAsm.GetType(string.Concat(assemblyFullName.Split('.')[0], ".", _class));
            
            return (IPlugin)MAppDomain.CreateInstanceFromAndUnwrap(refAsm.Location, pluginType.FullName);
        }

        public void UnloadAppDomain()
        {
            AppDomain.Unload(MAppDomain);
        }
    }
}