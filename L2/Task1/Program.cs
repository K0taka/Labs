using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    internal class Program
    {
        static void Main()
        {
            //Вариант 10, 10 задача
            //переменные
            int numElements, sumElements;

            Console.WriteLine("Программа считает сумму максимального и минимального элементов заданной последовательности");
            Console.Write("Пожалуйста, введите количество элементов последовательности >>> ");
            numElements = GetNumElements(); //получим длину последовательности в функции
            sumElements = ActualElementHendler(numElements); //получим элементы в функции, сюда вернем уже искомую сумму
            Console.WriteLine($"Сумма максимального и минимального элемента последовательности равна {sumElements}");
            Console.Read(); //программа завершена.
        }
        
        static int GetNumElements() //функция для получения длины последовательности
        {
            //используемые переменные
            int numElements = 0;
            bool isCorrect = false;

            do
            {
                //цикл для получения значения numElements от пользователя
                try //обработчик ошибок позволяет в случае ошибке понятно донести это пользователю
                {
                    numElements = int.Parse(Console.ReadLine()); //получение числа
                    if (numElements == 0) //обработка пустой последовательности
                    {
                        Console.Write("Внимание! Ваша последовательность пуста! Введите натуральное число >>> ");
                    }
                    else if (numElements < 0) //обработка отрицательной длины последовательности
                    {
                        Console.Write("Внимание! Вы хотите ввести последовательность отрицательной длины!" +
                            " Введите натуральное число! >>> ");
                    }
                    else
                        isCorrect = true; //если число введено верно - выйдем из цикла
                }
                catch (OverflowException) //обработка слишком большого числа
                {
                    Console.Write("Вы вышли за пределы допустимых значений. Число должно быть натуральным," +
                        " не превышающим 2 147 483 647 >>> ");
                }
                catch (FormatException) //обработка вещественных чисел и не чисел
                {
                    Console.Write("Внимание! Длина последовательности должна быть целым положительным числом." +
                        " Укажите натуральное число! >>> ");
                }
            } while (!isCorrect);
            return numElements;
        }

        static int ActualElementHendler(int numElements) //функция для получения и обработки элеиентов последовательности
        {
            //переменные
            int maxElement = 0, minElement = 0;
            int actualElement;
            bool isCorrect;

            for (int currElement = 0; currElement < numElements; currElement++) //цикл с параметром так как есть конкретное кол-во элементов
            {
                //получаем элементы последовательности
                Console.Write($"Введите элемент последовательности #{currElement + 1} >>> ");
                isCorrect = false;
                do
                {
                    //получаем кореектный элемент
                    try //аналогично, проверка на правильность получения числа и обработка ошибок
                    {
                        actualElement = int.Parse(Console.ReadLine()); //собственно элемент последовательности
                        if (actualElement < -1073741824 || actualElement > 1073741823)
                        {
                            Console.Write("Элемент последовательности не дожен выходить за пределы" +
                            " от -1 073 741 824 до 1 073 741 823, пожалуйста, введите другое целое число! >>> ");
                            continue;
                        }

                        isCorrect = true;
                        if (currElement == 0)
                        {
                            //устанавливаем корректный минимальный и максимальный элемент тут, чтобы они были из последовательности
                            minElement = actualElement;
                            maxElement = actualElement;
                        }
                        if (actualElement > maxElement) //обновляем максимальный элемент по необходимости
                            maxElement = actualElement;
                        if (actualElement < minElement) //обновляем минимальный элемент по необходимости
                            minElement = actualElement;
                    }
                    catch (OverflowException) //обработка слишком больших чисел
                    {
                        Console.Write("Элемент последовательности не дожен выходить за пределы" +
                            " от -1 073 741 824 до 1 073 741 823, пожалуйста, введите другое целое число! >>> ");
                    }
                    catch (FormatException) //обработка вещественных чисел и не чисел
                    {
                        Console.Write("Внимание! Элемент последовательности должен быть целым числом, повторите ввод >>> ");
                    }
                } while (!isCorrect);
            }
            Console.WriteLine("Ввод элементов завершен!");
            return maxElement + minElement; //вернем сумму максимального элемента и минимального как в условии
        }
    }
}
