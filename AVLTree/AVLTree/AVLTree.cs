namespace AVLTree
{
    public class AVLTree<TKey, TValue>: ICloneable, IEnumerable<KeyValuePair<TKey, TValue>>, IDictionary<TKey,TValue>,
        ICollection<KeyValuePair<TKey, TValue>>  where TKey: notnull, IComparable<TKey>
    {

    }
}
