using lab;
using System.Text.Json.Serialization;
using System.Threading.Channels;

namespace Tests
{
    [TestClass]
    public class JournalTest
    {
        readonly List<string> list = new();

        public void InsertToList(string entry)
        {
            list.Add(entry);
        }


        [TestMethod]
        public void CountChangedTest()
        {
            list.Clear();
            Journal journal = new(InsertToList);
            string name = "Tree of ints";
            ObservedCollection<int, int> tree = new(name);
            tree.RegisterCountChangedHandler(journal.Add);
            tree.Add(1, 2);
            journal.ShowJournal();

            Assert.AreEqual("\tВ коллекции Tree of ints\n\tПроизошло событие: Successfully inserted in AVL-tree\n\tВызванное объектом [1, 2]", list[0]);
        }

        [TestMethod]
        public void CountChangedUnregTest()
        {
            list.Clear();
            Journal journal = new(InsertToList);
            string name = "SomeTree";
            ObservedCollection<int, int> tree = new(name);
            tree.RegisterCountChangedHandler(journal.Add);
            tree.Add(1, 2);
            journal.ShowJournal();
            Assert.IsTrue(list.Count == 1);
            tree.UnregisterCountChangedHandler(journal.Add);
            list.Clear();
            tree.Add(2, 3);
            journal.ShowJournal();
            Assert.IsTrue(list.Count == 1);
        }

        [TestMethod]
        public void RefChangedTest()
        { 
            list.Clear();
            Journal journal = new(InsertToList);
            string name = "Tree of ints";
            ObservedCollection<int, int> tree = new(name);
            tree.RegisterReferenceChangedHandler(journal.Add);
            tree.Add(1, 2);
            tree[1] = 3;
            journal.ShowJournal();

            Assert.AreEqual("\tВ коллекции Tree of ints\n\tПроизошло событие: The value with the key was changed\n\tВызванное объектом 1", list[0]);
        }

        [TestMethod]
        public void RefChangedUnregTest()
        {
            list.Clear();
            Journal journal = new(InsertToList);
            string name = "SomeTree";
            ObservedCollection<int, int> tree = new(name);
            tree.RegisterReferenceChangedHandler(journal.Add);
            tree.Add(1, 2);
            tree[1] = 3;
            journal.ShowJournal();
            Assert.IsTrue(list.Count == 1);
            tree.UnregisterReferenceChangedHandler(journal.Add);
            list.Clear();
            tree[1] = 5;
            journal.ShowJournal();
            Assert.IsTrue(list.Count == 1);
        }

        [TestMethod]
        public void ErrorThrowedTest()
        {
            list.Clear();
            Journal journal = new(InsertToList);
            string name = "Tree of ints";
            ObservedCollection<int, int> tree = new(name);
            tree.RegisterThrowedErrorHandler(journal.Add);
            tree.IsReadOnly = true;
            try
            {
                tree[1] = 3;
            }
            catch { }
            journal.ShowJournal();

            Assert.AreEqual("\tВ коллекции Tree of ints\n\tПроизошло событие: Error while updating the value because collection was ReadOnly\n\tВызванное объектом 1", list[0]);
        }

        [TestMethod]
        public void ErrorThrowedUnregTest()
        {
            list.Clear();
            Journal journal = new(InsertToList);
            string name = "Tree of ints";
            ObservedCollection<int, int> tree = new(name);
            tree.RegisterThrowedErrorHandler(journal.Add);
            tree.IsReadOnly = true;
            try
            {
                tree[1] = 3;
            }
            catch { }
            journal.ShowJournal();
            Assert.IsTrue(list.Count == 1);
            tree.UnregisterThrowedErrorHandler(journal.Add);
            list.Clear();
            try
            {
                tree[1] = 3;
            }
            catch { }
            journal.ShowJournal();
            Assert.IsTrue(list.Count == 1);
        }

        [TestMethod]
        public void PrintMethodTest()
        {
            Journal journal = new(InsertToList);

            Assert.AreEqual(journal.PrintMethod, InsertToList);

            journal.SetPrintMetod(Console.WriteLine);

            Assert.AreEqual(journal.PrintMethod, Console.WriteLine);
        }
    }
}
