namespace Lab10Lib
{
    public class Cordinates
    {
        private readonly uint[] cordinates;

        /// <summary>
        /// Инициализирует объект в начале координат
        /// </summary>
        public Cordinates()
        {
            cordinates = [0, 0];
        }

        /// <summary>
        /// Индексатор для получения доступа: чтения или изменения - координат массива
        /// </summary>
        /// <param name="index">Индекс, по которому производится действие</param>
        /// <returns>Соответствующую координату</returns>
        /// <exception cref="IndexOutOfRangeException">Индекс выходит за пределы массива координат</exception>
        public uint this[int index]
        {
            get
            {
                if (index >= 0 && index <  cordinates.Length)
                    return cordinates[index];
                throw new IndexOutOfRangeException();
            }
            set
            {
                if (index >= 0 && index < cordinates.Length)
                    cordinates[index] = value;
            }
        }
    }
}
