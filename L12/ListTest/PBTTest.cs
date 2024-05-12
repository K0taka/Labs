using lab;
using Lab10Lib;

namespace Tests
{
    [TestClass]
    public class PBTTest
    {
        public ControlElement[] FillArr(int count)
        {
            ControlElement[] arr = new ControlElement[count];
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = new ControlElement();
                arr[i].RandomInit();
            }
            return arr;
        }

        [TestMethod]
        public void CreationTest()
        {
            ControlElement[] arr = FillArr(5);

            PBT<ControlElement> tree = new(arr);

            Assert.AreEqual(5, tree.Capacity);
        }

        [TestMethod]
        public void EmptyCreationTest()
        {
            ControlElement[] arr = [];
            PBT<ControlElement> tree;
            Assert.ThrowsException<ArgumentException>(()=> tree = new(arr));
        }

        [TestMethod]
        public void EnumerationTest()
        {
            ControlElement[] arr = FillArr(5);

            PBT<ControlElement> tree = new(arr);

            int index = 0;
            foreach(var item in tree)
            {
                Assert.AreEqual(arr[index++], item.Data);
            }
        }
    }
}
