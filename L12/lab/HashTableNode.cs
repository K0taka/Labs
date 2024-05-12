namespace lab
{
    public class HashTableNode<TKey, TValue> where TKey: notnull
    {

        /// <summary>
        /// Автосвойство для ключа
        /// </summary>
        public TKey Key { get; set; }

        /// <summary>
        /// Автосвойство для значения
        /// </summary>
        public TValue Data { get; set; }

        /// <summary>
        /// Следующий узел в цепочке
        /// </summary>
        public HashTableNode<TKey, TValue>? Next { get; set; }

        /// <summary>
        /// Конструктор для узла
        /// </summary>
        /// <param name="key">Ключ для добавления</param>
        /// <param name="data">Значение для добавлеия</param>
        /// <param name="next">Следующий элемент в цепочке</param>
        public HashTableNode(TKey key, TValue data, HashTableNode<TKey, TValue>? next = null)
        {
            Key = key;
            Data = data;
            Next = next;
        }

        /// <summary>
        /// хэш-код узла
        /// </summary>
        /// <returns>Хэш-код сохраненного ключа</returns>
        public override int GetHashCode()
        {
            return Key.GetHashCode();
        }
    }
}
