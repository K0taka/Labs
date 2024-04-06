using System.Collections;
using System.Xml;

namespace lab
{
    public class List<T>: IList<T>, ICloneable, IEnumerable<T>, IDisposable where T: ICloneable, IDisposable
    {
        /// <summary>
        /// Текущая длина списка
        /// </summary>
        private int len;

        /// <summary>
        /// Возвращает количество элементов в списке
        /// </summary>
        public int Count { get { return len; } }

        /// <summary>
        /// Автосвойство головы списка
        /// </summary>
        public ListNode<T>? Head { get; set; }

        public ListNode<T>? Last { get; set; }

        /// <summary>
        /// Инициализирует пустой список
        /// </summary>
        public List() { len = 0; }

        /// <summary>
        /// Добавляет элемент в конец списка
        /// </summary>
        /// <param name="element">элемент для добавления</param>
        public void Add(T element)
        {
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

        public void AddFirst(T element)
        {
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
        /// Удаляет первое вхождение указанного элемента
        /// </summary>
        /// <param name="element">Элемент, который необходимо удалить</param>
        /// <returns>Логическое об успешности удаления, true если выполнено</returns>
        public bool Remove(T element)
        {
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
        /// Индексатор для списка
        /// </summary>
        /// <param name="index">Индекс необходимого элемента</param>
        /// <returns>Элемент на указанной позиции</returns>
        /// <exception cref="IndexOutOfRangeException">Указанный индекс за границами списка</exception>
        public T this[int index]
        {
            get
            {
                ListNode<T>? curr = NodeAt(index) ?? throw new NullReferenceException();
                return curr.Data;
            }

            set
            {
                ListNode<T>? curr = NodeAt(index) ?? throw new NullReferenceException();
                curr.Data = value;
            }
        }

        private ListNode<T>? NodeAt(int index)
        {
            if (index < 0 || index >= Count)
                throw new IndexOutOfRangeException();
            ListNode<T>? curr = Head;
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
            List<T> clone = new();
            foreach(T element in this)
            {
                clone.Add( (T)element.Clone() );
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
        public bool AddAfter(T afterElement, T addElement)
        {
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
        public bool DeleteAllBefore(T element)
        {
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
