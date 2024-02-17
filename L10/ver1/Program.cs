using Lab10Lib;

namespace ver1
{
    internal class Program
    {
        static void Main()
        {
            ControlElement[] elements = CreateArray();

            foreach (ControlElement element in elements)
            {
                IO.WriteLine(element.ToString());
                element.Show();
            }
            IO.WaitAnyButton();

            elements[0] = new ControlElement();
            elements[0].RandomInit();
            elements[0].Show();
            CollectGarbige();
            elements[1] = new ControlElement();
            elements[1].RandomInit();
            elements[1].Show();

        }

        static ControlElement[] CreateArray()
        {
            ControlElement[] elements = new ControlElement[20];
            for (int i = 0; i < elements.Length; i++)
            {
                switch (i % 4)
                {
                    case 0:
                        elements[i] = new ControlElement();
                        elements[i].RandomInit();
                        break;
                    case 1:
                        elements[i] = new Button();
                        elements[i].RandomInit();
                        break;
                    case 2:
                        elements[i] = new MultButton();
                        elements[i].RandomInit();
                        break;
                    case 3:
                        elements[i] = new TextField();
                        elements[i].RandomInit();
                        break;
                }
            }
            return elements;
        }

        static void CollectGarbige()
        {
            GC.Collect();
            Task.Delay(100);
        }
    }
}
