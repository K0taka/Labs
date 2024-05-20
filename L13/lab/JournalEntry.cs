namespace lab
{
    public class JournalEntry
    {
        public string Collection { get; set; }

        public string? Item { get; set; }

        public string Action { get; set; }

        public JournalEntry(string collection, string? item, string action)
        {
            Collection = collection;
            Item = item;
            Action = action;
        }

        public override string ToString()
        {
            return $"В коллекции {Collection}\n\t" +
                $"Произошло событие: {Action}\n\t" +
                Item == null ? "" : $"Вызванное объектом {Item}";
        }
    }
}
