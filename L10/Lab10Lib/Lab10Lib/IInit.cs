namespace Lab10Lib
{
    /// <summary>
    /// Интерфейс IInit определяет функции для инициализации объекта
    /// </summary>
    public interface IInit
    {
        /// <summary>
        /// Инициализация объекта вручную с клавиатуры
        /// </summary>
        void Init();

        /// <summary>
        /// Инициализация объекта случайными значениями
        /// </summary>
        void RandomInit();
    }
}
