using System.Collections;

namespace lab
{
    public class MyHashTable<TKey, TValue>: IEnumerable<KeyValuePair<TKey, TValue>> where TValue : ICloneable where TKey : notnull, ICloneable
    {
        readonly HashTableChain<TKey, TValue>?[] table;
        
        public int Capacity => table.Length;

        public int Count { get; private set; }

        public TKey[] Keys
        {
            get
            {
                TKey[] keys = new TKey[Count];
                int index = 0;
                foreach (var chain in table)
                {
                    if(chain == null || chain.Count == 0)
                        continue;
                    foreach (var pair in chain)
                    {
                        keys[index++] = pair.Key;
                    }
                }
                return keys;
            }
        }

        public TValue[] Values
        {
            get
            {
                TValue[] values = new TValue[Count];
                int index = 0;
                foreach(var chain in table)
                {
                    if (chain == null || chain.Count == 0)
                        continue;
                    foreach(var pair in chain)
                    {
                        values[index++] = pair.Value;
                    }
                }
                return values;
            }
        }

        public MyHashTable(int capacity = 16)
        {
            ArgumentOutOfRangeException.ThrowIfLessThan(capacity, 1);
            table = new HashTableChain<TKey, TValue>[capacity];
            Count = 0;
        }

        private int GetIndex(TKey key) => Math.Abs(key.GetHashCode()) % Capacity;

        public void Clear()
        {
            Count = 0;
            for (int i = 0; i < Capacity; i++)
            {
                table[i]?.Clear();
            }
        }

        public bool Add(TKey key, TValue element)
        {
            int index = GetIndex(key);
            table[index] ??= [];
            try
            {
                table[index]!.Add(key, element);
                Count += 1;
                return true;
            }
            catch (ArgumentException)
            {
                return false;
            }
        }

        public bool Contains(TKey key)
        {
            var chain = table[GetIndex(key)];
            if (chain == null)
                return false;
            return chain.Contains(key);
        }

        public bool Remove(TKey key)
        {
            try
            {
                table[GetIndex(key)].Remove(key);
                Count -= 1;
                return true;
            }
            catch
            {
                return false;
            }
        }

        public TValue this[TKey key]
        {
            get
            {
                int index = GetIndex(key);

                if (table[index] == null)
                    throw new KeyNotFoundException();
                try
                {
                    return table[index][key];
                }
                catch
                {
                    throw new KeyNotFoundException();
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            foreach (var chain in table)
            {
                if (chain == null)
                    continue;
                foreach(var pair in chain)
                {
                    yield return pair;
                }
            }
            yield break;
        }
    }
}
