using System.Collections;

namespace lab
{
    public class List<T>: IList<T>, ICloneable, IEnumerable<T>, IDisposable where T: ICloneable, IDisposable
    {
        /// <summary>
        /// Текущая длина списка
        /// </summary>
        private int len;

        /// <summary>
        /// Список доступен только для чтения
        /// </summary>
        private readonly bool readOnly = false;

        /// <summary>
        /// Возвращает количество элементов в списке
        /// </summary>
        public int Count { get => len; }

        /// <summary>
        /// Возвращает логическое, true если список доступен только для чтения
        /// </summary>
        public bool IsReadOnly { get => readOnly; }

        /// <summary>
        /// Автосвойство головы списка
        /// </summary>
        public ListNode<T>? Head { get; set; }

        /// <summary>
        /// Автосвойство последнего элемента списка
        /// </summary>
        public ListNode<T>? Last { get; set; }

        /// <summary>
        /// Инициализирует пустой список
        /// </summary>
        public List() { len = 0; }

        /// <summary>
        /// Конструктор для списка из массива
        /// </summary>
        /// <param name="array">Массив элементов</param>
        /// <exception cref="ArgumentNullException">Массив не инициализирован</exception>
        public List(T[] array, bool readOnly = false)
        {
            ArgumentNullException.ThrowIfNull(array);
            len = 0;
            foreach (var item in array)
            {
                Add(item);
                len += 1;
            }
            this.readOnly = readOnly;
        }

        /// <summary>
        /// Копировать элементы
        /// </summary>
        /// <param name="array">Массив в который производится копирование</param>
        /// <param name="arrayIndex">Индекс начала</param>
        /// <exception cref="ArgumentException">Массив недостаточной размерности</exception>
        public void CopyTo(T[] array, int arrayIndex)
        {
            ArgumentNullException.ThrowIfNull(array);
            ArgumentOutOfRangeException.ThrowIfNegative(arrayIndex);
            if (Count > array.Length - arrayIndex)
                throw new ArgumentException("Not enought space");
            for (int index = arrayIndex; index < array.Length; index++)
            {
                array[index] = (T)this[index].Clone();
            }
        }

        /// <summary>
        /// Добавляет элемент в конец списка
        /// </summary>
        /// <param name="element">элемент для добавления</param>
        /// <exception cref="MemberAccessException">Невозможно поместить, список только для чтения</exception>
        public void Add(T element)
        {
            if (readOnly)
                throw new MemberAccessException();
            if (Last == null)
            {
                Head = new ListNode<T>((T)element.Clone());
                Last = Head;
                len += 1;
                GC.ReRegisterForFinalize(this);
                return;
            }

            //Создаем элемент в конце со ссылкой на предыдущий, точно не null - этот вариант отработал выше
            Last = new((T)element.Clone(), previous: Last);
            //Устанавливаем ссылку с предыдущего на текущий Last, Previous точно не null, см выше
            Last.Previous.Next = Last;
            len += 1;
        }

        /// <summary>
        /// Добавляет элемент в начало списка
        /// </summary>
        /// <param name="element">Элемент для добавления</param>
        /// <exception cref="MemberAccessException">Невозможно поместить, список только для чтения</exception>
        public void AddFirst(T element)
        {
            if (readOnly)
                throw new MemberAccessException();
            if (Head == null)
            {
                Last = new ListNode<T>((T)element.Clone());
                Head = Last;
                len += 1;
                GC.ReRegisterForFinalize(this);
                return;
            }
            //Создаем новый Head со ссылкой (точно не null) на предыдущую голову
            Head = new ListNode<T>((T)element.Clone(), next: Head);
            //У предыдущей головы добавим ссылку назад на новую голову
            Head.Next.Previous = Head;
            len += 1;
        }

        /// <summary>
        /// Поместить на эказанный индекс элемент
        /// </summary>
        /// <param name="index">Индекс для прмещения объекта</param>
        /// <param name="element">Элемент который нужно поместить</param>
        /// <exception cref="MemberAccessException">Невозможно поместить, список только для чтения</exception>
        /// <exception cref="IndexOutOfRangeException">Указан индекс за пределами массива</exception>
        public void Insert(int index, T element)
        {
            if (readOnly)
                throw new MemberAccessException();
            if (index == Count)
            {
                Add(element); //try не требуется, исключение от Add уже проверено выше
                return;
            }
            else if (index == 0)
            {
                AddFirst(element);
                return;
            }
            ListNode<T> wkNode = NodeAt(index) ?? throw new IndexOutOfRangeException();
            //предыдущий элемент точно есть, index > 0
            ListNode<T> add = new((T)element.Clone(), next:  wkNode, previous: wkNode.Previous);
            wkNode.Previous.Next = add;
            wkNode.Previous = add;
            len += 1;
        }

        /// <summary>
        /// Удаляет первое вхождение указанного элемента
        /// </summary>
        /// <param name="element">Элемент, который необходимо удалить</param>
        /// <returns>Логическое об успешности удаления, true если выполнено</returns>
        /// <exception cref="MemberAccessException">Невозможно удалить, список только для чтения</exception>
        public bool Remove(T element)
        {
            if (readOnly)
                throw new MemberAccessException();
            ListNode<T>? curr = SearchFirstNodeWithData(element);
            

            if (curr != null)
            {
                if (curr.Previous != null)
                    curr.Previous.Next = curr.Next;
                else
                    Head = curr.Next;

                if (curr.Next != null)
                    curr.Next.Previous = curr.Previous;
                else
                    Last = curr.Previous;

                len -= 1;
                curr.Dispose();
                return true;
            }
            return false;
        }

        /// <summary>
        /// Удаляет элемент на указанной позиции
        /// </summary>
        /// <param name="index">Индекс удаляемого элемента</param>
        /// <exception cref="MemberAccessException">Операция невозможно, список в режиме только для чтения</exception>
        /// <exception cref="NullReferenceException">Список пуст</exception>
        /// <exception cref="IndexOutOfRangeException">Индекс за пределами списка</exception>
        public void RemoveAt(int index)
        {
            if (readOnly)
                throw new MemberAccessException();
            if (Count == 0)
                throw new NullReferenceException();
            ListNode<T> removing = NodeAt(index) ?? throw new IndexOutOfRangeException();
            if (index == 0)
            {
                Head = removing.Next;
                if (Head != null)
                    Head.Previous = null;
                else
                    Last = null;
                removing.Dispose();
                len--;
                return;
            }
            if (index == Count - 1)
            {
                Last = Last.Previous;
                Last.Next = null;
                removing.Dispose();
                len--;
                return;
            }
            removing.Previous.Next = removing.Next;
            removing.Next.Previous = removing.Previous;
            removing.Dispose();
            len--;
        }

        /// <summary>
        /// Очистить список
        /// </summary>
        /// <exception cref="MemberAccessException">Список доступен только для чтения</exception>
        public void Clear()
        {
            if (readOnly)
                throw new MemberAccessException();
            Dispose();
        }

        /// <summary>
        /// Индекс первого вхождения указанного элемента в список
        /// </summary>
        /// <param name="element">Элемент, индекс которого ищется</param>
        /// <returns>Число - индекс; -1 в случае, если не найден</returns>
        public int IndexOf(T element)
        {
            if (Head == null)
                return -1;
            int index = 0;
            ListNode<T> curr = Head;
            while (curr.Next != null && !curr.Data.Equals(element)) 
            {
                curr = curr.Next;
                index++;
            }
            if (curr.Data.Equals(element))
                return index;
            return -1;
        }

        /// <summary>
        /// Список содержит указанный элемент
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public bool Contains(T element)
        {
            return !(IndexOf(element) < 0);
        }

        /// <summary>
        /// Индексатор для списка
        /// </summary>
        /// <param name="index">Индекс необходимого элемента</param>
        /// <returns>Элемент на указанной позиции</returns>
        /// <exception cref="IndexOutOfRangeException">Указанный индекс за границами списка</exception>
        public T this[int index]
        {
            get
            {
                ListNode<T> curr = NodeAt(index) ?? throw new IndexOutOfRangeException();
                return curr.Data;
            }

            set
            {
                if (readOnly)
                    throw new MemberAccessException();
                ListNode<T> curr = NodeAt(index) ?? throw new IndexOutOfRangeException();
                curr.Data = value;
            }
        }

        /// <summary>
        /// Возвращает узел по его индексу
        /// </summary>
        /// <param name="index">Индекс необходимого узла</param>
        /// <returns>Найденный узел</returns>
        /// <exception cref="IndexOutOfRangeException">Индекс узла за границами списка</exception>
        private ListNode<T>? NodeAt(int index)
        {
            if (index < 0 || index >= Count)
                return null;
            ListNode<T> curr = Head;
            for (int currIndex = 1; currIndex <= index; currIndex++)
                curr = curr.Next;
            return curr;
        }

        /// <summary>
        /// Энумератор для перебора элементов по порядку
        /// </summary>
        /// <returns>Текущий элемент итерации</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// Энумератор для перебора элементов по порядку
        /// </summary>
        /// <returns>Текущий элемент итерации</returns>
        public IEnumerator<T> GetEnumerator()
        {
            ListNode<T>? curr = Head;
            while (curr != null)
            {
                yield return curr.Data;
                curr = curr.Next;
            }
            yield break;
        }

        /// <summary>
        /// Глубокая копия списка
        /// </summary>
        /// <returns>Список - глубокая копия текущего</returns>
        public object Clone()
        {
            List<T> clone = [];
            foreach(T element in this)
            {
                //конструкцию try можно опустить, только что созданный список имеет readonly = false
                clone.Add(element); //так как при добавлении используется Clone, то не нужно его делать дважды
            }
            return clone;
        }

        /// <summary>
        /// Удаляет весь список
        /// </summary>
        public void Dispose()
        {
            ListNode<T>? curr = Head;
            ListNode<T>? next;
            while (curr != null)
            {
                next = curr.Next;
                curr.Dispose();
                curr = next;
            }
            Head = null;
            Last = null;
            len = 0;
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Добавить элемент после первого вхождения указанного
        /// </summary>
        /// <param name="afterElement">Элемент после первого вхождения которого будет добавлен новый</param>
        /// <param name="addElement">Значение нового элемента</param>
        /// <returns>Возвращает bool об успешности операции, true если выполнена</returns>
        /// <exception cref="MemberAccessException">Невозможно поместить, список только для чтения</exception>
        public bool AddAfter(T afterElement, T addElement)
        {
            if (readOnly)
                throw new MemberAccessException();
            ListNode<T>? curr = SearchFirstNodeWithData(afterElement);

            if (curr != null)
            {
                curr.Next = new((T)addElement.Clone(), next: curr.Next, previous: curr);
                len += 1;

                if (curr.Next.Next != null)
                    curr.Next.Next.Previous = curr.Next;

                if (Last == curr)
                    Last = curr.Next;

                return true;
            }
            return false;
        }

        /// <summary>
        /// Удаляет все значения перед первым вхождением указанного, если он имеется в списке
        /// </summary>
        /// <param name="element">Элемент до первого вхождения которого производится удаление</param>
        /// <returns>Логическое об успешности операции, true если удалено</returns>
        /// <exception cref="MemberAccessException">Невозможно удалить, список только для чтения</exception>
        public bool DeleteAllBefore(T element)
        {
            if (readOnly)
                throw new MemberAccessException();
            ListNode<T>? found = SearchFirstNodeWithData(element);
            if (found == null)
                return false;
            Head = found;
            ListNode<T>? deleteTo = Head.Previous;
            Head.Previous = null;
            while (deleteTo != null)
            {
                deleteTo.Next = null;
                deleteTo = deleteTo.Previous;
                len -= 1;
            }
            return true;
        }

        /// <summary>
        /// Поиск первого узла в списке с указанным значением data
        /// </summary>
        /// <param name="data">Значение для поиска</param>
        /// <returns>Найденный узел или null</returns>
        private ListNode<T>? SearchFirstNodeWithData(T data)
        {
            if (Head == null)
                return null;
            ListNode<T> result = Head;
            while (result.Next != null && !result.Data.Equals(data))
            { 
                result = result.Next;
            }
            if (result.Data.Equals(data))
                return result;
            return null;
        }

        ~List()
        {
            Dispose();
        }
    }
}
