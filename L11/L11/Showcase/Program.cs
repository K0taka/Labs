using Lab10Lib;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace Showcase
{
    internal class Program
    {
        const int standartLen = 10;
        static readonly Random rand = new();
        static void Main()
        {
            TFirst();
            GC.Collect();
            GC.WaitForPendingFinalizers();
            TSecond();
            GC.Collect();
            GC.WaitForPendingFinalizers();
            TThird();
        }

        static void TFirst()
        {
            Console.Clear();
            Console.WriteLine("Часть 1: Работа с коллекцией SortedList");
            SortedList universalCollection = []; //Объявление коллекции SortedList

            FillCollection(universalCollection);
            Print(universalCollection);
            Console.ReadKey(false);


            uint keyToDel = (uint)universalCollection.GetKey(rand.Next(standartLen)); //id, добавляемое как ключ - число. Но потребовалось
                                                                                                    //явное приведение типов object -> int
                                                                                                    //так как универсальная коллекция хранит object
            Console.WriteLine($"Удаляем элемент с ключем {keyToDel}");
            universalCollection.Remove(keyToDel);
            Print(universalCollection);
            Console.ReadKey(false);


            uint xCor = (uint)rand.Next(0, 1981);
            uint rId = (uint)rand.Next(standartLen);
            Console.WriteLine("Запросы:");
            Console.Write($"Количество элементов с координатой x < {xCor}: ");
            Console.WriteLine(FewerThanX(universalCollection, xCor));
            Console.Write($"Количество элементов с id в радиусе 3 от {rId}: ");
            Console.WriteLine(AllObjNearId(universalCollection, rId));
            Console.Write($"Количество элементов с координатой x < y: ");
            Console.WriteLine(AllxLessY(universalCollection));
            Console.ReadKey(false);

            Console.WriteLine("Вывод всех ключей на экран:");
            foreach (DictionaryEntry entry in universalCollection)
            {
                Console.Write(entry.Key);
                Console.Write(" ");
            }
            Console.Write("\n");
            Console.ReadKey(false);

            SortedList shallowCopy = (SortedList)universalCollection.Clone();
            SortedList clone = DeepCopy(universalCollection);

            Console.WriteLine($"На данный момент по индексу 0 находится следующий элемент: \n{universalCollection.GetByIndex(0)}");
            ((ControlElement)clone.GetByIndex(0)).X = 1920; ((ControlElement)clone.GetByIndex(0)).Y = 1080;
            Console.WriteLine($"После изменения координат в клоне на 1920 1080 элемент по индексу 0 выглядит так: \n{universalCollection.GetByIndex(0)}");
            ((ControlElement)shallowCopy.GetByIndex(0)).X = 1920; ((ControlElement)shallowCopy.GetByIndex(0)).Y = 1080;
            Console.WriteLine($"После изменения координат в копии на 1920 1080 элемент по индексу 0 выглядит так: \n{universalCollection.GetByIndex(0)}");

            //сортировка не требуется, так как коллекция сортируется по-умолчанию
            Console.WriteLine("Производим поиск элементов");
            ControlElement toFind = new(1010, 1010);
            uint toFindId = toFind.Id;
            FoundCheck(universalCollection.IndexOfKey(toFindId), toFindId.ToString()); //результат - индекс элемента -1, так как он еще не в коллекции
            FoundCheck(universalCollection.IndexOfValue(toFind), toFind.ToString()); //результат - индекс -1, так как он еще не в коллекции
            universalCollection.Add(toFindId, toFind);
            FoundCheck(universalCollection.IndexOfKey(toFindId), toFindId.ToString()); //результат - индекс элемента, такое значение теперь есть
            FoundCheck(universalCollection.IndexOfValue(toFind), toFind.ToString()); //результат - индекс, такое значение есть
        }

        static void TSecond()
        {
            Console.Clear();
            Console.WriteLine("Часть 2: работа с колелкцией Dictionary<TKey, TValue>");
            Dictionary<uint, ControlElement> genericCollection = []; //Объявление коллекции SortedList

            FillCollection(genericCollection);
            Print(genericCollection);
            Console.ReadKey(false);


            uint keyToDel = genericCollection.GetKeyByIndex(rand.Next(standartLen));
            Console.WriteLine($"Удаляем элемент с ключем {keyToDel}");
            genericCollection.Remove(keyToDel);
            Print(genericCollection);
            Console.ReadKey(false);


            uint xCor = (uint)rand.Next(0, 1981);
            uint rId = (uint)rand.Next(standartLen);
            Console.WriteLine("Запросы:");
            Console.Write($"Количество элементов с координатой x < {xCor}: ");
            Console.WriteLine(FewerThanX(genericCollection, xCor));
            Console.Write($"Количество элементов с id в радиусе 3 от {rId}: ");
            Console.WriteLine(AllObjNearId(genericCollection, rId));
            Console.Write($"Количество элементов с координатой x < y: ");
            Console.WriteLine(AllxLessY(genericCollection));
            Console.ReadKey(false);

            Console.WriteLine("Вывод всех ключей на экран:");
            foreach (KeyValuePair<uint, ControlElement> entry in genericCollection)
            {
                Console.Write(entry.Key);
                Console.Write(" ");
            }
            Console.Write("\n");
            Console.ReadKey(false);
            uint keyToInspect = genericCollection.GetKeyByIndex(rand.Next(standartLen));
            Dictionary<uint, ControlElement> clone = Clone(genericCollection);
            Dictionary<uint, ControlElement> shallowCopy = ShallowCopy(genericCollection);

            Console.WriteLine($"На данный момент по ключу {keyToInspect} находится следующий элемент: \n{genericCollection[keyToInspect]}");
            clone[keyToInspect].X = 1920; clone[keyToInspect].Y = 1080;
            Console.WriteLine($"После изменения координат в клоне на 1920 1080 элемент по индексу 0 выглядит так: \n{genericCollection[keyToInspect]}");
            shallowCopy[keyToInspect].X = 1920; shallowCopy[keyToInspect].Y = 1080;
            Console.WriteLine($"После изменения координат в копии на 1920 1080 элемент по индексу 0 выглядит так: \n{genericCollection[keyToInspect]}");

            //сортировка не требуется, так как коллекция ее не поддерживает
            Console.WriteLine("Производим поиск элементов");
            ControlElement toFind = new(1010, 1010);
            uint toFindId = toFind.Id;
            FoundCheck(genericCollection.ContainsKey(toFindId), toFindId.ToString()); //результат - false, элемент еще не в коллекции
            FoundCheck(genericCollection.ContainsValue(toFind), toFind.ToString()); //результат - false, элемент еще не в коллекции

            genericCollection.Add(toFindId, toFind);

            FoundCheck(genericCollection.ContainsKey(toFindId), toFindId.ToString()); //результат - true, элемент еще не в коллекции
            FoundCheck(genericCollection.ContainsValue(toFind), toFind.ToString()); //результат - true, элемент еще не в коллекции
        }

        static void TThird()
        {
            Console.Clear();
            Console.WriteLine("Часть 3: Измерение скорости доступа, среднее время");
            TestCollections tests = new();
            long[] startResults = tests.MeasureStart();
            long[] midResults = tests.MeasureMid();
            long[] endResults = tests.MeasureEnd();
            long[] notExistResults = tests.MeasureNotExist();
            string sep = "\n-------------------------------------------------------------------\n";
            Console.WriteLine(sep);
            MeasurePrint(startResults, "вначале");
            Console.ReadKey(false);
            Console.WriteLine(sep);
            MeasurePrint(midResults, "в середине");
            Console.ReadKey(false);
            Console.WriteLine(sep);
            MeasurePrint(endResults, "в конце");
            Console.ReadKey(false);
            Console.WriteLine(sep);
            MeasurePrint(notExistResults, "вне коллекции");

        }

        static void MeasurePrint(long[] result, string pos)
        {
            Console.WriteLine($"Измерение {pos}, Stack<Button>: {result[0] / TestCollections.numTests} тиков");
            Console.WriteLine($"Измерение {pos}, Stack<string>: {result[1] / TestCollections.numTests} тиков");
            Console.WriteLine($"Измерение значения {pos}, SortedDictionary<ControlElement, Button>: {result[2] / TestCollections.numTests} тиков");
            Console.WriteLine($"Измерение значения {pos}, SortedDictionary<string, Button>: {result[3] / TestCollections.numTests} тиков");
            Console.WriteLine($"Измерение ключа {pos}, SortedDictionary<ControlElement, Button>: {result[4] / TestCollections.numTests} тиков");
            Console.WriteLine($"Измерение ключа {pos}, SortedDictionary<string, Button>: {result[5] / TestCollections.numTests} тиков");
        }

        static void FillCollection(IDictionary collection)
        {
            for (int i = 0; i < standartLen; i += 2) //Добавление элементов в коллекцию
            {
                ControlElement controlElement = new();
                controlElement.RandomInit();

                Button button = new();
                button.RandomInit();

                collection.Add(controlElement.Id, controlElement);
                collection.Add(button.Id, button);
            }
        }

        static int FewerThanX(IDictionary collection, uint xCor)
        {
            int found = 0;
            foreach (DictionaryEntry item in collection)
            {
                if (item.Value is ControlElement ce)
                {
                    if (ce.X < xCor)
                        found++;
                }
            }
            return found;
        }

        static int AllObjNearId(IDictionary collection, uint id)
        {
            int found = 0;
            foreach (DictionaryEntry item in collection)
            {
                if (item.Key is uint cId)
                {
                    if (cId >= id - 3 && cId <= id + 3 && cId != id)
                        found++;
                }
            }
            return found;
        }

        static int AllxLessY(IDictionary collection)
        {
            int found = 0;
            foreach (DictionaryEntry item in collection)
            {
                if (item.Value is ControlElement ce)
                {
                    if (ce.X < ce.Y)
                        found++;
                }
            }
            return found;
        }

        static void Print(IDictionary collection)
        {
            Console.WriteLine("--------------------------------------------------------------------------------");
            Console.WriteLine("Текущее состояние объекта:");
            foreach (DictionaryEntry item in collection) //для представления объекта коллекции используется структура DictionaryEntry
            {
                Console.WriteLine($"Ключ: {item.Key}, соответствующий ему элемент: {item.Value}");
            }
            Console.WriteLine("--------------------------------------------------------------------------------");
        }

        static void FoundCheck(int index, string elem)
        {
            if (index < 0)
                Console.WriteLine($"Указанный элемент ({elem}) не найден");
            else
                Console.WriteLine($"Элемент {elem} найден, его индекс {index}");
        }

        static void FoundCheck(bool isFound, string elem)
        {
            if (!isFound)
                Console.WriteLine($"Указанный элемент ({elem}) не найден");
            else
                Console.WriteLine($"Элемент {elem} найден");
        }

        public static Dictionary<uint, ControlElement> Clone(Dictionary<uint, ControlElement> dict)
        {
            Dictionary<uint, ControlElement> nDict = [];
            foreach (KeyValuePair<uint, ControlElement> kvp in dict)
                nDict.Add(kvp.Key, (ControlElement)kvp.Value.Clone());
            return nDict;
        }

        public static Dictionary<uint, ControlElement> ShallowCopy(Dictionary<uint, ControlElement> dict)
        {
            Dictionary<uint, ControlElement> nDict = [];
            foreach (KeyValuePair<uint, ControlElement> kvp in dict)
                nDict.Add(kvp.Key, (ControlElement)kvp.Value.ShallowCopy());
            return nDict;
        }

        public static SortedList DeepCopy(SortedList list)
        {
            SortedList result = [];
            foreach (DictionaryEntry item in list)
            {
                if (item.Key is uint && item.Value is ControlElement ce)
                    result.Add(item.Key, ce.Clone());
                else
                    throw new InvalidDataException();
            }
            return result;
        }
    }

    public static class DictionaryExtention
    {
        public static TKey GetKeyByIndex<TKey, TValue>(this Dictionary<TKey,TValue> dict, int index) where TKey : notnull
        {
            int currIndex = 0;
            foreach(KeyValuePair<TKey,TValue> kvp in dict)
            {
                if (currIndex == index)
                    return kvp.Key;
                currIndex++;
            }
            throw new KeyNotFoundException();
        }
    }
}
