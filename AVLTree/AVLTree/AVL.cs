using System.Collections;
using System.Diagnostics.CodeAnalysis;

namespace AVLTree
{
    public class AVL<TKey, TValue> : ICloneable, IEnumerable<KeyValuePair<TKey, TValue>>, IDictionary<TKey, TValue>,
        ICollection<KeyValuePair<TKey, TValue>> where TKey : notnull, IComparable<TKey> where TValue : notnull
    {
        private Node<TKey, TValue>? root;

        /// <summary>
        /// Свойство для получения корня дерева
        /// </summary>
        public Node<TKey, TValue>? Root => root;

        private int count;

        /// <summary>
        /// Свойство для получения количества элементов
        /// </summary>
        public int Count => count;

        /// <summary>
        /// Автосвойства для установки дерева в режим "только для чтения"
        /// </summary>
        public bool IsReadOnly { get; set; }

        /// <summary>
        /// Свойство для получения всех ключей в дереве
        /// </summary>
        public ICollection<TKey> Keys
        {
            get
            {
                TKey[] keys = new TKey[count];
                int index = 0;
                foreach(var item in this)
                {
                    keys[index++] = item.Key is ICloneable key? (TKey)key.Clone() : item.Key;
                }

                return keys;
            }
        }

        /// <summary>
        /// Свойство для получения всех значений в дереве
        /// </summary>
        public ICollection<TValue> Values
        {
            get
            {
                TValue[] values = new TValue[count];
                int index = 0;
                foreach (var item in this)
                {
                    values[index++] = item.Value is ICloneable value ? (TValue)value.Clone() : item.Value;
                }

                return values;
            }
        }

        /// <summary>
        /// Конструктор пустого дерева
        /// </summary>
        public AVL() { }

        /// <summary>
        /// Конструктор копирования из другого дерева
        /// </summary>
        /// <param name="tree">Дерево, из которого требуется копировать элементы</param>
        public AVL(AVL<TKey, TValue> tree)
        {
            if (typeof(TKey).GetInterfaces().Contains(typeof(ICloneable)))
            {
                TKey[] keys = (TKey[])tree.Keys;

                foreach(var item in tree.InWideEnumerator())
                {
                    int index = Array.IndexOf(keys, item.Key);
                    Add(keys[index], item.Value is ICloneable value ? (TValue)value.Clone() : item.Value);
                }   
            }
            else
            {
                foreach (var item in tree.InWideEnumerator())
                {
                    Add(item.Key, item.Value is ICloneable value ? (TValue)value.Clone() : item.Value);
                }
            }
        }

        /// <summary>
        /// Функция для добавления ключа и значения в дерево
        /// </summary>
        /// <param name="key">Ключ, должен быть уникальный</param>
        /// <param name="value">Значение соответствующее этому ключу</param>
        public void Add(TKey key, TValue value) { try { Add(new KeyValuePair<TKey, TValue>(key, value)); } catch { throw; } }

        /// <summary>
        /// Функция для добавления ключа и значения в дерево
        /// </summary>
        /// <param name="item">Пара ключ-значение для добавления</param>
        /// <exception cref="NotSupportedException">Дерево сейчас только для чтения</exception>
        /// <exception cref="ArgumentNullException">Ключ был null</exception>
        public void Add(KeyValuePair<TKey, TValue> item)
        {
            if (IsReadOnly)
                throw new NotSupportedException();
            if (item.Key == null)
                throw new ArgumentNullException("key");
            if (root == null) { root = new(item); count++; return; }
            try { Add(item, root); } catch { throw; }
        }

        /// <summary>
        /// Рекурсивная функция для добавления в дерево и последующей балансировки оного
        /// </summary>
        /// <param name="item">Пара ключ-значение для добавления</param>
        /// <param name="node">Текущий узел</param>
        /// <exception cref="ArgumentException">Такое ключ уже существует</exception>
        private void Add(KeyValuePair<TKey, TValue> item, Node<TKey, TValue> node)
        {
            switch(node.Entry.Key.CompareTo(item.Key))
            {
                case 0:
                    throw new ArgumentException("Entry with such key already exists");
                case 1:
                    if (node.Left == null)
                    {
                        node.Left = new(item);
                        count++;
                    }
                    else
                        Add(item, node.Left);
                    break;

                case -1:
                    if (node.Right == null)
                    {
                        node.Right = new(item);
                        count++;
                    }
                    else
                        Add(item, node.Right);
                    break;
            }
            UpdateHight(node);
            Balance(node);
        }

        /// <summary>
        /// Балансирует поддерево от указанного узла
        /// </summary>
        /// <param name="node"></param>
        private static void Balance(Node<TKey, TValue> node)
        {
            int balance = GetBalance(node);
            if (balance == 2)
            {
                if (GetBalance(node.Right) == -1)
                    RightRotate(node.Right);
                LeftRotate(node);
            }
            else if (balance == -2)
            {
                if (GetBalance(node.Left) == 1)
                    LeftRotate(node.Left);
                RightRotate(node);
            }
        }

        /// <summary>
        /// Осуществляет правый поворот дерева
        /// </summary>
        /// <param name="node">Узел относительно которого совершается поворот</param>
        private static void RightRotate(Node<TKey, TValue> node)
        {
            Swap(node, node.Left);
            Node<TKey, TValue>? rightNode = node.Right;
            node.Right = node.Left;
            node.Left = node.Right.Left;
            node.Right.Left = node.Right.Right;
            node.Right.Right = rightNode;
            UpdateHight(node.Right);
            UpdateHight(node);
        }

        /// <summary>
        /// Осуществляет левый поворот дерева
        /// </summary>
        /// <param name="node">Узел относительно которого совершается поворот</param>
        private static void LeftRotate(Node<TKey, TValue> node)
        {
            Swap(node, node.Right);
            Node<TKey, TValue>? leftNode = node.Left;
            node.Left = node.Right;
            node.Right = node.Left.Right;
            node.Left.Right = node.Left.Left;
            node.Left.Left = leftNode;
            UpdateHight(node.Left);
            UpdateHight(node);
        }

        /// <summary>
        /// Функция для обновления высоты узла
        /// </summary>
        /// <param name="node">Узел для обновления</param>
        private static void UpdateHight(Node<TKey, TValue> node)
        {
            node.Hight = Math.Max(node.Left == null ? -1 : node.Left.Hight, node.Right == null ? -1 : node.Right.Hight) + 1;
        }

        /// <summary>
        /// Функция для получения коэф. баланса у указанного узла
        /// </summary>
        /// <param name="node">Узел для получения баланса</param>
        /// <returns>Число - значение баланса</returns>
        private static int GetBalance(Node<TKey, TValue> node)
        {
            return (node.Right == null ? -1 : node.Right.Hight) - (node.Left == null ? -1 : node.Left.Hight);
        }
        
        /// <summary>
        /// Меняет записи двух узлов
        /// </summary>
        /// <param name="firstNode">Первый узел для замены</param>
        /// <param name="secondNode">Второй узел для замены</param>
        private static void Swap(Node<TKey, TValue> firstNode, Node<TKey, TValue> secondNode)
        {
            (firstNode.Entry, secondNode.Entry) = (secondNode.Entry, firstNode.Entry);
        }

        /// <summary>
        /// Функция для очистки дерева
        /// </summary>
        /// <exception cref="NotSupportedException"></exception>
        public void Clear()
        {
            if (IsReadOnly)
                throw new NotSupportedException();
            root = null;
            count = 0;
        }

        /// <summary>
        /// Функция для проверки наличия конкретной пары в дереве
        /// </summary>
        /// <param name="item">Пара, наличие которой проверяется</param>
        /// <returns>true если пара существует в дереве, иначе false</returns>
        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            var current = root;
            while (current != null)
            {
                if (current.Entry.Key.Equals(item.Key) && current.Entry.Value.Equals(item.Value))
                    return true;
                switch (current.Entry.Key.CompareTo(item.Key))
                {
                    case 0:
                        return false;
                    case 1:
                        current = current.Left;
                        break;
                    case -1:
                        current = current.Right;
                        break;
                }
            }
            return false;
        }

        [ExcludeFromCodeCoverage]
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// Получение энумератора для дерева
        /// </summary>
        /// <returns>Нумератор в отсортированном по удаленности от точки (0; 0)</returns>
        public IEnumerator<KeyValuePair<TKey,TValue>> GetEnumerator()
        {
            return InOrderEmumerator(root).GetEnumerator();
        }

        /// <summary>
        /// Функция формирующая рекурсивно отсортированный по удаленности (по возрастанию) дерево
        /// </summary>
        /// <param name="node">Текущий узел</param>
        /// <returns>Созданный энумератор</returns>
        private static IEnumerable<KeyValuePair<TKey, TValue>> InOrderEmumerator(Node<TKey, TValue> node)
        {
            if (node != null)
            {
                foreach (var item in InOrderEmumerator(node.Left))
                {
                    yield return item;
                }
                yield return node.Entry;
                foreach (var item in InOrderEmumerator(node.Right))
                { 
                    yield return item;
                }
            }
        }

        /// <summary>
        /// Обход дерева в ширину
        /// </summary>
        /// <returns>Созданый нумератор</returns>
        public IEnumerable<KeyValuePair<TKey,TValue>> InWideEnumerator()
        {
            if (root == null)
                yield break;
            else
            {
                Queue<Node<TKey, TValue>> nodes = new Queue<Node<TKey, TValue>>();
                nodes.Enqueue(root);
                while (nodes.Count > 0)
                {
                    var curr = nodes.Dequeue();
                    yield return curr.Entry;
                    if (curr.Left != null)
                        nodes.Enqueue(curr.Left);
                    if (curr.Right != null)
                        nodes.Enqueue(curr.Right);
                }
                yield break;
            }
        }

        /// <summary>
        /// Удаление пары ключ-значение из дерева
        /// </summary>
        /// <param name="item">Пара для удаления</param>
        /// <returns>true если удалено, иначе false</returns>
        /// <exception cref="NotSupportedException">Доступно только для чтения</exception>
        /// <exception cref="ArgumentNullException">Ключ был null</exception>
        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            if (IsReadOnly)
                throw new NotSupportedException();
            if (item.Key == null)
                throw new ArgumentNullException("key");
            if (root == null)
                return false;

            if (Contains(item))
            {
                bool isDeleted = false;
                root = Remove(root, item.Key, ref isDeleted);
                return isDeleted;
            }

            return false;
        }

        /// <summary>
        /// Удаление пары из дерева по ключу
        /// </summary>
        /// <param name="key">Ключ, по которому производится удаление</param>
        /// <returns>true если удалено, иначе false</returns>
        /// <exception cref="NotSupportedException">Доступно только для чтения</exception>
        /// <exception cref="ArgumentNullException">Ключ был null</exception>
        public bool Remove(TKey key)
        {
            if (IsReadOnly)
                throw new NotSupportedException();
            if (key == null)
                throw new ArgumentNullException(nameof(key));
            if (root == null)
                return false;
            bool isDeleted = false;
            root = Remove(root, key, ref isDeleted);
            return isDeleted;
        }

        /// <summary>
        /// Рекурсивная функция удаления по ключу из дерева
        /// </summary>
        /// <param name="node">Текущий узел</param>
        /// <param name="key">Ключ по которому производится удаление</param>
        /// <param name="isDeleted">Флаг об успешности удаления</param>
        /// <returns>Измененный узел</returns>
        private Node<TKey, TValue>? Remove(Node<TKey, TValue>? node, TKey key, ref bool isDeleted)
        {
            if (node == null)
                return null;
            
            switch(node.Entry.Key.CompareTo(key))
            {
                case 0:
                    if (node.Left == null || node.Right == null)
                    {
                        node = node.Right ?? node.Left;
                        count--;
                    }
                    else
                    {
                        var min = GetMin(node.Right);
                        node.Entry = min.Entry;
                        node.Right = Remove(node.Right, min.Entry.Key, ref isDeleted);
                    }
                    isDeleted = true;
                    break;
                case 1:
                    node.Left = Remove(node.Left, key, ref isDeleted);
                    break;
                case -1:
                    node.Right = Remove(node.Right, key, ref isDeleted);
                    break;
            }

            if (node == null)
                return null;

            UpdateHight(node);
            Balance(node);
            return node;
        }

        /// <summary>
        /// Функция для получения минимального узла в поддереве
        /// </summary>
        /// <param name="node">Узел начала поддерева</param>
        /// <returns>Минимальный узел</returns>
        private static Node<TKey, TValue> GetMin(Node<TKey, TValue> node)
        {
            return node.Left == null ? node : GetMin(node.Left);
        }

        /// <summary>
        /// Функция для копирования элементов в дереве в массив в порядке возрастания расстояния от начала
        /// </summary>
        /// <param name="array">массив для копирования</param>
        /// <param name="index">позиция начала</param>
        /// <exception cref="ArgumentNullException">список был null</exception>
        /// <exception cref="ArgumentOutOfRangeException">index был меньше 0</exception>
        /// <exception cref="ArgumentException">недостаточно места в массиве</exception>
        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int index)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));
            if (index < 0)
                throw new ArgumentOutOfRangeException(nameof(index));
            if (array.Length - index < count)
                throw new ArgumentException("Not enoght space in array");
            foreach(var item in this)
            {
                array[index++] = new(
                    item.Key is not ICloneable key ? item.Key : (TKey)key.Clone(),
                    item.Value is not ICloneable value ? item.Value : (TValue)value.Clone());
            }
        }

        /// <summary>
        /// Индексатор для получения значения по ключу
        /// </summary>
        /// <param name="key">Ключ по которому необходимо получить значение</param>
        /// <returns>Значение находящаемся по заданному ключу в дереве</returns>
        /// <exception cref="ArgumentNullException">Ключ был null</exception>
        /// <exception cref="KeyNotFoundException">Ключ не найден</exception>
        /// <exception cref="NotSupportedException">Доступно только для чтения</exception>
        public TValue this[TKey key]
        {
            get
            {
                if (key == null)
                    throw new ArgumentNullException(nameof(key));
                var current = root;
                while (current != null)
                {
                    switch(current.Entry.Key.CompareTo(key))
                    {
                        case 0:
                            return current.Entry.Value;
                        case 1:
                            current = current.Left;
                            break;
                        case -1:
                            current = current.Right;
                            break;
                    }
                }
                throw new KeyNotFoundException();
            }

            set
            {
                if (IsReadOnly)
                    throw new NotSupportedException();
                if (key == null)
                    throw new ArgumentNullException(nameof(key));
                var current = root;
                while (current != null)
                {
                    switch (current.Entry.Key.CompareTo(key))
                    {
                        case 0:
                            current.Entry = new KeyValuePair<TKey, TValue>(key, value);
                            return;
                        case 1:
                            current = current.Left;
                            break;
                        case -1:
                            current = current.Right;
                            break;
                    }
                }
                Add(key, value);
            }
        }

        /// <summary>
        /// Проверка на наличие ключа в дереве
        /// </summary>
        /// <param name="key">Ключ для проверки</param>
        /// <returns>true если ключ содержится, иначе false</returns>
        /// <exception cref="ArgumentNullException">ключ был null</exception>
        public bool ContainsKey(TKey key)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));
            var current = root;
            while (current != null)
            {
                switch (current.Entry.Key.CompareTo(key))
                {
                    case 0:
                        return true;
                    case 1:
                        current = current.Left;
                        break;
                    case -1:
                        current = current.Right;
                        break;
                }
            }
            return false;
        }

        /// <summary>
        /// Функция для получения значения через индексатор, при ошибке вернет false
        /// </summary>
        /// <param name="key">Ключ для получения значения</param>
        /// <param name="value">Значение по указанному ключу</param>
        /// <returns>true в случае успеха, иначе false</returns>
        public bool TryGetValue(TKey key, out TValue value)
        {
            try
            {
                value = this[key];
                return true;
            }
            catch
            {
                value = default;
                return false;
            }
        }

        /// <summary>
        /// Глубокое копирование дерева
        /// </summary>
        /// <returns>Новое дерево</returns>
        public object Clone()
        {
            return new AVL<TKey, TValue>(this);
        }

        /// <summary>
        /// Проверка деревьев на равенство
        /// </summary>
        /// <param name="obj">для сравнения</param>
        /// <returns>true если ранвы, иначе false</returns>
        public override bool Equals(object? obj)
        {
            if (obj is not AVL<TKey, TValue> tree)
                return false;
            if (tree.Count != count)
                return false;
            var thisArray = this.ToArray();
            var treeArray = tree.ToArray();
            for (var i = 0;  i < count; i++)
            {
                if (thisArray[i].Key.Equals(treeArray[i].Key) && thisArray[i].Value.Equals(treeArray[i].Value))
                    continue;
                return false;
            }
            return true;
        }

        /// <summary>
        /// Поверхностная копия объекта
        /// </summary>
        /// <returns>новый объект - поверхностная копия текущего дерева</returns>
        public object ShallowCopy()
        {
            return this.MemberwiseClone();
        }

        /// <summary>
        /// Проверка на наличие значения в дереве
        /// </summary>
        /// <param name="value">значение для поиска</param>
        /// <returns>true если найдено, иначе false</returns>
        public bool ContainsValue(TValue value)
        {
            foreach( var item in this)
            {
                if (item.Value.Equals(value))
                    return true;
            }
            return false;
        }
    }
}
