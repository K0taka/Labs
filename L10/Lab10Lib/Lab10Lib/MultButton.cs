namespace Lab10Lib
{
    public class MultButton: Button, IInit
    {
        public bool IsEnabled { get; set; }

        public MultButton() : base()
        {
            IsEnabled = false;
        }

        public MultButton(uint x, uint y, string text, bool enabled) : base(x, y, text)
        {
            IsEnabled = enabled;
        }

        public MultButton(MultButton button) : base(button)
        {
            IsEnabled = button.IsEnabled;
        }

        public override string ToString()
        {
            return base.ToString() + $" Она {(IsEnabled ? "включена" : "выключена")}.";
        }

        public override bool Equals(object? obj)
        {
            if (obj is not MultButton mltBtn)
                return false;
            return base.Equals(mltBtn) && IsEnabled == mltBtn.IsEnabled;
        }

        public override void Init()
        {
            base.Init();
            IsEnabled = bool.Parse(GetTextAnswer("Укажите любое число кроме 0 для состояния \"Включить\" или 0 для состояния \"Выключить\" >>> "));
        }

        public override void RandomInit()
        {
            base.RandomInit();
            IsEnabled = rnd.Next(0, 2) == 0;
        }

        public new void Show()
        {
            WriteLine($"Элемент типа {GetType()} имеет ID {Id}, и находится по координатам x = {X}, y = {Y}. Она имеет текст: \"{Text}\". Она {(IsEnabled ? "включена" : "выключена")}.");
        }

        ~MultButton()
        {
            destroyedIds.Add(id);
        }
    }
}
