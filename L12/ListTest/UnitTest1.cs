using Lab10Lib;
using lab;

namespace ListTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void EmptyListIndexTest()
        {
            MyList<ControlElement> list = new();
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
        public void CreationFromEmptyArrayTest() 
        {
            ControlElement[] arr = [];
            MyList<ControlElement> list;

            Assert.ThrowsException<ArgumentNullException>(() => list = new(arr));
        }
    }
}