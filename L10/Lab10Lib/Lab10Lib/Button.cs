using System.Text.RegularExpressions;

namespace Lab10Lib
{
    public partial class Button: ControlElement, IInit, IComparable, ICloneable
    {
        private static readonly Regex checkSpace = NotEmptyText();
        protected string? text;
        
        public string Text
        {
            get { return text!; }
            set { text = value == null || value == "" || !checkSpace.IsMatch(value) ? throw new ArgumentNullException(message: "Button text must not be empty", paramName: text) :
                    value.Length > 100 ? throw new NotSupportedException("Text is too long!") : value; }
        }

        public Button() : base()
        {
            Text = "Вы наблюдаете отсутствие текста";
        }

        public Button(uint x, uint y, string text) : base(x, y)
        {
            Text = text;
        }

        [GeneratedRegex(@"\S+", RegexOptions.Compiled)]
        private static partial Regex NotEmptyText();

        public override string ToString()
        {
            return base.ToString() + $" Она имеет текст: \"{Text}\".";
        }

        public override void Init()
        {
            base.Init();
            bool isCorrect = false;
            do
            {
                try
                {
                    Text = GetTextAnswer("Введите текст кнопки >>> ")!;
                    isCorrect = true;
                }
                catch (Exception)
                {
                    ReturnError(ErrorCodes.IncorrectClassInit);
                    WriteLine("Повторите ввод!");
                }
            } while (!isCorrect);
        }

        public override void RandomInit()
        {
            base.RandomInit();
            Text = $"Кнопка {rnd.Next(0, 100)}";
        }

        public override bool Equals(object? obj)
        {
            if (obj is not Button btn)
                return false;
            return base.Equals(btn) && btn.Text == Text;
        }

        public new void Show()
        {
            WriteLine($"Элемент типа {GetType()} имеет ID {Id}, и находится по координатам x = {X}, y = {Y}. Она имеет текст: \"{Text}\".");
        }

        public override object Clone()
        {
            return new Button(X, Y, Text);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode() + Text.GetHashCode() * 13;
        }

        ~Button()
        {
            ID.destroyedIds.Add(id);
        }
    }
}
