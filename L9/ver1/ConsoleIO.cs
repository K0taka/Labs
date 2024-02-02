namespace ver1
{
    internal class ConsoleIO
    {
        public enum ErrorCodes: byte
        {
            NotANumber,
            OverflowError,
            IncorrectWeather,
            ArrayOverflowError,
            NumberIsOutOfBounds
        }

        public static long GetUserAnswer(string getter, long lower = Int32.MinValue, long upper = Int32.MaxValue)
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
                        ReturnError(ErrorCodes.NumberIsOutOfBounds);
                    else
                        isCorrect = true;
                } 
                catch (FormatException)
                {
                    ReturnError(ErrorCodes.NotANumber);
                }
                catch (OverflowException)
                {
                    ReturnError(ErrorCodes.OverflowError);
                }
            } while (!isCorrect);
            return userAnswer;
        }
        public static double GetUserAnswer(string getter, double lower = Double.MinValue, double upper = Double.MaxValue)
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
                        ReturnError(ErrorCodes.NumberIsOutOfBounds);
                    else
                        isCorrect = true;
                }
                catch (FormatException)
                {
                    ReturnError(ErrorCodes.NotANumber);
                }
                catch (OverflowException)
                {
                    ReturnError(ErrorCodes.OverflowError);
                }
            } while (!isCorrect);
            return userAnswer;
        }

        public static void WriteLine(string line) => Console.WriteLine($"[{DateTime.Now}] {line}");

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
                case ErrorCodes.IncorrectWeather:
                    Console.WriteLine("Указанная погода не может существать в Земных условиях!");
                    break;
                case ErrorCodes.ArrayOverflowError:
                    Console.WriteLine("Массив переполнен!");
                    break;
                case ErrorCodes.NumberIsOutOfBounds:
                    Console.WriteLine("Введенное число выходит за допустимые границы!");
                    break;
            }
        }

        public static void WaitAnyButton()
        {
            WriteLine("Нажмите любую клавишу для продолжения...");
            Console.ReadKey(true);
            Console.Clear();
        }
    }
}
