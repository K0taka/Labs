namespace lab
{
    public class Node<T>: IDisposable where T: IDisposable
    {
        /// <summary>
        /// Автосвойство для хранения объекта; null не предусмотрен
        /// </summary>
        public T Data { get; set; }

        /// <summary>
        /// Ссылка на следующий элементъ
        /// </summary>
        public Node<T>? Next { get; set; }

        /// <summary>
        /// Ссылка на предыдущий элемент
        /// </summary>
        public Node<T>? Previous { get; set; }

        /// <summary>
        /// Конструктор для создания узла с информацией и ссылками на следующий и предыдущий элементы
        /// </summary>
        /// <param name="data">Объект в узле</param>
        /// <param name="next">Ссылка на следующий</param>
        /// <param name="previous">Ссылка на предыдущий</param>
        public Node(T data, Node<T>? next = null, Node<T>? previous = null)
        {
            Data = data;
            Next = next;
            Previous = previous;
        }

        /// <summary>
        /// Информация о содержащимся объекте в строковом представлении
        /// </summary>
        /// <returns>Строка с информацией об объекте</returns>
        public override string? ToString()
        {
            return Data != null ? Data.ToString() : "";
        }

        /// <summary>
        /// Уничтожение узла
        /// </summary>
        public void Dispose()
        {
            Previous = null;
            Next = null;
            Data.Dispose();
            GC.SuppressFinalize(this);
        }

        ~Node() 
        {
            Dispose();
        }
    }
}
