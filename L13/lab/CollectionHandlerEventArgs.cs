namespace lab
{
    public class CollectionHandlerEventArgs: EventArgs
    {
        public string Action { get; set; }

        public object? Item { get; set; }

        public CollectionHandlerEventArgs(string action, object? item)
        {
            Action = action;
            Item = item;
        }
    }
}
