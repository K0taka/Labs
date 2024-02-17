using System.Text.RegularExpressions;

namespace Lab10Lib
{
    public partial class TextField: ControlElement
    {
        private string Hint { get; set; }
        private static readonly Regex checkSpace = NotEmptyText();
        private string? text;
        public string Text
        {
            get { return text!; }
            set { text = value == null || value == "" || !checkSpace.IsMatch(value) ? throw new ArgumentNullException(message: "Textfield's text must not be empty", paramName: text) : value; }
        }

        public TextField() : base()
        {
            Hint = string.Empty;
            text = "Отсутствие текста!!!";
        }

        public TextField(uint x, uint y, string hint, string text) : base(x, y)
        {
            Hint = hint;
            Text = text;
        }

        public TextField(TextField tF) : base(tF)
        {
            Hint = tF.Hint;
            Text = tF.Text;
        }

        [GeneratedRegex(@"\S+", RegexOptions.Compiled)]
        private static partial Regex NotEmptyText();

        public override bool Equals(object? obj)
        {
            if (obj is not TextField tF)
                return false;
            return base.Equals(tF) && tF.Text == Text && tF.Hint == Hint;
        }

        public override string ToString()
        {
            return base.ToString() + $" Значение подсказки Hint: \"{Hint}\", а текст: \"{Text}\".";
        }

        public override void Init()
        {
            base.Init();
            Hint = GetTextAnswer("Введите подсказку >>> ");
            bool isCorrect = false;
            do
            {
                try
                {
                    Text = GetTextAnswer("Введите текст >>> ");
                    isCorrect = true;
                }
                catch(ArgumentNullException)
                {
                    ReturnError(ErrorCodes.IncorrectClassInit);
                    WriteLine("Повторите ввод!");
                }
            } while (!isCorrect);

        }

        public override void RandomInit()
        {
            base.RandomInit();
            Hint = $"Очень серьезный запрос для ввода номер {rnd.Next(0,100)}";
            Text = $"Это уже {rnd.Next(5, 100)} кружка кофе, выпитая за время написания этой работы";
        }

        ~TextField()
        {
            destroyedIds.Add(id);
        }
    }
}
