using Lab10Lib;
using static Lab10Lib.IO;
using static Lab10Lib.Requests;
namespace ver1
{
    internal class Program
    {
        static void Main()
        {
            //демонстрация разницы меджду virtual и обычными методами

            ControlElement[] elements = CreateArray(); //инициализация массива со случайными элементами внутри

            for (int index = 0; index < elements.Length; index++)
            {
                WriteLine($"Элемент {index+1}");
                WriteLine(elements[index].ToString());//виртуальный метод
                elements[index].Show();//обычный метод
                EmptyLine();
            }
            WaitAnyButton();//ожидание нажатие любой кнопки перед продолжением работы проги

            Clear();
            //демонстрация работы id

            elements[0] = new ControlElement();//заменяем первый по счету элемент
            elements[0].RandomInit();//заполняем его случайными значениями
            elements[0].Show();//между обычным методом и vitual в данном случае нет разницы, так как для ControlElement они показывают одинаковый результат.
            CollectGarbige();//собираем мусор из памяти
            elements[1] = new Button();
            elements[1].RandomInit();
            elements[1].Show();
            WaitAnyButton();

            Clear();
            //демотнрация запросов
            //зададим несколько объект с одинаковыми X:
            uint x = elements[0].X;
            WriteLine($"Координата X с множеством объектов: {x}");
            for (int index = 1; index < 5; index++)
            {
                elements[index].X = x;
            }

            //выполнение запросов
            string[] EnableMutiBtnText = SendRequest(elements, Request.EnableMultButtonText, out bool isEMBTFound);
            string[] AllElementsAtXPos = SendRequest(elements, Request.AllElementsAtXPos, out bool isAEAXFound);
            string[] ExistTextWithExistHint = SendRequest(elements, Request.ExistTextWithExistHint, out bool isETWEHFound);

            WriteLine("Текущий массив выглядит так:");
            for (int index = 0; index < elements.Length; index++)
            {
                WriteLine($"Элемент {index + 1}:: {elements[index]}");
            }

            EmptyLine();
            WriteLine("Запрос на получение всего текста у включенных кнопок");
            if (isEMBTFound)
                ShowArray(EnableMutiBtnText);
            else
                WriteLine(EnableMutiBtnText[0]);

            EmptyLine();
            WriteLine("Запрос на получение всего существующего текста у текстовых полей с непустыми подсказками");
            if (isETWEHFound)
                ShowArray(ExistTextWithExistHint);
            else
                WriteLine(ExistTextWithExistHint[0]);

            EmptyLine();
            WriteLine("Запрос на получение информации о всех элементах на позиции X");
            if (isAEAXFound)
                ShowArray(AllElementsAtXPos);
            else
                WriteLine(AllElementsAtXPos[0]);
            WaitAnyButton();

            Clear();

            //Clone
            elements = new Button[2];
            CollectGarbige();
            elements[0] = new Button();
            elements[0].RandomInit();
            elements[1] = (Button)elements[0].Clone();

            ((Button)elements[1]).Text = "Другой текст!!!";

            foreach (Button button in elements.Cast<Button>())
            {
                WriteLine(button.ToString());
            }

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

        static void ShowArray(string[] array)
        {
            for (int index = 0; index < array.Length; index++)
            {
                WriteLine($"Элемент {index+1}:: {array[index]}");
            }
        }
    }
}
