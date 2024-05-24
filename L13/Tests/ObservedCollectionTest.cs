using lab;
using Lab10Lib;

namespace Tests
{
    [TestClass]
    public class ObservedCollectionTest
    {
        public static void Fill(ObservedCollection<ControlElement, Button> tree, out ControlElement[] keys, out Button[] values, int count = 5)
        {
            keys = new ControlElement[count];
            values = new Button[count];

            for (int i = 0; i < count; i++)
            {
                values[i] = new Button();
                values[i].RandomInit();
                keys[i] = values[i].GetBase;
            }
            Array.Sort(keys);
            Array.Sort(values);

            for (int i = 0; i < count; i++)
            {
                tree.Add(keys[i], values[i]);
            }
        }

        [TestMethod]
        public void AddTest()
        {
            ObservedCollection<ControlElement, Button> tree = new("Tree");
            ControlElement[] keys = new ControlElement[10];
            Button[] values = new Button[10];

            for (int i = 0; i < 10; i++)
            {
                values[i] = new Button();
                values[i].RandomInit();
                keys[i] = values[i].GetBase;
            }

            tree.Add(keys[7], values[7]);
            tree.Add(keys[9], values[9]);
            tree.Add(keys[8], values[8]);

            tree.Add(keys[6], values[6]);
            tree.Add(keys[4], values[4]);
            tree.Add(keys[5], values[5]);

            for (int i = 0; i < 4; i++)
            {
                tree.Add(keys[i], values[i]);
            }

            for (int i = 0; i < 10; i++)
            {
                Assert.AreEqual(values[i], tree[keys[i]]);
            }
            Assert.AreEqual(10, tree.Count);

        }

        [TestMethod]
        public void AddReadOnlyTest()
        {
            ObservedCollection<ControlElement, Button> tree = new();
            tree.IsReadOnly = true;
            Assert.ThrowsException<NotSupportedException>(() => tree.Add(new(), new()));
        }

        [TestMethod]
        public void AddNullKeyTest()
        {
            ObservedCollection<ControlElement, Button> tree = new();
            Assert.ThrowsException<ArgumentNullException>(() => tree.Add(null, new()));
        }

        [TestMethod]
        public void AddExistKeyTest()
        {
            ObservedCollection<int, ControlElement> tree = new();
            tree.Add(0, new Button());
            Assert.ThrowsException<ArgumentException>(() => tree.Add(0, new()));
        }

        [TestMethod]
        public void ClearTest()
        {
            ObservedCollection<ControlElement, Button> tree = new();
            Fill(tree, out _, out _);
            tree.Clear();
            Assert.AreEqual(0, tree.Count);
        }

        [TestMethod]
        public void ClearReadOnlyTest()
        {
            ObservedCollection<ControlElement, Button> tree = new();
            Fill(tree, out _, out _);
            tree.IsReadOnly = true;
            Assert.ThrowsException<NotSupportedException>(() => tree.Clear());
        }

        [TestMethod]
        public void RemovePairTest()
        {
            ObservedCollection<ControlElement, Button> tree = new();
            Fill(tree, out ControlElement[] keys, out Button[] values, 10);

            tree.Remove(new KeyValuePair<ControlElement, Button>(keys[7], values[7]));
            Assert.AreEqual(9, tree.Count);
            for (int i = 0; i < keys.Length; i++)
            {
                KeyValuePair<ControlElement, Button> pair = new(keys[i], values[i]);
                if (i == 7)
                    Assert.IsFalse(tree.Contains(pair));
                else
                    Assert.IsTrue(tree.Contains(pair));
            }
        }

        [TestMethod]
        public void RemoveReadOnlyPairTest()
        {
            ObservedCollection<ControlElement, Button> tree = new();
            Fill(tree, out ControlElement[] keys, out Button[] values);
            tree.IsReadOnly = true;
            Assert.ThrowsException<NotSupportedException>(() => tree.Remove(new KeyValuePair<ControlElement, Button>(keys[0], values[0])));
        }

        [TestMethod]
        public void RemoveNullKeyPairTest()
        {
            ObservedCollection<ControlElement, Button> tree = new();
            Fill(tree, out ControlElement[] keys, out Button[] values);
            Assert.ThrowsException<ArgumentNullException>(() => tree.Remove(new KeyValuePair<ControlElement, Button>(null, values[0])));
        }

        [TestMethod]
        public void RemoveEmptyTreePairTest()
        {
            ObservedCollection<ControlElement, Button> tree = new();
            Assert.IsFalse(tree.Remove(new KeyValuePair<ControlElement, Button>(new(), new())));
        }

        [TestMethod]
        public void RemoveNotExistPairTest()
        {
            ObservedCollection<ControlElement, Button> tree = new();
            Fill(tree, out _, out _);
            Assert.IsFalse(tree.Remove(new KeyValuePair<ControlElement, Button>(new(), new())));
        }

        [TestMethod]
        public void RemoveKeyTest()
        {
            ObservedCollection<ControlElement, Button> tree = new();
            Fill(tree, out ControlElement[] keys, out _, 10);

            tree.Remove(keys[1]);
            Assert.AreEqual(9, tree.Count);
            for (int i = 0; i < keys.Length; i++)
            {
                if (i == 1)
                    Assert.IsFalse(tree.ContainsKey(keys[i]));
                else
                    Assert.IsTrue(tree.ContainsKey(keys[i]));
            }
        }

        [TestMethod]
        public void RemoveReadOnlyKeyTest()
        {
            ObservedCollection<ControlElement, Button> tree = new();
            Fill(tree, out ControlElement[] keys, out _);
            tree.IsReadOnly = true;
            Assert.ThrowsException<NotSupportedException>(() => tree.Remove(keys[0]));
        }

        [TestMethod]
        public void RemoveNullKeyTest()
        {
            ObservedCollection<ControlElement, Button> tree = new();
            Fill(tree, out _, out Button[] values);
            Assert.ThrowsException<ArgumentNullException>(() => tree.Remove(null));
        }

        [TestMethod]
        public void RemoveEmptyTreeKeyTest()
        {
            ObservedCollection<ControlElement, Button> tree = new();
            Assert.IsFalse(tree.Remove(new ControlElement()));
        }

        [TestMethod]
        public void RemoveNotExistKeyTest()
        {
            ObservedCollection<ControlElement, Button> tree = new();
            Fill(tree, out _, out _);
            Assert.IsFalse(tree.Remove(new ControlElement()));
        }

        [TestMethod]
        public void TryGetValueTest()
        {
            ObservedCollection<ControlElement, Button> tree = new();
            Fill(tree, out ControlElement[] keys, out Button[] values);

            Assert.IsTrue(tree.TryGetValue(keys[0], out Button value));
            Assert.AreEqual(values[0], value);
        }

        [TestMethod]
        public void TryGetNotExistValueTest()
        {
            ObservedCollection<ControlElement, Button> tree = new();
            Fill(tree, out _, out _);

            Assert.IsFalse(tree.TryGetValue(new ControlElement(), out Button value));
            Assert.AreEqual(null, value);
        }

        [TestMethod]
        public void IndexGetTest()
        {
            ObservedCollection<ControlElement, Button> tree = new();
            Fill(tree, out ControlElement[] keys, out Button[] values);
            Assert.AreEqual(values[1], tree[keys[1]]);
        }

        [TestMethod]
        public void IndexGetNullKeyTest()
        {
            ObservedCollection<ControlElement, Button> tree = new();
            Fill(tree, out _, out _);
            Assert.ThrowsException<ArgumentNullException>(() => tree[null]);
        }

        [TestMethod]
        public void IndexGetNotExistKeyTest()
        {
            ObservedCollection<ControlElement, Button> tree = new();
            Fill(tree, out _, out _);
            Assert.ThrowsException<KeyNotFoundException>(() => tree[new ControlElement()]);
        }

        [TestMethod]
        public void IndexSetTest()
        {
            ObservedCollection<ControlElement, Button> tree = new();
            Fill(tree, out ControlElement[] keys, out _);
            var value = new Button();
            value.RandomInit();

            tree[keys[2]] = value;
            Assert.AreEqual(value, tree[keys[2]]);
        }

        [TestMethod]
        public void IndexSetNullKeyTest()
        {
            ObservedCollection<ControlElement, Button> tree = new();
            Fill(tree, out _, out _);
            Assert.ThrowsException<ArgumentNullException>(() => tree[null] = new Button());
        }

        [TestMethod]
        public void IndexSetNotExistKeyTest()
        {
            ObservedCollection<ControlElement, Button> tree = new();
            Fill(tree, out _, out _);
            var value = new Button();
            value.RandomInit();
            var key = value.GetBase;

            tree[key] = value;
            Assert.AreEqual(value, tree[key]);
        }

        [TestMethod]
        public void IndexSetReadOnlyTest()
        {
            ObservedCollection<ControlElement, Button> tree = new();
            Fill(tree, out ControlElement[] keys, out _);
            var value = new Button();
            value.RandomInit();
            tree.IsReadOnly = true;
            Assert.ThrowsException<NotSupportedException>(() => tree[keys[0]] = value);
        }

        [TestMethod]
        public void CopyInitTest()
        {
            ObservedCollection<ControlElement, Button> tree = new();
            Fill(tree, out _, out _);
            var copy = new ObservedCollection<ControlElement, Button>(tree);

            Assert.AreEqual(tree, copy);
        }

        [TestMethod]
        public void NotEqualsTest()
        {
            ObservedCollection<ControlElement, Button> tree = new();
            Fill(tree, out _, out _);
            ObservedCollection<ControlElement, Button> newTree = new();

            Assert.AreNotEqual(tree, newTree);
        }
    }
}