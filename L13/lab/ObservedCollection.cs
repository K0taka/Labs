using AVLTree;

namespace lab
{
    public delegate void CollectionHandler(object source, CollectionHandlerEventArgs args);

    public class ObservedCollection<TKey, TValue>: AVL<TKey, TValue> where TKey: notnull, IComparable<TKey> where TValue : notnull
    {
        public string Name { get; private set; }

        private event CollectionHandler? CollectionCountChanged;
        private event CollectionHandler? CollectionReferenceChanged;
        private event CollectionHandler? CollectionThrowedError;

        public ObservedCollection(): base() { Name = "AVL tree"; }
        public ObservedCollection(string name):base() { Name = name; }

        public ObservedCollection(AVL<TKey, TValue> collection) : base()
        {
            if (collection is ObservedCollection<TKey, TValue> observed)
                Name = observed.Name;
            else
                Name = "AVL tree";


            if (typeof(TKey).GetInterfaces().Contains(typeof(ICloneable)))
            {
                TKey[] keys = (TKey[])collection.Keys;

                foreach (var item in collection.InWideEnumerator())
                {
                    int index = Array.IndexOf(keys, item.Key);
                    Add(keys[index], item.Value is ICloneable value ? (TValue)value.Clone() : item.Value);
                }
            }
            else
            {
                foreach (var item in collection.InWideEnumerator())
                {
                    Add(item.Key, item.Value is ICloneable value ? (TValue)value.Clone() : item.Value);
                }
            }
        }

        public void RegisterCountChangedHandler(CollectionHandler countHandler) => CollectionCountChanged += countHandler;
        public void UnregisterCountChangedHandler(CollectionHandler countHandler) => CollectionCountChanged -= countHandler;
        public void RegisterReferenceChangedHandler(CollectionHandler refHandler) => CollectionReferenceChanged += refHandler;
        public void UnregisterReferenceChangedHandler(CollectionHandler refHandler) => CollectionReferenceChanged -= refHandler;
        public void RegisterThrowedErrorHandler(CollectionHandler errorHandler) => CollectionThrowedError += errorHandler;
        public void UnregisterThrowedErrorHandler(CollectionHandler errorHandler) => CollectionThrowedError -= errorHandler;

        private void OnCollectionCountChanged(object source, CollectionHandlerEventArgs args) => CollectionCountChanged?.Invoke(source, args);
        private void OnCollectionReferenceChanged(object source, CollectionHandlerEventArgs args) => CollectionReferenceChanged?.Invoke(source, args);
        private void OnCollectionThrowedError(object source, CollectionHandlerEventArgs args) => CollectionThrowedError?.Invoke(source, args);


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
                OnCollectionCountChanged(this, new(Name,"Successfully inserted in AVL-tree", pair));
            }
            catch (ArgumentNullException)
            {
                OnCollectionThrowedError(this, new(Name, "Was not inserted in AVL-tree, the error is key was null", pair));
                throw;
            }
            catch (ArgumentException)
            {
                OnCollectionThrowedError(this, new(Name, "Was not inserted in AVL-tree, the error is key already exists", pair));
                throw;
            }
            catch (NotSupportedException)
            {
                OnCollectionThrowedError(this, new(Name, "Was not inserted in AVL-tree, the error is the collection was ReadOnly", pair));
                throw;
            }
        }

        public new void Clear()
        {
            base.Clear();
            OnCollectionCountChanged(this, new CollectionHandlerEventArgs(Name, "Collection was cleared", null));
        }

        public new bool Remove(KeyValuePair<TKey, TValue> pair)
        {
            try
            {
                if (base.Remove(pair))
                {
                    OnCollectionCountChanged(this, new(Name, "The pair was removed from collection", pair));
                    return true;
                }
                else
                    OnCollectionThrowedError(this, new(Name, "The pair wasn't removed from collection, collection didn't contain the pair", pair));
            }
            catch (NotSupportedException)
            {
                OnCollectionThrowedError(this, new(Name, "The pair wasn't removed from collection, collection was ReadOnly", pair));
            }
            catch (ArgumentNullException)
            {
                OnCollectionThrowedError(this, new(Name, "The pair wasn't removed from collection, the key in the pair was null", pair));
            }
            return false;
        }

        public new bool Remove(TKey key)
        {
            try
            {
                if (base.Remove(key))
                {
                    OnCollectionCountChanged(this, new(Name, "The pair with key was removed from collection", key));
                    return true;
                }
                else
                    OnCollectionThrowedError(this, new(Name, "The pair with key wasn't removed from collection, collection didn't contain the key", key));
            }
            catch (NotSupportedException)
            {
                OnCollectionThrowedError(this, new(Name, "The pair with key wasn't removed from collection, collection is ReadOnly", key));
            }
            catch (ArgumentNullException)
            {
                OnCollectionThrowedError(this, new(Name, "The pair with key wasn't removed from collection, key was null", key));
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
                    OnCollectionThrowedError(this, new(Name, "Error while getting the value by key because key was null", key));
                    throw;
                }
                catch(KeyNotFoundException)
                {
                    OnCollectionThrowedError(this, new(Name, "Error while getting the value by key because key was not found", key));
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
                        OnCollectionReferenceChanged(this, new(Name, "The value with the key was changed", key));
                    else
                        OnCollectionCountChanged(this, new(Name, "The new node with the key was inserted", key));
                }
                catch(NotSupportedException)
                {
                    OnCollectionThrowedError(this, new(Name, "Error while updating the value because collection was ReadOnly", key));
                    throw;
                }
                catch (KeyNotFoundException)
                {
                    OnCollectionThrowedError(this, new(Name, "Error while updating the value because the key was null", key));
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
