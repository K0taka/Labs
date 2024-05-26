using AVLTree;
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

        static void Remove(int coll)
        {
            Clear();
            var remove = new ControlElement();
            if (coll == 0)
            {
                if (firstCollection.Count == 0)
                    WriteLine("Коллекция пуста!");

                else
                {
                    remove.Init();
                    if (firstCollection.Remove(remove))
                        WriteLine("Элемент успешно удален");
                    else
                        WriteLine("Элемент не удален!");
                }
            }
            else
            {
                if (secondCollection.Count == 0)
                    WriteLine("Коллекция пуста!");
                else
                {
                    remove.Init();
                    if (secondCollection.Remove(remove))
                        WriteLine("Элемент успешно удален");
                    else
                        WriteLine("Элемент не удален!");
                }
            }
            WaitAnyButton();
            Clear();
        }

        static void Change(int collection)
        {
            Clear();
            ControlElement change = collection == 0 ? new Button() : new TextField();
            change.Init();
            ControlElement key = new(change.X, change.Y);

            if (collection == 0)
                firstCollection[key] = (Button)change;
            else
                secondCollection[key] = (TextField)change;
            WaitAnyButton();
            Clear();
        }

        static void Print()
        {
            Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            WriteLine("Коллекция 1");
            Console.ResetColor();
            if (firstCollection != null && firstCollection.Count != 0)
                WriteLine("\n" + CreateStringHorizontalLayout(firstCollection.Root));
            else
                WriteLine("Коллекция 1 пуста");
            Console.ForegroundColor = ConsoleColor.Yellow;
            WriteLine("Коллекция 2");
            Console.ResetColor();
            if (secondCollection != null && secondCollection.Count != 0)
                WriteLine("\n" + CreateStringHorizontalLayout(secondCollection.Root));
            else
                WriteLine("Коллекция 2 пуста");
            WaitAnyButton();
            Clear();
        }

        /// <summary>
        /// Создание для АВЛ-дерева альбомного расположения дерева
        /// </summary>
        /// <param name="node">Текущий узел</param>
        /// <param name="spaces">Количество отступов</param>
        /// <returns>Созданная строка с отступами</returns>
        static string CreateStringHorizontalLayout<TValue> (Node<ControlElement, TValue> node, int spaces = 0)
            where TValue : notnull
        {
            if (node == null)
                return "";
            string line = "";
            line = CreateSpace(spaces) + $"Key: {node.Entry.Key},\n{CreateSpace(spaces)}Value: {node.Entry.Value}\nCompare: {Math.Sqrt(Math.Pow(node.Entry.Key.X, 2) + Math.Pow(node.Entry.Key.Y, 2))}\n";
            if (node.Left != null)
                line = line + CreateStringHorizontalLayout(node.Left, spaces + 5);
            if (node.Right != null)
                line = CreateStringHorizontalLayout(node.Right, spaces + 5) + line;
            return line;
        }

        /// <summary>
        /// Создает строку с указанным кол-вом пробелом
        /// </summary>
        /// <param name="space"></param>
        /// <returns></returns>
        static string CreateSpace(int space)
        {
            string res = "";
            for (int i = 0; i < space; i++)
                res += " ";
            return res;
        }

        static void Start()
        {
            Menu menu = new(["Добавить элемент в коллекцию 1", "Добавить элемент в коллекцию 2",
                "Удалить элемент в коллекции 1", "Удалить элемент в коллекции 2",
                "Изменить элемент в коллекции 1", "Изменить элемент в коллекции 2",
                "Печать журнала 1", "Печать журнала 2", "Ошибки", "Печать"]);

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
                        Remove(0);
                        break;
                    case 4:
                        Remove(1);
                        break;
                    case 5:
                        Change(0);
                        break;
                    case 6:
                        Change(1);
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
                    case 10:
                        Print();
                        break;
                }
            }
        }
    }
}
