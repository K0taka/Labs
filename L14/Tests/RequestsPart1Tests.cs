using Lab10Lib;
using System.ComponentModel.Design;
using System.Security.Cryptography.X509Certificates;
using static lab.Part1;

namespace Tests
{
    [TestClass]
    public class RequestsPart1Tests
    {
        static void Fill(Stack<Dictionary<ControlElement, ControlElement>> collection, List<Function> funcs, out List<KeyValuePair<ControlElement, string>> pairs)
        {
            collection.Clear();
            funcs.Clear();

            ControlElement[] elements = [new Button(0, 0, "Button"), new MultButton(9, 0, "MultButton", true),
                                         new ControlElement(8, 6), new Button(0, 0, "Button"), new TextField(9, 0, "Hint", "Text"),
                                         new ControlElement(8, 6), new TextField(5, 12, "NotAHint", "NotAText"), new ControlElement(0, 0),
                                         new Button(9, 0, "Button"), new ControlElement(8, 6)];

            Dictionary<ControlElement, ControlElement> window1 = new()
            {
                { new ControlElement(elements[0].X, elements[0].Y),  elements[0]},
                { new ControlElement(elements[1].X, elements[1].Y), elements[1]},
                { new ControlElement(elements[2].X, elements[2].Y), elements[2]}
            };

            Dictionary<ControlElement, ControlElement> window2 = new()
            {
                { new ControlElement(elements[3].X, elements[3].Y),  elements[3]},
                { new ControlElement(elements[4].X, elements[4].Y), elements[4]},
                { new ControlElement(elements[5].X, elements[5].Y), elements[5]},
                { new ControlElement (elements[6].X, elements[6].Y), elements[6]}
            };

            Dictionary<ControlElement, ControlElement> window3 = new()
            {
                { new ControlElement(elements[7].X, elements[7].Y), elements[7]},
                { new ControlElement(elements[8].X, elements[8].Y), elements[8]},
                { new ControlElement(elements[9].X, elements[9].Y), elements[9]}
            };

            collection.Push(window1);
            collection.Push(window2);
            collection.Push(window3);

            pairs = [];
            foreach (ControlElement element in elements)
            {
                Function f = new(element.Id, $"Funtion for element with id {element.Id}");
                funcs.Add(f);
                pairs.Add(new(element, f.Description));
            }
        }

        [TestMethod]
        public void LINQGetElementsFarThenNoResultTest()
        {
            Stack<Dictionary<ControlElement, ControlElement>> collection = new();
            Fill(collection, [], out _);
            var result = LINQGetElementsFarThen(collection, 1000);
            Assert.AreEqual(0, result.Count());
        }

        [TestMethod]
        public void LINQGetElementsFarThenTest()
        {
            Stack<Dictionary<ControlElement, ControlElement>> collection = new();
            int[] expected = [3, 1];
            Fill(collection, [], out _);
            var result = LINQGetElementsFarThen(collection, 9);
            int i = 0;
            foreach (var group  in result)
            {
                Assert.AreEqual(expected[i++], group.Count());
            }
        }

        [TestMethod]
        public void EXTGetElementsFarThenNoResultTest()
        {
            Stack<Dictionary<ControlElement, ControlElement>> collection = new();
            Fill(collection, [], out _);
            var result = EXTGetElementsFarThen(collection, 1000);
            Assert.AreEqual(0, result.Count());
        }

        [TestMethod]
        public void EXTGetElementsFarThenTest()
        {
            Stack<Dictionary<ControlElement, ControlElement>> collection = new();
            int[] expected = [3, 1];
            Fill(collection, [], out _);
            var result = EXTGetElementsFarThen(collection, 9);
            int i = 0;
            foreach (var group in result)
            {
                Assert.AreEqual(expected[i++], group.Count());
            }
        }

        [TestMethod]
        public void LINQGetMinDistanseInEveryWindowTest()
        {
            ControlElement[] exp = [new ControlElement(0, 0), new Button(0, 0, "Button"), new Button(0, 0, "Button")];
            Stack<Dictionary<ControlElement, ControlElement>> collection = new();
            Fill(collection, [], out _);
            var result = LINQGetMinDistanseInEveryWindow(collection);
            int i = 0;
            foreach(var element in result)
            {
                Assert.AreEqual(exp[i++], element);
            }
        }

        [TestMethod]
        public void EXTGetMinDistanseInEveryWindowTest()
        {
            ControlElement[] exp = [new ControlElement(0, 0), new Button(0, 0, "Button"), new Button(0, 0, "Button")];
            Stack<Dictionary<ControlElement, ControlElement>> collection = new();
            Fill(collection, [], out _);
            var result = EXTGetMinDistanseInEveryWindow(collection);
            int i = 0;
            foreach (var element in result)
            {
                Assert.AreEqual(exp[i++], element);
            }
        }

        [TestMethod]
        public void LINQGetMaxDistanseInEveryWindowTest()
        {
            ControlElement[] exp = [new ControlElement(8, 6), new TextField(5, 12, "NotAHint", "NotAText"), new ControlElement(8, 6)];
            Stack<Dictionary<ControlElement, ControlElement>> collection = new();
            Fill(collection, [], out _);
            var result = LINQGetMaxDistanseInEveryWindow(collection);
            int i = 0;
            foreach (var element in result)
            {
                Assert.AreEqual(exp[i++], element);
            }
        }

        [TestMethod]
        public void EXTGetMaxDistanseInEveryWindowTest()
        {
            ControlElement[] exp = [new ControlElement(8, 6), new TextField(5, 12, "NotAHint", "NotAText"), new ControlElement(8, 6)];
            Stack<Dictionary<ControlElement, ControlElement>> collection = new();
            Fill(collection, [], out _);
            var result = EXTGetMaxDistanseInEveryWindow(collection);
            int i = 0;
            foreach (var element in result)
            {
                Assert.AreEqual(exp[i++], element);
            }
        }

        [TestMethod]
        public void LINQAverageDistanceOfElementsTest()
        {
            Stack<Dictionary<ControlElement, ControlElement>> collection = new();
            Fill(collection, [], out _);
            var result = LINQAverageDistanceOfElements(collection);
            Assert.AreEqual(7, result);
        }

        [TestMethod]
        public void EXTAverageDistanceOfElementsTest()
        {
            Stack<Dictionary<ControlElement, ControlElement>> collection = new();
            Fill(collection, [], out _);
            var result = EXTAverageDistanceOfElements(collection);
            Assert.AreEqual(7, result);
        }

        [TestMethod]
        public void LINQUnicElementsTest()
        {
            Stack<Dictionary<ControlElement, ControlElement>> collection = new();
            Fill(collection, [], out _);
            var result = LINQUnicElements(collection);
            List<ControlElement> exp = [new MultButton(9, 0, "MultButton", true), new TextField(9, 0, "Hint", "Text"), new TextField(5, 12, "NotAHint", "NotAText"),
                new ControlElement(0, 0), new Button(9, 0, "Button")];
            Assert.AreEqual(exp.Count, result.Count());
            foreach (var item in result)
            {
                Assert.IsTrue(exp.Contains(item));
            }
        }

        [TestMethod]
        public void EXTUnicElementsTest()
        {
            Stack<Dictionary<ControlElement, ControlElement>> collection = new();
            Fill(collection, [], out _);
            var result = EXTUnicElements(collection);
            List<ControlElement> exp = [new MultButton(9, 0, "MultButton", true), new TextField(9, 0, "Hint", "Text"), new TextField(5, 12, "NotAHint", "NotAText"),
                new ControlElement(0, 0), new Button(9, 0, "Button")];
            Assert.AreEqual(exp.Count, result.Count());
            foreach (var item in result)
            {
                Assert.IsTrue(exp.Contains(item));
            }
        }

        [TestMethod]
        public void LINQCommonElementsTest()
        {
            Stack<Dictionary<ControlElement, ControlElement>> collection = new();
            Fill(collection, [], out _);
            var result = LINQCommonElements(collection);
            List<ControlElement> exp = [new Button(0, 0, "Button"), new Button(0, 0, "Button"), new ControlElement(8, 6), new ControlElement(8, 6), new ControlElement(8, 6)];
            Assert.AreEqual(exp.Count, result.Count());
            foreach (var item in result)
            {
                Assert.IsTrue(exp.Contains(item));
            }
        }

        [TestMethod]
        public void EXTCommonElementsTest()
        {
            Stack<Dictionary<ControlElement, ControlElement>> collection = new();
            Fill(collection, [], out _);
            var result = EXTCommonElements(collection);
            List<ControlElement> exp = [new Button(0, 0, "Button"), new Button(0, 0, "Button"), new ControlElement(8, 6), new ControlElement(8, 6), new ControlElement(8, 6)];
            Assert.AreEqual(exp.Count, result.Count());
            foreach (var item in result)
            {
                Assert.IsTrue(exp.Contains(item));
            }
        }

        [TestMethod]
        public void LINQElementAndItsFuntionTest()
        {
            Stack<Dictionary<ControlElement, ControlElement>> collection = new();
            List<Function> funcs = [];
            List<KeyValuePair<ControlElement, string>> pairs;
            Fill(collection, funcs, out pairs);
            var result = LINQElementAndItsFuntion(collection, funcs);
            Assert.AreEqual(pairs.Count, result.Count());
            foreach (var pair in result)
            {
                ControlElement val = pair.Item;
                string fn = pair.Function;
                Assert.IsTrue(pairs.Contains(new KeyValuePair<ControlElement, string>(val, fn)));
            }
        }

        [TestMethod]
        public void EXTElementAndItsFuntionTest()
        {
            Stack<Dictionary<ControlElement, ControlElement>> collection = new();
            List<Function> funcs = [];
            List<KeyValuePair<ControlElement, string>> pairs;
            Fill(collection, funcs, out pairs);
            var result = EXTElementAndItsFuntion(collection, funcs);
            Assert.AreEqual(pairs.Count, result.Count());
            foreach (var pair in result)
            {
                ControlElement val = pair.Item;
                string fn = pair.Function;
                Assert.IsTrue(pairs.Contains(new KeyValuePair<ControlElement, string>(val, fn)));
            }
        }
    }
}