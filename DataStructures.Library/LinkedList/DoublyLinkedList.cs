using System;
using System.Collections;
using System.Collections.Generic;

namespace DataStructures.Library
{
    public class DoublyLinkedList<T> : ILinkedList<T>
    {
        public int Length { get; private set; } = 0;

        public bool IsEmpty => Length == 0;

        public DoublyLinkedList()
        {
            _head.Next = _tail;
            _tail.Prev = _head;
        }

        public void Add(T item)
        {
            AddLast(item);
        }

        public void AddFirst(T item)
        {
            InsertAfter(_head, item);
        }

        public void AddLast(T item)
        {
            InsertAfter(Last, item);
        }

        public void Clear()
        {
            var curr = First;
            while (curr != _tail)
            {
                RemoveNode(curr);
                curr = First;
            }
        }

        public bool Contains(T item)
        {
            return (IndexOf(item) != -1);
        }

        public int IndexOf(T itemToFind)
        {
            return IndexOf(itemToFind, out _);
        }

        public T PeekFirst()
        {
            if (IsEmpty) throw new InvalidOperationException();

            return First.Item;
        }

        public T PeekLast()
        {
            if (IsEmpty) throw new InvalidOperationException();

            return Last.Item;
        }

        public bool Remove(T item)
        {
            Node nodeToRemove;

            var index = IndexOf(item, out nodeToRemove);

            if (index == -1) return false;

            RemoveNode(nodeToRemove);
            return true;
        }

        public T RemoveAt(int index)
        {
            return RemoveNode(NodeAt(index));
        }

        public T RemoveFirst()
        {
            if (IsEmpty) throw new InvalidOperationException();

            return RemoveNode(First);
        }

        public T RemoveLast()
        {

            if (IsEmpty) throw new InvalidOperationException();

            return RemoveNode(Last);
        }

        public IEnumerator<T> GetEnumerator()
        {
            var curr = First;

            while (curr != _tail)
            {
                yield return curr.Item;
                curr = curr.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private Node _head = new Node(default);
        private Node _tail = new Node(default);

        private Node First => _head.Next;
        private Node Last => _tail.Prev;

        private class Node
        {
            public T Item { get; set; }
            public Node Next { get; set; } = null;
            public Node Prev { get; set; } = null;

            public Node(T item)
            {
                Item = item;
            }

            public void Clear()
            {
                Item = default;
                Next = null;
                Prev = null;
            }
        }

        private void InsertAfter(Node node, T newItem)
        {
            var newNode = new Node(newItem);

            newNode.Next = node.Next;
            newNode.Prev = node;

            node.Next = newNode;
            newNode.Next.Prev = newNode;

            Length++;
        }

        private T RemoveNode(Node curr)
        {
            curr.Prev.Next = curr.Next;
            curr.Next.Prev = curr.Prev;

            var value = curr.Item;

            curr.Clear();
            curr = null;

            Length--;
            return value;
        }

        private int IndexOf(T itemToFind, out Node node)
        {
            node = null;
            if (IsEmpty) return -1;

            node = First;
            var index = 0;
            while (node != _tail)
            {
                if (node.Item.Equals(itemToFind))
                {
                    break;
                }
                node = node.Next;
                index++;
            }

            return (node == _tail) ? -1 : index;
        }

        private Node NodeAt(int index)
        {
            if (index < 0 || index >= Length) throw new IndexOutOfRangeException();

            if (IsEmpty) throw new InvalidOperationException();

            var node = First;
            for (var i = 0; i < index; i++) node = node.Next;

            return node;
        }
    }
}