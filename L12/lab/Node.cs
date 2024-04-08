namespace lab
{
    public class Node<T>: IDisposable where T: IDisposable
    {
        public T Data { get; set; }

        public Node<T>? Next { get; set; }

        public Node<T>? Previous { get; set; }

        public Node(T data, Node<T>? next = null, Node<T>? previous = null)
        {
            Data = data;
            Next = next;
            Previous = previous;
        }

        public override string? ToString()
        {
            return Data != null ? Data.ToString() : "";
        }

        public void Dispose()
        {
            Previous = null;
            Next = null;
            Data.Dispose();
            GC.SuppressFinalize(this);
        }

        ~Node() 
        {
            Dispose();
        }
    }
}
