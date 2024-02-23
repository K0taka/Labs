using System.Diagnostics.CodeAnalysis;
using static IOLib.IO;
namespace Lab10Lib
{
    public partial class TextField: ControlElement, IInit, IComparable, ICloneable
    {
        /// <summary>
        /// Автосвойство для текстовой подсказки
        /// </summary>
        public string Hint { get; set; }

        /// <summary>
        /// Автосвойство для текста
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Создание объекта со значениями по-умолчанию
        /// </summary>
        public TextField() : base()
        {
            Hint = string.Empty;
            Text = string.Empty;
        }

        /// <summary>
        /// Создание объекта по заданным значениям
        /// </summary>
        /// <param name="x">Координата x</param>
        /// <param name="y">Координата y</param>
        /// <param name="hint">Текст подсказки</param>
        /// <param name="text">Значение текстового поля</param>
        public TextField(uint x, uint y, string hint, string text) : base(x, y)
        {
            Hint = hint;
            Text = text;
        }

        /// <summary>
        /// Определяет равен ли переданный объект текущему
        /// </summary>
        /// <param name="obj">Объект для сравнения</param>
        /// <returns>Возвращает логическое значение о равенстве объектов, true если равны</returns>
        public override bool Equals(object? obj)
        {
            if (obj is not TextField tF)
                return false;
            return base.Equals(tF) && tF.Text == Text && tF.Hint == Hint;
        }

        /// <summary>
        /// Строковое представление информации об объекте
        /// </summary>
        /// <returns>Строка с информацией об объекте</returns>
        [ExcludeFromCodeCoverage]
        public override string ToString()
        {
            return base.ToString() + $" Значение подсказки Hint: \"{Hint}\", а текст: \"{Text}\".";
        }

        /// <summary>
        /// Инициализация объекта с клавиатуры
        /// </summary>
        [ExcludeFromCodeCoverage]
        public override void Init()
        {
            base.Init();
            Hint = GetTextAnswer("Введите подсказку >>> ");
            Text = GetTextAnswer("Введите текст >>> ");
        }

        /// <summary>
        /// Инициализация объекта случайными значениями
        /// </summary>
        [ExcludeFromCodeCoverage]
        public override void RandomInit()
        {
            base.RandomInit();
            Hint = $"Очень серьезный запрос для ввода номер {rnd.Next(0,100)}";
            Text = $"Это уже {rnd.Next(5, 100)} кружка кофе, выпитая за время написания этой работы";
        }

        /// <summary>
        /// Вывод информации об объекте на экран
        /// </summary>
        [ExcludeFromCodeCoverage]
        public new void Show()
        {
            WriteLine($"Элемент типа {GetType().Name} имеет ID {Id}, и находится по координатам x = {X}, y = {Y}. Значение подсказки Hint: \"{Hint}\", а текст: \"{Text}\".");
        }

        /// <summary>
        /// Клонирование объекта (глубокая копия)
        /// </summary>
        /// <returns>Возвращает объект созданный с помощью глубокого копирования</returns>
        public override object Clone()
        {
            return new TextField(X, Y, Hint, Text);
        }

        /// <summary>
        /// Вычисляет хэш-код объекта
        /// </summary>
        /// <returns>Возвращает вычисленный хэш-код</returns>
        public override int GetHashCode()
        {
            return base.GetHashCode() + Hint.GetHashCode() * 11 + Text.GetHashCode() * 13;
        }
    }
}
