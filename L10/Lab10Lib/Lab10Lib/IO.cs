global using static Lab10Lib.IO;
namespace Lab10Lib
{
    public class IO
    {
        /// <summary>
        /// Коды ошибок для возврата описаний
        /// </summary>
        public enum ErrorCodes : byte
        {
            NotANumber,
            OverflowError,
            IncorrectClassInit,
            ArrayOverflowError,
            NumberIsOutOfBounds
        }

        /// <summary>
        /// Получение ответа от пользователя
        /// </summary>
        /// <param name="getter">Строка-запрос ввода</param>
        /// <param name="lower">Нижняя грань</param>
        /// <param name="upper">Верхняя грань</param>
        /// <returns>Введенное число</returns>
        public static long GetIntegerAnswer(string getter, long lower = Int32.MinValue, long upper = Int32.MaxValue)
        {
            Console.Write($"[{DateTime.Now}] {getter}");
            bool isCorrect = false;
            long userAnswer = 0;
            do
            {
                try
                {
                    userAnswer = long.Parse(Console.ReadLine()!);
                    if (userAnswer > upper || userAnswer < lower)
                    {
                        ReturnError(ErrorCodes.NumberIsOutOfBounds);
                        Console.Write("Повторите ввод >>> ");
                    }
                    else
                        isCorrect = true;
                }
                catch (FormatException)
                {
                    ReturnError(ErrorCodes.NotANumber);
                    Console.Write("Повторите ввод >>> ");
                }
                catch (OverflowException)
                {
                    ReturnError(ErrorCodes.OverflowError);
                    Console.Write("Повторите ввод >>> ");
                }
            } while (!isCorrect);
            return userAnswer;
        }

        /// <summary>
        /// Поулчение вещественного ответа от пользователя
        /// </summary>
        /// <param name="getter">Строка-запрос</param>
        /// <param name="lower">Нижжняя граница</param>
        /// <param name="upper">верхняя граница</param>
        /// <returns>Полученное значение</returns>
        public static double GetDoubleAnswer(string getter, double lower = Double.MinValue, double upper = Double.MaxValue)
        {
            Console.Write($"[{DateTime.Now}] {getter}");
            bool isCorrect = false;
            double userAnswer = 0;
            do
            {
                try
                {
                    userAnswer = double.Parse(Console.ReadLine()!);
                    if (userAnswer > upper || userAnswer < lower)
                    {
                        ReturnError(ErrorCodes.NumberIsOutOfBounds);
                        Console.Write("Повторите ввод >>> ");
                    }
                    else
                        isCorrect = true;
                }
                catch (FormatException)
                {
                    ReturnError(ErrorCodes.NotANumber);
                    Console.Write("Повторите ввод >>> ");
                }
                catch (OverflowException)
                {
                    ReturnError(ErrorCodes.OverflowError);
                    Console.Write("Повторите ввод >>> ");
                }
            } while (!isCorrect);
            return userAnswer;
        }

        /// <summary>
        /// Запрос и получение текстовой строки у пользователя
        /// </summary>
        /// <param name="getter">Строка-запрос</param>
        /// <returns>Полученный ответ пользователя</returns>
        public static string GetTextAnswer(string getter)
        {
            Console.Write($"[{DateTime.Now}] {getter}");
            return Console.ReadLine()!;
        }

        /// <summary>
        /// Получение логического значения от пользователя
        /// </summary>
        /// <param name="getter">Строка-запрос</param>
        /// <returns>Логическое от пользователя</returns>
        public static bool GetBoolAnswer(string getter)
        {
            bool isCorrect = false;
            bool userAnswer = false;
            do
            {
                try
                {
                    userAnswer = bool.Parse(GetTextAnswer(getter));
                    isCorrect = true;
                }
                catch
                {
                    WriteLine("Некорректный ввод! Повторите попытку!");
                }
            } while(!isCorrect);
            return userAnswer;
        }

        /// <summary>
        /// Вывод на экран с декоратором
        /// </summary>
        /// <param name="line">Выводимая строка</param>
        public static void WriteLine(string line) => Console.WriteLine($"[{DateTime.Now}] {line}");

        /// <summary>
        /// Выводит строку на экран с декоратором без символа переноса строки
        /// </summary>
        /// <param name="line"></param>
        public static void Write(string line) => Console.Write($"[{DateTime.Now}] {line}");

        /// <summary>
        /// Вывод на экран пустой строки
        /// </summary>
        public static void EmptyLine() => Console.WriteLine();

        /// <summary>
        /// Очистка консоли
        /// </summary>
        public static void Clear() => Console.Clear();

        /// <summary>
        /// Возврат описания ошибки
        /// </summary>
        /// <param name="errorCode">Код нужной ошибки</param>
        public static void ReturnError(ErrorCodes errorCode)
        {
            Console.Write($"[{DateTime.Now}] При выполнении была следующая ошибка: ");
            switch (errorCode)
            {
                case ErrorCodes.NotANumber:
                    Console.WriteLine("Указано не число!");
                    break;
                case ErrorCodes.OverflowError:
                    Console.WriteLine("Указанное число выходит за пределы типа!");
                    break;
                case ErrorCodes.IncorrectClassInit:
                    Console.WriteLine("Класс не может быть инициализирован с указанными значениями!");
                    break;
                case ErrorCodes.ArrayOverflowError:
                    Console.WriteLine("Массив переполнен!");
                    break;
                case ErrorCodes.NumberIsOutOfBounds:
                    Console.WriteLine("Введенное число выходит за допустимые границы!");
                    break;
            }
        }

        /// <summary>
        /// Ожидание нажатия любой кнопки
        /// </summary>
        public static void WaitAnyButton()
        {
            WriteLine("Нажмите любую клавишу для продолжения...");
            Console.ReadKey(true);
            Console.Clear();
        }
    }
}
