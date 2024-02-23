using System.Diagnostics.CodeAnalysis;
using static IOLib.IO;
namespace Lab10Lib
{
    public class ControlElement: IInit, IComparable, ICloneable, IDisposable
    {
        protected static readonly Random rnd = new();
        protected ID id;
        protected Cordinates cordinates = new();

        /// <summary>
        /// Свойство (геттер) для поля id
        /// </summary>
        public uint Id
        {
            get { return id; }
        }

        /// <summary>
        /// Свойства для поля x
        /// </summary>
        public uint X
        {
            get { return cordinates[0]; }
            set 
            {
                if (value >= 0 && value <= 1920) //стандартное расширение экрана
                    cordinates[0] = value;
                else
                    throw new InvalidOperationException("Incorrect x cordinate");
            }
        }

        /// <summary>
        /// Свойства для поля y
        /// </summary>
        public uint Y
        {
            get { return cordinates[1]; }
            set 
            {
                if (value >= 0 && value <= 1080)//стандартное расширение экрана
                    cordinates[1] = value;
                else
                    throw new InvalidOperationException("Incorrect y cordinate");
            }
        }

        /// <summary>
        /// Создает объект со значениями по-умолчанию
        /// </summary>
        public ControlElement()
        {
            id = new ID();
            X = 0;
            Y = 0;
        }

        /// <summary>
        /// Создает объект по заданным значениям
        /// </summary>
        /// <param name="x">значение координаты x</param>
        /// <param name="y">значение координаты y</param>
        public ControlElement(uint x, uint y)
        {
            id = new ID();
            X = x;
            Y = y;
        }

        /// <summary>
        /// Строковое представление информации об объекте
        /// </summary>
        /// <returns>Строка с информацией об объекте</returns>
        [ExcludeFromCodeCoverage]
        public override string ToString()
        {
            return $"Элемент типа {GetType().Name} имеет ID {Id}, и находится по координатам x = {X}, y = {Y}.";
        }

        /// <summary>
        /// Определяет равен ли передаенный объект текущему
        /// </summary>
        /// <param name="obj">Объект для сравнения</param>
        /// <returns>Возвращает логическое значение о равенстве объектов, true если равны</returns>
        public override bool Equals(object? obj)
        {
            if (obj is not ControlElement el)
                return false;
            return el.X == X && el.Y == Y;
        }

        /// <summary>
        /// Инициализирует объект по заданным значениям
        /// </summary>
        [ExcludeFromCodeCoverage]
        public virtual void Init()
        {
            X = (uint)GetIntegerAnswer("Введите координату X >>> ", 0, 1921);
            Y = (uint)GetIntegerAnswer("Введите координату Y >>> ", 0, 1081);
        }

        /// <summary>
        /// Инициализирует объект случайными объектами
        /// </summary>
        [ExcludeFromCodeCoverage]
        public virtual void RandomInit()
        {
            X = (uint)rnd.Next(0, 1921);
            Y = (uint)rnd.Next(0, 1081);
        }

        /// <summary>
        /// Сравнение объектов по ID
        /// </summary>
        /// <param name="obj">Сравниваемый объект</param>
        /// <returns>Целое число, -1 если переданный объект больше, 0 если равны, 1 если переданный объект меньше</returns>
        public int CompareTo(object? obj)
        {
            if (obj is not ControlElement element)
                return -1;
            return Id.CompareTo(element.Id);
        }

        /// <summary>
        /// Показать информацию об объекте на экране
        /// </summary>
        [ExcludeFromCodeCoverage]
        public void Show()
        {
            WriteLine($"Элемент типа {GetType().Name} имеет ID {Id}, и находится по координатам x = {X}, y = {Y}.");
        }

        /// <summary>
        /// Создать глубокую копию объекта
        /// </summary>
        /// <returns>Объект созданный глубоким копированием</returns>
        public virtual object Clone()
        {
            return new ControlElement(X, Y);
        }

        /// <summary>
        /// Создает поверхностную (побайтовую) копию объекта
        /// </summary>
        /// <returns>Объект созданный поверхностным копированием</returns>
        public object ShallowCopy()
        {
            return this.MemberwiseClone();
        }

        /// <summary>
        /// Вычисляет хэш-код объекта
        /// </summary>
        /// <returns>Возвращает вычисленный хэш-код</returns>
        public override int GetHashCode()
        {
            return id.GetHashCode() + X.GetHashCode()*5 + Y.GetHashCode()*7;
        }

        /// <summary>
        /// Выполняет занесение ID в список свободных
        /// </summary>
        public void Dispose()
        {
            if(!ID.destroyedIds.Contains(Id))
                ID.destroyedIds.Add(Id);
            GC.SuppressFinalize(this);
        }

        ~ControlElement()
        {
            Dispose();
        }
    }
}
