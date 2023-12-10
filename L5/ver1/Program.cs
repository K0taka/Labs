using System;

namespace ver1
{
    internal class Program
    {
        static readonly ArrayLogic userArray = new ArrayLogic();
        static readonly Menu taskMenu = new Menu(new string[] {"Использовать одномерный массив и выполнить задачу 1",
                                                               "Использовать двумерный массив и выполнить задачу 2",
                                                               "Использовать рваный массив и выполнить задачу 3",
                                                               "Выход"});
        static readonly Menu arrayGenerationMenu = new Menu(new string[] {"Ввести массив вручную",
                                                                          "Создать массив с помощью ДСЧ заданной размерности",
                                                                          "Вернуться к выбору задачи",
                                                                          "Выход"});
        static readonly Menu postTaskMenu = new Menu(new string[] { "Вернуться и создать новый массив",
                                                                    "Вернуться к выбору задачи",
                                                                    "Выход" });
        static void Main()
        {
            bool isMainMenu = true;
            while(true)
            {
                if (isMainMenu)
                    ChangeMenu(0, ref isMainMenu);
                else
                    ChangeMenu(1, ref isMainMenu);
            }
        }

        static void ChangeMenu(byte wishMenu, ref bool isMainMenu)
        {
            switch(wishMenu)
            {
                case 0:
                    Console.Clear();
                    Console.WriteLine("Это программа для ЛР5. Сейчас Вы находитесь в главном меню. Здесь доступны следующие функции:");
                    taskMenu.ShowMenu();
                    taskMenu.SetUserAnswer();
                    ChangeMenu(1, ref isMainMenu);
                    break;
                case 1:
                    Console.Clear();
                    Console.WriteLine($"Вы выбрали: {taskMenu.MenuObj[taskMenu.UserAnswer - 1]}");
                    arrayGenerationMenu.ShowMenu();
                    if (arrayGenerationMenu.MenuObj[arrayGenerationMenu.SetUserAnswer() -1 ] == arrayGenerationMenu.MenuObj[arrayGenerationMenu.MenuObj.Length - 2])
                    {
                        isMainMenu = true;
                        return;
                    }
                    ArrayInit();
                    int[,] ext = null;
                    if (taskMenu.UserAnswer == 2 && userArray.GetArray().GetLength(0) != 0)
                        ext = CreateExtention();
                    Console.Clear();
                    Console.WriteLine("Текущий массив:");
                    userArray.PrintArray();
                    userArray.DoTask(ext);
                    ChangeMenu(2, ref isMainMenu);
                    break;
                case 2:
                    if (userArray.IsCorrectArray())
                    {
                        Console.WriteLine("Массив после выполнения задачи:");
                        userArray.PrintArray();
                    }
                    else
                    {
                        if (taskMenu.UserAnswer != 2)
                            Console.WriteLine("\nВыполнение задачи невозможно, массив пуст");
                        else
                            Console.WriteLine("\nПосле выполнения задачи массив остался пуст");
                    }
                    postTaskMenu.ShowMenu();
                    switch(postTaskMenu.SetUserAnswer())
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

        static void ArrayInit()
        {
            void SingleDimArrayFill()
            {
                switch(arrayGenerationMenu.UserAnswer)
                {
                    case 1:
                        for (int index = 0; index < userArray.GetArray().GetLength(0); index++)
                        {
                            userArray.SetElement(GetElement($"{index + 1}"), index);
                        }
                        break;
                    case 2:
                        userArray.RandomFillArray();
                        break;
                }
            }

            void MultiDimArrayFill()
            {
                switch(arrayGenerationMenu.UserAnswer)
                {
                    case 1:
                        for (int row = 0; row < userArray.GetArray().GetLength(0); row++)
                        {
                            for (int col = 0; col <  userArray.GetArray().GetLength(1); col++)
                            {
                                userArray.SetElement(GetElement($"[{row + 1},{col + 1}]"), row, col);
                            }
                        }
                        break;
                    case 2:
                        userArray.RandomFillArray();
                        break;
                }
            }

            void JaggedArrayFill()
            {
                switch (arrayGenerationMenu.UserAnswer)
                {
                    case 1:
                        for (int row = 0; row < userArray.GetArray().GetLength(0); row++)
                        {
                            userArray.SetElement(GenerateArray(row), row);
                        }
                        break;
                    case 2:
                        userArray.RandomFillArray();
                        break;
                }
            }

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
                    userArray.CreateSingleDimArray(GetLength("Введите длину массива >>> "));
                    SingleDimArrayFill();
                    break;
                case 2:
                    int rows = GetLength("Введите количество строк >>> ");
                    if (rows != 0)
                    {
                        int cols = GetLength("Введите количество колонок >>> ");
                        userArray.CreateMultiDimArray(rows, cols);
                        MultiDimArrayFill();
                    }
                    else
                        userArray.CreateMultiDimArray(0, 0);
                    break;
                case 3:
                    userArray.CreateMultiDimArray(GetLength("Введите количество строк >>> "));
                    JaggedArrayFill();
                    break;
            }
        }


        static int[,] CreateExtention()
        {
            int[,] result = new int[userArray.GetArray().GetLength(0), GetLength("Введите количество добовляемых столбцов >>> ")];
            for (int row = 0;  row < result.GetLength(0); row++)
            {
                for (int col = 0; col < result.GetLength(1); col++)
                {
                    result[row, col] = GetElement($"[{row+1},{col+1}]");
                }
            }
            return result;
        }
        
        static int GetLength(string getter)
        {
            Console.Write(getter);
            int len = 0;
            bool isCorrect = false;
            do
            {
                try
                {
                    len = int.Parse(Console.ReadLine());
                    if (len < 0)
                        throw new ArgumentException("Указанное значение меньше 0, невозможно расширить массив на отрицательное значение! Повторите ввод >>> ");
                    if (len > 532675567)
                        throw new OverflowException();
                    isCorrect = true;
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

        static int GetElement(string getter)
        {
            Console.Write($"Введите элемент #{getter} >>> ");
            int el = 0;
            bool isCorrect = false;
            do
            {
                try
                {
                    el = int.Parse(Console.ReadLine());
                    isCorrect = true;
                }
                catch (FormatException)
                {
                    Console.Write("Принимаемое значение целое неотрицательное число! Повторите ввод >>> ");
                }
                catch (OverflowException)
                {
                    Console.Write("Вы ввели значение за границами типа int >>> ");
                }
            } while (!isCorrect);
            return el;
        }
    }
}
