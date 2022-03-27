﻿using System;
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

        public int Length { get; set; }

        public delegate int PrimerDelegado(T value);
        PrimerDelegado PriorityFunc;

        public delegate bool SegundoDelegado(T v1, T v2);
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
            Insert(value, BinaryPosition(this.Length + 1), ref this.Root);
            return true;
        }

        public void Insert(T value, string position, ref PriorityNode<T> actualNode)
        {
            PriorityNode<T> newNode = new PriorityNode<T>(value);
            if (actualNode == null)
            {
                actualNode = newNode;
                this.Length++;
            }
            else
            {
                position = position.Substring(1);
                int place = Convert.ToInt32(position.Substring(0,1));
                if (place == 0)
                {
                    this.Insert(value, position, ref actualNode.Left);
                }
                else
                {
                    this.Insert(value, position, ref actualNode.Rigth);
                }
        }
        }

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
        /// Método para remover de la cola de prioridad al primer elemento (raiz) con mayor prioridad
        /// x = raiz
        /// y = último valor insertado
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
                    this.Root = y;
                    this.OrdenInvarianteDown(ref this.Root);
                    
                }
            }
            return default(T);
        }

        /// <summary>
        /// Función recursiva que recupera el orden invariante luego de haber eliminado el primer elemento de la cola de prioridad
        /// </summary>
        /// <param name="actualNode">Nodo utilizado para comparar la prioridad con sus hijos izquierdo y derecho</param>
        void OrdenInvarianteDown(ref PriorityNode<T> actualNode)
        {
            if (actualNode.Rigth != null && actualNode.Left != null)
            {
                if (Comparator1(actualNode.Value, actualNode.Left.Value) || Comparator1(actualNode.Value, actualNode.Rigth.Value))
                {
                    if (Comparator1(actualNode.Left.Value, actualNode.Rigth.Value))
                    {
                        T aux = actualNode.Value;
                        actualNode.Value = actualNode.Left.Value;
                        actualNode.Left.Value = aux;
                        this.OrdenInvarianteDown(ref actualNode.Left);
                    }
                    else
                    {
                        T aux = actualNode.Value;
                        actualNode.Value = actualNode.Rigth.Value;
                        actualNode.Rigth.Value = aux;
                        this.OrdenInvarianteDown(ref actualNode.Rigth);
                    }
                }
            }
            else if (actualNode.Rigth == null)
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
