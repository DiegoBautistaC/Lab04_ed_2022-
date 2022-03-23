using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class QueuePriority <T>
    {
        public PriorityNode<T> Root;

        public delegate int PrimerDelegado(T value);
        PrimerDelegado PriorityFunc;

        public delegate void SegundoDelegado(T v1, T v2);
        SegundoDelegado Comparator1;

        public QueuePriority(PrimerDelegado priorityFunc, SegundoDelegado comparator1)
        {
            this.PriorityFunc = priorityFunc;
            this.Comparator1 = comparator1;
        }

        public bool Insert()
        {

        }

        public bool 
    }
}
