using AVLTree;
using System.Collections;

namespace lab
{
    public class PBT<T>: IEnumerable<TreeNode<T>> where T: notnull
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

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<TreeNode<T>> GetEnumerator() => InOrderEnumerator(Root).GetEnumerator();

        private IEnumerable<TreeNode<T>> InOrderEnumerator(TreeNode<T> node)
        {
            if (node != null)
            {
                foreach (var item in InOrderEnumerator(node.Left))
                    yield return item;
                yield return node;
                foreach(var item in InOrderEnumerator(node.Right))
                    yield return item;
            }
        }
    }
}
