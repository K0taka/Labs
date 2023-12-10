using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstTask
{
    internal class Program
    {
        static void Main()
        {
            //Вариант 10
            //первое задание
            //используемые переменные для вычислений, имена подобны тем, что используются в задании
            int m, n;
            double x;
            bool isM, isN, isX;
            //используемые переменные для записи ответов
            double answer;
            bool isCorrect;

            Console.WriteLine("Задача 1");
            Console.Write("Введите числовое значение m  >>> ");
            do //запрос m
            {
                isM = int.TryParse(Console.ReadLine(), out m);
                if (!isM)//запрос ввести число заново уже правильно
                    Console.Write("Ошибка! Введите число! >>> ");
            } while (!isM);

            Console.Write("Введите числовое значение n  >>> ");
            do //запрос n
            {
                isN = int.TryParse(Console.ReadLine(), out n);
                if (!isN) //запрос ввести число заново уже правильно
                    Console.Write("Ошибка! Введите число! >>> ");

                if (isN && (n == 1 || n == 0))
                {
                    //при значении n=0 или n=1 произойдет деление на 0
                    Console.Write("Ошибка! Введите число неравное 0 или 1! >>> ");
                    isN = false;
                }
            } while (!isN);

            Console.Write("Введите числовое значение x  >>> ");
            do //запрос x
            {
                isX = double.TryParse(Console.ReadLine(), out x);
                if (!isX) //запрос ввести число заново уже правильно
                    Console.Write("Ошибка! Введите число! >>> ");
            } while (!isX);

            Console.WriteLine("----------------------------------------------------");
            Console.WriteLine($"Текущее значение m = {m}, текущее значение n = {n}");
            Console.Write("Результат операции m/--n++ = ");
            /*
             * Данную операцию разделим на две, так как иначе будет ошибка в работе программы
             * посфиксный инкримент не влияет на результат, а лишь изменит значение переменной
             * после того, как будет записан ответ. Поэтому его можно вынести в отдельную строчку
             * n +=1. Тогда сама операция будет без ошибки. Значения переменных же для следующей операции
             * не поменяются, так как сначала n уменьшается на 1, а потом снова увеличивается на 1
             * n изначально не должно быть 1, иначе произойдет деление на 0
             */
            answer = m / --n;
            n += 1;
            Console.WriteLine(answer);

            Console.WriteLine("----------------------------------------------------");
            Console.WriteLine($"Текущее значение m = {m}, текущее значение n = {n}");
            Console.Write("Результат операции m/n < n-- = ");
            isCorrect = m / n < n--; //если изначальное n = 0, то происходит деление на 0
            Console.WriteLine(isCorrect);

            Console.WriteLine("----------------------------------------------------");
            Console.WriteLine($"Текущее значение m = {m}, текущее значение n = {n}");
            Console.Write("Результат операции m+n++ > n+m = ");
            isCorrect = m + n++ > n + m;
            Console.WriteLine(isCorrect);

            Console.WriteLine("----------------------------------------------------");
            Console.WriteLine($"Текущее значение x = {x}");
            Console.Write("Результат операции x^5 * sqrt(abs(x-1)) + abs(25-x^5) = ");
            answer = Math.Pow(x, 5) * Math.Sqrt(Math.Abs(x - 1)) + Math.Abs(25 - Math.Pow(x, 5));
            Console.WriteLine(answer);

            Console.Read();
        }
    }
}
