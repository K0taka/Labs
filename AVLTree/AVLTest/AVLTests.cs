using AVLTree;
using Lab10Lib;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Engine;

namespace AVLTest
{
    [TestClass]
    public class AVLTests
    {
        public static void Fill(AVL<ControlElement, Button> tree, out ControlElement[] keys, out Button[] values, int count = 5)
        {
            keys = new ControlElement[count];
            values = new Button[count];

            for (int i = 0; i < count; i++)
            {
                values[i] = new Button();
                values[i].RandomInit();
                keys[i] = values[i].GetBase;
                tree.Add(keys[i], values[i]);
            }
        }

        [TestMethod]
        public void AddTest()
        {
            AVL<ControlElement, Button> tree = new();
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
            AVL<ControlElement, Button> tree = new();
            tree.IsReadOnly = true;
            Assert.ThrowsException<NotSupportedException>(() => tree.Add(new(), new()));
        }

        [TestMethod]
        public void AddNullKeyTest()
        {
            AVL<ControlElement, Button> tree = new();
            Assert.ThrowsException<ArgumentNullException>(() => tree.Add(null, new()));
        }

        [TestMethod]
        public void AddExistKeyTest()
        {
            AVL<int, ControlElement> tree = new();
            tree.Add(0, new Button());
            Assert.ThrowsException<ArgumentException>(() => tree.Add(0, new()));
        }

        [TestMethod]
        public void ClearTest()
        {
            AVL<ControlElement, Button> tree = new();
            Fill(tree, out _, out _);
            tree.Clear();
            Assert.AreEqual(0, tree.Count);
        }

        [TestMethod]
        public void ClearReadOnlyTest()
        {
            AVL<ControlElement, Button> tree = new();
            Fill(tree, out _, out _);
            tree.IsReadOnly = true;
            Assert.ThrowsException<NotSupportedException>(() => tree.Clear());
        }

        [TestMethod]
        public void ContainsTest()
        {
            AVL<ControlElement, Button> tree = new();
            Fill(tree, out ControlElement[] keys, out Button[] values, 10);
            for (int i = 0; i < 10; i++)
            {
                Assert.IsTrue(tree.Contains(new KeyValuePair<ControlElement, Button>(keys[i], values[i])));
            }
        }

        [TestMethod]
        public void NotContainsTest()
        {
            AVL<ControlElement, Button> tree = new();
            Fill(tree, out _, out _, 10);
            Assert.IsFalse(tree.Contains(new KeyValuePair<ControlElement, Button>(new(), new())));
        }

        [TestMethod]
        public void ContainsWithAnotherValueTest()
        {
            AVL<ControlElement, Button> tree = new();
            Fill(tree, out ControlElement[] keys, out Button[] values, 10);
            Assert.IsFalse(tree.Contains(new KeyValuePair<ControlElement, Button>(keys[0], new())));
        }

        [TestMethod]
        public void InOrederEnumeratorTest()
        {
            AVL<ControlElement, Button> tree = new();
            Fill(tree, out ControlElement[] keys, out Button[] values);
            int index = 0;
            foreach(var item in tree)
            {
                Assert.AreEqual(keys[index], item.Key);
                Assert.AreEqual(values[index++], item.Value);
            }
        }

        [TestMethod]
        public void InWideEnumeratorTest()
        {
            int[] order = [1, 0, 3, 2, 4];
            int index = 0;
            AVL<ControlElement, Button> tree = new();
            Fill(tree, out ControlElement[] keys, out Button[] values);
            foreach(var item in tree.InWideEnumerator())
            {
                Assert.AreEqual(keys[order[index]], item.Key);
                Assert.AreEqual(values[order[index++]], item.Value);
            }
        }

        [TestMethod]
        public void RemovePairTest()
        {
            AVL<ControlElement, Button> tree = new();
            Fill(tree, out ControlElement[] keys, out Button[] values, 10);

            tree.Remove(new KeyValuePair<ControlElement, Button>(keys[7], values[7]));
            Assert.AreEqual(9, tree.Count);
            for(int i = 0; i < keys.Length; i++)
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
            AVL<ControlElement, Button> tree = new();
            Fill(tree, out ControlElement[] keys, out Button[] values);
            tree.IsReadOnly = true;
            Assert.ThrowsException<NotSupportedException>(()=>tree.Remove(new KeyValuePair<ControlElement, Button>(keys[0], values[0])));
        }

        [TestMethod]
        public void RemoveNullKeyPairTest()
        {
            AVL<ControlElement, Button> tree = new();
            Fill(tree, out ControlElement[] keys, out Button[] values);
            Assert.ThrowsException<ArgumentNullException>(() => tree.Remove(new KeyValuePair<ControlElement, Button>(null, values[0])));
        }

        [TestMethod]
        public void RemoveEmptyTreePairTest()
        {
            AVL<ControlElement, Button> tree = new();
            Assert.IsFalse(tree.Remove(new KeyValuePair<ControlElement, Button>(new(), new())));
        }

        [TestMethod]
        public void RemoveNotExistPairTest()
        {
            AVL<ControlElement, Button> tree = new();
            Fill(tree, out ControlElement[] keys, out Button[] values);
            Assert.IsFalse(tree.Remove(new KeyValuePair<ControlElement, Button>(new(), new())));
        }

        [TestMethod]
        public void RemoveKeyTest()
        {
            AVL<ControlElement, Button> tree = new();
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
            AVL<ControlElement, Button> tree = new();
            Fill(tree, out ControlElement[] keys, out _);
            tree.IsReadOnly = true;
            Assert.ThrowsException<NotSupportedException>(() => tree.Remove(keys[0]));
        }

        [TestMethod]
        public void RemoveNullKeyTest()
        {
            AVL<ControlElement, Button> tree = new();
            Fill(tree, out ControlElement[] keys, out Button[] values);
            Assert.ThrowsException<ArgumentNullException>(() => tree.Remove(null));
        }

        [TestMethod]
        public void RemoveEmptyTreeKeyTest()
        {
            AVL<ControlElement, Button> tree = new();
            Assert.IsFalse(tree.Remove(new ControlElement()));
        }

        [TestMethod]
        public void RemoveNotExistKeyTest()
        {
            AVL<ControlElement, Button> tree = new();
            Fill(tree, out ControlElement[] keys, out Button[] values);
            Assert.IsFalse(tree.Remove(new ControlElement()));
        }

        [TestMethod]
        public void CopyToTest()
        {
            KeyValuePair<ControlElement, Button>[] array = new KeyValuePair<ControlElement, Button>[5];
            AVL<ControlElement, Button> tree = new();
            Fill(tree, out ControlElement[] keys, out Button[] values);

            tree.CopyTo(array, 0);
            for (int i = 0; i < array.Length; i++)
            {
                Assert.AreEqual(keys[i], array[i].Key);
                Assert.AreEqual(values[i], array[i].Value);

                Assert.AreEqual(keys[i].X, array[i].Key.X);
                Assert.AreEqual(keys[i].Y, array[i].Key.Y);

                Assert.AreEqual(values[i].X, array[i].Value.X);
                Assert.AreEqual(values[i].Y, array[i].Value.Y);
                Assert.AreEqual(values[i].Text, array[i].Value.Text);
            }
        }

        [TestMethod]
        public void CopyToEmptyArrayTest()
        {
            KeyValuePair<ControlElement, Button>[]? array = null;
            AVL<ControlElement, Button> tree = new();
            Fill(tree, out _, out _);
            Assert.ThrowsException<ArgumentNullException>(() => tree.CopyTo(array, 0));
        }

        [TestMethod]
        public void CopyToLowZeroIndexTest()
        {
            KeyValuePair<ControlElement, Button>[] array = new KeyValuePair<ControlElement, Button>[5];
            AVL<ControlElement, Button> tree = new();
            Fill(tree, out _, out _);
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => tree.CopyTo(array, -5));
        }

        [TestMethod]
        public void CopyToNotEnoughSpaceTest()
        {
            KeyValuePair<ControlElement, Button>[] array = new KeyValuePair<ControlElement, Button>[5];
            AVL<ControlElement, Button> tree = new();
            Fill(tree, out _, out _);
            Assert.ThrowsException<ArgumentException>(() =>  tree.CopyTo(array, 1));
        }

        [TestMethod]
        public void IndexGetTest()
        {
            AVL<ControlElement, Button> tree = new();
            Fill(tree, out ControlElement[] keys, out Button[] values);
            Assert.AreEqual(values[1], tree[keys[1]]);
        }

        [TestMethod]
        public void IndexGetNullKeyTest()
        {
            AVL<ControlElement, Button> tree = new();
            Fill(tree, out _, out _);
            Assert.ThrowsException<ArgumentNullException>(() => tree[null]);
        }

        [TestMethod]
        public void IndexGetNotExistKeyTest()
        {
            AVL<ControlElement, Button> tree = new();
            Fill(tree, out _, out _);
            Assert.ThrowsException<KeyNotFoundException>(() => tree[new ControlElement()]);
        }

        [TestMethod]
        public void IndexSetTest()
        {
            AVL<ControlElement, Button> tree = new();
            Fill(tree, out ControlElement[] keys, out _);
            var value = new Button();
            value.RandomInit();

            tree[keys[2]] = value;
            Assert.AreEqual(value, tree[keys[2]]);
        }

        [TestMethod]
        public void IndexSetNullKeyTest()
        {
            AVL<ControlElement, Button> tree = new();
            Fill(tree, out _, out _);
            Assert.ThrowsException<ArgumentNullException>(() => tree[null] = new Button());
        }

        [TestMethod]
        public void IndexSetNotExistKeyTest()
        {
            AVL<ControlElement, Button> tree = new();
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
            AVL<ControlElement, Button> tree = new();
            Fill(tree, out ControlElement[] keys, out _);
            var value = new Button();
            value.RandomInit();
            tree.IsReadOnly = true;
            Assert.ThrowsException<NotSupportedException>(() => tree[keys[0]] = value);
        }

        [TestMethod]
        public void ContainsKeyTest()
        {
            AVL<ControlElement, Button> tree = new();
            Fill(tree, out ControlElement[] keys, out _);
            foreach(ControlElement key in keys)
            {
                Assert.IsTrue(tree.ContainsKey(key));
            }
        }

        [TestMethod]
        public void ContainsKeyNullTest()
        {
            AVL<ControlElement, Button> tree = new();
            Fill(tree, out _, out _);

            Assert.ThrowsException<ArgumentNullException>(() => tree.ContainsKey(null));
        }

        [TestMethod]
        public void NotContainsKeyTest()
        {
            AVL<ControlElement, Button> tree = new();
            Fill(tree, out _, out _);

            Assert.IsFalse(tree.ContainsKey(new ControlElement()));
        }

        [TestMethod]
        public void TryGetValueTest()
        {
            AVL<ControlElement, Button> tree = new();
            Fill(tree, out ControlElement[] keys, out Button[] values);

            Assert.IsTrue(tree.TryGetValue(keys[0], out Button value));
            Assert.AreEqual(values[0], value);
        }

        [TestMethod]
        public void TryGetNotExistValueTest()
        {
            AVL<ControlElement, Button> tree = new();
            Fill(tree, out _, out _);

            Assert.IsFalse(tree.TryGetValue(new ControlElement(), out Button value));
            Assert.AreEqual(null, value);
        }

        [TestMethod]
        public void CloneWithIClonableKeyTest()
        {
            AVL<ControlElement, Button> first = new();
            Fill(first, out ControlElement[] keys, out _);

            AVL<ControlElement, Button> second = (AVL<ControlElement, Button>)first.Clone();

            Assert.AreEqual(first, second);
            second[keys[0]] = new();
            Assert.AreNotEqual(first, second);
        }

        [TestMethod]
        public void CloneWithoutIClonableKeyTest()
        {
            AVL<int, Button> first = new();
            for(int i = 0; i < 10; i++)
            {
                var value = new Button();
                value.RandomInit();
                first.Add(i, value);
            }
            var second = (AVL<int, Button>)first.Clone();
            Assert.AreEqual(first, second);
            second[0] = new();
            Assert.AreNotEqual(first, second);
        }

        [TestMethod]
        public void EqualsTest()
        {
            AVL<ControlElement, Button> first = new();
            AVL<ControlElement, Button> second = new();

            Assert.AreEqual(first, second);

            for (int i = 0; i < 5; i++)
            {
                Button value = new();
                value.RandomInit();
                var key = value.GetBase;

                first.Add(key, value);
                second.Add(key, value);
            }

            Assert.AreEqual(first, second);
        }

        [TestMethod]
        public void KeysTest()
        {
            AVL<ControlElement, Button> tree = new();
            Fill(tree, out ControlElement[] keys, out _);
            ControlElement[] Keys = (ControlElement[])tree.Keys;
            for (int i = 0;i < Keys.Length; i++)
            {
                Assert.AreEqual(keys[i], Keys[i]);
            }
        }

        [TestMethod]
        public void ValuesTest()
        {
            AVL<ControlElement, Button> tree = new();
            Fill(tree, out _, out Button[] values);
            Button[] Values = (Button[])tree.Values;
            for (int i = 0; i < values.Length; i++)
            {
                Assert.AreEqual(values[i], Values[i]);
            }
        }
    }
}