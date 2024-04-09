//Work in progress
namespace lab
{
    public class MyHashTable<T>: IDisposable where T : ICloneable, IDisposable
    {
        readonly HashTableNode<T>[] table;

        public int Count => table.Length;

        public MyHashTable(int capacity = 16)
        {
            table = new HashTableNode<T>[capacity];
        }

        private int GetIndex(T element) => Math.Abs(element.GetHashCode()) % Count;

        public void Dispose()
        {
            foreach (var element in table)
            {
                HashTableNode<T> curr = element;
                while (curr != null)
                {
                    curr.Dispose();
                    HashTableNode<T> next = curr;
                    curr.Next = null;
                    curr = next;
                }
            }
            GC.SuppressFinalize(this);
        }

        public bool Add(T element)
        {
            int index = GetIndex(element);
            if (table[index] == null)
            {
                table[index] = new((T)element.Clone());
                return true;
            }
            var curr = table[index];
            while (curr.Next != null)
            {
                curr = curr.Next;
            }
            curr.Next = new((T)element.Clone());
            return true;
        }

        public bool Contains(T element)
        {
            return FindNode(element).Item2 != null;
        }

        public bool Remove(T element)
        {
            HashTableNode<T>? prev, curr;
            (prev, curr) = FindNode(element);
            if (curr == null)
                return false;
            if (prev != null)
            {
                prev.Next = curr.Next;
                curr.Next = null;
                return true;
            }
            table[GetIndex(element)] = curr.Next!;
            curr.Next = null;
            return true;
        }

        private (HashTableNode<T>?, HashTableNode<T>?) FindNode(T element)
        {
            int index = GetIndex(element);
            HashTableNode<T>? curr = table[index];
            HashTableNode<T>? next = curr.Next ?? null;
            if (curr == null)
                return (null, null);
            if (curr.Equals(element))
                return (null, curr);
            while (next != null && !next.Data.Equals(element))
                (curr, next) = (next, next.Next);
            return (curr, next);
        }
    }
}
