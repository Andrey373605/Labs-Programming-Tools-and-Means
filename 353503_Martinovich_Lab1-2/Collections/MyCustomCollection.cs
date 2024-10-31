using _353503_Martinovich_Lab1.Interfaces;
using _353503_Martinovich_Lab1.Exceptions;

namespace _353503_Martinovich_Lab1.Collections
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    internal class MyCustomCollection<T> : ICustomCollection<T>, IEnumerable<T>
    {
        private class Node
        {
            public T Data;
            public Node? Next;

            public Node(T data)
            {
                Data = data;
                Next = null;
            }
        }

        private Node? head;
        private Node? current;
        private int count;

        public MyCustomCollection()
        {
            head = null;
            current = head;
            count = 0;
        }

        public T this[int index]
        {
            get
            {
                Node? node = GetNodeAt(index);
                return node != null ? node.Data : throw new IndexOutOfRangeException();
            }
            set
            {
                Node? node = GetNodeAt(index);
                if (node != null)
                {
                    node.Data = value;
                }
                else
                {
                    throw new IndexOutOfRangeException();
                }
            }
        }

        public int Count => count;

        public void Add(T item)
        {
            Node newNode = new Node(item);
            if (head == null)
            {
                head = newNode;
                current = newNode;
            }
            else
            {
                Node last = head;
                while (last.Next != null)
                {
                    if (last.Data.Equals(item)) return;
                    last = last.Next;
                }
                last.Next = newNode;
            }
            count++;
        }

        public T Current()
        {
            return current != null ? current.Data : throw new IndexOutOfRangeException();
        }

        public bool CurrentIsNull()
        {
            return current == null;
        }

        public void Next()
        {
            if (current != null)
            {
                current = current.Next;
            }
        }

        public void Remove(T item)
        {
            Node? current_node = head;
            Node? previous = null;
            if (current_node == null) return;

            while (current_node != null)
            {
                if (current_node.Data.Equals(item))
                {
                    if (previous == null)
                    {
                        head = current_node.Next;
                    }
                    else
                    {
                        previous.Next = current_node.Next;
                    }
                    count--;
                    break;
                }
                previous = current_node;
                current_node = current_node.Next;
            }

            Reset();
            throw new MyCustomException("No items");
        }

        public T RemoveCurrent()
        {
            if (current == null)
            {
                throw new InvalidOperationException("Current is null");
            }

            T data = current.Data;
            Remove(data);
            return data;
        }

        public void Reset()
        {
            current = head;
        }

        private Node? GetNodeAt(int index)
        {
            if (index < 0 || index >= count)
            {
                return null;
            }
            else
            {
                Node node = head;
                for (int i = 0; i < index; i++)
                {
                    node = node.Next;
                }
                return node;
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new MyCustomEnumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private class MyCustomEnumerator : IEnumerator<T>
        {
            private MyCustomCollection<T> collection;
            private Node? currentNode;
            private bool started;

            public MyCustomEnumerator(MyCustomCollection<T> collection)
            {
                this.collection = collection;
                this.currentNode = null;
                this.started = false;
            }

            public T Current
            {
                get
                {
                    if (currentNode == null)
                    {
                        throw new InvalidOperationException();
                    }
                    return currentNode.Data;
                }
            }

            object IEnumerator.Current => Current;

            public bool MoveNext()
            {
                if (!started)
                {
                    currentNode = collection.head;
                    started = true;
                }
                else if (currentNode != null)
                {
                    currentNode = currentNode.Next;
                }

                return currentNode != null;
            }

            public void Reset()
            {
                currentNode = null;
                started = false;
            }

            public void Dispose()
            {
            }
        }
    }

}
