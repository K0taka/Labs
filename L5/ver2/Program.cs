using System.Runtime.InteropServices.Marshalling;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace ver2
{

    internal class Program
    {
        static readonly ArrayLogic userArray = new(); //создаем объект массива
        static readonly Menu taskMenu = new(["Использовать одномерный массив и выполнить задачу 1",
                                             "Использовать двумерный массив и выполнить задачу 2",
                                             "Использовать рваный массив и выполнить задачу 3",
                                             "Выход"]); //объект меню, содержащий страницу выбора задачи
        static readonly Menu arrayGenerationMenu = new(["Ввести массив вручную",
                                                        "Создать массив с помощью ДСЧ заданной размерности",
                                                        "Вернуться к выбору задачи",
                                                        "Выход"]); //объект меню, содержащий выбор способа задания массива
        static readonly Menu postTaskMenu = new([ "Повторить задачу",
                                                  "Вернуться и создать новый массив",
                                                  "Вернуться к выбору задачи",
                                                  "Выход" ]); //объект меню, содержащий выбор опции после выполнения задачи
        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            byte startingMenu = 0; //переменная для конкретезации какое меню вывести
            while (true) //выход из цикла предусмотрен в меню путем завершения процесса приложения
            {
                switch (startingMenu)
                {
                    case 0:
                        ChangeMenu(0, ref startingMenu);//вызываем нужное меню по ситуации
                        break;
                    case 1:
                        ChangeMenu(1, ref startingMenu);//передаем переменную ref, чтобы изменять ее внутри функции и видеть изменения здесь
                        break;
                    case 2:
                        InitTask(ref startingMenu);
                        break;
                }
            }
        }

        /// <summary>
        /// Функция для смены текущего окна
        /// </summary>
        /// <param name="wishMenu">Указатель, на какое меню следует переключиться</param>
        /// <param name="isMainMenu">Указатель на меню, с которого следует начать следующую итерацию</param>
        static void ChangeMenu(byte wishMenu, ref byte isMainMenu, bool isExted = true)
        {
            switch (wishMenu)//выбираем соотв. задачу
            {
                case 0:
                    Console.Clear();
                    Console.WriteLine("Это программа для ЛР5. Сейчас Вы находитесь в главном меню. Здесь доступны следующие функции:");
                    taskMenu.ShowMenu();//печатаем меню
                    taskMenu.SetUserAnswer();//получаем ответ от пользователя
                    ChangeMenu(1, ref isMainMenu);//идем к следующему меню
                    break;
                case 1:
                    Console.Clear();
                    Console.WriteLine($"Вы выбрали: {taskMenu.MenuObj[taskMenu.UserAnswer - 1]}");
                    arrayGenerationMenu.ShowMenu();//печатаем меню
                    if (arrayGenerationMenu.MenuObj[arrayGenerationMenu.SetUserAnswer() - 1] == arrayGenerationMenu.MenuObj[^2])
                    //в случае выбора пункта вернуться на главную - возвращаемся
                    {
                        isMainMenu = 0; //начинать будем с главного меню
                        return;
                    }
                    ArrayInit();//создаем массив
                    InitTask(ref isMainMenu);
                    break;
                case 2:
                    if (userArray.IsCorrectArray() && isExted)//вывод текста в соотвествтии с состоянием массива
                    {
                        Console.WriteLine("--Массив после выполнения задачи--");
                        userArray.PrintArray();//напечатать массив после выполнения задачи
                    }
                    else
                    {
                        if (taskMenu.UserAnswer != 2)
                            Console.WriteLine("\nВыполнение задачи невозможно, массив пуст");
                        else if (isExted)
                            Console.WriteLine("\nПосле выполнения задачи массив остался пуст");
                    }
                    postTaskMenu.ShowMenu();//показать меню
                    switch (postTaskMenu.SetUserAnswer())//запросить ответ от пользователя и согласну ему настроить следующую итерацию меню
                    {
                        case 1:
                            isMainMenu = 2;
                            break;
                        case 2:
                            isMainMenu = 1;
                            break;
                        case 3:
                            isMainMenu = 0;
                            break;
                    }
                    break;
            }
        }

        static void InitTask(ref byte isMainMenu)
        {
            bool isExted = true;
            int[,] ext = null!;//создаем расширение, изначально null
            Console.Clear();
            if (taskMenu.UserAnswer == 2 && userArray.GetArray().GetLength(0) != 0)
                ext = CreateExtention(out isExted);//меняем его на введенное пользователем
            
            Console.WriteLine("--Текущий массив--");
            userArray.PrintArray();//выводим текущий массив
            if (taskMenu.UserAnswer != 2 || ext != null)
                userArray.DoTask(ext);//приступаем к выполнению задачи
            
            ChangeMenu(2, ref isMainMenu, isExted);//переходим к меню после задачи
        }

        /// <summary>
        /// Подготовка массива к работе
        /// </summary>
        static void ArrayInit()
        {
            //Наполняет одномерный массив
            void SingleDimArrayFill()
            {
                switch (arrayGenerationMenu.UserAnswer)
                {
                    case 1:
                        for (int index = 0; index < userArray.GetArray().GetLength(0); index++)
                        {
                            userArray.SetElement(GetElement($"{index + 1}"), index); //Получает элемент от пользователя и заносим его в массив
                        }
                        break;
                    case 2:
                        userArray.RandomFillArray(); //Случайно заполняет массив
                        break;
                }
            }

            //наполняет двумерный массив
            void MultiDimArrayFill()
            {
                switch (arrayGenerationMenu.UserAnswer)
                {
                    case 1:
                        for (int row = 0; row < userArray.GetArray().GetLength(0); row++)
                        {
                            for (int col = 0; col < userArray.GetArray().GetLength(1); col++)
                            {
                                userArray.SetElement(GetElement($"[{row + 1},{col + 1}]"), row, col); //прочитать элемент и занести в массив
                            }
                        }
                        break;
                    case 2:
                        userArray.RandomFillArray(); //случайно заполнить массив
                        break;
                }
            }

            //наполняет рваный массив
            void JaggedArrayFill()
            {
                switch (arrayGenerationMenu.UserAnswer)
                {
                    case 1:
                        for (int row = 0; row < userArray.GetArray().GetLength(0); row++)
                        {
                            userArray.SetElement(GenerateArray(row), row);//создает массив и отправляет ссылку на него в рваный массив
                        }
                        break;
                    case 2:
                        userArray.RandomFillArray();//случайно сгенерировать массив
                        break;
                }
            }

            //сгенерировать массив для добавления в рваный массив
            int[] GenerateArray(int row)
            {
                int[] result = new int[GetLength($"Введите длину ряда #{row + 1} >>> ")];
                for (int index = 0; index < result.Length; index++)
                {
                    result[index] = GetElement($"{index + 1}");
                }
                return result;
            }

            switch (taskMenu.UserAnswer)
            {
                case 1:
                    userArray.CreateSingleDimArray(GetLength("Введите длину массива >>> "));//создать одномерный массив
                    SingleDimArrayFill();//наполнить одномерный массив
                    break;
                case 2:
                    int rows = GetLength("Введите количество строк >>> ");//Получаем количество строчек в двумерном массиве
                    if (rows != 0)//продолжаем если их больше 0
                    {
                        bool isCorrectCols;
                        int cols;
                        do
                        {
                            cols = GetLength("Введите количество колонок >>> ");//Узнаем у пользователя количество колонок
                            if ((long)rows * (long)cols > 532675567)
                            {
                                isCorrectCols = false;
                                Console.WriteLine($"На данный момент вы не можете добавить более {532675567 / rows} колонок");
                            }
                            else
                                isCorrectCols = true;
                        } while (!isCorrectCols);
                        userArray.CreateMultiDimArray(rows, cols);//создаем двумерный массив этой размерности
                        MultiDimArrayFill();//и наполняем его
                    }
                    else
                        userArray.CreateMultiDimArray(0, 0);//во имя избежания ошибки инициализируем полностью пустой массив
                    break;
                case 3:
                    userArray.CreateMultiDimArray(GetLength("Введите количество строк >>> "));//создаем рваный массив
                    JaggedArrayFill();//наполняем его
                    break;
            }
        }

        /// <summary>
        /// Задаем расширение для двумерного массива
        /// </summary>
        /// <returns>Возвращает двумерный массив, который будет присоединен к изначальному массиву</returns>
        static int[,] CreateExtention(out bool isExted)
        {
            //создаем массив размерности количество строк изначального массива на полученное от пользователя количество столбцов
            int colms;
            bool isCorrect = false;
            int numEl = 16 - userArray.GetArray().GetLength(1);
            if (numEl < 1)
            {
                Console.WriteLine("К сожалению массив переполнен, добавить в него колонки не получится :(");
                isExted = false;
                return null!;
            }
            do
            {
                colms = GetLength("Введите количество добовляемых столбцов >>> ");
                if (colms == 0)
                {
                    Console.WriteLine("При добавлении 0 колонок массив остается без изменений");
                    isExted = false;
                    return null!;
                }
                if (colms <= numEl)
                    isCorrect = true;
                else
                    Console.WriteLine($"Вы не можете добавить больше {numEl} колонок");
            } while (!isCorrect);

            Menu option = new(["Заполнить вручную", "Заполнить автоматически", "Выход"]);
            Random rand = new();
            int[,] result = new int[userArray.GetArray().GetLength(0), colms];
            option.ShowMenu();
            byte ans = option.SetUserAnswer();
            switch (ans)
            {
                case 1:
                    for (int row = 0; row < result.GetLength(0); row++)
                    {
                        for (int col = 0; col < result.GetLength(1); col++)
                        {
                            result[row, col] = GetElement($"[{row + 1},{col + 1}]"); //заносим элемент в таблицу для расширения
                        }
                    }
                    Console.Clear();
                    isExted = true;
                    return result;
                case 2:
                    for (int row = 0; row < result.GetLength(0); row++)
                    {
                        for (int coloum = 0; coloum < result.GetLength(1); coloum++)
                        {
                            result.SetValue(rand.Next(-999, 10000), row, coloum);//случайное значение от -128 до 127 включ. на позиии ROW x COLOUMN
                        }
                    }
                    Console.Clear();
                    isExted = true;
                    return result;
                default:
                    isExted = false;
                    return null!;
            }
        }

        /// <summary>
        /// Функция для получения длины ряда
        /// </summary>
        /// <param name="getter">Запрос, который увидит пользователь</param>
        /// <returns>Целое число - длину ряда</returns>
        static int GetLength(string getter)
        {
            Console.Write(getter);
            int len = 0;//переменная для хранения длины массива
            bool isCorrect = false;//переменная для коректности ввода
            do
            {
                try//инициализируем обработчик ошибок
                {
                    len = int.Parse(Console.ReadLine()!);
                    if (len < 0) //если длина меньше 0 кидаем соотв. исключение
                        throw new ArgumentException("Указанное значение меньше 0, невозможно использовать отрицательную длину! Повторите ввод >>> ");
                    if (len > 16)//при первышении этого значения будет исключение при инициализации массива, ловим его заранее 532675567
                        throw new OverflowException();
                    isCorrect = true; //если условия не прошли то результат ввода верный
                }
                catch (ArgumentException ex)
                {
                    Console.Write($"{ex.Message}");
                }
                catch (FormatException)
                {
                    Console.Write("Принимаемое значение целое неотрицательное число! Повторите ввод >>> ");
                }
                catch (OverflowException)
                {
                    Console.Write("Вы ввели слишком большое значение, система не способно обрабатывать ТАКИЕ числа! >>> ");
                }
            } while (!isCorrect);
            return len;
        }

        /// <summary>
        /// Функция для получения элемента массива
        /// </summary>
        /// <param name="getter"></param>
        /// <returns>Целое число int, введенное пользователем</returns>
        static int GetElement(string getter)
        {
            Console.Write($"Введите элемент #{getter} >>> ");
            int el = 0;//храним элемент тут
            bool isCorrect = false;//верность ввода
            do
            {
                try
                {
                    el = int.Parse(Console.ReadLine()!);
                    if (-1000 < el && el < 10000)
                        isCorrect = true;//особых ограничений нет, кроме границ числа int
                    else
                        throw new ArgumentException("Поддерживаются значания в диапазоне [-999; 9999] >>> ");
                }
                catch (FormatException)
                {
                    Console.Write("Принимаемое значение целое неотрицательное число! Повторите ввод >>> ");
                }
                catch (OverflowException)
                {
                    Console.Write("Вы ввели значение за границами типа int >>> ");
                }
                catch (ArgumentException ex)
                {
                    Console.Write(ex.Message);
                }
            } while (!isCorrect);//цикла пока не введем корректный элемент
            return el;
        }
    }

}