using Lab10Lib;
using AVLTree;
using IOLib;
using static IOLib.IO;
using System.Security.Principal;

namespace lab
{
    internal class Program
    {
        static readonly Random rnd = new Random();

        static void Main(string[] args)
        {
            Menu mainPage = new(["Часть 1: стандартные коллекции", "Часть 2: Собственная коллекция"]);
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

        #region part 1
        static void Part1()
        {
            Menu part1Menu = new(["Заполнить коллекцию (ДСЦ)",
                                  "Заполнить коллекцию вручную (долго)",
                                  "Использовать шаблонное заполнение",
                                  "Запрос 1: элементы, дальше чем N",
                                  "Запрос 2 и 3: Получить элементы с макс. и мин. дистанцией в каждом окне",
                                  "Запрос 4: Среднее расстояние между элементами",
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
                        break;
                    case 5:
                        break;
                    case 6:
                        break;
                    case 7:
                        break;
                    case 8:
                        break;
                    case 9:
                        break;
                    case 10:
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
            Dictionary<ControlElement, ControlElement> window1 = new()
            {
                { new ControlElement(0, 0), new Button(0, 0, "Button") },
                { new ControlElement(1, 0), new MultButton(1, 0, "MultButton", true)},
                { new ControlElement(5, 5), new ControlElement(5, 5)}
            };

            Dictionary<ControlElement, ControlElement> window2 = new()
            {
                { new ControlElement(0, 0), new Button(0, 0, "Button") },
                { new ControlElement(1, 0), new TextField(1, 0, "Hint", "Text")},
                { new ControlElement(5, 5), new ControlElement(5, 5)}
            };

            Dictionary<ControlElement, ControlElement> window3 = new()
            {
                { new ControlElement(0, 0), new ControlElement(0, 0) },
                { new ControlElement(1, 0), new Button(1, 0, "Button")},
                { new ControlElement(5, 5), new ControlElement(5, 5)}
            };
        }
        #endregion part 1


        #region part 2
        static void Part2()
        {

        }


        #endregion part 2
    }
}
