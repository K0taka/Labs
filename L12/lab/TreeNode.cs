namespace lab
{
    public class TreeNode<T> where T: notnull
    {
        /// <summary>
        /// Автосвойство для значения дерева
        /// </summary>
        public T Data { get; set; }

        /// <summary>
        /// Автосвойство для узла слева
        /// </summary>
        public TreeNode<T>? Left { get; set; }

        /// <summary>
        /// Автосвойство для узла справа
        /// </summary>
        public TreeNode<T>? Right { get; set; }

        /// <summary>
        /// Конструктор для узла дерева
        /// </summary>
        /// <param name="data">элемент дерева</param>
        /// <param name="left">левый узел</param>
        /// <param name="right">правый узел</param>
        public TreeNode(T data, TreeNode<T>? left = null, TreeNode<T>? right = null)
        {
            Data = data;
            this.Left = left;
            this.Right = right;
        }
    }
}
