namespace lab
{
    public class TreeNode<T> where T: notnull
    {
        public T Data { get; set; }

        public TreeNode<T>? Left { get; set; }

        public TreeNode<T>? Right { get; set; }


        public TreeNode(T data, TreeNode<T>? left = null, TreeNode<T>? right = null)
        {
            Data = data;
            this.Left = left;
            this.Right = right;
        }
    }
}
