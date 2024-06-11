using Lab10Lib;
using lab;

namespace Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void FindCommonElementsBetweenWindowsTest()
        {
            Stack<Dictionary<ControlElement, ControlElement>> app = new();

            Dictionary<ControlElement, ControlElement> window1 = new()
            {
                { new ControlElement(0, 0), new Button(0, 0, "Button") },
                { new ControlElement(1, 0), new MultButton(1, 0, "MultButton", true)},
                { new ControlElement(5, 5), new ControlElement(5, 5)}
            };

            Dictionary<ControlElement, ControlElement> window2 = new()
            {
                { new ControlElement(0, 0), new Button(0, 0, "Button") },
                { new ControlElement(1, 0), new TextField(1, 0, "Hint", "Text")},
                { new ControlElement(5, 5), new ControlElement(5, 5)}
            };

            Dictionary<ControlElement, ControlElement> window3 = new()
            {
                { new ControlElement(0, 0), new ControlElement(0, 0) },
                { new ControlElement(1, 0), new Button(1, 0, "Button")},
                { new ControlElement(5, 5), new ControlElement(5, 5)}
            };

            app.Push(window1);
            app.Push(window2);
            app.Push(window3);

            Part1.LINQCommonElements(app);
        }
    }
}