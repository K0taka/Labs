using IOLib;
using Lab10Lib;
using static IOLib.IO;

namespace lab
{
    internal class Program
    {
        static readonly ObservedCollection<ControlElement, Button> firstCollection = new("Button tree");
        static readonly ObservedCollection<ControlElement, TextField> secondCollection = new("Fields tree");
        static readonly Journal j1 = new(WriteLine);
        static readonly Journal j2 = new(WriteLine);
        static readonly Journal jErrors = new(WriteLine);

        static void Main()
        {
            firstCollection.RegisterCountChangedHandler(j1.Add);
            firstCollection.RegisterReferenceChangedHandler(j1.Add);

            firstCollection.RegisterReferenceChangedHandler(j2.Add);
            secondCollection.RegisterReferenceChangedHandler(j2.Add);

            firstCollection.RegisterThrowedErrorHandler(jErrors.Add);
            secondCollection.RegisterThrowedErrorHandler(jErrors.Add);

            Start();
        }

        static void ShowJournal(Journal journal)
        {
            Clear();
            journal.ShowJournal();
            WaitAnyButton();
            Clear();
        }

        static void Add(int collection)
        {
            Clear();
            ControlElement add = collection == 0 ? new Button() : new TextField();
            
            bool isCorrect = false;
            do
            {
                add.Init();
                try
                {
                    if (collection == 0)
                        firstCollection.Add(new(add.X, add.Y), (Button)add);
                    else
                        secondCollection.Add(new(add.X, add.Y), (TextField)add);
                    isCorrect = true;
                }
                catch
                {
                    WriteLine("Произошла ошибка во время добавления. Такой элемент уже существует");
                }
            } while(!isCorrect);
            WriteLine("Элемент успешно добавлен");
            WaitAnyButton();
            Clear();
        }

        static void Start()
        {
            Menu menu = new(["Добавить элемент в коллекцию 1", "Добавить элемент в коллекцию 2",
                "Удалить элемент в коллекции 1", "Удалить элемент в коллекции 2",
                "Изменить элемент в коллекции 1", "Изменить элемент в коллекции 2",
                "Печать журнала 1", "Печать журнала 2", "Ошибки"]);

            while (true)
            {
                Clear();
                menu.ShowMenu();
                switch (menu.SetUserAnswer())
                {
                    case 1:
                        Add(0);
                        break;
                    case 2:
                        Add(1);
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                    case 5:
                        break;
                    case 6:
                        break;
                    case 7:
                        ShowJournal(j1);
                        break;
                    case 8:
                        ShowJournal(j2);
                        break;
                    case 9:
                        ShowJournal(jErrors);
                        break;
                }
            }
        }
    }
}
