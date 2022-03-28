using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class CustomQueue<T>
    {
        public class Node<T>
        {
            public T Value { get; set; }
            public Node<T> Next;

            public Node(T value)
            {
                this.Value = value;
                this.Next = null;
            }
        }

        public static Node<T> Head;
        public static Node<T> Queue;
        public static int Tamaño;

        public CustomQueue()
        {
            Head = null;
            Queue = null;
            Tamaño = 0;
        }

        public bool Encolar(T value)
        {
            Node<T> newValue = new Node<T>(value);
            if (Tamaño == 0)
            {
                Head = newValue;
            }
            else
            {
                Queue.Next = newValue;
            }
            Queue = newValue;
            Tamaño++;
            return true;
        }

        public T Desencolar()
        {
            T valor = Head.Value;
            if (Tamaño == 1)
            {
                Head = null;
                Queue = null;
            }
            else
            {
                Head = Head.Next;
            }
            Tamaño--;
            return valor;
        }

        public bool IsEmpty()
        {
            return Tamaño == 0;
        }
    }
}
