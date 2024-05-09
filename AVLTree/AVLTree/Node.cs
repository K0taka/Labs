namespace AVLTree
{
    public class Node<TKey, TValue> where TKey: notnull, IComparable<TKey> where TValue : notnull
    {
        public KeyValuePair<TKey, TValue> Entry { get; set; }

        public Node<TKey, TValue>? Left { get; set; }

        public Node<TKey, TValue>? Right { get; set; }

        public int Hight { get; set; }
        
        public bool Color { get; set; }

        public Node(KeyValuePair<TKey, TValue> item)
        {
            Entry = item;
        }
    }
}
