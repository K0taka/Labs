namespace lab
{
    public class CollectionHandlerEventArgs: EventArgs
    {
        string Action { get; set; }

        object? Item { get; set; }

        public CollectionHandlerEventArgs(string action, object? item)
        {
            Action = action;
            Item = item;
        }
    }
}
