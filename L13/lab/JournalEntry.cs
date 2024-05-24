namespace lab
{
    public class JournalEntry
    {
        public string Collection { get; set; }

        public string Item { get; set; }

        public string Action { get; set; }

        public JournalEntry(string collection, string item, string action)
        {
            Collection = collection;
            Item = item;
            Action = action;
        }

        public override string ToString()
        {
            string str = $"\tВ коллекции {Collection}\n\tПроизошло событие: {Action}\n\tВызванное объектом {Item}";
            return str;
        }
    }
}
