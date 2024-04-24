using lab;
using Lab10Lib;

namespace Tests
{
    [TestClass]
    public class HashTableTest
    {
        public static ControlElement[] FillHashTable(int len, MyHashTable<ControlElement, Button> hashTable, out Button[] values)
        {
            ControlElement[] keys = new ControlElement[len];
            values = new Button[len];
            for (int i = 0; i < len; i++)
            {
                Button bt = new();
                bt.RandomInit();
                ControlElement key = new(bt.X, bt.Y);

                if(!hashTable.Add(key, bt))
                {
                    i--;
                    continue;
                }
                keys[i] = key;
                values[i] = bt;
            }
            return keys;
        }

        [TestMethod]
        public void CapacityTest()
        {
            MyHashTable<ControlElement, Button> hashTable;
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => hashTable = new(0));
        }

        [TestMethod]
        public void CountTest()
        {
            MyHashTable<ControlElement, Button> hashTable = new();
            FillHashTable(5, hashTable, out _);

            Assert.AreEqual(5, hashTable.Count);
        }

        [TestMethod]
        public void AddExistTest()
        {
            MyHashTable<ControlElement, Button> hashTable = new();
            var keys = FillHashTable(5, hashTable, out _);

            Assert.IsFalse(hashTable.Add(keys[3], new()));
        }

        [TestMethod]
        public void KeysTest()
        {
            MyHashTable<ControlElement, Button> hashTable = new();
            var expKeys = FillHashTable(5, hashTable, out _);

            var actKeys = hashTable.Keys;
            Array.Sort(actKeys);

            for (int i = 0; i < expKeys.Length; i++)
            {
                Assert.AreEqual(expKeys[i], actKeys[i]);
            }   
        }

        [TestMethod]
        public void ValuesTest()
        {
            MyHashTable<ControlElement, Button> hashTable = new();
            FillHashTable(5, hashTable, out Button[] expValues);
            var actValues = hashTable.Values;
            Array.Sort(actValues);

            for (int i = 0; i < expValues.Length; i++)
            {
                Assert.AreEqual(expValues[i], actValues[i]);
            }
        }

        [TestMethod]
        public void ClearTest()
        {
            MyHashTable<ControlElement, Button> hashTable = [];
            FillHashTable(5, hashTable, out _);

            hashTable.Clear();

            Assert.AreEqual(0, hashTable.Count);
            Assert.AreEqual(0, hashTable.Keys.Length);
            Assert.AreEqual(0, hashTable.Values.Length);

        }

        [TestMethod]
        public void ContainsExistTest()
        {
            MyHashTable<ControlElement, Button> hashTable = [];
            var keys = FillHashTable(5, hashTable, out _);

            Assert.IsTrue(hashTable.Contains(keys[3]));
        }

        [TestMethod]
        public void ContainsNotExistTest()
        {
            MyHashTable<ControlElement, Button> hashTable = [];
            FillHashTable(5, hashTable, out _);

            Assert.IsFalse(hashTable.Contains(new()));
        }

        [TestMethod]
        public void RemoveSoloTest()
        {
            MyHashTable<ControlElement, Button> hashTable = new();
            var keys = FillHashTable(5, hashTable, out _);
            Assert.IsTrue(hashTable.Remove(keys[3]));
            foreach (var key in hashTable.Keys)
            {
                Assert.AreNotEqual(keys[3], key);
            }
            Assert.AreEqual(4, hashTable.Count);
        }

        [TestMethod]
        public void RemoveNotExistTest()
        {
            MyHashTable<ControlElement, Button> hashTable = [];
            FillHashTable(5, hashTable, out _);
            Assert.IsFalse(hashTable.Remove(new()));
            Assert.AreEqual(5, hashTable.Count);
        }

        [TestMethod]
        public void RemoveFirstTest()
        {
            MyHashTable<ControlElement, Button> hashTable = new(1);
            var keys = FillHashTable(5, hashTable, out _);
            Assert.IsTrue(hashTable.Remove(keys[0]));
            foreach (var key in hashTable.Keys)
            {
                Assert.AreNotEqual(keys[0], key);
            }
            Assert.AreEqual(4, hashTable.Count);
        }

        [TestMethod]
        public void RemoveInCahinTest()
        {
            MyHashTable<ControlElement, Button> hashTable = new(1);
            var keys = FillHashTable(5, hashTable, out _);
            Assert.IsTrue(hashTable.Remove(keys[3]));
            foreach (var key in hashTable.Keys)
            {
                Assert.AreNotEqual(keys[3], key);
            }
            Assert.AreEqual(4, hashTable.Count);
        }

        [TestMethod]
        public void IndexTest()
        {
            MyHashTable<ControlElement, Button> hashTable = [];
            var keys = FillHashTable(5, hashTable, out var values);
            for (int i = 0; i < keys.Length; i++)
            {
                Assert.AreEqual(values[i], hashTable[keys[i]]);
            }
        }

        [TestMethod]
        public void IndexNotExistTest()
        {
            MyHashTable<ControlElement, Button> hashTable = [];
            FillHashTable(5, hashTable, out _);
            Assert.ThrowsException<KeyNotFoundException>(() => hashTable[new()]);
        }

        [TestMethod]
        public void EnumerationTest()
        {
            MyHashTable<ControlElement, Button> hashTable = new();
            var keys = FillHashTable(5, hashTable, out var values);

            foreach (var chain in hashTable)
            {
                if (chain == null)
                    continue;
                foreach (var node in chain)
                    Assert.AreEqual(Array.IndexOf(keys, node.Key), Array.IndexOf(values, node.Data));
            }
        }

        [TestMethod]
        public void NodeHashCodeTest()
        {
            Button value = new Button();
            value.RandomInit();
            ControlElement key = new ControlElement(value.X, value.Y);

            HashTableNode<ControlElement, Button> node = new(key, value);

            Assert.AreEqual(key.GetHashCode(), node.GetHashCode());
        }
    }
}
