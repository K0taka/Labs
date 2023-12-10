using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2
{
    internal class Program
    {
        static void Main()
        {
            //Варивант 10, з25
            //Переменные
            int actualElement = 0, actualPos = 0;
            int minElement = 0, posMin = 0;
            bool isCorrect;

            Console.WriteLine("Найдем номер минимального элемента в последовательности");
            Console.WriteLine("Начинайте ввод элементов! 0 для завершения ввода.");
            do //цикл для запроса чисел пока не будет введен 0
            {
                isCorrect = false; //пока false - ждем корректное значение от пользователя
                actualPos += 1; //номер текущего элемента
                Console.Write($"Введите элемент #{actualPos} >>> ");
                do //цикл для получения корректного ввода от пользователя
                {
                    try
                    {
                        actualElement = int.Parse(Console.ReadLine());
                        if (actualElement == 0 && actualPos == 1)
                            //проверка на пустую последовательность
                            Console.Write("Ошибка! Последовательность пуста! Повторите ввод >>> ");
                        else if (actualPos == 1)
                        { //задаются корректные значения для минимального элемента.
                          //можно столкнуться с присваиванием в начале minElement = 99999.
                          //если пользователь не введет числа меньше этого произойдет ошибка
                          //поэтому устанавливаются корректные значения уже из последовательности
                            minElement = actualElement;
                            posMin = actualPos;
                            isCorrect = true;
                        }
                        else
                            isCorrect = true;

                        if (actualElement < minElement && actualElement != 0)
                        { //обновление минимального элемента и его позиции
                          //по условию 0 не входит в последовательность
                            posMin = actualPos;
                            minElement = actualElement;
                        }
                    }
                    catch (FormatException) //обработка не чисел и вещественных чисел
                    {
                        Console.Write("Ошибка! Программа принимает только целые числа! >>> ");
                    }
                    catch (OverflowException) //обработка ввода превышающего пределы типа int
                    {
                        Console.Write("Элемент последовательности не дожен выходить за пределы" +
                            " от −2 147 483 648 до 2 147 483 647, пожалуйста, введите другое целое число! >>> ");
                    }
                } while (!isCorrect);
            } while (actualElement != 0);
            Console.WriteLine($"Позиция минимального элемента: {posMin}");

            Console.ReadLine();
        }
    }
}
