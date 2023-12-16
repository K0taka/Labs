using System.Text.RegularExpressions;

namespace ver1
{
    internal class Program
    {
        static readonly Menu generateStringMenu = new(["Создать строку вручную", "Случайно сгенерировать строку", "Выход"]);
        static readonly Menu postTaskMenu = new(["Вернуться на главную", "Выход"]);
        static void Main()
        {
            while (true)
            {
                ChangeMenu(0);
            }
        }

        static void ChangeMenu(byte wishMenu)
        {
            switch (wishMenu)
            {
                case 0:
                    Console.Clear();
                    generateStringMenu.ShowMenu();
                    string wkStr = ChooseGeneration(generateStringMenu.SetUserAnswer());
                    if (wkStr == "")
                    {
                        ChangeMenu(1);
                        return;
                    }
                    string[] result = WorkingOnString(wkStr);
                    Console.Clear();
                    PrintResult(result, wkStr);
                    ChangeMenu(1);
                    break;
                case 1:
                    postTaskMenu.ShowMenu();
                    postTaskMenu.SetUserAnswer();
                    break;
            }
        }

        static string ChooseGeneration(byte uAnsw)
        {
            string input = string.Empty;
            switch(uAnsw)
            {
                case 1:
                    input = ManualCreateString();
                    break;
                case 2:
                    input = RandomCreateString();
                    break;
            }
            return input;
        }

        static string[] WorkingOnString(string str)
        {
            Regex symbs = new(@"^([a-z]|_)([a-z_0-9]|_)*", RegexOptions.IgnoreCase | RegexOptions.Compiled);
            Regex marks = new(@"(!|\u002e|\u003f|,|;|:)$");

            str = str
                    .Replace(".", ". ")
                    .Replace("!", "! ")
                    .Replace("?", "? ")
                    .Replace(",", ", ")
                    .Replace(";", "; ")
                    .Replace(":", ": ")
                    .Trim(); //не удаляет внутри строки пробелы. Значение минимума это слова языка и больше с такой же длиной нет

            string[] words = str.Split(" ", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            List<string> correctWords = [];
            string[] result = null!;
            string[] keywords = ["abstract", "as", "base", "bool", "break", "byte", "case", "catch", "char", "checked", "class", "const", "continue",
                                 "decimal", "default", "delegate", "do", "double", "else", "enum", "event", "explicit", "extern", "false", "true",
                                 "finally", "fixed", "float", "for", "foreach", "goto", "if", "implicit", "in", "int", "interface", "internal", "is",
                                 "lock", "long", "namespace", "new", "null", "object", "operator", "out", "override", "params", "private", "protected",
                                 "public", "readonly", "ref", "return", "sbyte", "sealed", "short", "sizeof", "stackalloc", "static", "string", "struct",
                                 "switch", "this", "throw", "true", "try", "typeof", "uint", "ulong", "unchecked", "unsafe", "ushort", "using", "virtual",
                                 "void", "volatile", "while", "add", "and", "alias", "ascending", "args", "async", "await", "by", "descending", "dynamic",
                                 "equals", "file", "from", "get", "global", "group", "init", "into", "join", "let", "managed", "nameof", "nint", "not", "notnull",
                                 "nuint", "on", "or", "orderby", "partial", "record", "remove", "required", "scoped", "select", "set", "unmanaged",
                                 "value", "var", "when", "where", "with", "yield"];
            for (int index = 0; index < words.Length; index++)
            {
                string word = words[index];
                if (marks.IsMatch(word))
                    word = word
                        .Replace("?", "")
                        .Replace("!", "")
                        .Replace(".", "")
                        .Replace(",", "")
                        .Replace(";", "")
                        .Replace(":", "");
                if (symbs.IsMatch(word))
                    correctWords.Add(word);
            }
            correctWords = correctWords.Except(keywords).ToList();
            if (correctWords.Count > 0)
            {
                int minLen = Enumerable.Min(correctWords.Select(x => x.Length));
                result = correctWords.Distinct().Where(x => x.Length == minLen).ToArray();
            }
            return result!;
        }

        static string ManualCreateString()
        {
            bool isCorrect = false;
            string input;
            Console.Clear();
            Console.Write("Укажите вашу строку >>> ");
            do
            {
                input = Console.ReadLine()!;
                try
                {
                    StringCheck(input);
                    isCorrect = true;
                }
                catch (ArgumentException ex)
                {
                    Console.Write(ex.Message);
                } 
            } while (!isCorrect);
            return input;
        }

        static string RandomCreateString()
        {
            string[] tests = File.ReadAllLines("D:\\University\\dz\\K0taka\\Vikentieva\\L6\\ver1\\Tests.txt");
            Random rand = new();
            string input = tests[rand.Next(tests.Length)];
            try
            {
                StringCheck(input);
                return input;
            }
            catch (Exception ex)
            {
                Console.Clear();
                Console.WriteLine($"Случайная строка следующего содержания\n{input}\nне удовлетворяет условиям вводимой строки");
                Console.WriteLine($"Расшифровка ошибки: {ex.Message.Remove(ex.Message.IndexOf('.'))}");
            }
            return "";
        }

        static void PrintResult(string[] result, string start)
        {
            if (result == null || result.Length == 0)
            {
                Console.WriteLine("В строке нет индентефикаторов!");
                return;
            }
            Console.WriteLine($"В введенной строке {start} были найдены следующие идентефикаторы:");
            string toPrint = string.Join(" | ", result);
            Console.WriteLine(toPrint);
        }

        static void StringCheck(string str)
        {
            Regex pCheck = new(@"^([a-z_0-9]|_)([a-z_0-9]|_|!|\u003f|\u002e|,|;|:|\u0020)*(\.|\?|!)$", RegexOptions.IgnoreCase | RegexOptions.Compiled); //оканчивается зн. преп. и не нач с него
            Regex sFormat = new(@"^([a-z_0-9]|_|!|\u003f|\u002e|,|;|:|\u0020)+$", RegexOptions.IgnoreCase | RegexOptions.Compiled); //содержание символов
            Regex dCheck = new(@"(!|\u003f|\u002e|,|;|:|\u0020)(!|\u003f|\u002e|,|;|:)+", RegexOptions.IgnoreCase | RegexOptions.Compiled);//несколько спец. символов подряд
            Regex dSpace = new(@"\u0020{2}");
            Regex onlyPoint = new(@"^(\.|\?|!)$");
            if (str == null || str.Length == 0)
                throw new ArgumentException("Выполнен пусстой ввод. Повторите ввод предложений >>> ");
            if (!sFormat.IsMatch(str))
                throw new ArgumentException("В строке присутствуют символы, не относящиеся к поддерживаемому алфавиту. Повторите ввод предложений >>> ");
            if (!pCheck.IsMatch(str))
                throw new ArgumentException("Ошибка в знаках препинания. Повторите ввод предложений >>> ");
            if (dCheck.IsMatch(str) || dSpace.IsMatch(str))
                throw new ArgumentException("Дублирование специальных знаков. Повторите ввод предложений >>> ");
            if (onlyPoint.IsMatch(str))
                throw new ArgumentException($"В строке нет иных символов, кроме {str}. Повторите ввод предложений >>> ");
            return;
        }
    }
}
