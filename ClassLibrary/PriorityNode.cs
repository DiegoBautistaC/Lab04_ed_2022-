using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class PriorityNode <T>
    {
        public T Value { get; set; }
        public int Priority { get; set; }
        public int Height { get; set; }

        public PriorityNode<T> Left;

        public PriorityNode<T> Rigth;

        public PriorityNode(T value)
        {
            this.Value = value;
            this.Priority = 0;
            this.Height = 1;
            this.Left = null;
            this.Rigth = null;
        }
    }
}
