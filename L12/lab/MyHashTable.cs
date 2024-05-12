using System.Collections;

namespace lab
{
    public class MyHashTable<TKey, TValue>: IEnumerable<HashTableChain<TKey, TValue>?> where TValue : ICloneable where TKey : notnull, ICloneable
    {
        /// <summary>
        /// Собственно хэш-таблица
        /// </summary>
        readonly HashTableChain<TKey, TValue>?[] table;
        
        /// <summary>
        /// Количество цепочек в таблице, то есть ее вместимость
        /// </summary>
        public int Capacity => table.Length;

        /// <summary>
        /// Количество элементов в таблице
        /// </summary>
        public int Count { get; private set; }

        /// <summary>
        /// Получение всех ключей в таблице
        /// </summary>
        public TKey[] Keys
        {
            get
            {
                TKey[] keys = new TKey[Count];
                int index = 0;
                foreach (var chain in table)
                {
                    if (chain == null || chain.Count == 0)
                        continue;
                    foreach (var node in chain)
                    {
                        keys[index++] = node.Key;
                    }
                }
                return keys;
            }
        }

        /// <summary>
        /// Получение всех значений в таблице
        /// </summary>
        public TValue[] Values
        {
            get
            {
                TValue[] values = new TValue[Count];
                int index = 0;
                foreach (var chain in table)
                {
                    if (chain == null || chain.Count == 0)
                        continue;
                    foreach (var node in chain)
                    {
                        values[index++] = node.Data;
                    }
                }
                return values;
            }
        }

        /// <summary>
        /// Конструктор для таблицы заданной размерности
        /// </summary>
        /// <param name="capacity">Размерность таблицы</param>
        public MyHashTable(int capacity = 16)
        {
            ArgumentOutOfRangeException.ThrowIfLessThan(capacity, 1);
            table = new HashTableChain<TKey, TValue>[capacity];
            Count = 0;
        }

        /// <summary>
        /// Функция для получения индекса для ключа
        /// </summary>
        /// <param name="key">Ключ индекс которого ищется</param>
        /// <returns>Индекс в таблице</returns>
        private int GetIndex(TKey key) => Math.Abs(key.GetHashCode()) % Capacity;

        /// <summary>
        /// Очистка таблицы
        /// </summary>
        public void Clear()
        {
            Count = 0;
            for (int i = 0; i < Capacity; i++)
            {
                table[i]?.Clear();
            }
        }

        /// <summary>
        /// Добавление пары ключ-значение в таблицу, ключ уникальныйЫ
        /// </summary>
        /// <param name="key">Ключ для добавления</param>
        /// <param name="element">Элемент для добавления</param>
        /// <returns>Логическое, true если успешно, иначе false</returns>
        public bool Add(TKey key, TValue element)
        {
            int index = GetIndex(key);
            table[index] ??= [];
            try
            {
                table[index]!.Add(key, element);
                Count += 1;
                return true;
            }
            catch (ArgumentException)
            {
                return false;
            }
        }

        /// <summary>
        /// Поиск ключа в таблице
        /// </summary>
        /// <param name="key">Ключ для поиска</param>
        /// <returns>true если найден, иначе false</returns>
        public bool Contains(TKey key)
        {
            var chain = table[GetIndex(key)];
            if (chain == null)
                return false;
            return chain.Contains(key);
        }

        /// <summary>
        /// Удаление из таблицы по ключу
        /// </summary>
        /// <param name="key">Ключ который нужно удалить</param>
        /// <returns>true если успешно удалено, иначе false</returns>
        public bool Remove(TKey key)
        {
            try
            {
                table[GetIndex(key)].Remove(key);
                Count -= 1;
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Индексатор для получения значения по ключу
        /// </summary>
        /// <param name="key">Ключ для получения значения</param>
        /// <returns>Значение по данному ключу</returns>
        /// <exception cref="KeyNotFoundException">Указанный ключ не найден</exception>
        public TValue this[TKey key]
        {
            get
            {
                int index = GetIndex(key);

                if (table[index] == null)
                    throw new KeyNotFoundException();
                try
                {
                    return table[index][key];
                }
                catch
                {
                    throw new KeyNotFoundException();
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        /// <summary>
        /// Нумератор для перебора цепочек в таблице
        /// </summary>
        /// <returns>Нумератор из цепочей в ячейках</returns>
        public IEnumerator<HashTableChain<TKey, TValue>?> GetEnumerator()
        {
            foreach (var chain in table)
            {
                yield return chain;
            }
            yield break;
        }
    }
}
