using System;

namespace PluginInterface
{
    public enum Complexity { Linear, Quadratic }

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public sealed class PluginAttribute : Attribute
    {

        private readonly bool _asyncOperation = false;
        private readonly string _name;
        private readonly Complexity _complexity;
        public string Name => _name;

        public bool AsynsOperation => _asyncOperation;
        public PluginAttribute(string name)
            : this(name, Complexity.Linear)
        {
        }

        public PluginAttribute(string name, Complexity complexity)
        {
            _name = name;
            _complexity = complexity;
        }
        public PluginAttribute(string name, Complexity complexity, bool asyncOperation)
        {
            _name = name;
            _complexity = complexity;
            _asyncOperation = _asyncOperation;
        }

        public PluginAttribute(string name, bool asyncOperation)
            : this(name, Complexity.Linear)
        {
            _asyncOperation = asyncOperation;
        }

        
    }
}
