global using Lab10Lib;
global using IOLib;
global using static IOLib.IO;
using System.ComponentModel.Design;
using System.Runtime.ConstrainedExecution;

namespace lab
{
    internal class Program
    {
        static readonly Random rand = new();
        static void Main()
        {
            Menu MainMenu = new(["Часть 1. Список", "Часть 2. ???", "Часть 3. ???"]);
            while (true)
            {
                MainMenu.ShowMenu();
                switch(MainMenu.SetUserAnswer())
                {
                    case 1:
                        Clear();
                        ListPart();
                        break;
                    default:
                        Clear();
                        WorkInProgress();
                        break;
                }
            }
        }

        static void ListPart()
        {
            List<ControlElement> list = [];
            ControlElement? chosEl = null;
            Menu ListMenu = new([
            "Напечатать список и выбранный элемент",
            "Заполнить список случайными элементами",
            "Добавить элемент в начало списка",
            "Добавить элемент в конец списка",
            "Выбрать элемент из списка",
            "Определить индекс выбранного элемента",
            "Добавить элемент после выбранного элемента",
            "Удалить выбранный элемент",
            "Удалить все элементы до выбранного",
            "Демонстрация работы с копией",
            "Назад в главное меню"]);

            bool isClosed = true;
            while (isClosed)
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();

                ListMenu.ShowMenu();
                switch (ListMenu.SetUserAnswer())
                {
                    case 1:
                        Clear();
                        PrintList(list);
                        WaitAnyButton();
                        if (chosEl == null)
                            WriteLine("Элемент еще не выбран!");
                        else
                            WriteLine($"Выбран элемент: {chosEl}");
                        WaitAnyButton();
                        Clear();
                        break;
                    case 2:
                        FillList((int)GetIntegerAnswer("Введите количество элементов >>> ", 0, 20), list);
                        Clear();
                        break;
                    case 3:
                        Clear();
                        ControlElement first = new();
                        first.Init();
                        list.AddFirst(first);
                        Clear();
                        break;
                    case 4:
                        Clear();
                        ControlElement last = new();
                        last.Init();
                        list.AddFirst(last);
                        Clear();
                        break;
                    case 5:
                        Clear();
                        PrintList(list);
                        if (list == null || list.Count == 0)
                        {
                            WriteLine("Не из чего выбирать! Список пуст");
                            WaitAnyButton();
                        }
                        else
                            chosEl = (ControlElement?)list[(int)GetIntegerAnswer("Выберите номер предпочитаемого элемента >>> ", 1, list.Count) - 1];
                        Clear();
                        break;
                    case 6:
                        Clear();
                        if (chosEl == null)
                            WriteLine("Элемент еще не выбран, поиск невозможен");
                        else
                            WriteLine($"Выбран элемент\n\t{chosEl}\nПервый эквивалентный ему элемент находится на позиции {list.IndexOf(chosEl)}");
                        WaitAnyButton();
                        Clear();
                        break;
                    case 7:
                        Clear();
                        if (chosEl == null)
                        {
                            WriteLine("Невозможно добавить, так как не выбран элемент");
                            WaitAnyButton();
                        }
                        else
                        {
                            ControlElement adding = new();
                            adding.Init();
                            list.AddAfter(chosEl, adding);
                        }
                        Clear();
                        break;
                    case 8:
                        Clear();
                        if (chosEl == null)
                            WriteLine("Элемент не выбран, удаление невозможно");
                        else
                        {
                            list.Remove(chosEl);
                            WriteLine($"Элемент\n\t{chosEl}\nудален");
                            chosEl.Dispose();
                            chosEl = null;  
                        }
                        WaitAnyButton();
                        Clear();
                        break;
                    case 9:
                        Clear();
                        if (chosEl == null)
                            WriteLine("Элемент не выбран, удаление невозможно");
                        else
                        {
                            list.DeleteAllBefore(chosEl);
                            WriteLine($"Элемент\n\t{chosEl}\nудален");
                            chosEl.Dispose();
                            chosEl = null;
                        }
                        WaitAnyButton();
                        Clear();
                        break;
                    case 10:
                        Clear();
                        break;
                    case 11:
                        Clear();
                        isClosed = false;
                        break;
                }
            }
            Clear();
        }

        static ControlElement GenerateRandHierObj()
        {
            ControlElement element = rand.Next(0, 4) switch
            {
                1 => new Button(),
                2 => new MultButton(),
                3 => new TextField(),
                _ => new ControlElement()
            };
            element.RandomInit();
            return element;
        }

        static void FillList(int len, List<ControlElement> list)
        {
            list.Dispose();
            GC.Collect();
            GC.WaitForPendingFinalizers();
            for (int _ = 0; _ < len; _++)
            {
                list.Add(GenerateRandHierObj());
            }
        }

        static void PrintList<T>(List<T> list) where T : IDisposable, ICloneable
        {
            WriteLine("На текущий момент двусвязный список выглядит так:");
            if (list == null || list.Count == 0)
            {
                WriteLine("Список пуст :(");
                return;
            }
            for (int i = 0; i < list.Count; i++)
            {
                WriteLine($"Элемент {i + 1}: {list[i]}");
            }
        }

        static void WorkInProgress()
        {
            Menu wkProgress= new(["Работа все еще кипит! :3"]);
            wkProgress.ShowMenu();
            wkProgress.SetUserAnswer();
            Clear();
        }
    }
}
