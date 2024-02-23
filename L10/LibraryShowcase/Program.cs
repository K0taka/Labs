using Lab10Lib;
using Lab9Lib;
using static IOLib.IO;
using static Lab10Lib.Requests;

namespace LibraryShowcase
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //-------------------------------------------------------------------------------------------------
            //демонстрация разницы меджду virtual и обычными методами
            //-------------------------------------------------------------------------------------------------
            WriteLine("Демостнрация разницы между затенением и virtual методами классов");
            EmptyLine();

            ControlElement[] elements = CreateArray(20); //инициализация массива со случайными элементами внутри

            for (int index = 0; index < elements.Length; index++)
            {
                WriteLine($"Элемент {index + 1}");
                WriteLine(elements[index].ToString());//виртуальный метод
                elements[index].Show();//обычный метод
                EmptyLine();
            }
            WaitAnyButton();//ожидание нажатие любой кнопки перед продолжением работы проги

            Clear();

            //-------------------------------------------------------------------------------------------------
            //демонстрация работы id
            //-------------------------------------------------------------------------------------------------
            WriteLine("Демонстрация работы ID");
            EmptyLine();

            elements[0] = new ControlElement();//заменяем первый по счету элемент
            elements[0].RandomInit();//заполняем его случайными значениями
            elements[0].Show();//между обычным методом и vitual в данном случае нет разницы, так как для ControlElement они показывают одинаковый результат.
            CollectGarbige();//собираем мусор из памяти
            elements[1] = new Button();
            elements[1].RandomInit();
            elements[1].Show();
            WaitAnyButton();

            Clear();

            //-------------------------------------------------------------------------------------------------
            //демотнрация запросов
            //-------------------------------------------------------------------------------------------------
            WriteLine("Демонстрация запросов");
            EmptyLine();

            //зададим несколько объект с одинаковыми X:
            uint x = elements[0].X;
            WriteLine($"Координата X с множеством объектов: {x}");
            for (int index = 1; index < 5; index++)
            {
                elements[index].X = x;
            }

            //-------------------------------------------------------------------------------------------------
            //выполнение запросов
            //-------------------------------------------------------------------------------------------------
            uint xPos = (uint)GetIntegerAnswer("Введите x для поиска >>> ", 0, 1980);
            string[] EnableMutiBtnText = SendRequest(elements, Request.EnableMultButtonText);//запрос текста на включенных мульти-кнопках
            string[] AllElementsAtXPos = SendRequest(elements, Request.AllElementsAtXPos, xPos);//запрос всех элементов на заданной X
            string[] ExistTextWithExistHint = SendRequest(elements, Request.ExistTextWithExistHint);//запрос всего текста у текстовых полей с имеющейся подсказкой

            WriteLine("Текущий массив выглядит так:");//печать текущего массива
            PrintArray(elements);

            EmptyLine();//пустая строка-разделитель
            WriteLine("Запрос на получение всего текста у включенных кнопок");
            if (EnableMutiBtnText.Length > 0)//если были найдены нужные кнопки
                ShowRequestResult(EnableMutiBtnText);//вывод текста
            else
                WriteLine("Нет таких кнопок");//вывод сообщения о том что ничего не найдено

            EmptyLine();
            WriteLine("Запрос на получение всего существующего текста у текстовых полей с непустыми подсказками");
            if (ExistTextWithExistHint.Length > 0)//если нашлись такие текстовые поля
                ShowRequestResult(ExistTextWithExistHint);//то выведем их содержимое (текст)
            else
                WriteLine("Нет удовлетворяющих запросу текстовых полей");//иначе сообщение что таких полей нет 

            EmptyLine();
            WriteLine("Запрос на получение информации о всех элементах на позиции X");
            if (AllElementsAtXPos.Length > 0)//если нашлись элементы по заданной координате
                ShowRequestResult(AllElementsAtXPos);//то выведем их
            else
                WriteLine("Нет элементов по заданной координате X");//иначе сообщение о том что элементов нет
            WaitAnyButton();

            Clear();

            //-------------------------------------------------------------------------------------------------
            //Clone and ShallowCopy
            //-------------------------------------------------------------------------------------------------
            WriteLine("Демонстрация различий Clone и ShallowCopy");
            EmptyLine();

            DisposeArray(elements);//перед выведением всех элементов из массива - Despose на каждый из них (выключает деструктор при GC для них).
            elements = new Button[3];
            elements[0] = new Button(0, 120, "Просто текст");//создаем кнопку
            elements[1] = (Button)elements[0].Clone();//слонируем ее
            elements[2] = (Button)elements[0].ShallowCopy();//копируем ее

            WriteLine($"Изначальный объект: {elements[0]}");//показываем начальный объект
            EmptyLine();

            ((Button)elements[1]).X = 1920;//так как Clone - не меняется координаты первоначального объекта
            WriteLine($"Изначальный объект: {elements[0]}");
            WriteLine($"Объект созданный методом Clone: {elements[1]}");
            EmptyLine();

            ((Button)elements[2]).X = 1919;//так как ShallowCopy - координаты изначального меняются
            WriteLine($"Изначальный объект: {elements[0]}");
            WriteLine($"Объект созданный методом ShallowCopy: {elements[2]}");
            EmptyLine();

            WaitAnyButton();
            DisposeArray(elements);
            Clear();

            //-------------------------------------------------------------------------------------------------
            //Init[] и проход по нему. Все объекты из иерархии + Weather
            //-------------------------------------------------------------------------------------------------
            WriteLine("Демонстрация работы с интерфейсом");
            EmptyLine();

            IInit[] iElements = CreateIInitArray();//массив объектов, реализующих интерфейс IInit
            Dictionary<Type, byte> numTypes = [];//словарь для хранения кол-ва элементов в виде ТИП::КОЛИЧЕСТВО

            foreach (IInit element in iElements)//цикл по массиву
            {
                if (numTypes.ContainsKey(element.GetType()))//если тип элемента в словаре
                    numTypes[element.GetType()]++;//то увеличим счетчик этого типа
                else
                    numTypes[element.GetType()] = 1;//иначе внесет тип, укажем для этого типа счетчик 1
            }

            foreach (KeyValuePair<Type, byte> item in numTypes)//для каждой пары ТИП::КОЛИЧЕСТВО из словаря
            {
                WriteLine($"Объектов типа {item.Key.Name} содержится {item.Value}");//выведем тип и количество
            }
            WaitAnyButton();
            Clear();
            DisposeArray(iElements);


            //-------------------------------------------------------------------------------------------------
            //Сортировка массива из элементов иерархии. IComparer (по X, Y) и IComparable по ID. Бинарный поиск
            //-------------------------------------------------------------------------------------------------
            WriteLine("Сортировка массива и бинарный поиск");
            EmptyLine();

            elements = CreateArray(5);
            ControlElement searchTo = elements[2];

            WriteLine("Текущий массив выглядит так:");
            PrintArray(elements);

            Array.Sort(elements, new SortByX());//сортировка по X, интерфейс IComparer
            EmptyLine();
            PrintArray(elements);

            int binsearch = Array.BinarySearch(elements, searchTo, new SortByX());//бинарный поиск по X, интерфейс IComparer
            WriteLine($"Результат бинарного поиска: {binsearch}");

            Array.Sort(elements, new SortByY());//сортировка по Y, интерфейс IComparer
            EmptyLine();
            PrintArray(elements);

            binsearch = Array.BinarySearch(elements, searchTo, new SortByY());//бинарный поиск по Y, интерфейс IComparer
            WriteLine($"Результат бинарного поиска: {binsearch}");

            Array.Sort(elements);//стандартная сортировка по ID, интерфейс IComparable
            EmptyLine();
            PrintArray(elements);
            binsearch = Array.BinarySearch(elements, searchTo);//бинпоиск по-умолчанию, ID
            WriteLine($"Результат бинарного поиска: {binsearch}");

            WaitAnyButton();
            DisposeArray(elements);
            Clear();

            //-------------------------------------------------------------------------------------------------
            //Демонстрация разницы в использовании is, as и typeof
            //-------------------------------------------------------------------------------------------------
            WriteLine("Демонстрация разницы в использовании is, as и typeof");
            EmptyLine();

            Button Btn = new();
            Btn.RandomInit();

            WriteLine($"Результат выражения Btn is ControlElement = {Btn is ControlElement}");
            WriteLine($"Результат выражения typeof(ControlElement) == Btn.GetType(): {typeof(ControlElement) == Btn.GetType()}");
            EmptyLine();
            WriteLine($"Результат выражения Btn is Button = {Btn is Button}");
            WriteLine($"Результат выражения typeof(ControlElement) == Btn.GetType(): {typeof(Button) == Btn.GetType()}");
            EmptyLine();
            (Btn as ControlElement).Show();
            WriteLine($"Результат выражения Btn as Button: {(Btn as MultButton) == null}");
        }

        static ControlElement[] CreateArray(int len)
        {
            ControlElement[] elements = new ControlElement[len];
            for (int i = 0; i < elements.Length; i++)
            {
                switch (i % 4)
                {
                    case 0:
                        elements[i] = new ControlElement();
                        elements[i].RandomInit();
                        break;
                    case 1:
                        elements[i] = new Button();
                        elements[i].RandomInit();
                        break;
                    case 2:
                        elements[i] = new MultButton();
                        elements[i].RandomInit();
                        break;
                    case 3:
                        elements[i] = new TextField();
                        elements[i].RandomInit();
                        break;
                }
            }
            return elements;
        }

        static IInit[] CreateIInitArray()
        {
            IInit[] elements = new IInit[20];
            for (int i = 0; i < elements.Length; i++)
            {
                switch (i % 5)
                {
                    case 0:
                        elements[i] = new ControlElement();
                        elements[i].RandomInit();
                        break;
                    case 1:
                        elements[i] = new Button();
                        elements[i].RandomInit();
                        break;
                    case 2:
                        elements[i] = new MultButton();
                        elements[i].RandomInit();
                        break;
                    case 3:
                        elements[i] = new TextField();
                        elements[i].RandomInit();
                        break;
                    case 4:
                        elements[i] = new Weather();
                        elements[i].RandomInit();
                        break;
                }
            }
            return elements;
        }

        static void CollectGarbige()
        {
            GC.Collect(GC.MaxGeneration, GCCollectionMode.Aggressive, true);
            GC.WaitForPendingFinalizers();
        }

        static void DisposeArray(Array arr)
        {
            for (int index = 0; index < arr.Length; index++)
            {
                if (arr.GetValue(index) is ControlElement element)
                {
                    element.Dispose();
                    arr.SetValue(null, index);
                }
            }
            CollectGarbige();
        }

        static void ShowRequestResult(string[] array)
        {
            for (int index = 0; index < array.Length; index++)
            {
                WriteLine($"Элемент {index + 1}:: {array[index]}");
            }
        }

        static void PrintArray(Array arr)
        {
            for (int index = 0; index < arr.Length; index++)
            {
                WriteLine($"Элемент {index + 1}:: {arr.GetValue(index)}");
            }
        }
    }
}
