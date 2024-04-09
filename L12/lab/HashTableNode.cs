namespace lab
{
    public class HashTableNode<T> : IDisposable where T : IDisposable
    {
        public T Data { get; set; }

        public HashTableNode<T>? Next { get; set; }

        public HashTableNode(T data, HashTableNode<T>? next = null)
        {
            Data = data;
            Next = next;
        }

        public void Dispose()
        {
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
