namespace lab
{
    public class HashTableNode<TKey, TValue> where TKey: notnull
    {

        public TKey Key { get; set; }

        public TValue Data { get; set; }

        public HashTableNode<TKey, TValue>? Next { get; set; }

        public HashTableNode(TKey key, TValue data, HashTableNode<TKey, TValue>? next = null)
        {
            Key = key;
            Data = data;
            Next = next;
        }

        public override int GetHashCode()
        {
            return Key.GetHashCode();
        }
    }
}
