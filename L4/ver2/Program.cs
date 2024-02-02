using System;

namespace ver2
{
    internal class Program
    {
        static readonly Menu arrayPage = new Menu(new string[]
                    {"Ввести массив вручную",
                    "Использовать случайный массив элементов [-128, 127] заданной длины",
                    "Использовать случайный массив элементов [-128, 127] случайной длины из диапазона [1, 100]",
                    "Выход"});
        static readonly Menu funcPage = new Menu(new string[]
                    {"Найти и удалить максимальные элементы массива",
                    "Добавить K элементов в конец массива",
                    "Сдвинуть циклически элементы массива на M элементов влево",
                    "Найти первый отрицатальный элемент",
                    "Сортировка массива с помощью простого обмена",
                    "Сортировка массива с помощью быстрой сортировки по алгоритму Хоара",
                    "Выполнить бинарный поиск",
                    "Отменить и вернуться в меню выбора массива",
                    "Выход"});
        static readonly Menu afterPartyPage = new Menu(new string[]
                    {"Новое действие с текущим массивом",
                    "Закончить и вернуться в меню выбора массива",
                    "Выход"});
        static readonly ArrayLogic userArray = new ArrayLogic();
        static void Main()
        {
            bool isMainPoint = true;
            while (true)
            {
                if (isMainPoint)
                    SwitchMenu(0, ref isMainPoint);
                else
                    SwitchMenu(1, ref isMainPoint);
            }
        }

        static void SwitchMenu(byte wishMenu, ref bool isMainPoint)
        {
            switch (wishMenu)
            {
                case 0:
                    Console.Clear();
                    Console.WriteLine("Это программа для ЛР4. Сейчас Вы находитесь в главном меню. Здесь доступны следующие функции:");
                    arrayPage.ShowMenu();
                    arrayPage.SetUserAnswer();
                    Console.Clear();
                    Console.WriteLine($"Вы выбрали: {arrayPage.MenuObj[arrayPage.UserAnswer - 1]}");
                    CreateArray(arrayPage.UserAnswer);
                    SwitchMenu(1, ref isMainPoint);
                    break;
                case 1:
                    Console.Clear();
                    userArray.PrintArray();
                    funcPage.ShowMenu();
                    FuncPageLogic();
                    if (funcPage.UserAnswer == funcPage.MenuObj.Length - 1)
                    {
                        isMainPoint = true;
                        return;
                    }
                    SwitchMenu(2, ref isMainPoint);
                    break;
                case 2:
                    userArray.PrintArray();
                    afterPartyPage.ShowMenu();
                    afterPartyPage.SetUserAnswer();
                    if (afterPartyPage.UserAnswer == 1)
                    {
                        isMainPoint = false;
                        return;
                    }
                    isMainPoint = true;
                    break;
            }
        }

        static void CreateArray(byte creationMethod)
        {
            switch (creationMethod)
            {
                case 1:
                    Console.Write("Введите желаемую длину массива >>> ");
                    int arrayLen = GetArrayLen();
                    userArray.ManualArrayInit(arrayLen);
                    for (int index = 0; index < arrayLen; index++)
                    {
                        Console.Write($"Введите элемент #{index + 1} >>> ");
                        userArray.SetElement(index, GetCorrectEl());
                    }
                    break;
                case 2:
                    Console.Write("Введите желаемую длину массива >>> ");
                    userArray.RandomArrayWithN(GetArrayLen());
                    break;
                case 3:
                    userArray.RandomArrayWithoutN();
                    break;
            }
        }

        static int GetArrayLen()
        {
            int length;
            bool isCorrect;
            do
            {
                isCorrect = int.TryParse(Console.ReadLine(), out length);
                if (!isCorrect)
                {
                    Console.Write("Вы ввели значение, которые не может быть использовано для задания длины массива. Повторите ввод >>> ");
                }
                if (isCorrect && length < 0 || length > 532675567)
                {
                    isCorrect = false;
                    Console.Write("Недопустимое значение длины! Повторите ввод! >>> ");
                }
            } while (!isCorrect);
            return length;
        }

        static int GetCorrectEl()
        {
            int elem;
            bool isCorrect;
            do
            {
                isCorrect = int.TryParse(Console.ReadLine(), out elem);
                if (!isCorrect)
                {
                    Console.Write("Введите корректный элемент-целое число >>> ");
                }
            } while (!isCorrect);
            return elem;
        }

        static void FuncPageLogic()
        {
            byte userAnswer = funcPage.SetUserAnswer();
            Console.Clear();
            Console.WriteLine($"Текущее действие: {funcPage.MenuObj[funcPage.UserAnswer - 1]}\n");
            switch (userAnswer)
            {
                case 1:
                    if (userArray.IsEmptyArray())
                        return;
                    int maxN = userArray.FindMaxElements(out int numMax);
                    userArray.DeleteMaxElements(maxN, numMax);
                    break;
                case 2:
                    AddElementsToArray();
                    break;
                case 3:
                    if (userArray.IsEmptyArray())
                        return;
                    MoveLeft();
                    break;
                case 4:
                    if (userArray.IsEmptyArray())
                        return;
                    userArray.FindFirstNegative();
                    break;
                case 5:
                    if (userArray.IsEmptyArray())
                        return;
                    userArray.BubbleSorting();
                    break;
                case 6:
                    if (userArray.IsEmptyArray())
                        return;
                    userArray.FastSortingInit(0, userArray.Length - 1);
                    break;
                case 7:
                    if (userArray.IsEmptyArray())
                        return;
                    BinSearchInit();
                    break;
            }
        }

        static void AddElementsToArray()
        {
            Console.Write("Введите количество элементов >>>> ");
            int length = GetArrayLen();//получаем длину последовательности
            if (length != 0)
            {
                userArray.ExtendArray(length);
                for (int index = userArray.Length - length; index < userArray.Length; index++)
                {
                    Console.Write($"Введите элемент #{index + 1} >>> ");
                    userArray.SetElement(index, GetCorrectEl());
                }
            }
        }

        static void BinSearchInit()
        {
            bool isSorted = userArray.SortCheck();//проверка на необходимость сортировки
            if (!isSorted)//в том случае, если необходима - проводится быстрая сортировка
            {
                Console.WriteLine("Для бинарного поиска требуется отсортированный массив. Будет проведена быстрая сортировка по алгоритму Хоара");
                userArray.FastSortingInit(0, userArray.Length - 1);//запускаем быструю сортировку
                userArray.PrintArray();//печатает отсортированный массив
                Console.WriteLine();
            }
            else
            {
                userArray.PrintArray();//печатаем массив для наглядности
                Console.WriteLine();
            }
            Console.Write("Введите искомый элемент >>> ");
            int searchKey = GetCorrectEl();//получение элемента который нужно найти
            int keyIndex = userArray.BinSearch(searchKey, out int compares);//запускаем бинарный поиск
            Console.WriteLine($"Для выполнения операции бинарного поиска было задействовано {compares} сравнений");
            if (keyIndex == -1)//если не найдено - вернем сообщение об этом, иначе вернем обратное сообщение
            {
                Console.WriteLine($"В последовательности нет искомого элемента {searchKey}");
                return;
            }
            Console.WriteLine($"Искомый элемент {searchKey} находится на позиции {keyIndex}");
        }

        static void MoveLeft()
        {
            Console.Write("Введите значение M, на которое необходимо сдвинуть влево >>> ");
            int slide = GetArrayLen();//получаем сдвиг, может быть нулем
            slide %= userArray.Length;//обновляем сдвиг вычитая "лишние круги" в сдвиге
            if (slide == 0)//при свдвиге 0 массив не трубется менять
                return;
            userArray.MoveLeft(slide);
        }
    }
}