using System.Collections;

namespace lab
{
    public class HashTableChain<TKey, TValue> : IEnumerable<KeyValuePair<TKey,TValue>> where TValue : ICloneable where TKey : notnull, ICloneable
    {
        HashTableNode<TKey, TValue>? Head;

        public int Count { get; private set; }

        public HashTableChain()
        {
            Head = null;
            Count = 0;
        }

        public bool Contains(TKey key)
        {
            var curr = Head;
            while (curr != null)
            {
                if (curr.Key.Equals(key))
                    return true;
                curr = curr.Next;
            }
            return false;
        }

        public void Add(TKey key, TValue value)
        {
            if (Contains(key))
                throw new ArgumentException("This key is already exists", paramName: nameof(key));
            Head = new((TKey)key.Clone(), (TValue)value.Clone(), Head);
            Count += 1;
        }

        public void Clear() { Head = null; Count = 0; }

        public void Remove(TKey key)
        {
            var curr = Head ?? throw new InvalidOperationException();
            if (curr.Key.Equals(key))
            {
                Head = curr.Next;
                Count--;
                return;
            }

            while (curr.Next != null && !curr.Next.Key.Equals(key))
                curr = curr.Next;
            if (curr.Next == null)
                throw new KeyNotFoundException();
            Count--;
            curr.Next = curr.Next.Next;
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            var curr = Head;
            while (curr != null)
            {
                yield return new KeyValuePair<TKey, TValue>(curr.Key, curr.Data);
                curr = curr.Next;
            }
            yield break;
        }

        public TValue this[TKey key]
        {
            get
            {
                var curr = Head;
                while (curr != null)
                {
                    if (curr.Key.Equals(key))
                        return curr.Data;
                    curr = curr.Next;
                }
                throw new KeyNotFoundException();
            }
        }
    }
}
