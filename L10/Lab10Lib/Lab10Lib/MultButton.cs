using System.Diagnostics.CodeAnalysis;
using static IOLib.IO;
namespace Lab10Lib
{
    public class MultButton: Button, IInit, IComparable, ICloneable
    {
        /// <summary>
        /// Реализация логического значения включения кнопки при помощи автосвойства
        /// </summary>
        public bool IsEnabled { get; set; }

        /// <summary>
        /// Определение объекта со значениями по-умолчанию
        /// </summary>
        public MultButton() : base()
        {
            IsEnabled = false;
        }

        /// <summary>
        /// Определения объекта с заранее заданными значениями
        /// </summary>
        /// <param name="x">координата x</param>
        /// <param name="y">координата y</param>
        /// <param name="text">текст кнопки</param>
        /// <param name="enabled">состояние включенности</param>
        public MultButton(uint x, uint y, string text, bool enabled) : base(x, y, text)
        {
            IsEnabled = enabled;
        }

        /// <summary>
        /// Представление информации об объекте в виде строки
        /// </summary>
        /// <returns>Строка с информацией об объекте</returns>
        [ExcludeFromCodeCoverage]
        public override string ToString()
        {
            return base.ToString() + $" Она {(IsEnabled ? "включена" : "выключена")}.";
        }

        /// <summary>
        /// Определяет равен ли переданный объект текущему
        /// </summary>
        /// <param name="obj">Объект для сравнения</param>
        /// <returns>Логическое о равенстве объектов, true если равны</returns>
        public override bool Equals(object? obj)
        {
            if (obj is not MultButton mltBtn)
                return false;
            return base.Equals(mltBtn) && IsEnabled == mltBtn.IsEnabled;
        }

        /// <summary>
        /// Инициализация объекта с клавиатуры
        /// </summary>
        [ExcludeFromCodeCoverage]
        public override void Init()
        {
            base.Init();
            IsEnabled = GetBoolAnswer("Укажите \"true\" для состояния \"Включить\" или \"false\" для состояния \"Выключить\" >>> ");
        }

        /// <summary>
        /// Инициализация объекта случайными значениями
        /// </summary>
        [ExcludeFromCodeCoverage]
        public override void RandomInit()
        {
            base.RandomInit();
            IsEnabled = rnd.Next(0, 2) == 0;
        }

        /// <summary>
        /// Вывод информации об объекте на экран
        /// </summary>
        [ExcludeFromCodeCoverage]
        public new void Show()
        {
            WriteLine($"Элемент типа {GetType().Name} имеет ID {Id}, и находится по координатам x = {X}, y = {Y}. Она имеет текст: \"{Text}\". Она {(IsEnabled ? "включена" : "выключена")}.");
        }

        /// <summary>
        /// Создание глубокой копии объекта
        /// </summary>
        /// <returns>Объект созданный при помощи глубокого копирования</returns>
        public override object Clone()
        {
            return new MultButton(X, Y, Text, IsEnabled);
        }

        /// <summary>
        /// Вычислияет хэш-код для объекта
        /// </summary>
        /// <returns>Вычисленный хэш-код</returns>
        public override int GetHashCode()
        {
            return base.GetHashCode() + IsEnabled.GetHashCode() * 17;
        }
    }
}
