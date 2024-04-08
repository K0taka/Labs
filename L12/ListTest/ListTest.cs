using Lab10Lib;
using lab;

namespace Tests
{
    [TestClass]
    public class ListTest
    {
        [TestMethod]
        public void EmptyListIndexTest()
        {
            MyList<ControlElement> list = new();

            Assert.ThrowsException<IndexOutOfRangeException>(() => list[0]);
        }

        [TestMethod]
        public void IndexTest()
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
            for (int i = 0; i < 5; i++)
            {
                ControlElement element = new();
                element.RandomInit();
                list.Add(element);
            }

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
            for (int i = 0; i < 5; i++)
            {
                ControlElement element = new();
                element.RandomInit();
                list.Add(element);
            }

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
            for (int i = 0; i < 10; i++)
            {
                ControlElement element = new();
                element.RandomInit();
                list.Add(element);
            }

            ControlElement[] arr = new ControlElement[5];

            Assert.ThrowsException<ArgumentException>(() => list.CopyTo(arr, 0));
        }

        [TestMethod]
        public void AddMethodTest()
        {
            ControlElement last = new ControlElement();
            last.RandomInit();

            MyList<ControlElement> list = new();
            for (int i = 0; i < 10; i++)
            {
                ControlElement element = new();
                element.RandomInit();
                list.Add(element);
            }

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
            for (int i = 0; i < 10; i++)
            {
                ControlElement element = new();
                element.RandomInit();
                list.AddFirst(element);
            }

            list.AddFirst(first);

            Assert.AreEqual(first, list[0]);
            Assert.AreEqual(first, list.Head.Data);
        }
    }
}