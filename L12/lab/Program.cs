global using Lab10Lib;
global using IOLib;
global using static IOLib.IO;
using System.Transactions;

namespace lab
{
    internal class Program
    {
        static readonly Random rand = new();
        static void Main()
        {
            Menu MainMenu = new(["Часть 1. Список", "Часть 2. Hashtable с цепочками", "Часть 3. Идеально сбалансированное дерево"]);
            while (true)
            {
                MainMenu.ShowMenu();
                switch(MainMenu.SetUserAnswer())
                {
                    case 1:
                        Clear();
                        ListPart();
                        break;
                    case 2:
                        Clear();
                        HashTablePart();
                        break;

                    default:
                        Clear();
                        PBTPart();
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
            list.Dispose();
        }

        static ControlElement GenerateRandHierObj(int start = 0, int end = 4)
        {
            ControlElement element = rand.Next(start, end) switch
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

        static void HashTablePart()
        {
            Menu HashTableMenu = new(["Заполнить случайными", "Добавить элемент", "Печать", "Удалить элемент", "Поиск", "Назад в главное меню"]);
            int capacity = (int)GetIntegerAnswer("Укажите вместимость хэш-таблицы >>> ",1, 100);
            MyHashTable<ControlElement, ControlElement> hashTable = new(capacity);
            Clear();
            bool isClosed = false;
            while (!isClosed)
            {
                GC.Collect(GC.MaxGeneration);
                GC.WaitForPendingFinalizers();

                HashTableMenu.ShowMenu();
                switch (HashTableMenu.SetUserAnswer())
                {
                    case 1:
                        Clear();
                        int num = (int)GetIntegerAnswer("Укажите количество элементов >>> ", 0, 20);
                        FillHashTable(num, hashTable);
                        Clear();
                        break;
                    case 2:
                        Clear();
                        Button bt = new();
                        WriteLine("Укажите элемент для добавления");
                        bt.Init();
                        if (hashTable.Add(new(bt.X, bt.Y), bt))
                            WriteLine("Элемент успешно добавлен");
                        else
                            WriteLine("Не удалось добавить элемент");
                        WaitAnyButton();
                        Clear();
                        break;
                    case 3:
                        Clear();
                        PrintHashTable(hashTable);
                        WaitAnyButton();
                        Clear();
                        break;
                    case 4:
                        Clear();
                        PrintHashTable(hashTable);
                        if (hashTable.Count > 0)
                        {
                            WriteLine("Ключ для удаления:");
                            ControlElement remove = new();
                            remove.Init();
                            if (hashTable.Remove(remove))
                                WriteLine("Элемент успешно удален!");
                            else
                                WriteLine("Не удалось удалить элемент!");
                        }
                        WaitAnyButton();
                        Clear();
                        break;
                    case 5:
                        Clear();
                        PrintHashTable(hashTable);
                        if (hashTable.Count > 0)
                        {
                            WriteLine("Ключ для удаления:");
                            ControlElement find = new();
                            find.Init();
                            if (hashTable.Contains(find))
                            {
                                WriteLine("Элемент успешно найден!");
                                WriteLine($"\tKey: {find}\n\tValue: {hashTable[find]}");
                            }
                            else
                                WriteLine("Не удалось найти элемент!");
                        }
                        WaitAnyButton();
                        Clear();
                        break;
                    case 6:
                        Clear();
                        isClosed = true;
                        break;
                }
            }
            Clear();
            hashTable = null;
            GC.Collect(GC.MaxGeneration);
            GC.WaitForPendingFinalizers();
        }

        static void FillHashTable(int count, MyHashTable<ControlElement, ControlElement> hashTable)
        {
            for (int i = 0; i < count; i++)
            {
                ControlElement elem = GenerateRandHierObj(start: 1);
                ControlElement key = new(elem.X, elem.Y);
                if (!hashTable.Add(key, elem))
                    i--;
            }
        }

        static void PrintHashTable(MyHashTable<ControlElement, ControlElement> hashTable)
        {
            if (hashTable.Count == 0)
            {
                WriteLine("Хэш-таблица пуста в данный момент");
                return;
            }
            int code = 0;
            WriteLine("Хэш-таблица выглядит так:");
            foreach (var chain in hashTable)
            {
                WriteLine($"Ячейка {code++} в таблице:");
                if (chain == null || chain.Count == 0)
                {
                    WriteLine("Данная ячейка пуста!");
                }
                else
                {
                    string container = "";
                    foreach (var node in chain)
                    {
                        container = container + $"\n  Key: {node.Key}\n  Value: {node.Data}";
                    }
                    WriteLine(container);
                }
            }
        }

        static void PBTPart()
        {
            Menu PBTMenu = new(["Заполнить случайными", "Заполнить вручную", "Печать", "Количество листьев", "Назад в главное меню"]);
            PBT<ControlElement>? tree = null;
            bool isClosed = false;
            while (!isClosed)
            {
                GC.Collect(GC.MaxGeneration);
                GC.WaitForPendingFinalizers();
                PBTMenu.ShowMenu();
                switch (PBTMenu.SetUserAnswer())
                {
                    case 1:
                        Clear();
                        int count = (int)GetIntegerAnswer("Укажите количество элементов >>> ", 1, 20);
                        ControlElement[] arr = new ControlElement[count];
                        for(int i = 0; i < count; i++)
                            arr[i] = GenerateRandHierObj();
                        tree = new(arr);
                        Clear();
                        break;
                    case 2:
                        Clear();

                        Clear();
                        break;
                    case 3:
                        Clear();
                        if (tree == null)
                        {
                            WriteLine("Дерево не заполнено!");
                        }
                        else
                        {
                            WriteLine("\n" + CreateString(tree));
                        }
                        WaitAnyButton();
                        Clear();
                        break;
                    case 4:
                        break;
                    case 5:
                        isClosed = true;
                        break;
                }
            }
            Clear();
            tree = null;
            GC.Collect(GC.MaxGeneration);
            GC.WaitForPendingFinalizers();
        }

        static string CreateString(PBT<ControlElement> tree)
        {
            return tree.Capacity > 7 ? CreateStringHorizontalLayout(tree.Root) : CreateStringVerticalLayout(tree);
        }

        static string CreateStringVerticalLayout(PBT<ControlElement> tree)
        {
            if (tree.Root == null)
                return "Дерево пустое";

            string result = "";
            
            int levels = (int)Math.Log2(tree.Capacity) + 1;
            string[] spaces = new string[levels+1];
            int area = 145;  //(int)Math.Pow(2, levels)*(23+1);

            for (int level = 0; level <= levels; level++)
            {
                int elems = (int)Math.Pow(2, level);
                int len = (area - elems * 22) / (elems + 1);
                spaces[level] = CreateSpace(len);
            }

            Queue<TreeNode<ControlElement>?> elements = new();
            elements.Enqueue(tree.Root);
            int passed = 0;
            int currLevel = 0;
            while (elements.Count > 0)
            {
                TreeNode<ControlElement>? curr = elements.Dequeue();
                passed++;

                if ((int)Math.Log2(passed) + 1 > currLevel)
                {
                    currLevel = (int)Math.Log2(passed) + 1;
                    result += "\n";
                }

                if (curr != null)
                {
                    elements.Enqueue(curr.Left);
                    elements.Enqueue(curr.Right);
                    result += spaces[currLevel - 1] + $"ID: {curr.Data.Id} X: {curr.Data.X} Y: {curr.Data.Y}";
                }
                else
                    result += spaces[currLevel - 1] + $"                       ";
            }
            return result;
        }

        static string CreateStringHorizontalLayout(TreeNode<ControlElement> node, int spaces = 0)
        {
            if (node == null)
                return "";
            string line = "";
            line = CreateSpace(spaces) + $"ID: {node.Data.Id} X: {node.Data.X} Y: {node.Data.Y}\n";
            if (node.Left != null)
                line = line + CreateStringHorizontalLayout(node.Left, spaces+5);
            if (node.Right != null)
                line = CreateStringHorizontalLayout(node.Right, spaces+5) + line;
            return line;
        }

        static string CreateSpace(int space)
        {
            string res = "";
            for (int i = 0; i < space; i++)
                res += " ";
            return res;
        }
    }
}
