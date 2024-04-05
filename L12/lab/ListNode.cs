namespace lab
{
    public class ListNode<T>: IDisposable where T: IDisposable
    {
        public T Data { get; set; }

        public ListNode<T>? Next { get; set; }

        public ListNode<T>? Previous { get; set; }

        public ListNode(T data, ListNode<T>? next = null, ListNode<T>? previous = null)
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

        ~ListNode() 
        {
            Dispose();
        }
    }
}
