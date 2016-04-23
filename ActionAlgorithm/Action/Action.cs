using System;
using System.ComponentModel;

namespace ActionAlgorithm.Action
{
    public class Action: IAction 
    {
        public TypeOperation Operation { get; set; }
        public double[] Data { get; set; }

        public Action(): this(TypeOperation.Median, new double[] { })
        {
        }

        public Action(double[] data) : this(TypeOperation.Median, data)
        {
        }

        public Action(TypeOperation operation) : this(operation, new double[] {})
        {
        }

        public Action(TypeOperation operation, double[] data)
        {
            if (data == null) throw new ArgumentNullException(nameof(data));
            if (!Enum.IsDefined(typeof(TypeOperation), operation))
                throw new InvalidEnumArgumentException(nameof(operation), (int) operation, typeof(TypeOperation));

            this.Operation = operation;
            this.Data = data;
        }
    }
}