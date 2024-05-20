namespace lab
{
    public delegate void PrintHandler(string str);
    public class Journal
    {
        List<JournalEntry> journal;
        PrintHandler Print;

        public Journal(PrintHandler printMethod)
        {
            journal = [];
            Print = printMethod;
        }

        public void SetPrintMetod(PrintHandler printMethod)
        {
            Print = printMethod;
        }

        public void ShowJournal()
        {
            foreach (JournalEntry entry in journal)
            {
                Print(entry.ToString());
            }
        }

        public void Add(object source, CollectionHandlerEventArgs args)
        {
            journal.Add(new(source.GetType().Name, args.Item?.ToString(), args.Action.ToString()));
        }
    }
}
