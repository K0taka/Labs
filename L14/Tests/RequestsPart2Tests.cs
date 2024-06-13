using AVLTree;
using Lab10Lib;
using static lab.Part2;

namespace Tests
{
    [TestClass]
    public class RequestsPart2Tests
    {
        static void Fill(AVL<ControlElement, ControlElement> tree)
        {

            ControlElement[] elements = [new ControlElement(12, 5), new Button(0, 0, "Text of button"), new MultButton(0, 10, "Text text", true), new TextField(10, 0, "Hint", "Text")];

            foreach (ControlElement element in elements)
            {
                tree.Add(new(element.X, element.Y), element);
            }
        }


        [TestMethod]
        public void LINQCountElementsOfTypeTest()
        {
            AVL<ControlElement, ControlElement> tree = [];
            Fill(tree);
            Assert.AreEqual(1, LINQCountElementsOfType(tree, typeof(ControlElement)));
        }

        [TestMethod]
        public void EXTCountElementsOfTypeTest()
        {
            AVL<ControlElement, ControlElement> tree = [];
            Fill(tree);
            Assert.AreEqual(1, EXTCountElementsOfType(tree, typeof(ControlElement)));
        }

        [TestMethod]
        public void LINQGetElementWithMinXTest()
        {
            AVL<ControlElement, ControlElement> tree = [];
            Fill(tree);
            Assert.AreEqual(new Button(0, 0, "Text of button"), LINQGetElementWithMinX(tree));
        }

        [TestMethod]
        public void LINQGetElementWithMaxYTest()
        {
            AVL<ControlElement, ControlElement> tree = [];
            Fill(tree);
            Assert.AreEqual(new MultButton(0, 10, "Text text", true), LINQGetElementWithMaxY(tree));
        }

        [TestMethod]
        public void EXTGetElementWithMinXTest()
        {
            AVL<ControlElement, ControlElement> tree = [];
            Fill(tree);
            Assert.AreEqual(new Button(0, 0, "Text of button"), EXTGetElementWithMinX(tree));
        }

        [TestMethod]
        public void EXTGetElementWithMaxYTest()
        {
            AVL<ControlElement, ControlElement> tree = [];
            Fill(tree);
            Assert.AreEqual(new MultButton(0, 10, "Text text", true), EXTGetElementWithMaxY(tree));
        }

        [TestMethod]
        public void LINQAverageDistanceTest()
        {
            AVL<ControlElement, ControlElement> tree = [];
            Fill(tree);
            Assert.AreEqual(8.25, LINQAverageDistance(tree));
        }

        [TestMethod]
        public void EXTAverageDistanceTest()
        {
            AVL<ControlElement, ControlElement> tree = [];
            Fill(tree);
            Assert.AreEqual(8.25, EXTAverageDistance(tree));
        }

        [TestMethod]
        public void LINQSumOfXLeffThanTest()
        {
            AVL<ControlElement, ControlElement> tree = [];
            Fill(tree);
            Assert.AreEqual(10, LINQSumOfXLessThan(tree, 11));
        }

        [TestMethod]
        public void EXTQSumOfXLeffThanTest()
        {
            AVL<ControlElement, ControlElement> tree = [];
            Fill(tree);
            Assert.AreEqual(10, EXTSumOfXLessThan(tree, 11));
        }

        [TestMethod]
        public void LINQCountGroupsByTypesTest()
        {
            AVL<ControlElement, ControlElement> tree = [];
            Fill(tree);
            var result = LINQCountGroupsByTypes(tree);
            Assert.AreEqual(4, result.Count());
            foreach(var group in result)
            {
                foreach(var item in group)
                {
                    Assert.AreEqual(1, item);
                }
            }
        }

        [TestMethod]
        public void EXTCountGroupsByTypesTest()
        {
            AVL<ControlElement, ControlElement> tree = [];
            Fill(tree);
            var result = EXTCountGroupsByTypes(tree);
            Assert.AreEqual(4, result.Count());
            foreach (var group in result)
            {
                foreach (var item in group)
                {
                    Assert.AreEqual(1, item);
                }
            }
        }
    }
}
