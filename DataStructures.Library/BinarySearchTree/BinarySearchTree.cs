using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace DataStructures.Library
{
    public class BinarySearchTree<T> : IBinarySearchTree<T> where T : IComparable, IComparable<T>
    {
        public bool IsEmpty => Size == 0;

        public int Size { get; private set; } = 0;

        public int Height => GetHeight(root);

        private int GetHeight(Node node)
        {
            if (node == null) return 0;
            return Math.Max(GetHeight(node.Left), GetHeight(node.Right)) + 1;
        }

        public BinarySearchTree() { }

        public BinarySearchTree(ICollection<T> collection)
        {
            foreach (var item in collection) Add(item);
        }

        public bool Add(T item)
        {
            var node = new Node(item);

            if (Contains(item)) return false;

            root = AddNode(item, root);
            Size++;
            return true;
        }

        private Node AddNode(T item, Node node)
        {
            if (node == null) return new Node(item);

            if (item.CompareTo(node.Data) < 0) node.Left = AddNode(item, node.Left);
            else node.Right = AddNode(item, node.Right);

            return node;
        }

        private Node Find(T item, Node node)
        {
            if (node == null) return null;
            var c = item.CompareTo(node.Data);
            if (c == 0) return node;
            if (c < 0) return Find(item, node.Left);
            return Find(item, node.Right);
        }

        public bool Contains(T item)
        {
            return Find(item, root) != null;
        }

        public bool Remove(T item)
        {
            var nodeToRemove = Find(item, root);

            if (nodeToRemove == null) return false;

            root = RemoveNode(item, root);

            Size--;
            return true;
        }

        private Node RemoveNode(T item, Node node)
        {
            if (node == null) return null;

            var c = item.CompareTo(node.Data);

            if (c < 0) node.Left = RemoveNode(item, node.Left);
            else if (c > 0) node.Right = RemoveNode(item, node.Right);
            else // This is the node to remove
            {
                if (node.Left == null) return node.Right;
                else if (node.Right == null) return node.Left;
                else // Node has both left and right children...
                {
                    var n = FindMin(node.Right);

                    node.Data = n.Data;

                    node.Right = RemoveNode(n.Data, node.Right);
                }
            }

            return node;
        }

        private Node FindMin(Node node)
        {
            return node.Left == null ? node : FindMin(node.Left);
        }


        public IEnumerable<T> Traverse(TreeTraversalOrder order)
        {
            switch (order)
            {
                case TreeTraversalOrder.PRE_ORDER:
                    foreach (var i in InOrderTraversal(root, order)) yield return i;
                    break;
                case TreeTraversalOrder.IN_ORDER:
                    foreach (var i in InOrderTraversal(root, order)) yield return i;
                    break;
                case TreeTraversalOrder.POST_ORDER:
                    foreach (var i in InOrderTraversal(root, order)) yield return i;
                    break;
                case TreeTraversalOrder.LEVEL_ORDER:
                    foreach (var i in LevelOrderTraversal()) yield return i;
                    break;
                default:
                    throw new NotImplementedException();
            }
            //if (order == TreeTraversalOrder.PRE_ORDER
            //    || order == TreeTraversalOrder.IN_ORDER
            //    || order == TreeTraversalOrder.POST_ORDER)
            //{
            //    foreach (var i in InOrderTraversal(root, order)) yield return i;
            //}
            //else
            //{
            //    throw new NotImplementedException();
            //}
        }

        private IEnumerable<T> InOrderTraversal(Node node, TreeTraversalOrder order)
        {
            if (node == null) yield break;
            if (order == TreeTraversalOrder.PRE_ORDER) yield return node.Data;
            foreach (var i in InOrderTraversal(node.Left, order)) yield return i;
            if (order == TreeTraversalOrder.IN_ORDER) yield return node.Data;
            foreach (var i in InOrderTraversal(node.Right, order)) yield return i;
            if (order == TreeTraversalOrder.POST_ORDER) yield return node.Data;
        }

        private IEnumerable<T> LevelOrderTraversal()
        {
            var q = new Queue<Node>();
            q.Enqueue(root);
            while (q.Count > 0)
            {
                var node = q.Dequeue();
                yield return node.Data;
                if (node.Left != null) q.Enqueue(node.Left);
                if (node.Right != null) q.Enqueue(node.Right);
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            foreach (var i in Traverse(TreeTraversalOrder.IN_ORDER)) yield return i;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private Node root;

        private class Node
        {
            public T Data { get; set; }
            public Node Left { get; set; }
            public Node Right { get; set; }

            public Node(T item) : this(item, null, null) { }
            public Node(T item, Node left, Node right)
            {
                this.Data = item;
                this.Left = left;
                this.Right = right;
            }
        }
    }
}
