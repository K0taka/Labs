namespace Lab10Lib
{
    public class ID: IComparable
    {
        internal static readonly List<uint> destroyedIds = [];
        private static uint nextId;
        private readonly uint id;

        /// <summary>
        /// Инициализирует объект с уникальным ID
        /// </summary>
        public ID() => id = ChooseId();

        /// <summary>
        /// Неявное приведение типов в uint
        /// </summary>
        /// <param name="id">Объект ID</param>
        public static implicit operator uint(ID id) => id.id;

        /// <summary>
        /// Функция для сравнения объектов ID (реализация IComparable)
        /// </summary>
        /// <param name="obj">Объект с которым необходимо сравнить</param>
        /// <returns>Возвращает целое число - результат сравнения: 1, если переданный объект меньше; 0, если объекты равны; -1, если переданный объект больше</returns>
        public int CompareTo(object? obj)
        {
            if (obj is not ID id)
                return -1;
            return this.id.CompareTo(id);
        }

        /// <summary>
        /// Представление объекта в строковом виде
        /// </summary>
        /// <returns>Уникальный номер id в виде строки</returns>
        public override string ToString() => id.ToString();

        /// <summary>
        /// Определяет равенство текущего и переданного объектов
        /// </summary>
        /// <param name="obj">Объект с которым необходимо сравнить</param>
        /// <returns>Результат равенства объектов, true если равны</returns>
        public override bool Equals(object? obj)
        {
            if (obj is not ID id)
                return false;
            return this.id.Equals(id);
        }

        /// <summary>
        /// Вычисляет хэш-код для объекта
        /// </summary>
        /// <returns>Вычисленный хэш-код</returns>
        public override int GetHashCode()
        {
            return id.GetHashCode() * 3;
        }

        /// <summary>
        /// Определяет наименьший доступный свободный ID
        /// </summary>
        /// <returns>Возвращает найденный ID</returns>
        private static uint ChooseId()
        {
            if (destroyedIds != null && destroyedIds.Count != 0)
            {
                destroyedIds.Sort();
                uint res = destroyedIds[0];
                destroyedIds.RemoveAt(0);
                return res;
            }
            return nextId++;
        }
    }
}
