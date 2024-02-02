using System.Text.RegularExpressions;
using System;

namespace tests
{
    internal class Program
    {
        static int CheckInput(string obj, int b, int c) // проверка ввода на соответствие int и диапазону значений
        {
            int a; // ввод
            bool isCorrect; // ввод верный

            do
            {
                Console.Write($"Введите {obj}: "); // приглашение к вводу
                isCorrect = int.TryParse(Console.ReadLine(), out a); // проверка
                if (!isCorrect || a < b || a > c) // условия правильности ввода
                    Console.WriteLine("Некорректный ввод. Повторите попытку."); // сообщение об ошибке
            } while (!isCorrect || a < b || a > c); // условия правильности ввода
            return (a);
        }
        static void NextStep()
        {
            Console.WriteLine("\nНажмите Enter, чтобы продолжить.");
            Console.ReadLine();
        }
        static string CreateString()
        {
            string str;

            Console.Clear();
            Console.WriteLine("Выберите способ создания строки:");
            Console.WriteLine("1. Автоматически (готовая строка из коллекции)");
            Console.WriteLine("2. Вручную (ввод с клавиатуры)\n");

            int choice = CheckInput("номер команды", 1, 2);

            if (choice == 1)
            {
                str = "В траве сидел кузнечик! Кузнечик не трогал козявок и дружил с мухом.";
                Console.WriteLine($"\nНовая строка: \"{str}\"");
            }
            else
            {
                str = InputString();
            }

            return str;
        }
        static byte InputCorrect(string str)
        {
            Regex end = new(@"(!|\.|\?)$");
            Regex first = new(@"^[А-Я_0-9]");
            Regex contains = new(@"^([А-Яа-я_0-9]|\.|!|\?|,|;|:|\u0020)+$");
            Regex dublicate = new(@"(\.|!|\?|,|;|:|\u0020)(\.|!|\?|,|;|:)+");
            Regex doubleSpace = new(@"\u0020{2}");

            if (str.Length == 0)
                return 1;
            if (!contains.IsMatch(str))
                return 4;
            if (!end.IsMatch(str))
                return 2;
            if (!first.IsMatch(str))
                return 3;
            if (dublicate.IsMatch(str) || doubleSpace.IsMatch(str))
                return 5;
            return 0;
        }
        static string InputString()
        {
            string str;
            byte error;

            do
            {
                Console.Write("Введите строку: ");
                str = Console.ReadLine()!;
                error = InputCorrect(str);

                switch (error)
                {
                    case 1:
                        Console.WriteLine("Вы ничего не ввели! Повторите попытку.");
                        break;

                    case 2:
                        Console.WriteLine("Предложение должно заканчиваться знаком препинания! Повторите попытку.");
                        break;

                    case 3:
                        Console.WriteLine("Предложение должно начинаться с заглавной буквы! Повторите попытку.");
                        break;
                    case 4:
                        Console.WriteLine("Строка содержит недопустимые символы! Повторите попытку.");
                        break;
                    case 5:
                        Console.WriteLine("В строке повторяются знаки препинания! Повторите попытку.");
                        break;
                    case 0:
                        break;
                }
            } while (error != 0);

            return str;
        }
        /*static string DelWords(string str)
        {
            string[] sentences = str.Split(new char[] { '.', '!', '?' }, StringSplitOptions.RemoveEmptyEntries);
            int len = sentences.Length;

            for (int i = 0; i < len; i++) // для каждого предложения
            {
                string[] words = sentences[i].Split(new char[] { ' ', ',', ';', ':' }, StringSplitOptions.RemoveEmptyEntries);
                int wordCount = words.Length;

                for (int j = 0; j < words.Length; j++)
                {
                    char[] symbols = words[j].ToLower().ToCharArray();
                    int a = 0, b = symbols.Length - 1;

                    if (symbols[a] == symbols[b] && symbols.Length > 1)
                        words[j] = "";
                }
                sentences[i] = string.Join(" ", words);
            }
            str = string.Join(". ", sentences)
                .Replace("  ", " ")
                .Replace(" .", ".")
                .Replace(" !", "!")
                .Replace(" ?", "?")
                .Replace(" ;", ";")
                .Replace(" ,", ",")
                .Replace(" :", ":");
            return str;
        }*/
        static string DelWords(string str)
        {
            Regex endSent = new(@"(!|\?|\.)");
            Regex wordP = new(@".(,|;|:)$");
            Regex rep1 = new(@",(,|;|:)");
            Regex rep2 = new(@":(,|;|:)");
            Regex rep3 = new(@";(,|;|:)");
            int lPointer = 0, rPointer = 0;
            string ans = "";
            while (rPointer < str.Length)
            {
                string el = str[0].ToString();
                while (!endSent.IsMatch(el))
                {
                    el = str[rPointer].ToString();
                    rPointer++;
                }

                string sent = str.Substring(lPointer, rPointer - lPointer - 1);
                lPointer = rPointer;
                string[] words = sent.Split(" ", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                for (int index = 0; index < words.Length; index++)
                {
                    string word = words[index];
                    string chWord = word.ToLower();
                    if (wordP.IsMatch(word))
                        chWord = word[..^1].ToLower();
                    if (chWord.Length > 1 && chWord[0] == chWord[^1])
                    {
                        if (chWord.Length == word.Length)
                            words[index] = "";
                        else
                            words[index] = word[^1..];
                    }
                }
                ans += String.Join(" ", words) + str[rPointer-1].ToString();
            }
            ans = ans.Trim()
                     .Replace("  ", " ")
                     .Replace(" .", ".")
                     .Replace(" !", "!")
                     .Replace(" ?", "?")
                     .Replace(" ;", ";")
                     .Replace(" ,", ",")
                     .Replace(" :", ":");
            ans = rep1.Replace(ans, ",");
            ans = rep2.Replace(ans, ":");
            ans = rep3.Replace(ans, ";");
            return ans;
        }
        static void Main()
        {
            int choice;
            string text = "";

            do
            {
                Console.Clear();
                Console.WriteLine("Лабораторная работа №6 - Строки. Класс String.\n");
                Console.WriteLine("Меню команд:");
                Console.WriteLine("1. Создать строку");
                Console.WriteLine("2. Напечатать строку");
                Console.WriteLine("3. Удалить из строки все слова, начинающиеся и заканчивающиеся одним символом");
                Console.WriteLine("0. Выйти из программы");

                choice = CheckInput("номер команды", 0, 3);

                switch (choice)
                {
                    case 1:
                        Console.Clear();
                        text = CreateString();
                        NextStep();
                        break;

                    case 2:
                        Console.Clear();
                        Console.WriteLine("Ваша строка:\n");
                        Console.WriteLine(text);
                        NextStep();
                        break;

                    case 3:
                        Console.Clear();
                        Console.WriteLine("Слова, начинающиеся и заканчивающиеся на одну букву удалены!");
                        text = DelWords(text);
                        NextStep();
                        break;

                    case 0:
                        Console.WriteLine("Работа программы завершена!");
                        break;
                }
            } while (choice != 0);


        }
    }
}
