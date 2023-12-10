using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondTask
{
    internal class Program
    {
        static void Main()
        {
            //Вариант 10
            // используемые переменные для работы над заданиями
            double x, y;
            bool isX, isY;
            //используемые переменные для записи ответов
            bool isCorrect;

            Console.WriteLine("Задача 2");
            Console.Write("Введите абциссу точки >>> ");
            do //получаем x
            {
                isX = double.TryParse(Console.ReadLine(), out x);
                if (!isX)//запрос ввести число заново уже правильно
                    Console.Write("Ошибка! Введите число! >>> ");
            } while (!isX);


            Console.Write("Введите ординату точки >>> ");
            do //получаем y
            {
                isY = double.TryParse(Console.ReadLine(), out y);
                if (!isY)//запрос ввести число заново уже правильно
                    Console.Write("Ошибка! Введите число! >>> ");
            } while (!isY);

            isCorrect = (y >= (-1 / 7.0) * x - 1) && (y <= 0) && (x <= 0);
            Console.Write("Принадлежит ли точка области? Ответ программы: ");
            Console.WriteLine(isCorrect);

            Console.Read();
        }
    }
}
