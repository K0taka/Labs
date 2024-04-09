using Lab10Lib;
using lab;
using System.Collections.Generic;

namespace Tests
{
    [TestClass]
    public class ListTest
    {
        static void FillList(MyList<ControlElement> list, int n)
        {
            for (int i = 0; i < n; i++)
            {
                ControlElement elem = new();
                elem.RandomInit();
                list.Add(elem);
            }
        }

        [TestMethod]
        public void EmptyListIndexGetTest()
        {
            MyList<ControlElement> list = new();

            Assert.ThrowsException<IndexOutOfRangeException>(() => list[0]);
        }

        [TestMethod]
        public void IndexGetTest()
        {
            ControlElement first = new ControlElement();
            Button last = new Button();
            MyList<ControlElement> list = new();

            first.RandomInit();
            last.RandomInit();

            list.Add(first);
            list.Add(new ControlElement());
            list.Add(last);

            Assert.AreEqual(first, list[0]);
            Assert.AreEqual(last, list[2]);

        }

        [TestMethod]
        public void IndexGetLastTest()
        {
            MyList<ControlElement> list = new();
            FillList(list, 5);

            ControlElement last = new ControlElement(10, 10);
            list.Add(last);
            Assert.AreEqual(last, list[5]);
        }

        [TestMethod]
        public void IndexGetFirstTest()
        {
            MyList<ControlElement> list = new();
            for (int i = 0; i < 5; i++)
            {
                ControlElement elem = new();
                elem.RandomInit();
                list.Add(elem);
            }

            ControlElement first = new ControlElement(10, 10);
            list.AddFirst(first);
            Assert.AreEqual(first, list[0]);
        }

        [TestMethod]
        public void IndexGetIncorrectIndexTest()
        {
            MyList<ControlElement> list = new();
            FillList(list, 5);

            Assert.ThrowsException<IndexOutOfRangeException>(() => list[10]);
        }

        [TestMethod]
        public void IndexSetTest()
        {
            MyList<ControlElement> list = new();
            FillList(list, 5);

            ControlElement nElem = new ControlElement(10, 10);
            list[3] = nElem;
            Assert.AreEqual(nElem, list[3]);
        }

        [TestMethod]
        public void IndexSetFirstTest()
        {
            MyList<ControlElement> list = new();
            FillList(list, 5);

            ControlElement nElem = new(10, 10);
            list[0] = nElem;
            Assert.AreEqual(nElem, list[0]);
        }

        [TestMethod]
        public void IndexSetLastTest()
        {
            MyList<ControlElement> list = new();
            FillList(list, 5);

            ControlElement nElem = new(10, 10);
            list[^1] = nElem;
            Assert.AreEqual(nElem, list[^1]);
        }

        [TestMethod]
        public void IndexSetEmptyTest()
        {
            MyList<ControlElement> list = new();

            Assert.ThrowsException<IndexOutOfRangeException>(() => list[0] = new ControlElement());
        }

        [TestMethod]
        public void IndexSetOutOfRAngeTest()
        {
            MyList<ControlElement> list = new();
            FillList(list, 5);
            Assert.ThrowsException<IndexOutOfRangeException>(() => list[10] = new ControlElement());
        }

        [TestMethod]
        public void CreationFromArrayTest()
        {
            ControlElement[] arr = new ControlElement[5];
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = new ControlElement();
                arr[i].RandomInit();
            }

            MyList<ControlElement> list = new(arr);

            Assert.AreEqual(arr[0], list[0]);
            Assert.AreEqual(arr[1], list[1]);
            Assert.AreEqual(arr[2], list[2]);
            Assert.AreEqual(arr[3], list[3]);
            Assert.AreEqual(arr[4], list[4]);
        }

        [TestMethod]
        public void CreationFromNullArrayTest() 
        {
            ControlElement[]? arr = null;
            MyList<ControlElement> list;

            Assert.ThrowsException<ArgumentNullException>(() => list = new(arr));
        }

        [TestMethod]
        public void CreationFromEmptyArrayTest()
        {
            ControlElement[]? arr = [];
            MyList<ControlElement> list;

            list = new(arr);

            Assert.AreEqual(arr.Length, list.Count);
        }

        [TestMethod]
        public void CopyToArrayTest()
        {
            MyList<ControlElement> list = new();
            FillList(list, 5);

            ControlElement[] arr = new ControlElement[5];

            list.CopyTo(arr, 0);

            Assert.AreEqual(list[0], arr[0]);
            Assert.AreEqual(list[1], arr[1]);
            Assert.AreEqual(list[2], arr[2]);
            Assert.AreEqual(list[3], arr[3]);
            Assert.AreEqual(list[4], arr[4]);
        }

        [TestMethod]
        public void CopyToArrayWithIndexTest()
        {
            ControlElement[] arr = new ControlElement[10];

            MyList<ControlElement> list = new();
            FillList(list, 5);

            list.CopyTo(arr, 5);

            Assert.AreEqual(list[0], arr[5]);
            Assert.AreEqual(list[1], arr[6]);
            Assert.AreEqual(list[2], arr[7]);
            Assert.AreEqual(list[3], arr[8]);
            Assert.AreEqual(list[4], arr[9]);
        }

        [TestMethod]
        public void CopyToArrayNegativeIndexTest()
        {
            MyList<ControlElement> list = new();
            ControlElement[] arr = new ControlElement[5];
            
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => list.CopyTo(arr, -3));
        }

        [TestMethod]
        public void CopyToArrayWithNotEnoghtSpaceTest()
        {
            MyList<ControlElement> list = new();
            FillList(list, 10);

            ControlElement[] arr = new ControlElement[5];

            Assert.ThrowsException<ArgumentException>(() => list.CopyTo(arr, 0));
        }

        [TestMethod]
        public void AddMethodTest()
        {
            ControlElement last = new ControlElement();
            last.RandomInit();

            MyList<ControlElement> list = new();
            FillList(list, 10);

            list.Add(last);

            Assert.AreEqual(last, list[^1]);
            Assert.AreEqual(last, list.Last.Data);
        }

        [TestMethod]
        public void AddFirstMethodTest()
        {
            ControlElement first = new ControlElement();
            first.RandomInit();

            MyList<ControlElement> list = new();
            FillList(list, 10);

            list.AddFirst(first);

            Assert.AreEqual(first, list[0]);
            Assert.AreEqual(first, list.Head.Data);
        }

        [TestMethod]
        public void InsertTest()
        {
            MyList<ControlElement> list = new();
            FillList(list, 5);
            ControlElement insert = new();
            insert.RandomInit();

            list.Insert(3, insert);

            Assert.AreEqual(insert, list[3]);
        }

        [TestMethod]
        public void InsertFirstTest()
        {
            MyList<ControlElement> list = new();
            FillList(list, 5);
            ControlElement insert = new();
            insert.RandomInit();

            list.Insert(0, insert);

            Assert.AreEqual(insert, list[0]);
        }

        [TestMethod]
        public void InsertLastTest()
        {
            MyList<ControlElement> list = new();
            FillList(list, 5);
            ControlElement insert = new();
            insert.RandomInit();

            list.Insert(5, insert);

            Assert.AreEqual(insert, list[5]);
        }

        [TestMethod]
        public void InsertOutOfRangeTest()
        {
            MyList<ControlElement> list = new();
            FillList(list, 5);
            ControlElement insert = new();
            insert.RandomInit();

            Assert.ThrowsException<IndexOutOfRangeException>(() => list.Insert(6, insert));
        }

        [TestMethod]
        public void RemoveTest()
        {
            MyList<ControlElement> list = [];
            FillList(list, 5);

            ControlElement remove = new(10, 10);
            list.Insert(3, remove);

            Assert.AreEqual(remove, list[3]);

            list.Remove(remove);

            Assert.IsFalse(list.Contains(remove));
        }

        [TestMethod]
        public void RemoveNonExistTest()
        {
            MyList<ControlElement> list = [];
            FillList(list, 5);

            ControlElement remove = new(10, 10);

            Assert.IsFalse(list.Remove(remove));
        }

        [TestMethod]
        public void RemoveFirstTest()
        {
            MyList<ControlElement> list = [];
            FillList(list, 5);

            ControlElement remove = new(10, 10);
            list.AddFirst(remove);

            Assert.AreEqual(remove, list[0]);

            list.Remove(remove);

            Assert.IsFalse(list.Contains(remove));
            Assert.AreEqual(list[0], list.Head.Data);

        }

        [TestMethod]
        public void RemoveLastTest()
        {
            MyList<ControlElement> list = [];
            FillList(list, 5);

            ControlElement remove = new(10, 10);
            list.Add(remove);

            Assert.AreEqual(remove, list[^1]);

            list.Remove(remove);

            Assert.IsFalse(list.Contains(remove));
            Assert.AreEqual(list[^1], list.Last.Data);
        }

        [TestMethod]
        public void RemoveFromEmptyListTest()
        {
            MyList<ControlElement> list = [];

            ControlElement remove = new(10, 10);

            Assert.IsFalse(list.Remove(remove));
        }

        [TestMethod]
        public void RemoveAtTest()
        {
            MyList<ControlElement> list = new();
            FillList(list, 5);

            ControlElement removing = list[3];

            list.RemoveAt(3);
            
            Assert.IsFalse(list.Contains(removing));
        }

        [TestMethod]
        public void RemoveAtFirstTest()
        {
            MyList<ControlElement> list = new();
            FillList(list, 5);

            ControlElement removing = list[0];

            list.RemoveAt(0);

            Assert.IsFalse(list.Contains(removing));
            Assert.AreEqual(list[0], list.Head.Data);
        }

        [TestMethod]
        public void RemoveAtLastTest()
        {
            MyList<ControlElement> list = new();
            FillList(list, 5);

            ControlElement removing = list[4];

            list.RemoveAt(4);

            Assert.IsFalse(list.Contains(removing));
            Assert.AreEqual(list[^1], list.Last.Data);
        }

        [TestMethod]
        public void RemoveAtEmptyTest()
        {
            MyList<ControlElement> list = new();

            Assert.ThrowsException<NullReferenceException>(() => list.RemoveAt(0));
        }

        [TestMethod]
        public void RemoveAtOutOfRangeTest()
        {
            MyList<ControlElement> list = new();
            FillList(list, 5);

            Assert.ThrowsException<IndexOutOfRangeException>(() => list.RemoveAt(6));
        }

        [TestMethod]
        public void ClearTest()
        {
            MyList<ControlElement> list = new();
            FillList(list, 5);
            list.Clear();
            Assert.AreEqual(0, list.Count);

            GC.Collect();
            GC.WaitForPendingFinalizers();

            //Create two items to make sure the IDs are in order.
            ControlElement nElem = new();
            ControlElement nElem1 = new();
            Assert.AreEqual(0, (int)nElem.Id);
            Assert.AreEqual(1, (int)nElem1.Id);
        }

        [TestMethod]
        public void IndexOfTest()
        {
            MyList<ControlElement> list = new();
            FillList(list, 5);

            ControlElement add = new(10, 10);

            list.Insert(3, add);

            Assert.AreEqual(3, list.IndexOf(add));
        }

        [TestMethod]
        public void IndexOfFirstTest()
        {
            MyList<ControlElement> list = new();
            FillList(list, 5);

            ControlElement add = new(10, 10);

            list.AddFirst(add);

            Assert.AreEqual(0, list.IndexOf(add));
        }

        [TestMethod]
        public void IndexOfLastTest()
        {
            MyList<ControlElement> list = new();
            FillList(list, 5);

            ControlElement add = new(10, 10);

            list.Add(add);

            Assert.AreEqual(5, list.IndexOf(add));
        }

        [TestMethod]
        public void IndexOfNonExistTest()
        {
            MyList<ControlElement> list = new();
            FillList(list, 5);

            ControlElement add = new(10, 10);

            Assert.AreEqual(-1, list.IndexOf(add));
        }

        [TestMethod]
        public void IndexOfNullTest()
        {
            MyList<ControlElement> list = new();

            ControlElement add = new(10, 10);

            Assert.AreEqual(-1, list.IndexOf(add));
        }

        [TestMethod]
        public void ContainsTest()
        {
            MyList<ControlElement> list = [];
            FillList(list, 5);

            ControlElement contains = new(10, 10);
            list.Insert(3, contains);

            Assert.IsTrue(list.Contains(contains));
        }

        [TestMethod]
        public void ContainsFirstTest()
        {
            MyList<ControlElement> list = [];
            FillList(list, 5);

            ControlElement contains = new(10, 10);
            list.AddFirst(contains);

            Assert.IsTrue(list.Contains(contains));
        }

        [TestMethod]
        public void ContainsLastTest()
        {
            MyList<ControlElement> list = [];
            FillList(list, 5);

            ControlElement contains = new(10, 10);
            list.Add(contains);

            Assert.IsTrue(list.Contains(contains));
        }

        [TestMethod]
        public void ContainsNonExistTest()
        {
            MyList<ControlElement> list = [];
            FillList(list, 5);

            ControlElement contains = new(10, 10);

            Assert.IsFalse(list.Contains(contains));
        }

        [TestMethod]
        public void ContainsNullTest()
        {
            MyList<ControlElement> list = [];

            ControlElement contains = new(10, 10);

            Assert.IsFalse(list.Contains(contains));
        }

        [TestMethod]
        public void EnumeratorTest()
        {
            MyList<ControlElement> list = [];
            FillList(list, 5);
            int i = 0;
            foreach(ControlElement element in list)
            {
                Assert.AreEqual(list[i], element);
                i++;
            }
        }

        [TestMethod]
        public void CloneTest()
        {
            MyList<ControlElement> list = [];
            FillList(list, 5);

            MyList<ControlElement> clone = (MyList<ControlElement>)list.Clone();

            Assert.AreEqual(list, clone);
        }

        [TestMethod]
        public void DeepCloneTest()
        {
            MyList<ControlElement> list = [];
            FillList(list, 5);

            MyList<ControlElement> clone = (MyList<ControlElement>)list.Clone();

            clone[^1].X = 10; clone[^1].Y = 10;
            Assert.AreNotEqual(list, clone);
            for (int i = 0; i < list.Count - 1; i++)
            {
                Assert.AreEqual(list[i], clone[i]);
            }
            Assert.AreNotEqual(list[^1], clone[^1]);
        }

    }
}