using System.Collections;

namespace lab
{
    public class List: ICloneable, IEnumerable, IDisposable
    {
        /// <summary>
        /// Текущая длина списка
        /// </summary>
        private uint len;

        /// <summary>
        /// Возвращает количество элементов в списке
        /// </summary>
        public uint Count { get { return len; } }

        /// <summary>
        /// Автосвойство головы списка
        /// </summary>
        public ListNode? Head { get; set; }

        public ListNode? Last { get; set; }

        /// <summary>
        /// Инициализирует пустой список
        /// </summary>
        public List() { len = 0; }

        /// <summary>
        /// Добавляет элемент в конец списка
        /// </summary>
        /// <param name="element">элемент для добавления</param>
        public void AddLast(ControlElement element)
        {
            if (Last == null)
            {
                Head = new ListNode(element);
                Last = Head;
                len += 1;
                GC.ReRegisterForFinalize(this);
                return;
            }

            Last = new(element, previous: Last);
            Last.Previous.Next = Last;
            len += 1;
        }

        public void AddFirst(ControlElement element)
        {
            if (Head == null)
            {
                Last = new ListNode(element);
                Head = Last;
                len += 1;
                GC.ReRegisterForFinalize(this);
                return;
            }

            Head = new ListNode(element, next: Head);
            Head.Next.Previous = Head;
            len += 1;
        }

        /// <summary>
        /// Удаляет первое вхождение указанного элемента
        /// </summary>
        /// <param name="element">Элемент, который необходимо удалить</param>
        /// <returns>Логическое об успешности удаления, true если выполнено</returns>
        public bool Remove(ControlElement element)
        {
            if (Head == null)
                return false;

            ListNode? curr = SearchFirstNodeWithData(element);
            

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
        public int FirstIndexOf(ControlElement element)
        {
            if (Head == null)
                return -1;
            int index = 0;
            ListNode curr = Head;
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
        public ControlElement this[int index]
        {
            get
            {
                if (index >= 0 && index < len)
                {
                    ListNode curr = Head;
                    for (int currIndex = 1; currIndex < len; currIndex++)
                    {
                        curr = curr.Next;
                    }
                    return curr.Data;
                }
                throw new IndexOutOfRangeException();
            }

            set
            {
                if (index >= 0 && index < len)
                {
                    ListNode curr = Head;
                    for (int currIndex = 1; currIndex < len; currIndex++)
                    {
                        curr = curr.Next;
                    }
                    curr.Data = value;
                }
                throw new IndexOutOfRangeException();
            }
        }

        /// <summary>
        /// Энумератор для перебора элементов по порядку
        /// </summary>
        /// <returns>Текущий элемент итерации</returns>
        public IEnumerator GetEnumerator()
        {
            ListNode? curr = Head;
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
            List clone = new();
            foreach(ControlElement element in this)
            {
                clone.AddLast((ControlElement)element.Clone());
            }
            return clone;
        }

        /// <summary>
        /// Удаляет весь список
        /// </summary>
        public void Dispose()
        {
            ListNode? curr = Head;
            ListNode? next;
            while (curr != null)
            {
                next = curr.Next;
                curr.Dispose();
                curr = next;
            }
            Head = null;
            len = 0;
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Добавить элемент после первого вхождения указанного
        /// </summary>
        /// <param name="afterElement">Элемент после первого вхождения которого будет добавлен новый</param>
        /// <param name="addElement">Значение нового элемента</param>
        /// <returns>Возвращает bool об успешности операции, true если выполнена</returns>
        public bool AddAfter(ControlElement afterElement, ControlElement addElement)
        {
            if (Head == null)
                return false;

            ListNode? curr = SearchFirstNodeWithData(afterElement);

            if (curr != null)
            {
                curr.Next = new(addElement, next: curr.Next, previous: curr);
                len += 1;

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
        public bool DeleteAllBefore(ControlElement element)
        {
            if (Head == null)
                return false;
            ListNode? found = SearchFirstNodeWithData(element);
            if (found == null)
                return false;
            Head = found;
            ListNode? deleteTo = Head.Previous;
            Head.Previous = null;
            while (deleteTo != null)
            {
                deleteTo.Next = null;
                deleteTo = deleteTo.Previous;
                len -= 1;
            }
            return true;
        }

        private ListNode? SearchFirstNodeWithData(ControlElement data)
        {
            if (Head == null)
                return null;
            ListNode result = Head;
            while (result.Next != null && !result.Data.Equals(data))
            { 
                result = Head.Next;
            }
            if (result.Data.Equals(data))
                return result;
            return null;
        }
    }
}
