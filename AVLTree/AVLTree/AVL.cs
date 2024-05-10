using System.Collections;
using System.Diagnostics.CodeAnalysis;

namespace AVLTree
{
    public class AVL<TKey, TValue> : ICloneable, IEnumerable<KeyValuePair<TKey, TValue>>, IDictionary<TKey, TValue>,
        ICollection<KeyValuePair<TKey, TValue>> where TKey : notnull, IComparable<TKey> where TValue : notnull
    {
        private Node<TKey, TValue>? root;

        private int count;

        public int Count => count;

        public bool IsReadOnly { get; set; }

        public ICollection<TKey> Keys
        {
            get
            {
                TKey[] keys = new TKey[count];
                int index = 0;
                foreach(var item in this)
                {
                    keys[index++] = item.Key is ICloneable key? (TKey)key.Clone() : item.Key;
                }

                return keys;
            }
        }

        public ICollection<TValue> Values
        {
            get
            {
                TValue[] values = new TValue[count];
                int index = 0;
                foreach (var item in this)
                {
                    values[index++] = item.Value is ICloneable value ? (TValue)value.Clone() : item.Value;
                }

                return values;
            }
        }

        public AVL() { }

        public AVL(AVL<TKey, TValue> tree)
        {
            if (typeof(TKey).GetInterfaces().Contains(typeof(ICloneable)))
            {
                TKey[] keys = (TKey[])tree.Keys;

                foreach(var item in tree.InWideEnumerator())
                {
                    int index = Array.IndexOf(keys, item.Key);
                    Add(keys[index], item.Value is ICloneable value ? (TValue)value.Clone() : item.Value);
                }   
            }
            else
            {
                foreach (var item in tree.InWideEnumerator())
                {
                    Add(item.Key, item.Value is ICloneable value ? (TValue)value.Clone() : item.Value);
                }
            }
        }

        public void Add(TKey key, TValue value) { try { Add(new KeyValuePair<TKey, TValue>(key, value)); } catch { throw; } }

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            if (IsReadOnly)
                throw new NotSupportedException();
            if (item.Key == null)
                throw new ArgumentNullException("key");
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
            if (IsReadOnly)
                throw new NotSupportedException();
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

        [ExcludeFromCodeCoverage]
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

        public IEnumerable<KeyValuePair<TKey,TValue>> InWideEnumerator()
        {
            if (root == null)
                yield break;
            else
            {
                Queue<Node<TKey, TValue>> nodes = new Queue<Node<TKey, TValue>>();
                nodes.Enqueue(root);
                while (nodes.Count > 0)
                {
                    var curr = nodes.Dequeue();
                    yield return curr.Entry;
                    if (curr.Left != null)
                        nodes.Enqueue(curr.Left);
                    if (curr.Right != null)
                        nodes.Enqueue(curr.Right);
                }
                yield break;
            }
        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            if (IsReadOnly)
                throw new NotSupportedException();
            if (item.Key == null)
                throw new ArgumentNullException("key");
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
            if (IsReadOnly)
                throw new NotSupportedException();
            if (key == null)
                throw new ArgumentNullException(nameof(key));
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

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int index)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));
            if (index < 0)
                throw new ArgumentOutOfRangeException(nameof(index));
            if (array.Length - index < count)
                throw new ArgumentException("Not enoght space in array");
            foreach(var item in this)
            {
                array[index++] = new(
                    item.Key is not ICloneable key ? item.Key : (TKey)key.Clone(),
                    item.Value is not ICloneable value ? item.Value : (TValue)value.Clone());
            }
        }

        public TValue this[TKey key]
        {
            get
            {
                if (key == null)
                    throw new ArgumentNullException(nameof(key));
                var current = root;
                while (current != null)
                {
                    switch(current.Entry.Key.CompareTo(key))
                    {
                        case 0:
                            return current.Entry.Value;
                        case 1:
                            current = current.Left;
                            break;
                        case -1:
                            current = current.Right;
                            break;
                    }
                }
                throw new KeyNotFoundException();
            }

            set
            {
                if (IsReadOnly)
                    throw new NotSupportedException();
                if (key == null)
                    throw new ArgumentNullException(nameof(key));
                var current = root;
                while (current != null)
                {
                    switch (current.Entry.Key.CompareTo(key))
                    {
                        case 0:
                            current.Entry = new KeyValuePair<TKey, TValue>(key, value);
                            return;
                        case 1:
                            current = current.Left;
                            break;
                        case -1:
                            current = current.Right;
                            break;
                    }
                }
                Add(key, value);
            }
        }

        public bool ContainsKey(TKey key)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));
            var current = root;
            while (current != null)
            {
                switch (current.Entry.Key.CompareTo(key))
                {
                    case 0:
                        return true;
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

        public bool TryGetValue(TKey key, out TValue value)
        {
            try
            {
                value = this[key];
                return true;
            }
            catch
            {
                value = default;
                return false;
            }
        }

        public object Clone()
        {
            return new AVL<TKey, TValue>(this);
        }

        public override bool Equals(object? obj)
        {
            if (obj is not AVL<TKey, TValue> tree)
                return false;
            if (tree.Count != count)
                return false;
            var thisArray = this.ToArray();
            var treeArray = tree.ToArray();
            for (var i = 0;  i < count; i++)
            {
                if (thisArray[i].Key.Equals(treeArray[i].Key) && thisArray[i].Value.Equals(treeArray[i].Value))
                    continue;
                return false;
            }
            return true;
        }
    }
}
