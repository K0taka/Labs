namespace lab
{
    public class HashTableNode<T> : IDisposable where T : IDisposable
    {
        public int ChainLen { get; set; }

        public T Data { get; set; }

        public HashTableNode<T>? Next { get; set; }

        public HashTableNode(T data, HashTableNode<T>? next = null, int chainLen = 1)
        {
            Data = data;
            Next = next;
            ChainLen = chainLen;
        }

        public void Dispose()
        {
            ChainLen = 0;
            Data.Dispose();
            Next = null;
            GC.SuppressFinalize(this);
        }

        public override string ToString()
        {
            return Data.ToString() ?? "";
        }

        public override int GetHashCode()
        {
            return Data.GetHashCode();
        }

        ~HashTableNode()
        {
            Dispose();
        }
    }
}
