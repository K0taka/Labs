using System.Collections;

namespace lab
{
    public class HashTableChain<TKey, TValue> : IEnumerable<HashTableNode<TKey,TValue>> where TValue : ICloneable where TKey : notnull, ICloneable
    {
        /// <summary>
        /// Голова списка
        /// </summary>
        HashTableNode<TKey, TValue>? Head;

        /// <summary>
        /// Количество элементов в списке
        /// </summary>
        public int Count { get; private set; }

        /// <summary>
        /// Конструктор пустой цепочки
        /// </summary>
        public HashTableChain()
        {
            Head = null;
            Count = 0;
        }

        /// <summary>
        /// Содержание ключа в цепочке
        /// </summary>
        /// <param name="key">Ключ для проверки</param>
        /// <returns>true если содержится, иначе false</returns>
        public bool Contains(TKey key)
        {
            var curr = Head;
            while (curr != null)
            {
                if (curr.Key.Equals(key))
                    return true;
                curr = curr.Next;
            }
            return false;
        }

        /// <summary>
        /// Добавление в цепочку ключа и значения
        /// </summary>
        /// <param name="key">Ключ для добавления</param>
        /// <param name="value">Значение для добавления</param>
        /// <exception cref="ArgumentException">такой ключ уже добавлен</exception>
        public void Add(TKey key, TValue value)
        {
            if (Contains(key))
                throw new ArgumentException("This key is already exists", paramName: nameof(key));
            Head = new((TKey)key.Clone(), (TValue)value.Clone(), Head);
            Count += 1;
        }

        /// <summary>
        /// Очистка цепочки
        /// </summary>
        public void Clear() { Head = null; Count = 0; }

        /// <summary>
        /// Удаление элемента из цепочки по ключу
        /// </summary>
        /// <param name="key">Ключ для удаления</param>
        /// <exception cref="InvalidOperationException">Цепочка пуста</exception>
        /// <exception cref="KeyNotFoundException">Ключ не найден</exception>
        public void Remove(TKey key)
        {
            var curr = Head ?? throw new InvalidOperationException();
            if (curr.Key.Equals(key))
            {
                Head = curr.Next;
                Count--;
                return;
            }

            while (curr.Next != null && !curr.Next.Key.Equals(key))
                curr = curr.Next;
            if (curr.Next == null)
                throw new KeyNotFoundException();
            Count--;
            curr.Next = curr.Next.Next;
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        /// <summary>
        /// Полуение нумератора для цепочки
        /// </summary>
        /// <returns>Нумератор из узлов в цепочке</returns>
        public IEnumerator<HashTableNode<TKey, TValue>> GetEnumerator()
        {
            var curr = Head;
            while (curr != null)
            {
                yield return curr;
                curr = curr.Next;
            }
            yield break;
        }

        /// <summary>
        /// Индексатор для получения значения по ключу
        /// </summary>
        /// <param name="key">Ключ для получения значения</param>
        /// <returns>Значения у соотв. ключа</returns>
        /// <exception cref="KeyNotFoundException">Ключ не найден</exception>
        public TValue this[TKey key]
        {
            get
            {
                var curr = Head;
                while (curr != null)
                {
                    if (curr.Key.Equals(key))
                        return curr.Data;
                    curr = curr.Next;
                }
                throw new KeyNotFoundException();
            }
        }
    }
}
