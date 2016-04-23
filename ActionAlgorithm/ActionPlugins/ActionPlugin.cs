using System;
using System.ComponentModel;

namespace ActionAlgorithm.ActionPlugins
{
    public class ActionPlugin: IActionPlugin
    {

        public TypeOperation Operation { get; set; }
        public string Class { get; set; }
        public string Assembly { get; set; }

        public ActionPlugin() : this(TypeOperation.Median, null, null)
        {
        }

        public ActionPlugin(TypeOperation operation, string _class, string assembly)
        {
            if (_class == null) throw new ArgumentNullException(nameof(_class));
            if (assembly == null) throw new ArgumentNullException(nameof(assembly));
            if (!Enum.IsDefined(typeof(TypeOperation), operation))
                throw new InvalidEnumArgumentException(nameof(operation), (int) operation, typeof(TypeOperation));

            Operation = operation;
            Class = _class;
            Assembly = assembly;
        }

    }
}