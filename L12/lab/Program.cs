global using Lab10Lib;
global using IOLib;
global using static IOLib.IO;

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
            MyList<ControlElement> list = [];
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

            bool isClosed = false;
            while (!isClosed)
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
                        first.Dispose();
                        break;
                    case 4:
                        Clear();
                        ControlElement last = new();
                        last.Init();
                        list.Add(last);
                        last.Dispose();
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
                        {
                            chosEl = FindWithX(list, (uint)GetIntegerAnswer("Выберите координату X предпочитаемого элемента >>> ", 0, 1920));
                            if (chosEl == null)
                                WriteLine("Элемент с такой координатой X не найден");
                        }
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
                            adding.Dispose();
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
                            WriteLine($"Элементы до элемента\n\t{chosEl}\nудалены");
                        }
                        WaitAnyButton();
                        Clear();
                        break;
                    case 10:
                        Clear();
                        WorkWithListCopy(list);
                        Clear();
                        break;
                    case 11:
                        Clear();
                        isClosed = true;
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

        static void FillList(int len, MyList<ControlElement> list)
        {
            list.Dispose();
            GC.Collect();
            GC.WaitForPendingFinalizers();
            for (int _ = 0; _ < len; _++)
            {
                list.Add(GenerateRandHierObj());
            }
        }

        static void PrintList<T>(MyList<T> list) where T : IDisposable, ICloneable
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

        static void WorkWithListCopy(MyList<ControlElement> list)
        {
            MyList<ControlElement> copy = (MyList<ControlElement>)list.Clone();
            Menu cloneMenu = new([
                "Напечатать изначальный список",
                "Напечатать копию",
                "Добавить в конец изначального списка",
                "Добавить в конец клона",
                "Удалить последний элемент из начального списка",
                "Удалить последний элемент из клона",
                "Редактировать элемент в начальном списке",
                "Редактировать элемент в клоне",
                "Назад"]);


            bool isClosed = false;
            while(!isClosed)
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();

                cloneMenu.ShowMenu();
                switch (cloneMenu.SetUserAnswer())
                {
                    case 1:
                        Clear();
                        PrintList(list);
                        WaitAnyButton();
                        Clear();
                        break;
                    case 2:
                        Clear();
                        PrintList(copy);
                        WaitAnyButton();
                        Clear();
                        break;
                    case 3:
                        Clear();
                        ControlElement newToList = new();
                        newToList.Init();
                        list.Add(newToList);
                        Clear();
                        break;
                    case 4:
                        Clear();
                        ControlElement newToClone = new();
                        newToClone.Init();
                        copy.Add(newToClone);
                        Clear();
                        break;
                    case 5:
                        Clear();
                        try
                        {
                            ControlElement removedFromList = list[^1];
                            list.RemoveAt(list.Count - 1);
                            WriteLine($"Элемент\n\t{removedFromList}\nудален");
                        }
                        catch
                        {
                            WriteLine("Ошибка! Список пуст.");
                        }
                        WaitAnyButton();
                        Clear();
                        break;
                    case 6:
                        Clear();
                        try
                        {
                            ControlElement removedFromCopy = copy[^1];
                            copy.RemoveAt(copy.Count - 1);
                            WriteLine($"Элемент\n\t{removedFromCopy}\nудален");
                        }
                        catch
                        {
                            WriteLine("Ошибка! Список пуст.");
                        }
                        WaitAnyButton();
                        Clear();
                        break;
                    case 7:
                        Clear();
                        PrintList(list);
                        list[(int)GetIntegerAnswer("Выберите элемент >>> ", 1, list.Count) - 1].Init();
                        Clear();
                        break;
                    case 8:
                        Clear();
                        PrintList(copy);
                        copy[(int)GetIntegerAnswer("Выберите элемент >>> ", 1, copy.Count) - 1].Init();
                        Clear();
                        break;
                    case 9:
                        Clear();
                        isClosed = true;
                        break;
                }
            }
            copy.Dispose();
            Clear();
        }

        static ControlElement? FindWithX(MyList<ControlElement> list, uint x)
        {
            foreach(ControlElement element in list)
            {
                if (element.X == x) return element;
            }
            return null;
        }
    }
}
