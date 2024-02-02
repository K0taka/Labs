﻿using static System.Console;
using static System.Math;

namespace Task1
{
    internal class Program
    {
        //10 ВАРИАНТ
        /*объявление значений заданных в ЛР, const тк они не будут меняться в ходе работы программы
          double как среднее между float (недостаточно точности) и decimal (меньше производительности, высокая точность вычислений
          этого типа дыннх игнорируется так как результат обрезается до 5 знаков после запятой)*/
        const double accuracy = 0.0001; //точность для вычисления S(e) по усл. ЛР;
        const double step = (1 - 0.1) / 10; //шаг с которым изменяется x в заданной функции
        /*тип byte используется тк это наименьший по занимаемому объему целочисленный тип. Число повторений 20 помещяется
          в границы данного типа 0...255*/
        const byte repeat = (byte)20; //кол-во повторений для вычисления S(n) по усл. ЛР

        static void Main()
        {
            WriteLine("| Знач. X | Значение Y  | Значение S(n)  | Значение S(e) |");
            for (double x = 0.1; x < 1; x += step) //x изменяется от 0.1 до 1 по условию ЛР, x - стандартное обозначение аргумента функции
            {
                double y, sumN, sumAcc; //объявление переменных для записи результатов вычислений
                                        //y - стандартное обозначение значения функции
                                        //sumN - обозначение для суммы вычисленной заданное число раз S(n)
                                        //sumAcc - обозначение для суммы вычисленной до заданной точности члена суммы S(e)

                x = Round(x, 2); //x округляем для избежания накопления ошибки вычислений последующего x
                y = CalcY(x); //вычисляем y в соотв. функции
                sumN = CalcSN(x); //вычисляем S(n) в соотв. функции (заданное кол-во повторений)
                sumAcc = CalcSAcc(x); //вычисляем S(e) в соотв. функции (вычисляем пока не достигнем необходимой точности
                                                //члена суммы)
                WriteLine($"|X = {x,3:F2} | Y = {y,6:F5} | S(n) = {sumN,6:F5} | S(e) = {sumAcc,6:F5}|"); //вывод результата вычислений
                                                                                                       //с точностью до 5 знаков
            }
            ReadLine(); //избегаение закрытия консоли после вывода результата
        }

        /// <summary>
        /// Функция для вычисления значения функции y по формуле y=e^cos(x) * cos(sin(x))
        /// </summary>
        /// <param name="x">Вещественное число</param>
        /// <returns>Вещесвтенное значение указанной функции для переданного x</returns>
        static double CalcY(double x)
        {
            double y; //переменная для хранения вычисленного значения, стандартное обозначение y для значения функции
            y = Pow(E, Cos(x)) * Cos(Sin(x));
            return y;
        }

        /// <summary>
        /// Функция для вычисления S(n), то есть суммы, полученной путем сложения repeat членов для указанного x 
        /// </summary>
        /// <param name="x">Вещественное число, значение аргумента функции</param>
        /// <returns>Вещественное значение указанной суммы для переданного x</returns>
        static double CalcSN(double x)
        {
            double sumN = 1; //заранее записываем 1 член суммы заранее для удобства работы с циклом, переменная хранит искомую сумму
            double lasRec = 1;//заранее записываем послюднюю найденную рекурретно часть члена (0! = 1), переменная хранит
                                                                                          //рекуррентно вычисляемую часть

            for (byte actualNum = 1; actualNum <= repeat; actualNum++) //вычисляем сумму для заданного числа повторений repeat,
                                                                       //изначальное значение 1 (второй член суммы)
            {
                lasRec *= actualNum; //находим текущую рекуррентную часть
                sumN += Cos(actualNum * x) / lasRec; //обновляем сумму, первая числитель находим по формуле, знаменатель рекуррентно
            }
            return sumN;
        }

        /// <summary>
        /// Функция для вычисления S(e), то есть суммы, полученной путем сложения стольких членов, пока не будет
        /// вычислен такой, абсолютное значение которого не будет меньше, чем заданная точность accuracy
        /// </summary>
        /// <param name="x">Вещественное число, значение аргумента функции</param>
        /// <returns>Вещественное значение указанной суммы для переданного x</returns>
        static double CalcSAcc(double x)
        {
            double lastRec = 1; //заранее записываем послюднюю найденную рекурретно часть члена (0! = 1), переменная хранит
                                //рекуррентно вычисляемую часть
            double sumAcc = 1; //переменная хранит вычисляемую сумму, изначально = 1, так как в нее уже записан нулевой член
            double curElem = 1; //переменная хранит элемент суммы для сравнения с точностью, изначально равен нулевому члену
            
            for (int actualNum = 1; Abs(curElem) > accuracy; actualNum++) //вычисляем искомую сумму, пока элемент не достигнет заданной точности
            {
                lastRec *= actualNum; //вычисляем текующую рекуррентную часть выражения
                curElem = Cos(x * actualNum) / lastRec; //вычисляем текущий элемент
                sumAcc += curElem; //добавляем его к сумме
            }
            return sumAcc;
        }
    }
}