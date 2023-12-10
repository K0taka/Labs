using System;

namespace ver1
{
    internal class Program
    {
        static readonly ArrayLogic userArray = new ArrayLogic(); //создаем объект массива
        static readonly Menu taskMenu = new Menu(new string[] {"Использовать одномерный массив и выполнить задачу 1",
                                                               "Использовать двумерный массив и выполнить задачу 2",
                                                               "Использовать рваный массив и выполнить задачу 3",
                                                               "Выход"}); //объект меню, содержащий страницу выбора задачи
        static readonly Menu arrayGenerationMenu = new Menu(new string[] {"Ввести массив вручную",
                                                                          "Создать массив с помощью ДСЧ заданной размерности",
                                                                          "Вернуться к выбору задачи",
                                                                          "Выход"}); //объект меню, содержащий выбор способа задания массива
        static readonly Menu postTaskMenu = new Menu(new string[] { "Вернуться и создать новый массив",
                                                                    "Вернуться к выбору задачи",
                                                                    "Выход" }); //объект меню, содержащий выбор опции после выполнения задачи
        static void Main()
        {
            bool isMainMenu = true; //переменная для конкретезации какое меню вывести
            while(true) //выход из цикла предусмотрен в меню путем завершения процесса приложения
            {
                if (isMainMenu)
                    ChangeMenu(0, ref isMainMenu);//вызываем нужное меню по ситуации
                else
                    ChangeMenu(1, ref isMainMenu);//передаем переменную ref, чтобы изменять ее внутри функции и видеть изменения здесь
            }
        }

        /// <summary>
        /// Функция для смены текущего окна
        /// </summary>
        /// <param name="wishMenu">Указатель, на какое меню следует переключиться</param>
        /// <param name="isMainMenu">Указатель на меню, с которого следует начать следующую итерацию</param>
        static void ChangeMenu(byte wishMenu, ref bool isMainMenu)
        {
            switch(wishMenu)//выбираем соотв. задачу
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
                    if (arrayGenerationMenu.MenuObj[arrayGenerationMenu.SetUserAnswer() -1 ] == arrayGenerationMenu.MenuObj[arrayGenerationMenu.MenuObj.Length - 2])
                        //в случае выбора пункта вернуться на главную - возвращаемся
                    {
                        isMainMenu = true; //начинать будем с главного меню
                        return;
                    }
                    ArrayInit();//создаем массив
                    int[,] ext = null;//создаем расширение, изначально null
                    if (taskMenu.UserAnswer == 2 && userArray.GetArray().GetLength(0) != 0)
                        ext = CreateExtention();//меняем его на введенное пользователем
                    Console.Clear();
                    Console.WriteLine("Текущий массив:");
                    userArray.PrintArray();//выводим текущий массив
                    userArray.DoTask(ext);//приступаем к выполнению задачи
                    ChangeMenu(2, ref isMainMenu);//переходим к меню после задачи
                    break;
                case 2:
                    if (userArray.IsCorrectArray())//вывод текста в соотвествтии с состоянием массива
                    {
                        Console.WriteLine("Массив после выполнения задачи:");
                        userArray.PrintArray();//напечатать массив после выполнения задачи
                    }
                    else
                    {
                        if (taskMenu.UserAnswer != 2)
                            Console.WriteLine("\nВыполнение задачи невозможно, массив пуст");
                        else
                            Console.WriteLine("\nПосле выполнения задачи массив остался пуст");
                    }
                    postTaskMenu.ShowMenu();//показать меню
                    switch(postTaskMenu.SetUserAnswer())//запросить ответ от пользователя и согласну ему настроить следующую итерацию меню
                    {
                        case 1:
                            isMainMenu = false;
                            break;
                        case 2:
                            isMainMenu = true;
                            break;
                    }
                    break;
            }
        }

        /// <summary>
        /// Подготовка массива к работе
        /// </summary>
        static void ArrayInit()
        {
            //Наполняет одномерный массив
            void SingleDimArrayFill()
            {
                switch(arrayGenerationMenu.UserAnswer)
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
                switch(arrayGenerationMenu.UserAnswer)
                {
                    case 1:
                        for (int row = 0; row < userArray.GetArray().GetLength(0); row++)
                        {
                            for (int col = 0; col <  userArray.GetArray().GetLength(1); col++)
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
                        int cols = GetLength("Введите количество колонок >>> ");//Узнаем у пользователя количество колонок
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
        static int[,] CreateExtention()
        {
            //создаем массив размерности количество строк изначального массива на полученное от пользователя количество столбцов
            int[,] result = new int[userArray.GetArray().GetLength(0), GetLength("Введите количество добовляемых столбцов >>> ")];
            for (int row = 0;  row < result.GetLength(0); row++)
            {
                for (int col = 0; col < result.GetLength(1); col++)
                {
                    result[row, col] = GetElement($"[{row+1},{col+1}]"); //заносим элемент в таблицу для расширения
                }
            }
            return result;
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
                    len = int.Parse(Console.ReadLine());
                    if (len < 0) //если длина меньше 0 кидаем соотв. исключение
                        throw new ArgumentException("Указанное значение меньше 0, невозможно расширить массив на отрицательное значение! Повторите ввод >>> ");
                    if (len > 532675567)//при первышении этого значения будет исключение при инициализации массива, ловим его заранее
                        throw new OverflowException();
                    isCorrect = true; //если условия не прошли то результат ввода верный
                }
                catch(ArgumentException ex)
                {
                    Console.Write($"{ex.Message}");
                }
                catch(FormatException)
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
                    el = int.Parse(Console.ReadLine());
                    isCorrect = true;//особых ограничений нет, кроме границ числа int
                }
                catch (FormatException)
                {
                    Console.Write("Принимаемое значение целое неотрицательное число! Повторите ввод >>> ");
                }
                catch (OverflowException)
                {
                    Console.Write("Вы ввели значение за границами типа int >>> ");
                }
            } while (!isCorrect);//цикла пока не введем корректный элемент
            return el;
        }
    }
}
