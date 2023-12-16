using System.Text.RegularExpressions;

namespace ver1
{
    internal class Program
    {
        static readonly Menu generateStringMenu = new(["Создать строку вручную", "Случайно сгенерировать строку", "Выход"]);//создаем меню с пунктами
        static readonly Menu postTaskMenu = new(["Вернуться на главную", "Выход"]);//меню после выполнения задачи
        static readonly Random rand = new();//объект класса Random для случайных значений
        static void Main()
        {
            while (true)//выход из цикла осуществляется при нажатии кнопки выход в интерфейсе
            {
                ChangeMenu(0);//переключаемся на первое меню
            }
        }

        /// <summary>
        /// Функция лля сменя меню на экране
        /// </summary>
        /// <param name="wishMenu">Номер меню, которое следует включить</param>
        static void ChangeMenu(byte wishMenu)
        {
            switch (wishMenu)
            {
                case 0://действия для первого меню
                    Console.Clear();//готовим консоль к выводу
                    generateStringMenu.ShowMenu();//печатаем элементы меню
                    string wkStr = ChooseGeneration(generateStringMenu.SetUserAnswer());//устанавливаем строку, полученную в зависимости от ответа пользователя
                    if (wkStr == "")
                    {
                        ChangeMenu(1);
                        return;
                    }
                    string[] result = WorkingOnString(wkStr); //получаем подходящие слова из строки в виде массива
                    Console.Clear(); //чистим консоль от лишнего
                    PrintResult(result, wkStr); //и выводим эти слова на экран
                    ChangeMenu(1);//печатаем следующее меню
                    break;
                case 1://действия для втого меню
                    postTaskMenu.ShowMenu();//выводим элементы на экран
                    postTaskMenu.SetUserAnswer();//получаем ответ
                    break;
            }
        }

        /// <summary>
        /// Инициализирует получение строки от пользователя или случайную строку в зависимости от выбора пользователя
        /// </summary>
        /// <param name="uAnsw">Ответ пользователя</param>
        /// <returns>Созданную строку</returns>
        static string ChooseGeneration(byte uAnsw)
        {
            string input = string.Empty;
            switch(uAnsw)
            {
                case 1:
                    input = ManualCreateString();//ручной ввод строки
                    break;
                case 2:
                    input = RandomCreateString();//случайно выбранная строка из файла
                    break;
            }
            return input;
        }

        /// <summary>
        /// Обработка строки
        /// </summary>
        /// <param name="str">Строка, которую нужно обработать</param>
        /// <returns>Массив подходящих слов</returns>
        static string[] WorkingOnString(string str)
        {
            Regex symbs = new(@"^([a-z]|_)([a-z_0-9]|_)*", RegexOptions.IgnoreCase | RegexOptions.Compiled); //регулярки для проверки содержания строки
            Regex marks = new(@"(!|\u002e|\u003f|,|;|:)$"); //знаки препинания в конце

            str = str
                    .Replace(".", ". ")
                    .Replace("!", "! ")
                    .Replace("?", "? ")
                    .Replace(",", ", ")
                    .Replace(";", "; ")
                    .Replace(":", ": ")
                    .Trim(); //подготовим строку

            string[] words = str.Split(" ", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);//разделим по пробелам с удалением лишних элементов
            List<string> correctWords = [];//создаем список с корректными словами словами
            string[] result = null!;//слова-ответ
            string[] keywords = ["abstract", "as", "base", "bool", "break", "byte", "case", "catch", "char", "checked", "class", "const", "continue",
                                 "decimal", "default", "delegate", "do", "double", "else", "enum", "event", "explicit", "extern", "false", "true",
                                 "finally", "fixed", "float", "for", "foreach", "goto", "if", "implicit", "in", "int", "interface", "internal", "is",
                                 "lock", "long", "namespace", "new", "null", "object", "operator", "out", "override", "params", "private", "protected",
                                 "public", "readonly", "ref", "return", "sbyte", "sealed", "short", "sizeof", "stackalloc", "static", "string", "struct",
                                 "switch", "this", "throw", "true", "try", "typeof", "uint", "ulong", "unchecked", "unsafe", "ushort", "using", "virtual",
                                 "void", "volatile", "while", "add", "and", "alias", "ascending", "args", "async", "await", "by", "descending", "dynamic",
                                 "equals", "file", "from", "get", "global", "group", "init", "into", "join", "let", "managed", "nameof", "nint", "not", "notnull",
                                 "nuint", "on", "or", "orderby", "partial", "record", "remove", "required", "scoped", "select", "set", "unmanaged",
                                 "value", "var", "when", "where", "with", "yield"];//ключевые слова языка
            for (int index = 0; index < words.Length; index++)
            {
                string word = words[index]; //выбираем слово из списка слов
                if (marks.IsMatch(word))
                    word = word
                        .Replace("?", "")
                        .Replace("!", "")
                        .Replace(".", "")
                        .Replace(",", "")
                        .Replace(";", "")
                        .Replace(":", ""); //удаляем из его знаки препинания если есть
                if (symbs.IsMatch(word))//если слово подходит
                    correctWords.Add(word);//добавляем его в список подходящих слов
            }
            correctWords = correctWords.Except(keywords).ToList();//удаляем из списка слов все ключевые слова языка
            if (correctWords.Count > 0)
            {
                int minLen = Enumerable.Min(correctWords.Select(x => x.Length));//находим минимальное слово
                result = correctWords.Distinct().Where(x => x.Length == minLen).ToArray();//выбираем слова минимальной длины в результат
            }
            return result!;//вернем результат
        }

        /// <summary>
        /// Функция для создания строки вручную
        /// </summary>
        /// <returns>Возвращает созданную строку</returns>
        static string ManualCreateString()
        {
            bool isCorrect = false; //корректность ввода
            string input;
            Console.Clear();
            Console.Write("Укажите вашу строку >>> ");
            do
            {
                input = Console.ReadLine()!;
                try
                {
                    StringCheck(input);//проверяем строку на правильность. Если она неправильна - функция бросает исключение
                    isCorrect = true;
                }
                catch (ArgumentException ex)
                {
                    Console.Write(ex.Message); //Печатаем сообщение об ошибке
                } 
            } while (!isCorrect);
            return input;
        }

        /// <summary>
        /// Создаем строку, случайно выбранную из файла
        /// </summary>
        /// <returns>Возвращает созданную строку</returns>
        static string RandomCreateString()
        {
            string[] tests = File.ReadAllLines("D:\\University\\dz\\K0taka\\Vikentieva\\L6\\ver1\\Tests.txt");//путь к файлу. К сожалению, абсолюнтый
            string input = tests[rand.Next(tests.Length)];//строку из списка доступных
            try
            {
                StringCheck(input); //проверяем ее на корректность
                return input;
            }
            catch (Exception ex)
            {
                Console.Clear();//иначе выводим сообщение об ошибке и ее содержание
                Console.WriteLine($"Случайная строка следующего содержания\n{input}\nне удовлетворяет условиям вводимой строки");
                Console.WriteLine($"Расшифровка ошибки: {ex.Message.Remove(ex.Message.IndexOf('.'))}");
            }
            return "";//и возвращаем пустую строку
        }

        /// <summary>
        /// Печатает массив с результатом на экран
        /// </summary>
        /// <param name="result"></param>
        /// <param name="start"></param>
        static void PrintResult(string[] result, string start)
        {
            if (result == null || result.Length == 0)
            {
                Console.WriteLine($"В строке\n{start}\nнет идентефикаторов!"); //Если идентефикаторов нет то так и напишем
                return;
            }
            Console.WriteLine($"В введенной строке {start} были найдены следующие идентефикаторы:"); //Выводим строку
            string toPrint = string.Join(" | ", result);//создаем строку со всеми найденными словами с разделителем |
            Console.WriteLine(toPrint);//печатаем ее
        }

        /// <summary>
        /// Проверяет строку на корректность
        /// </summary>
        /// <param name="str">Строка для проверки</param>
        /// <exception cref="ArgumentException">Исключение об ошибке строки</exception>
        static void StringCheck(string str)
        {
            Regex pCheck = new(@"^([a-z_0-9]|_)([a-z_0-9]|_|!|\u003f|\u002e|,|;|:|\u0020)*(\.|\?|!)$", RegexOptions.IgnoreCase | RegexOptions.Compiled); //оканчивается зн. преп. и не нач с него
            Regex sFormat = new(@"^([a-z_0-9]|_|!|\u003f|\u002e|,|;|:|\u0020)+$", RegexOptions.IgnoreCase | RegexOptions.Compiled); //содержание символов
            Regex dCheck = new(@"(!|\u003f|\u002e|,|;|:|\u0020)(!|\u003f|\u002e|,|;|:)+", RegexOptions.IgnoreCase | RegexOptions.Compiled);//несколько спец. символов подряд
            Regex dSpace = new(@"\u0020{2}");//несколько пробелов подряд
            Regex onlyPoint = new(@"^(\.|\?|!)$");//первый символ строки - спец. знак
            if (str == null || str.Length == 0)
                throw new ArgumentException("Выполнен пусстой ввод. Повторите ввод предложений >>> ");
            if (!sFormat.IsMatch(str))
                throw new ArgumentException("В строке присутствуют символы, не относящиеся к поддерживаемому алфавиту. Повторите ввод предложений >>> ");
            if (onlyPoint.IsMatch(str))
                throw new ArgumentException($"В строке нет иных символов, кроме \"{str}\". Повторите ввод предложений >>> ");
            if (!pCheck.IsMatch(str))
                throw new ArgumentException("Ошибка в знаках препинания. Повторите ввод предложений >>> ");
            if (dCheck.IsMatch(str) || dSpace.IsMatch(str))
                throw new ArgumentException("Дублирование специальных знаков. Повторите ввод предложений >>> ");
            return;
        }
    }
}
