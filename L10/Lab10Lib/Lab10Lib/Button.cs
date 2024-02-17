﻿using System.Text.RegularExpressions;

namespace Lab10Lib
{
    public partial class Button:ControlElement
    {
        private static readonly Regex checkSpace = NotEmptyText();
        protected string? text;
        
        public string Text
        {
            get { return text!; }
            set { text = value == null || value == "" || !checkSpace.IsMatch(value) ? throw new ArgumentNullException(message: "Button text must not be empty", paramName: text) : value; }
        }

        public Button() : base()
        {
            Text = "Вы наблюдаете отсутствие текста";
        }

        public Button(uint x, uint y, string text) : base(x, y)
        {
            Text = text;
        }

        public Button(Button button) : base(button)
        {
            Text = button.Text;
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
                catch (ArgumentNullException)
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

        ~Button()
        {
            destroyedIds.Add(id);
        }
    }
}
