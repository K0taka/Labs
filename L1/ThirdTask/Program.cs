using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThirdTask
{
    internal class Program
    {
        static void Main()
        {
            //константы для вычислений
            const double aDouble = 1000;
            const double bDouble = 0.0001;
            const float aFloat = 1000f;
            const float bFloat = 0.0001f;
            //переменные для хранений промежуточных резуальтатов
            double dStep1, dStep2, dStep3, dStep4, dStep5, dStep6;
            float fStep1, fStep2, fStep3, fStep4, fStep5, fStep6;
            //используемые переменные для записи ответов
            double dAnswer;
            float fAnswer;


            Console.WriteLine("Задача 3");
            Console.Write("Ответ 1, когда выражение записано в одну строку, типы double: ");
            dAnswer = (Math.Pow((aDouble + bDouble), 3) - (Math.Pow(aDouble, 3) + 3 * Math.Pow(aDouble, 2) * bDouble)) /
                (3 * aDouble * Math.Pow(bDouble, 2) + Math.Pow(bDouble, 3));
            Console.WriteLine(dAnswer);

            Console.Write("Ответ 2, когда выражение записано в одну строку, типы float: ");
            fAnswer = ((float)Math.Pow((aFloat + bFloat), 3) - ((float)Math.Pow(aFloat, 3) + 3 * (float)Math.Pow(aFloat, 2) * bFloat)) /
                (3 * aFloat * (float)Math.Pow(bFloat, 2) + (float)Math.Pow(bFloat, 3));
            Console.WriteLine(fAnswer);

            Console.Write("Ответ 3, когда выражение посчитано по частям, типы double: ");
            dStep1 = Math.Pow(aDouble + bDouble, 3);
            dStep2 = Math.Pow(aDouble, 3);
            dStep3 = Math.Pow(aDouble, 2);
            dStep4 = dStep1 - (dStep2 + 3 * dStep3 * bDouble);
            dStep5 = Math.Pow(bDouble, 2);
            dStep6 = Math.Pow(bDouble, 3);
            dAnswer = dStep4 / (3 * aDouble * dStep5 + dStep6);
            Console.WriteLine(dAnswer);

            Console.Write("Ответ 4, когда выражение посчитано по частям, типы float: ");
            fStep1 = (float)Math.Pow(aFloat + bFloat, 3);
            fStep2 = (float)Math.Pow(aFloat, 3);
            fStep3 = (float)Math.Pow(aFloat, 2);
            fStep4 = (float)(fStep1 - (fStep2 + (float)(3 * fStep3 * bFloat)));
            fStep5 = (float)Math.Pow(bFloat, 2);
            fStep6 = (float)Math.Pow(bFloat, 3);
            fAnswer = (float)(fStep4 / ((float)(3 * aFloat * fStep5 + fStep6)));
            Console.WriteLine(fAnswer);

            Console.WriteLine();
            Console.WriteLine("Что же происходит?");
            Console.WriteLine("Данные вычисления можно произвести на бумаге: раскрыв скобки и " +
                "сократив, получим ответ 1. Но программа выдает в случае double значение близкое к 1 и в " +
                "случае float значение даже не близкое к 1.");
            Console.WriteLine("Во-первых, известно, что при вычислении вещественных чисел компьютером" +
                "накапливается погрешность, что приводит к неточному результату в конце");
            Console.WriteLine("Во-вторых, помимо погрещности влияет и то, число какой точности может в себе" +
                "хранить тот или иной тип данных. В тип float в нашем случае не может поместиться весь результат," +
                "отчего накапливается еще большая ошибка");

            Console.Read();
        }
    }
}
