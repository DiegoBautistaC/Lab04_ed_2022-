using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class QueuePriority <T> : IEnumerable<T>
    {
        public PriorityNode<T> Root;

        public int Length { get; set; }

        public delegate int PrimerDelegado(T value);
        PrimerDelegado PriorityFunc;

        public delegate bool SegundoDelegado(PriorityNode<T> v1, PriorityNode<T> v2);
        SegundoDelegado Comparator;

        public delegate bool TercerDelegado(T v1, T v2);
        TercerDelegado Comparator2;

        public QueuePriority(PrimerDelegado priorityFunc, SegundoDelegado comparator1, TercerDelegado comparator2)
        {
            this.Root = null;
            this.PriorityFunc = priorityFunc;
            this.Comparator = comparator1;
            this.Comparator2 = comparator2;
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
                        actualNode.Priority = this.PriorityFunc(actualNode.Value);
                        actualNode.Left.Value = aux;
                        actualNode.Left.Priority = this.PriorityFunc(actualNode.Left.Value);
                    }
                    else
                    {
                        actualNode.Value = actualNode.Rigth.Value;
                        actualNode.Priority = this.PriorityFunc(actualNode.Value);
                        actualNode.Rigth.Value = aux;
                        actualNode.Rigth.Priority = this.PriorityFunc(actualNode.Rigth.Value);
                    }
                }
            }
            else if (actualNode.Rigth == null && Comparator(actualNode.Left, actualNode))
            {
                T aux = actualNode.Value;
                actualNode.Value = actualNode.Left.Value;
                actualNode.Priority = this.PriorityFunc(actualNode.Value);
                actualNode.Left.Value = aux;
                actualNode.Left.Priority = this.PriorityFunc(actualNode.Left.Value);
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
                    T eliminate = this.Root.Value;
                    this.Root = null;
                    this.Length--;
                    return eliminate;
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
                            this.Root.Priority = this.PriorityFunc(this.Root.Value);
                            this.InvariantSortToDown(ref this.Root);
                            this.Length--;
                            return eliminate;
                        }
                        else
                        {
                            PriorityNode<T> aux = actualNode.Rigth;
                            actualNode.Rigth = null;
                            aux.Left = this.Root.Left;
                            aux.Rigth = this.Root.Rigth;
                            this.Root = aux;
                            this.Root.Priority = this.PriorityFunc(this.Root.Value);
                            this.InvariantSortToDown(ref this.Root);
                            this.Length--;
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

        // Método recursivo utilizado para ordenar de forma invariante la cola de prioridad luego de haber eliminado el elemento con mayor prioridad
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
                        actualNode.Priority = this.PriorityFunc(actualNode.Value);
                        actualNode.Left.Value = aux;
                        actualNode.Left.Priority = this.PriorityFunc(actualNode.Left.Value);
                        this.InvariantSortToDown(ref actualNode.Left);
                    }
                    else
                    {
                        actualNode.Value = actualNode.Rigth.Value;
                        actualNode.Priority = this.PriorityFunc(actualNode.Value);
                        actualNode.Rigth.Value = aux;
                        actualNode.Rigth.Priority = this.PriorityFunc(actualNode.Rigth.Value);
                        this.InvariantSortToDown(ref actualNode.Left);
                    }
                }
            }
            else if (actualNode.Rigth == null && actualNode.Left != null && Comparator(actualNode.Left, actualNode))
            {
                T aux = actualNode.Value;
                actualNode.Value = actualNode.Left.Value;
                actualNode.Priority = this.PriorityFunc(actualNode.Value);
                actualNode.Left.Value = aux;
                actualNode.Left.Priority = this.PriorityFunc(actualNode.Left.Value);
            }
        }

        /// <summary>
        /// Función para consultar el valor que se encuentra delante de la cola con mayor prioridad
        /// </summary>
        /// <returns>Primero valor con mayor prioridad en la cola de prioridad</returns>
        public T Consulta(PriorityNode<T> actualNode, string position)
        {
            if (!this.IsEmpty())
            {
                if (position.Length == 1)
                {
                    return actualNode.Value;
                }
                else
                {
                    position = position.Substring(1);
                    int place = Convert.ToInt32(position.Substring(0, 1));
                    if (place == 0)
                    {
                        return Consulta(actualNode.Left, position);
                    }
                    else
                    {
                        return Consulta(actualNode.Rigth, position);
                    }
                }
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

        void Read(ref CustomQueue<T> queue, QueuePriority<T> colaPrioridad)
        {
            queue.Encolar(colaPrioridad.Remove(BinaryPosition(colaPrioridad.Length), ref colaPrioridad.Root));
            if (!colaPrioridad.IsEmpty())
            {
                Read(ref queue, colaPrioridad);
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            if (!this.IsEmpty())
            {
                var cola = new CustomQueue<T>();
                QueuePriority<T> colaPrioridad = new QueuePriority<T>(this.PriorityFunc, this.Comparator, this.Comparator2);
                colaPrioridad.Root = this.Root;
                colaPrioridad.Length = this.Length;
                colaPrioridad.Read(ref cola, colaPrioridad);
                this.Root = null;
                this.Length = 0;
                while (!cola.IsEmpty())
                {
                    var value = cola.Desencolar();
                    this.Insert(value);
                    yield return value;
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
