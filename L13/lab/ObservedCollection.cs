using AVLTree;

namespace lab
{
    public delegate void CollectionHandler(object source, CollectionHandlerEventArgs args);

    public class ObservedCollection<TKey, TValue>: AVL<TKey, TValue> where TKey: notnull, IComparable<TKey> where TValue : notnull
    {

        private event CollectionHandler? CollectionCountChanged;
        private event CollectionHandler? CollectionReferenceChanged;
        private event CollectionHandler? CollectionThrowedError;

        public ObservedCollection(): base() { }

        public void RegisterCountChangedHandler(CollectionHandler countHandler) => CollectionCountChanged += countHandler;
        public void UnregisterCountChangedHandler(CollectionHandler countHandler) => CollectionCountChanged -= countHandler;
        public void RegisterReferenceChangedHandler(CollectionHandler refHandler) => CollectionReferenceChanged += refHandler;
        public void UnregisterReferenceChangedHandler(CollectionHandler refHandler) => CollectionReferenceChanged -= refHandler;
        public void RegisterThrowedErrorHandler(CollectionHandler errorHandler) => CollectionThrowedError += errorHandler;
        public void UnregisterThrowedErrorHandler(CollectionHandler errorHandler) => CollectionThrowedError -= errorHandler;


        public new void Add(TKey key, TValue value)
        {
            try
            {
                Add(new KeyValuePair<TKey, TValue>(key, value));
            }
            catch
            {
                throw;
            }
        }

        public new void Add(KeyValuePair<TKey, TValue> pair)
        {
            try
            {
                base.Add(pair);
                CollectionCountChanged?.Invoke(this, new("Successfully inserted in AVL-tree", pair));
            }
            catch (ArgumentNullException)
            {
                CollectionThrowedError?.Invoke(this, new("Was not inserted in AVL-tree, the error is key was null", pair));
                throw;
            }
            catch (ArgumentException)
            {
                CollectionThrowedError?.Invoke(this, new("Was not inserted in AVL-tree, the error is key already exists", pair));
                throw;
            }
            catch (NotSupportedException)
            {
                CollectionThrowedError?.Invoke(this, new("Was not inserted in AVL-tree, the error is the collection was ReadOnly", pair));
                throw;
            }
        }

        public new void Clear()
        {
            base.Clear();
            CollectionCountChanged?.Invoke(this, new CollectionHandlerEventArgs("Collection was cleared", null));
        }

        public new bool Remove(KeyValuePair<TKey, TValue> pair)
        {
            try
            {
                if (base.Remove(pair))
                {
                    CollectionCountChanged?.Invoke(this, new("The pair was removed from collection", pair));
                    return true;
                }
                else
                    CollectionThrowedError?.Invoke(this, new("The pair wasn't removed from collection, collection didn't contain the pair", pair));
            }
            catch (NotSupportedException)
            {
                CollectionThrowedError?.Invoke(this, new("The pair wasn't removed from collection, collection was ReadOnly", pair));
            }
            catch (ArgumentNullException)
            {
                CollectionThrowedError?.Invoke(this, new("The pair wasn't removed from collection, the key in the pair was null", pair));
            }
            return false;
        }

        public new bool Remove(TKey key)
        {
            try
            {
                if (base.Remove(key))
                {
                    CollectionCountChanged?.Invoke(this, new("The pair with key was removed from collection", key));
                    return true;
                }
                else
                    CollectionThrowedError?.Invoke(this, new("The pair with key wasn't removed from collection, collection didn't contain the key", key));
            }
            catch (NotSupportedException)
            {
                CollectionThrowedError?.Invoke(this, new("The pair with key wasn't removed from collection, collection is ReadOnly", key));
            }
            catch (ArgumentNullException)
            {
                CollectionThrowedError?.Invoke(this, new("The pair with key wasn't removed from collection, key was null", key));
            }
            return false;
        }

        public new TValue this[TKey key]
        {
            get
            {
                try
                {
                    return base[key];
                }
                catch(ArgumentNullException)
                {
                    CollectionThrowedError?.Invoke(this, new("Error while getting the value by key because key was null", key));
                    throw;
                }
                catch(KeyNotFoundException)
                {
                    CollectionThrowedError?.Invoke(this, new("Error while getting the value by key because key was not found", key));
                    throw;
                }
            }

            set
            {
                try
                {
                    int wasCount = Count;
                    base[key] = value;
                    if (wasCount == Count)
                        CollectionReferenceChanged?.Invoke(this, new("The value with the key was changed", key));
                    else
                        CollectionCountChanged?.Invoke(this, new("The new node with the key was inserted", key));
                }
                catch(NotSupportedException)
                {
                    CollectionThrowedError?.Invoke(this, new("Error while updating the value because collection was ReadOnly", key));
                    throw;
                }
                catch (KeyNotFoundException)
                {
                    CollectionThrowedError?.Invoke(this, new("Error while updating the value because the key was null", key));
                    throw;
                }
            }
        }

        public new bool TryGetValue(TKey key, out TValue value)
        {
            try
            {
                value = this[key];
                return true;
            }
            catch
            {
                value = default;
                return false;
            }
        }
    }
}
