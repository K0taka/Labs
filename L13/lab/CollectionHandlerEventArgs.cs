namespace lab
{
    public class CollectionHandlerEventArgs: EventArgs
    {
        public string CollectionName { get; set; }
        public string Action { get; set; }

        public object Item { get; set; }

        public CollectionHandlerEventArgs(string name, string action, object item)
        {
            CollectionName = name;
            Action = action;
            Item = item;
        }
    }
}
