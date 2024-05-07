namespace lab
{
    public class PBT<T> where T: notnull
    {
        public TreeNode<T>? Root { get; private set; }

        public int Capacity { get; private set; }

        public PBT(T[] values)
        {
            if (values == null || values.Length == 0)
                throw new ArgumentException(message: "Empty collection", paramName: nameof(values));
            Root = Add(values, values.Length);
        }

        private TreeNode<T>? Add(T[] values, int count)
        {
            if (count == 0)
                return null;
            TreeNode<T> node = new(values[Capacity++]);
            node.Left = Add(values, count / 2);
            node.Right = Add(values, count - count/2 - 1);
            return node;
        }
    }
}
