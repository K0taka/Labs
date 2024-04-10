//Work in progress
namespace lab
{
    public class MyHashTable<TKey, TValue>: IDisposable where TValue : ICloneable, IDisposable
    {
        readonly HashTableNode<TValue>?[] table;

        public int Count => table.Length;

        public MyHashTable(int capacity = 16)
        {
            table = new HashTableNode<TValue>[capacity];
        }

        private int GetIndex(TKey element) => Math.Abs(element == null ? 0 : element.GetHashCode()) % Count;

        public void Clear()
        {
            Dispose();
        }

        public void Dispose()
        {
            foreach (var element in table)
            {
                HashTableNode<TValue>? curr = element;
                while (curr != null)
                {
                    HashTableNode<TValue> next = curr;
                    curr.Dispose();
                    curr = next;
                }
            }
            GC.SuppressFinalize(this);
        }

        public bool Add(TKey key, TValue element)
        {
            int index = GetIndex(key);
            if (table[index] == null)
            {
                table[index] = new((TValue)element.Clone());
                return true;
            }

            var curr = table[index];
            table[index] = new((TValue)element.Clone(), next: curr, chainLen: curr!.ChainLen + 1);
            
            return true;
        }

        public bool Contains(TKey key)
        {
            return table[GetIndex(key)] != null;
        }

        public bool Remove(TKey key)
        {
            HashTableNode<TValue>? curr, next;
            (curr, next) = FindNode(key);
            if (curr == null)
                return false;
            table[GetIndex(key)] = next;
            curr.Dispose();
            return true;
        }

        private (HashTableNode<TValue>?, HashTableNode<TValue>?) FindNode(TKey key)
        {
            int index = GetIndex(key);
            HashTableNode<TValue>? curr = table[index];
            HashTableNode<TValue>? next = curr?.Next;
            return (curr, next);
        }

        public TValue[] this[TKey key]
        {
            get
            {
                if (table == null)
                    throw new InvalidOperationException();
                if (table[GetIndex(key)] == null)
                    throw new ArgumentException("Key не существует в таблице");
                return MakeArray(key);
            }
        }

        private TValue[] MakeArray(TKey key)
        {
            var curr = table[GetIndex(key)];
            if (curr == null)
                return [];
            TValue[] arr = new TValue[curr.ChainLen];
            while (curr != null)
            {
                arr[^curr.ChainLen] = curr.Data;
                curr = curr.Next;
            }
            return arr;
        }

        ~MyHashTable()
        {
            Dispose();
        }
    }
}
