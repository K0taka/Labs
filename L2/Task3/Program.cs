using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3
{
    internal class Program
    {
        static void Main()
        {
            //вариант 10 з44
            //переменные
            int k; //k из условия
            int ans; //ответ на задачу

            Console.WriteLine("Определим является ли число натуральное k числом 3 в какой-либо целой степени.");
            Console.Write("Пожалуйста, введите натуральное число k >>> ");
            k = GetK();
            try
            {
                ans = Log3(k);
                Console.WriteLine($"Число k = {k} является числом 3 в степени {ans}");
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine(e.Message);
            }
            Console.Read();
        }
        static int GetK()
        {
            //получаем k от пользователя
            //переменные
            int k = 0;
            bool isCorrect = false;
            do
            {//цикл до момента пока не введут корректное значение
                try
                {
                    k = int.Parse(Console.ReadLine());
                    if (k >= 1)
                        isCorrect = true;
                    else
                        Console.Write("Внимание! Вы ввели отрицательное число!" +
                            " Число должно принадлежать к множеству натуральных числел >>> ");
                }
                catch (FormatException)
                {
                    Console.Write("Внимание! Введите число, являющиеся натуральным! >>> ");
                }
                catch (OverflowException)
                {
                    Console.Write("Вы вышли за пределы допустимых значений. Число должно быть натуральным," +
                        " не превышающим 2 147 483 647 >>> ");
                }
            } while (!isCorrect);
            return k;
        }

        static int Log3(int k)
        {
            //поиск степени, при возведении в которую 3 будет равно k
            //на вывод подается либо степень для удовлетворяющего числа, либо ошибка
            int residue = 0;
            int degree = 0;
            while (k > 1 && residue == 0) //цикл работает пока не появится остаток или все число не разделится
            {
                residue = k % 3; //получаем остаток
                k /= 3; //число делится нацело так как k и 3 - int
                degree += 1; //степень увеличиваем на 1
            }
            if (residue == 0) //при выходе из цикла остаток 0 - число подходит под условие задачи
            {
                return degree;
            }
            throw new InvalidOperationException($"Число k = {k} не является числом 3 в целой степени"); //иначе вернем ошибку
        }
    }
}
