using System.Collections;
using System.Diagnostics.CodeAnalysis;

namespace lab
{
    public class PBT<T>: IEnumerable<TreeNode<T>> where T: notnull
    {
        /// <summary>
        /// Автосвойство для корня дерева
        /// </summary>
        public TreeNode<T>? Root { get; private set; }

        /// <summary>
        /// Автосвойтсво для количества элементов дерева
        /// </summary>
        public int Capacity { get; private set; }

        /// <summary>
        /// Конструктор для ИСД
        /// </summary>
        /// <param name="values">Массив для элементов, из которых будет построено дерево</param>
        /// <exception cref="ArgumentException">Массив был пуст</exception>
        public PBT(T[] values)
        {
            if (values == null || values.Length == 0)
                throw new ArgumentException(message: "Empty collection", paramName: nameof(values));
            Root = Add(values, values.Length);
        }

        /// <summary>
        /// Добавление в ИСД из массива
        /// </summary>
        /// <param name="values">Массив значение</param>
        /// <param name="count">количество добавляемых элементов с этой стороны</param>
        /// <returns>Добавленный узел</returns>
        private TreeNode<T>? Add(T[] values, int count)
        {
            if (count == 0)
                return null;
            TreeNode<T> node = new(values[Capacity++]);
            node.Left = Add(values, count / 2);
            node.Right = Add(values, count - count/2 - 1);
            return node;
        }

        [ExcludeFromCodeCoverage]
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        /// <summary>
        /// Нумератор для дерева в порядке добавления
        /// </summary>
        /// <returns></returns>
        public IEnumerator<TreeNode<T>> GetEnumerator() => InAddOrder(Root).GetEnumerator();

        /// <summary>
        /// Именованый нумератор, рекурсивный. В порядке добавления
        /// </summary>
        /// <param name="node">Текущий узел</param>
        /// <returns>Созданный нумератор</returns>
        private IEnumerable<TreeNode<T>> InAddOrder(TreeNode<T> node)
        {
            if (node != null)
            {
                yield return node;
                foreach (var item in InAddOrder(node.Left))
                    yield return item;
                foreach(var item in InAddOrder(node.Right))
                    yield return item;
            }
        }
    }
}
