namespace Lab10Lib
{
    public partial class TextField: ControlElement, IInit, IComparable, ICloneable
    {
        public string Hint { get; set; }
        public string Text { get; set; }

        public TextField() : base()
        {
            Hint = string.Empty;
            Text = string.Empty;
        }

        public TextField(uint x, uint y, string hint, string text) : base(x, y)
        {
            Hint = hint;
            Text = text;
        }

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

        public new void Show()
        {
            WriteLine($"Элемент типа {GetType()} имеет ID {Id}, и находится по координатам x = {X}, y = {Y}. Значение подсказки Hint: \"{Hint}\", а текст: \"{Text}\".");
        }

        public override object Clone()
        {
            return new TextField(X, Y, Hint, Text);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode() + Hint.GetHashCode() * 11 + Text.GetHashCode() * 13;
        }

        ~TextField()
        {
            ID.destroyedIds.Add(id);
        }
    }
}
