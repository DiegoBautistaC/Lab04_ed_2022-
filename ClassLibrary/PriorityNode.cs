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
        PriorityNode<T> Left;
        PriorityNode<T> Rigth;

        public PriorityNode(T value)
        {
            this.Value = value;
            this.Left = null;
            this.Rigth = null;
        }
    }
}
