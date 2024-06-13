using Lab10Lib;
using AVLTree;
using IOLib;
using static IOLib.IO;
using static lab.Part1;
using static lab.Part2;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Tests")]
namespace lab
{
    internal class Program
    {
        static readonly Random rnd = new Random();

        static void Main(string[] args)
        {
            Menu mainPage = new(["Часть 1: Cтандартные коллекции", "Часть 2: Собственная коллекция"]);
            while (true)
            {
                Clear();
                mainPage.ShowMenu();
                switch (mainPage.SetUserAnswer())
                {
                    case 1:
                        Part1();
                        break;
                    case 2:
                        Part2();
                        break;
                    default:
                        WaitAnyButton();
                        break;
                }
            }
        }

        static ControlElement CreateRandomElement(int start, int end)
        {
            int seed = rnd.Next(start, end);
            ControlElement element = seed switch
            {
                1 => new Button(),
                2 => new MultButton(),
                3 => new TextField(),
                _ => new ControlElement()
            };
            element.RandomInit();
            return element;
        }

        static void PrintEnumerable(IEnumerable<ControlElement> elements)
        {
            if (!elements.Any())
                WriteLine("Пусто! :(");
            else
            {
                foreach (ControlElement element in elements)
                {
                    WriteLine(element.ToString());
                }
            }
        }

        #region part 1

        static void PrintIGroup(IEnumerable<IGrouping<double, ControlElement>> groups)
        {
            if (!groups.Any())
                WriteLine("Пусто :(");
            else
            {
                foreach (var group in groups)
                {
                    foreach (var element in group)
                    {
                        WriteLine($"В группе [{group.Key}] находится элементм: {element}");
                    }
                }
            }
        }

        static void PrintJoin(IEnumerable<dynamic> elements)
        {
            if (!elements.Any())
                WriteLine("Пусто :(");
            else
            {
                foreach (var pair in elements)
                {
                    WriteLine($"С элементом {pair.Item} связана функция {pair.Function}");
                }
            }
        }

        static void PrintCollection(Stack<Dictionary<ControlElement, ControlElement>> collection)
        {
            if (collection.Count > 0)
            {
                int num = 1;
                foreach (var window in collection)
                {
                    foreach (var element in window)
                    {
                        WriteLine($"В окне {num} находится элемент {element}");
                    }
                    num++;
                }
            }
            else
                WriteLine("Коллекция пуста");
        }

        static void Part1()
        {
            Menu part1Menu = new(["Заполнить коллекцию (ДСЦ)",
                                  "Заполнить коллекцию вручную (долго)",
                                  "Использовать шаблонное заполнение",
                                  "Запрос 1: элементы, дальше чем N",
                                  "Запрос 2 и 3: Получить элементы с макс. и мин. дистанцией в каждом окне",
                                  "Запрос 4: Среднее расстояние между элементами и началом координат",
                                  "Запрос 5: Уникальные элементы",
                                  "Запрос 6: Элементы, повторенные хотя бы раз повторились",
                                  "Запрос 7: Элемент и соотв. ему функция",
                                  "Печать коллекции",
                                  "Назад"]);

            Stack<Dictionary<ControlElement, ControlElement>> collection = new();
            List<Function> functions = new List<Function>();
            bool isClosed = false;
            while (!isClosed)
            {
                Clear();
                part1Menu.ShowMenu();
                switch(part1Menu.SetUserAnswer())
                {
                    case 1:
                        RandomFillPart1(collection, functions);
                        break;
                    case 2:
                        ManualFillPart1(collection, functions);
                        break;
                    case 3:
                        PatternFillPart1(collection, functions);
                        break;
                    case 4:
                        Clear();
                        if (collection.Count > 0)
                        {
                            double dist = GetDoubleAnswer("Укажите дистанцию, после которой необходимы элементы >>> ", 0);
                            WriteLine("Результат работы LINQ:");
                            PrintIGroup(LINQGetElementsFarThen(collection, dist));

                            EmptyLine();

                            WriteLine("Результат работы методов расширения:");
                            PrintIGroup(EXTGetElementsFarThen(collection, dist));
                        }
                        else
                            WriteLine("Сначала заполните коллекцию!");
                        WaitAnyButton();
                        break;
                    case 5:
                        Clear();
                        if (collection.Count > 0)
                        {
                            WriteLine("Результаты работы LINQ:");
                            WriteLine("Максимально удаленные элементы элементы:");
                            PrintEnumerable(LINQGetMaxDistanseInEveryWindow(collection));
                            WriteLine("Минимально удаленные элементы:");
                            PrintEnumerable(LINQGetMinDistanseInEveryWindow(collection));

                            EmptyLine();

                            WriteLine("Результаты работы методов расширения:");
                            WriteLine("Максимально удаленные элементы элементы:");
                            PrintEnumerable(EXTGetMaxDistanseInEveryWindow(collection));
                            WriteLine("Минимально удаленные элементы:");
                            PrintEnumerable(EXTGetMinDistanseInEveryWindow(collection));
                        }
                        else
                            WriteLine("Сначала заполните коллекцию");
                        WaitAnyButton();
                        break;
                    case 6:
                        Clear();
                        if (collection.Count > 0)
                        {
                            WriteLine("Результаты работы LINQ:");
                            WriteLine($"Средняя дистанция между элементами и началом координат: {LINQAverageDistanceOfElements(collection)}");

                            EmptyLine();

                            WriteLine("Результаты работы методов расширения:");
                            WriteLine($"Средняя дистанция между элементами и началом координат: {EXTAverageDistanceOfElements(collection)}");
                        }
                        else
                            WriteLine("Сначала заполните коллекцию");
                        WaitAnyButton();
                        break;
                    case 7:
                        Clear();
                        if (collection.Count > 0)
                        {
                            WriteLine("Результаты работы LINQ:");
                            PrintEnumerable(LINQUnicElements(collection));

                            EmptyLine();

                            WriteLine("Результаты работы методов расширения:");
                            PrintEnumerable(EXTUnicElements(collection));
                        }
                        else
                            WriteLine("Сначала заполните коллекцию");
                        WaitAnyButton();
                        break;
                    case 8:
                        Clear();
                        if (collection.Count > 0)
                        {
                            WriteLine("Результаты работы LINQ:");
                            PrintEnumerable(LINQCommonElements(collection));

                            EmptyLine();

                            WriteLine("Результаты работы методов расширения:");
                            PrintEnumerable(EXTCommonElements(collection));
                        }
                        else
                            WriteLine("Сначала заполните коллекцию");
                        WaitAnyButton();
                        break;
                    case 9:
                        Clear();
                        if (collection.Count > 0)
                        {
                            WriteLine("Результаты работы LINQ:");
                            PrintJoin(LINQElementAndItsFuntion(collection, functions));

                            EmptyLine();

                            WriteLine("Результаты работы методов расширения:");
                            PrintJoin(EXTElementAndItsFuntion(collection, functions));
                        }
                        else
                            WriteLine("Сначала заполните коллекцию");
                        WaitAnyButton();
                        break;
                    case 10:
                        Clear();
                        PrintCollection(collection);
                        WaitAnyButton();
                        break;
                    case 11:
                        isClosed = true;
                        break;
                }
            }
        }

        static void RandomFillPart1(Stack<Dictionary<ControlElement, ControlElement>> collection, List<Function> funcs)
        {
            collection.Clear();
            funcs.Clear();

            int windowsCount = rnd.Next(1, 5);
            for (int i = 0; i < windowsCount; i++)
            {
                Dictionary<ControlElement, ControlElement> dict = new();
                int dictCount = rnd.Next(1, 10);
                for (int j = 0; j < dictCount; j++)
                {
                    ControlElement element = CreateRandomElement(0, 4);
                    try
                    {
                        dict.Add(new(element.X, element.Y), element);
                        funcs.Add(new(element.Id, $"Func for element with id {element.Id}"));
                    }
                    catch { j--; }
                }
                collection.Push(dict);
            }
        }

        static void ManualFillPart1(Stack<Dictionary<ControlElement, ControlElement>> collection, List<Function> funcs)
        {
            Clear();
            collection.Clear();
            funcs.Clear();

            int windowsCount = (int)GetIntegerAnswer("Укажите количество окон в приложении >>> ", 1, 5);
            for (int i = 0; i < windowsCount; i++)
            {
                Dictionary<ControlElement, ControlElement> dict = new();
                int elementsCount = (int)GetIntegerAnswer($"Укажите колиечтсво элементов в онке {i+1} >>> ", 1, 10);
                for (int j = 0; j < elementsCount; j++)
                {
                    WriteLine($"Укажите элемент {j + 1}:");
                    Button button = new Button();
                    button.Init();

                    try
                    {
                        dict.Add(new(button.X, button.Y), button);
                        funcs.Add(new(button.Id, $"Funtion for button with id {button.Id}"));
                    }
                    catch { j--; }
                }
                collection.Push(dict);
            }
        }

        static void PatternFillPart1(Stack<Dictionary<ControlElement, ControlElement>> collection, List<Function> funcs)
        {
            collection.Clear();
            funcs.Clear();

            ControlElement[] elements = [new Button(0, 0, "Button"), new MultButton(9, 0, "MultButton", true),
                                         new ControlElement(8, 6), new Button(0, 0, "Button"), new TextField(9, 0, "Hint", "Text"),
                                         new ControlElement(8, 6), new TextField(5, 12, "NotAHint", "NotAText"), new ControlElement(0, 0),
                                         new Button(9, 0, "Button"), new ControlElement(8, 6)];

            Dictionary<ControlElement, ControlElement> window1 = new()
            {
                { new ControlElement(elements[0].X, elements[0].Y),  elements[0]},
                { new ControlElement(elements[1].X, elements[1].Y), elements[1]},
                { new ControlElement(elements[2].X, elements[2].Y), elements[2]}
            };

            Dictionary<ControlElement, ControlElement> window2 = new()
            {
                { new ControlElement(elements[3].X, elements[3].Y),  elements[3]},
                { new ControlElement(elements[4].X, elements[4].Y), elements[4]},
                { new ControlElement(elements[5].X, elements[5].Y), elements[5]},
                { new ControlElement (elements[6].X, elements[6].Y), elements[6]}
            };

            Dictionary<ControlElement, ControlElement> window3 = new()
            {
                { new ControlElement(elements[7].X, elements[7].Y), elements[7]},
                { new ControlElement(elements[8].X, elements[8].Y), elements[8]},
                { new ControlElement(elements[9].X, elements[9].Y), elements[9]}
            };

            collection.Push(window1);
            collection.Push(window2);
            collection.Push(window3);

            foreach(ControlElement element in elements)
            {
                funcs.Add(new(element.Id, $"Funtion for element with id {element.Id}"));
            }
        }
        
        #endregion part 1


        #region part 2
        static void Part2()
        {
            Menu part2Menu = new(["Заполнить ДСЦ",
                                  "Заполнить коллекцию вручную (долго)",
                                  "Использовать шаблонное заполнение",
                                  "Запрос 1: подсчитать все значения указанного типа",
                                  "Запросы 2,3: Элемент с минимальным X и элемент с максимальным Y",
                                  "Запрос 4: средняя удаленность от начала координат",
                                  "Запрос 5: количество элементов каждого типа",
                                  "Запрос 6: сумма всех координат X, меньших числа N",
                                  "Печать коллекции",
                                  "Назад"]);

            bool isClosed = false;
            AVL<ControlElement, ControlElement> tree = new();
            while (!isClosed)
            {
                Clear();
                part2Menu.ShowMenu();
                switch (part2Menu.SetUserAnswer())
                {
                    case 1:
                        RandomFillTree(tree);
                        break;
                    case 2:
                        ManualFillTree(tree);
                        break;
                    case 3:
                        PatternFillTree(tree);
                        break;
                    case 4:
                        Clear();
                        if (tree.Count > 0)
                        {
                            Type choosedType = GetIntegerAnswer("Укажите тип:\n1 - ControlElement\n2 - Button\n3 - MultButton\n4 - TextField\n>>>", 0, 3) switch
                            {
                                2 => typeof(Button),
                                3 => typeof(MultButton),
                                4 => typeof(TextField),
                                _ => typeof(ControlElement)
                            };
                            WriteLine($"LINQ\nЭлементов укзаанного типа: {LINQCountElementsOfType(tree, choosedType)}");
                            WriteLine($"EXT\nЭлементов укзаанного типа: {EXTCountElementsOfType(tree, choosedType)}");
                        }
                        else
                            WriteLine("Коллекция пуста!");
                        WaitAnyButton();
                        break;
                    case 5:
                        Clear();
                        if (tree.Count > 0)
                        {
                            WriteLine($"LINQ\nЭлемент с минимальным X: {LINQGetElementWithMinX(tree)}\nЭлемент с максимальным Y: {LINQGetElementWithMaxY(tree)}");
                            WriteLine($"EXT\nЭлемент с минимальным X: {EXTGetElementWithMinX(tree)}\nЭлемент с максимальным Y: {EXTGetElementWithMaxY(tree)}");
                        }
                        else
                            WriteLine("Коллекция пуста!");
                        WaitAnyButton();
                        break;
                    case 6:
                        Clear();
                        if (tree.Count > 0)
                        {
                            WriteLine($"LINQ\nСредняя дистанция: {LINQAverageDistance(tree)}");
                            WriteLine($"EXT\nСредняя дистанция: {EXTAverageDistance(tree)}");
                        }
                        else
                            WriteLine("Коллекция пуста!");
                        WaitAnyButton();
                        break;
                    case 7:
                        Clear();
                        if (tree.Count > 0)
                        {
                            WriteLine("LINQ:");
                            PrintIGroup(LINQCountGroupsByTypes(tree));
                            EmptyLine();
                            WriteLine("EXT:");
                            PrintIGroup(EXTCountGroupsByTypes(tree));
                        }
                        else
                            WriteLine("Коллекция пуста!");
                        WaitAnyButton();
                        break;
                    case 8:
                        Clear();
                        if (tree.Count > 0)
                        {
                            uint N = (uint)GetIntegerAnswer("Верхняя граница X >>> ", 0, 1920);
                            WriteLine($"LINQ\nСумма координат X элементов с координатой X < {N}: {LINQSumOfXLessThan(tree, N)}");
                            WriteLine($"EXT\nСумма координат X элементов с координатой X < {N}: {EXTSumOfXLessThan(tree, N)}");
                        }
                        else
                            WriteLine("Коллекция пуста!");
                        WaitAnyButton();
                        break;
                    case 9:
                        Clear();
                        if (tree.Count > 0)
                            WriteLine(CreateStringHorizontalLayout(tree.Root));
                        else
                            WriteLine("Сначала заполните коллекцию!");
                        WaitAnyButton();
                        break;
                    case 10:
                        isClosed = true;
                        break;
                }
            }
        }

        static void PrintIGroup(IEnumerable<IGrouping<Type, int>> groups)
        {
            if (groups.Any())
            {
                foreach (var group in groups)
                {
                    foreach (var element in group)
                    {
                        WriteLine($"В группе {group.Key.Name} {element} элементов");
                    }
                }
            }
            else
                WriteLine("Пусто! :(");
        }

        static void RandomFillTree(AVL<ControlElement, ControlElement> tree)
        {
            Clear();
            int num = (int)GetIntegerAnswer("Укажите количество элементов >>> ", 0, 20);
            for (int i = 0; i < num; i++)
            {
                var element = CreateRandomElement(0, 3);
                try
                {
                    tree.Add(new ControlElement(element.X, element.Y), element);
                }
                catch { i--; }
            }
        }

        static void ManualFillTree(AVL<ControlElement, ControlElement> tree)
        {
            Clear();
            int num = (int)GetIntegerAnswer("Укажите количество элементов >>> ", 0, 20);
            for (int i = 0; i < num; i++)
            {
                WriteLine($"Элемент {i}");
                var element = new Button();
                element.Init();
                try
                {
                    tree.Add(new ControlElement(element.X, element.Y), element);
                }
                catch { i--; }
            }
        }

        static void PatternFillTree(AVL<ControlElement, ControlElement> tree)
        {
            tree.Clear();

            ControlElement[] elements = [new ControlElement(12, 5), new Button(0, 0, "Text of button"), new MultButton(0, 10, "Text text", true), new TextField(10, 0, "Hint", "Text")];

            foreach (ControlElement element in elements)
            {
                tree.Add(new(element.X, element.Y), element);
            }
        }

        /// <summary>
        /// Создание для АВЛ-дерева альбомного расположения дерева
        /// </summary>
        /// <param name="node">Текущий узел</param>
        /// <param name="spaces">Количество отступов</param>
        /// <returns>Созданная строка с отступами</returns>
        static string CreateStringHorizontalLayout(Node<ControlElement, ControlElement> node, int spaces = 0)
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

        #endregion part 2
    }
}
