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

        public int Length { get; set; }

        public delegate int PrimerDelegado(T value);
        PrimerDelegado PriorityFunc;

        public delegate bool SegundoDelegado(PriorityNode<T> v1, PriorityNode<T> v2);
        SegundoDelegado Comparator;

        public QueuePriority(PrimerDelegado priorityFunc, SegundoDelegado comparator1)
        {
            this.Root = null;
            this.PriorityFunc = priorityFunc;
            this.Comparator = comparator1;
        }

        //Método invocable para insertar valores que recibe como parámetro el elemento a insertar.
        public void Insert(T value)
        {
            PriorityNode<T> newNode = new PriorityNode<T>(value);
            newNode.Priority = this.PriorityFunc(value);
            Insert(newNode, BinaryPosition(this.Length + 1), ref this.Root);
        }

        /// <summary>
        /// Método que inserta un elemento en el heap por medio de recursión y también posicionamiento binario.
        /// </summary>
        /// <param name="newNode">El nodo que contiene el valor que será incertado.</param>
        /// <param name="position">Inidicaciones binarias de hacia a qué lado insertar.</param>
        /// <param name="actualNode">La raíz actual que se está evaluando durante la ejecución</param>
        public void Insert(PriorityNode<T> newNode, string position, ref PriorityNode<T> actualNode)
        {
            if (actualNode == null)
            {
                actualNode = newNode;
                this.Length++;
            }
            else
            {
                position = position.Substring(1);
                int place = Convert.ToInt32(position.Substring(0, 1));
                if (place == 0)
                {
                    this.Insert(newNode, position, ref actualNode.Left);
                    this.InvariantSortToUp(ref actualNode);
                }
                else
                {
                    this.Insert(newNode, position, ref actualNode.Rigth);
                    this.InvariantSortToUp(ref actualNode);
                }
            }
        }

        // Conversión binaria de un número por medio de una función recrusiva 
        string BinaryPosition(int n)
        {
            if (n < 2)
            {
                return n.ToString();
            }
            else
            {
                return BinaryPosition(n / 2) + (n % 2).ToString();
            }
        }

        /// <summary>
        /// Método recursivo que recupera el orden invariante comparando la raiz con sus hijos por cada nodo que recibe como parámetro.
        /// </summary>
        /// <param name="actualNode">Nodo utilizado para comparar la prioridad con sus hijos izquierdo y derecho</param>
        void InvariantSortToUp(ref PriorityNode<T> actualNode)
        {
            if (actualNode.Rigth != null && actualNode.Left != null)
            {
                if (Comparator(actualNode.Left, actualNode) || Comparator(actualNode.Rigth, actualNode))
                {
                    T aux = actualNode.Value;
                    if (Comparator(actualNode.Left, actualNode.Rigth))
                    {
                        actualNode.Value = actualNode.Left.Value;
                        actualNode.Left.Value = aux;
                    }
                    else
                    {
                        actualNode.Value = actualNode.Rigth.Value;
                        actualNode.Rigth.Value = aux;
                    }
                }
            }
            else if (actualNode.Rigth == null && Comparator(actualNode.Left, actualNode))
            {
                T aux = actualNode.Value;
                actualNode.Value = actualNode.Left.Value;
                actualNode.Left.Value = aux;
            }
        }

       //Método invocable para remover el elemnto con mayor prioridad de la cola de prioridad
        public T Remove()
        {
            return Remove(BinaryPosition(this.Length), ref this.Root);
        }

        /// <summary>
        /// Método que encuentra recursivamente el elemento que será eliminado y ejecuta el prodecimiento para mantener el orden y forma invariante
        /// </summary>
        /// <param name="position"></param>
        /// <param name="actualNode"></param>
        /// <returns></returns>
        T Remove(string position, ref PriorityNode<T> actualNode)
        {
            if (!this.IsEmpty())
            {
                if (position.Length == 1)
                {
                    T eleminate = this.Root.Value;
                    this.Root = null;
                    return eleminate;
                }
                else
                {
                    if (position.Length == 2)
                    {
                        position = position.Substring(1);
                        int place = Convert.ToInt32(position.Substring(0, 1));
                        T eliminate = this.Root.Value;
                        if (place == 0)
                        {
                            PriorityNode<T> aux = actualNode.Left;
                            actualNode.Left = null;
                            aux.Left = this.Root.Left;
                            aux.Rigth = this.Root.Rigth;
                            this.Root = aux;
                            this.InvariantSortToDown(ref this.Root);
                            return eliminate;
                        }
                        else
                        {
                            PriorityNode<T> aux = actualNode.Rigth;
                            actualNode.Rigth = null;
                            aux.Left = this.Root.Left;
                            aux.Rigth = this.Root.Rigth;
                            this.Root = aux;
                            this.InvariantSortToDown(ref this.Root);
                            return eliminate;
                        }
                    }
                    else
                    {
                        position = position.Substring(1);
                        int place = Convert.ToInt32(position.Substring(0, 1));
                        if (place == 0)
                        {
                            return this.Remove(position, ref actualNode.Left);
                        }
                        else
                        {
                            return this.Remove(position, ref actualNode.Rigth);
                        }
                    }
                }
            }
            return default(T);
        }

        // Método recursico utilizado para ordenar de forma invariante la cola de prioridad luego de haber eliminado el elemento con mayor prioridad
        void InvariantSortToDown(ref PriorityNode<T> actualNode)
        {
            if (actualNode.Rigth != null && actualNode.Left != null)
            {
                if (Comparator(actualNode.Left, actualNode) || Comparator(actualNode.Rigth, actualNode))
                {
                    T aux = actualNode.Value;
                    if (Comparator(actualNode.Left, actualNode.Rigth))
                    {
                        actualNode.Value = actualNode.Left.Value;
                        actualNode.Left.Value = aux;
                        this.InvariantSortToDown(ref actualNode.Left);
                    }
                    else
                    {
                        actualNode.Value = actualNode.Rigth.Value;
                        actualNode.Rigth.Value = aux;
                        this.InvariantSortToDown(ref actualNode.Left);
                    }
                }
            }
            else if (actualNode.Rigth == null && Comparator(actualNode.Left, actualNode))
            {
                T aux = actualNode.Value;
                actualNode.Value = actualNode.Left.Value;
                actualNode.Left.Value = aux;
            }
        }

        /// <summary>
        /// Función para consultar el valor que se encuentra delante de la cola con mayor prioridad
        /// </summary>
        /// <returns>Primero valor con mayor prioridad en la cola de prioridad</returns>
        public T Consulta()
        {
            if (this.Root != null)
            {
                return this.Root.Value;
            }
            return default(T);
        }

        /// <summary>
        /// Función utilizada para verificar el esatado de la cola
        /// </summary>
        /// <returns>Verdadero si la cola de prioridad está vacía y falso si la cola de prioridad tiene al menos un elemento/returns>
        bool IsEmpty()
        {
            return this.Root == null;
        }
    }
}
