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
            this.Root = null;
            this.PriorityFunc = priorityFunc;
            this.Comparator1 = comparator1;
        }

        public bool Insert(T value)
        {
            return true;
        }

        public bool Insert(T value, ref PriorityNode<T> actualNode)
        {
            PriorityNode<T> newNode = new PriorityNode<T>(value);
            if (!this.IsEmpty())
            {
                if (actualNode.Left == null && actualNode.Rigth == null)
                {
                    actualNode.Left = newNode;
                }
                else if (actualNode.Rigth == null)
                {
                    actualNode.Rigth = newNode;
                }
                else
                {
                    if (actualNode.Left.Height != actualNode.Rigth.Height)
                    {

                    }
                    else
                    {
                        actualNode.Left.Left = newNode;
                    }
                }
                return true;
            }
            else
            {
                
                this.Root = newNode;
                return true;
            }
        }

        public bool Remove()
        {
            //Falta implementación
            return true;
        }

        public T Consulta()
        {
            if (this.Root != null)
            {
                return this.Root.Value;
            }
            return default(T);
        }

        bool IsEmpty()
        {
            return this.Root != null;
        }
    }
}
