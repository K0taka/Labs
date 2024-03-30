namespace lab
{
    public class ListNode: IDisposable
    {
        public ControlElement Data { get; set; }

        public ListNode? Next { get; set; }

        public ListNode? Previous { get; set; }

        public ListNode(ControlElement data, ListNode? next = null, ListNode? previous = null)
        {
            Data = data;
            Next = next;
            Previous = previous;
        }

        public override string ToString()
        {
            return Data.ToString();
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
