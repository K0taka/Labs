global using Lab10Lib;
global using IOLib;
global using static IOLib.IO;
using AVLTree;
using System.Runtime.CompilerServices;
using System.ComponentModel.Design.Serialization;

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
                switch (MainMenu.SetUserAnswer())
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
            while (!isClosed)
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
            foreach (ControlElement element in list)
            {
                if (element.X == x) return element;
            }
            return null;
        }

        static void HashTablePart()
        {
            Menu HashTableMenu = new(["Заполнить случайными", "Добавить элемент", "Печать", "Удалить элемент", "Поиск", "Назад в главное меню"]);
            int capacity = (int)GetIntegerAnswer("Укажите вместимость хэш-таблицы >>> ", 1, 100);
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
            Menu PBTMenu = new(["Заполнить ИСД случайными", "Заполнить ИСД вручную", "Печать", "Количество листьев", "Заполнить АВЛ по ИСД",
                "Заполнить АВЛ случайными", "Заполнить АВЛ вручную", "Добавить в АВЛ элемент", "Удалить из АВЛ", "Поиск в АВЛ",
                "Работа с клонами", "Foreach", "Назад в главное меню"]);
            PBT<ControlElement>? PBTtree = null;
            AVL<ControlElement, ControlElement>? AVLtree = null;
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
                            arr[i] = GenerateRandHierObj(1);
                        PBTtree = new(arr);
                        Clear();
                        break;
                    case 2:
                        Clear();
                        int count2 = (int)GetIntegerAnswer("Укажите количество элементов >>> ", 1, 20);
                        ControlElement[] arr2 = new ControlElement[count2];
                        for (int i = 0; i < count2; i++)
                        {
                            Write($"Элемент {i+1}: \n");
                            arr2[i] = new();
                            arr2[i].Init();
                        }
                        PBTtree = new(arr2);
                        Clear();
                        break;
                    case 3:
                        Clear();
                        PrintTrees(PBTtree, AVLtree);
                        WaitAnyButton();
                        Clear();
                        break;
                    case 4:
                        Clear();
                        PrintLeafs(PBTtree, AVLtree);
                        WaitAnyButton();
                        Clear();
                        break;
                    case 5:
                        Clear();
                        if (PBTtree == null || PBTtree.Capacity == 0)
                            WriteLine("ИС-дерево пустое, не из чего копировать");
                        else
                        {
                            AVLtree = new();
                            foreach(var item in PBTtree)
                            {
                                try
                                {
                                    AVLtree.Add(new ControlElement(item.Data.X, item.Data.Y), (ControlElement)item.Data.Clone());
                                }
                                catch (Exception ex)
                                {
                                    WriteLine($"Внимание! была следующая ошибка: {ex.Message}, {ex.GetType().Name}");
                                }
                            }
                            WriteLine("Копирование успешно произведено!");
                        }
                        WaitAnyButton();
                        Clear();
                        break;
                    case 6:
                        Clear();
                        AVLtree = new();
                        int count3 = (int)GetIntegerAnswer("Укажите количество элементов >>> ", 1, 20);
                        for (int i = 0; i < count3; i++)
                        {
                            ControlElement obj = GenerateRandHierObj(1);
                            try
                            {
                                AVLtree.Add(new ControlElement(obj.X, obj.Y), obj);
                            }
                            catch
                            {
                                i--;
                            }
                        }
                        Clear();
                        break;
                    case 7:
                        Clear();
                        AVLtree = new();
                        int count4 = (int)GetIntegerAnswer("Укажите количество элементов >>> ", 1, 20);
                        for (int i = 0; i < count4; i++)
                        {
                            WriteLine($"Элемент {i}:");
                            ControlElement obj1 = GenerateRandHierObj(1);
                            obj1.Init();
                            try
                            {
                                AVLtree.Add(new ControlElement(obj1.X, obj1.Y), obj1);
                            }
                            catch
                            {
                                WriteLine("Элемент с таким ключем уже добавлен! Повторите попытку!");
                                i--;
                            }
                        }
                        Clear();
                        break;
                    case 8:
                        Clear();
                        Button toAdd = new();
                        WriteLine("Добавляем элемент >>> ");
                        toAdd.Init();
                        try
                        {
                            AVLtree.Add(new ControlElement(toAdd.X, toAdd.Y), toAdd);
                            WriteLine("Успешно!");
                        }
                        catch (NullReferenceException)
                        {
                            WriteLine("Дерево было не создано");
                            AVLtree = new();
                            AVLtree.Add(new ControlElement(toAdd.X, toAdd.Y), toAdd);
                            WriteLine("Успешно!");
                        }
                        catch
                        {
                            WriteLine("Элемент с таким ключом уже существует");
                        }
                        WaitAnyButton();
                        break;
                    case 9:
                        Clear();
                        if (AVLtree == null || AVLtree.Root == null)
                            WriteLine("Дерево пустое, ничего не удалить");
                        else
                        {
                            ControlElement toDelete = new();
                            WriteLine("Элемент для удаления:");
                            toDelete.Init();
                            if (AVLtree.Remove(toDelete))
                                WriteLine("Удалено!!");
                            else
                                WriteLine("Не удалено");
                        }
                        WaitAnyButton();
                        Clear();
                        break;
                    case 10:
                        Clear();
                        if (AVLtree == null)
                            WriteLine("Дерево не создано");
                        else
                        {
                            ControlElement toFind = new();
                            WriteLine("Элемент для поиска:");
                            toFind.Init();
                            if (AVLtree.ContainsKey(toFind))
                                WriteLine($"Найдено! Значение: {AVLtree[toFind]}");
                            else
                                WriteLine($"Не найдено!");
                        }
                        WaitAnyButton();
                        Clear();
                        break;
                    case 11:
                        Clear();
                        WorkWithTreeCopy(AVLtree);
                        Clear();
                        break;
                    case 12:
                        Clear();
                        if (AVLtree == null || AVLtree.Count == 0)
                        {
                            WriteLine("Пусто");
                        }
                        else
                        {
                            WriteLine("Демонстрация foreach. В порядке возрастания");
                            foreach(var item in AVLtree)
                            {
                                WriteLine($"Key {item.Key},\nValue{item.Value}\n");
                            }
                            WriteLine("Демонстрация foreach. В ширину.");
                            foreach (var item in AVLtree.InWideEnumerator())
                            {
                                WriteLine($"Key {item.Key},\nValue{item.Value}\n");
                            }
                        }
                        WaitAnyButton();
                        Clear();
                        break;
                    case 13:
                        isClosed = true;
                        break;
                }
            }
            Clear();
            PBTtree = null;
            GC.Collect(GC.MaxGeneration);
            GC.WaitForPendingFinalizers();
        }
        
        static void PrintTrees(PBT<ControlElement> PBTtree, AVL<ControlElement, ControlElement> AVLtree)
        {
            if (PBTtree == null || PBTtree.Root == null)
                WriteLine("ИС-дерево пустое");
            else
                WriteLine(CreateString(PBTtree));

            if (AVLtree == null || AVLtree.Root == null)
                WriteLine("AVL-дерево пустое");
            else
                WriteLine("\n"+CreateStringHorizontalLayout(AVLtree.Root));
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

        static string CreateStringHorizontalLayout(Node<ControlElement, ControlElement> node, int spaces = 0)
        {
            if (node == null)
                return "";
            string line = "";
            line = CreateSpace(spaces) + $"Key: {node.Entry.Key},\n{CreateSpace(spaces)}Value: {node.Entry.Value}\n";
            if (node.Left != null)
                line = line + CreateStringHorizontalLayout(node.Left, spaces + 5);
            if (node.Right != null)
                line = CreateStringHorizontalLayout(node.Right, spaces + 5) + line;
            return line;
        }

        static string CreateSpace(int space)
        {
            string res = "";
            for (int i = 0; i < space; i++)
                res += " ";
            return res;
        }

        static void PrintLeafs(PBT<ControlElement> PBTTree, AVL<ControlElement, ControlElement> AVlTree)
        {
            int count = 0;
            if (PBTTree == null)
                WriteLine("ИС-дерево пустое и листьев в нем нет");
            else
            {
                CountLeafsInPBT(PBTTree.Root, ref count);
                WriteLine($"В Ис-дереве содержится {count} листьев");
            }
            count = 0;
            if (AVlTree == null)
                WriteLine("AVL-дерево пустое и листьев в нем нет");
            else
            {
                CountLeafsInAVL(AVlTree.Root, ref count);
                WriteLine($"В AVL-дереве содержится {count} листьев");
            }
        }

        static void CountLeafsInPBT(TreeNode<ControlElement> node, ref int count)
        {
            if (node == null)
                return;
            if (node.Left == null && node.Right == null)
                count += 1;
            else
            {
                CountLeafsInPBT(node.Left, ref count);
                CountLeafsInPBT(node.Right, ref count);
            }
        }

        static void CountLeafsInAVL(Node<ControlElement, ControlElement> node, ref int count)
        {
            if (node == null)
                return;
            if (node.Left == null && node.Right == null)
            {
                count += 1;
            }
            else
            {
                CountLeafsInAVL(node.Left, ref count);
                CountLeafsInAVL(node.Right, ref count);
            }
        }

        static void WorkWithTreeCopy(AVL<ControlElement, ControlElement> tree)
        {
            if (tree == null)
                WriteLine("Дерево не инициализировано!");
            else
            {
                var clone = (AVL<ControlElement, ControlElement>)tree.Clone();
                var copy = (AVL<ControlElement, ControlElement>)tree.ShallowCopy();

                WriteLine("Оригинальное дерево:");
                if (tree.Count > 0)
                {
                    WriteLine("\n" + CreateStringHorizontalLayout(tree.Root));
                }
                else
                    WriteLine("Дерево пустое!");

                WriteLine("Глубоко скопированное дерево:");
                if (clone.Count > 0)
                {
                    WriteLine("\n" + CreateStringHorizontalLayout(clone.Root));
                }
                else
                    WriteLine("Дерево пустое!");

                WriteLine("Поверхностно скопированное дерево:");
                if (tree.Count > 0)
                {
                    WriteLine("\n" + CreateStringHorizontalLayout(copy.Root));
                }
                else
                    WriteLine("Дерево пустое!");

                WriteLine("Удалим корень из оригинального дерева: ");
                tree.Remove(tree.Root.Entry.Key);

                WriteLine("Оригинальное дерево:");
                if (tree.Count > 0)
                {
                    WriteLine("\n" + CreateStringHorizontalLayout(tree.Root));
                    WriteLine($"Количество элементов: {tree.Count}");
                }
                else
                    WriteLine("Дерево пустое!");

                WriteLine("Глубоко скопированное дерево:");
                if (clone.Count > 0)
                {
                    WriteLine("\n" + CreateStringHorizontalLayout(clone.Root));
                    WriteLine($"Количество элементов: {clone.Count}");
                }
                else
                    WriteLine("Дерево пустое!");

                WriteLine("Поверхностно скопированное дерево:");
                if (tree.Count > 0)
                {
                    WriteLine("\n" + CreateStringHorizontalLayout(copy.Root));
                    WriteLine($"Количество элементов: {copy.Count}");
                }
                else
                    WriteLine("Дерево пустое!");
            }
            WaitAnyButton();
        }
    }
}
