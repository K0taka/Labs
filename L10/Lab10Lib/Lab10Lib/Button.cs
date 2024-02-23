using static IOLib.IO;
using System.Text.RegularExpressions;

namespace Lab10Lib
{
    public partial class Button: ControlElement, IInit, IComparable, ICloneable
    {
        private static readonly Regex checkSpace = NotEmptyText(); //регулярное выражение на непустой текст (присутствует любой символ кроме пробельного)
        private string? text;//поле для текста кнопки
        
        /// <summary>
        /// Свойства для поля text
        /// </summary>
        public string Text
        {
            get { return text!; }

            //проверка на корректность устанавливаемого текста: он должен содержать непробельный символ и не превышать 100 символов в длине
            set { text = value == null || value == "" || !checkSpace.IsMatch(value) ? throw new ArgumentNullException(message: "Button text must not be empty", paramName: text) :
                    value.Length > 100 ? throw new NotSupportedException("Text is too long!") : value; }
        }

        /// <summary>
        /// Создает объект класса, инициализированный значениями по-умолчанию
        /// </summary>
        public Button() : base()
        {
            Text = "Вы наблюдаете отсутствие текста";
        }

        /// <summary>
        /// Инициализирует объект класса с заданными значениями
        /// </summary>
        /// <param name="x">uint координата x</param>
        /// <param name="y">uint координата y</param>
        /// <param name="text">Непустой текст кнопки</param>
        public Button(uint x, uint y, string text) : base(x, y)
        {
            Text = text;
        }

        [GeneratedRegex(@"\S+", RegexOptions.Compiled)]
        private static partial Regex NotEmptyText();

        /// <summary>
        /// Строковое представление всей информации об объекте
        /// </summary>
        /// <returns>Строка с информацией об объекте</returns>
        public override string ToString()
        {
            return base.ToString() + $" Она имеет текст: \"{Text}\".";
        }

        /// <summary>
        /// Метод для инициализации полей классса вручную
        /// </summary>
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

        /// <summary>
        /// Метод для инициализации полей класса случайным образом
        /// </summary>
        public override void RandomInit()
        {
            base.RandomInit();
            Text = $"Кнопка {rnd.Next(0, 100)}";
        }

        /// <summary>
        /// Определяет равен ли переданный объект текущему объекту
        /// </summary>
        /// <param name="obj">Сравниваемый объект</param>
        /// <returns>Логическое значение, true если равны</returns>
        public override bool Equals(object? obj)
        {
            if (obj is not Button btn)
                return false;
            return base.Equals(btn) && btn.Text == Text;
        }
        /// <summary>
        /// Выводит на экран информацию о текущем объекте
        /// </summary>
        public new void Show()
        {
            WriteLine($"Элемент типа {GetType().Name} имеет ID {Id}, и находится по координатам x = {X}, y = {Y}. Она имеет текст: \"{Text}\".");
        }

        /// <summary>
        /// Глубокое копирование объекта
        /// </summary>
        /// <returns>Объект созданный с помощью глубокого копирования</returns>
        public override object Clone()
        {
            return new Button(X, Y, Text);
        }

        /// <summary>
        /// Функция для нахождения хэш-кода для текущего объекта
        /// </summary>
        /// <returns>Возвращает найденный хэш-код</returns>
        public override int GetHashCode()
        {
            return base.GetHashCode() + Text.GetHashCode() * 13;
        }
    }
}
