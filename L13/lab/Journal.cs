namespace lab
{
    public delegate void PrintHandler(string str);
    public class Journal
    {
        private readonly List<JournalEntry> journal;
        public PrintHandler PrintMethod { get; private set; }

        public Journal(PrintHandler printMethod)
        {
            journal = [];
            PrintMethod = printMethod;
        }

        public void SetPrintMetod(PrintHandler printMethod)
        {
            PrintMethod = printMethod;
        }

        public void ShowJournal()
        {
            foreach (JournalEntry entry in journal)
            {
                PrintMethod(entry.ToString());
            }
        }

        public void Add(object source, CollectionHandlerEventArgs args)
        {
            journal.Add(new(args.CollectionName, args.Item.ToString()!, args.Action.ToString()));
        }
    }
}
