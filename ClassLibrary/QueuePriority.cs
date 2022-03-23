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

        PriorityNode<T> LastFather;

        public delegate int PrimerDelegado(T value);
        PrimerDelegado PriorityFunc;

        public delegate void SegundoDelegado(T v1, T v2);
        SegundoDelegado Comparator1;

        public QueuePriority(PrimerDelegado priorityFunc, SegundoDelegado comparator1)
        {
            this.Root = null;
            this.LastFather = null;
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
                    actualNode.Height = actualNode.Left.Height + 1;
                    this.LastFather = actualNode;
                }
                else if (actualNode.Rigth == null)
                {
                    actualNode.Rigth = newNode;
                    actualNode.Height = Math.Max(actualNode.Left.Height, actualNode.Rigth.Height);
                    this.LastFather = actualNode;
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

        /// <summary>
        /// Método para remover de la cola de prioridad al primer elemento (raiz) con mayor prioridad
        /// x = padre del último valor insertado
        /// xS = último valor insertado
        /// y = raiz
        /// </summary>
        /// <returns>El valor removido de la cola de prioridad</returns>
        public T Remove()
        {
            if (this.Root != null)
            {
                if (this.Root.Left == null && this.Root.Rigth == null)
                {
                    PriorityNode<T> aux = this.Root;
                    this.Root = null;
                    return aux.Value;
                }
                else
                {
                    var x = this.Root;
                    PriorityNode<T> y;
                    if (this.LastFather.Rigth != null)
                    {
                        y = this.LastFather.Rigth;
                        this.LastFather.Rigth = null;
                    }
                    else
                    {
                        y = this.LastFather.Left;
                        this.LastFather.Left = null;
                    }
                    y.Left = x.Left;
                    y.Rigth = x.Rigth;
                    this.Root = y
                }
            }
            return default(T);
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
