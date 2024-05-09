using Microsoft.VisualBasic.FileIO;
using System.Collections;
using System.Reflection.Metadata.Ecma335;
using System.Text.RegularExpressions;

namespace AVLTree
{
    public class AVL<TKey, TValue> : ICloneable, IEnumerable<KeyValuePair<TKey, TValue>>, IDictionary<TKey, TValue>,
        ICollection<KeyValuePair<TKey, TValue>> where TKey : notnull, IComparable<TKey> where TValue : notnull
    {
        private Node<TKey, TValue>? root;

        private int count;

        public int Count => count;

        public AVL() { }

        public void Add(TKey key, TValue value) { try { Add(new KeyValuePair<TKey, TValue>(key, value)); } catch { throw; } }

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            if (root == null) { root = new(item); count++; return; }
            try { Add(item, root); } catch { throw; }
        }

        private void Add(KeyValuePair<TKey, TValue> item, Node<TKey, TValue> node)
        {
            switch(node.Entry.Key.CompareTo(item.Key))
            {
                case 0:
                    throw new ArgumentException("Entry with such key already exists");
                case 1:
                    if (node.Left == null)
                    {
                        node.Left = new(item);
                        count++;
                    }
                    else
                        Add(item, node.Left);
                    break;

                case -1:
                    if (node.Right == null)
                    {
                        node.Right = new(item);
                        count++;
                    }
                    else
                        Add(item, node.Right);
                    break;
            }
            UpdateHight(node);
            Balance(node);
        }

        private static void Balance(Node<TKey, TValue> node)
        {
            int balance = GetBalance(node);
            if (balance == 2)
            {
                if (GetBalance(node.Right) == -1)
                    RightRotate(node.Right);
                LeftRotate(node);
            }
            else if (balance == -2)
            {
                if (GetBalance(node.Left) == 1)
                    LeftRotate(node.Left);
                RightRotate(node);
            }
        }

        private static void RightRotate(Node<TKey, TValue> node)
        {
            Swap(node, node.Left);
            Node<TKey, TValue>? buffer = node.Right;
            node.Right = node.Left;
            node.Left = node.Right.Left;
            node.Right.Left = node.Right.Right;
            node.Right.Right = buffer;
            UpdateHight(node.Right);
            UpdateHight(node);
        }

        private static void LeftRotate(Node<TKey, TValue> node)
        {
            Swap(node, node.Right);
            Node<TKey, TValue>? buffer = node.Left;
            node.Left = node.Right;
            node.Right = node.Left.Right;
            node.Left.Right = node.Left.Left;
            node.Left.Left = buffer;
            UpdateHight(node.Left);
            UpdateHight(node);
        }

        private static void UpdateHight(Node<TKey, TValue> node)
        {
            node.Hight = Math.Max(node.Left == null ? -1 : node.Left.Hight, node.Right == null ? -1 : node.Right.Hight) + 1;
        }

        private static int GetBalance(Node<TKey, TValue> node)
        {
            return (node.Right == null ? -1 : node.Right.Hight) - (node.Left == null ? -1 : node.Left.Hight);
        }
        
        private static void Swap(Node<TKey, TValue> firstNode, Node<TKey, TValue> secondNode)
        {
            (firstNode.Entry, secondNode.Entry) = (secondNode.Entry, firstNode.Entry);
        }

        public void Clear()
        {
            root = null;
            count = 0;
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            var current = root;
            while (current != null)
            {
                if (current.Entry.Key.Equals(item.Key) && current.Entry.Value.Equals(item.Value))
                    return true;
                switch (current.Entry.Key.CompareTo(item.Key))
                {
                    case 0:
                        return false;
                    case 1:
                        current = current.Left;
                        break;
                    case -1:
                        current = current.Right;
                        break;
                }
            }
            return false;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<KeyValuePair<TKey,TValue>> GetEnumerator()
        {
            return InOrderEmumerator(root).GetEnumerator();
        }

        private static IEnumerable<KeyValuePair<TKey, TValue>> InOrderEmumerator(Node<TKey, TValue> node)
        {
            if (node != null)
            {
                foreach (var item in InOrderEmumerator(node.Left))
                {
                    yield return item;
                }
                yield return node.Entry;
                foreach (var item in InOrderEmumerator(node.Right))
                { 
                    yield return item;
                }
            }
        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            if (root == null)
                return false;

            if (Contains(item))
            {
                bool isDeleted = false;
                root = Remove(root, item.Key, ref isDeleted);
                return isDeleted;
            }

            return false;
        }

        public bool Remove(TKey key)
        {
            if (root == null)
                return false;
            bool isDeleted = false;
            root = Remove(root, key, ref isDeleted);
            return isDeleted;
        }

        private Node<TKey, TValue>? Remove(Node<TKey, TValue>? node, TKey key, ref bool isDeleted)
        {
            if (node == null)
                return null;
            
            switch(node.Entry.Key.CompareTo(key))
            {
                case 0:
                    if (node.Left == null || node.Right == null)
                    {
                        node = node.Right ?? node.Left;
                        count--;
                    }
                    else
                    {
                        var min = GetMin(node.Right);
                        node.Entry = min.Entry;
                        node.Right = Remove(node.Right, min.Entry.Key, ref isDeleted);
                    }
                    isDeleted = true;
                    break;
                case 1:
                    node.Left = Remove(node.Left, key, ref isDeleted);
                    break;
                case -1:
                    node.Right = Remove(node.Right, key, ref isDeleted);
                    break;
            }

            if (node == null)
                return null;

            UpdateHight(node);
            Balance(node);
            return node;
        }

        private static Node<TKey, TValue> GetMin(Node<TKey, TValue> node)
        {
            return node.Left == null ? node : GetMin(node.Left);
        }
    }
}
