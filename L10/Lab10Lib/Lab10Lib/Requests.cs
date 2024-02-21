using System.Text.RegularExpressions;

namespace Lab10Lib
{
    public class Requests
    {
        public enum Request
        {
            EnableMultButtonText,
            ExistTextWithExistHint,
            AllElementsAtXPos
        }

        public static string[] SendRequest(ControlElement[] array, Request req, out bool isFound)
        {
            return req switch
            {
                Request.EnableMultButtonText => EnableMultButtonText(array, out isFound),
                Request.ExistTextWithExistHint => ExistTextWithExistHint(array, out isFound),
                Request.AllElementsAtXPos => AllElementsAtXPos(array, out isFound),
                _ => throw new KeyNotFoundException("Request do not exist")
            };
        }

        private static string[] EnableMultButtonText(ControlElement[] array, out bool isFound)
        {
            isFound = false;
            List<string> buttonsText = [];
            foreach (ControlElement element in array)
            {
                if (element is not MultButton btn)
                    continue;
                if (btn.IsEnabled)
                {
                    buttonsText.Add(btn.Text);
                    isFound = true;
                }
            }
            return buttonsText.Count > 0 ?  buttonsText.ToArray() : ["Нет таких кнопок"];
        }

        private static string[] ExistTextWithExistHint(ControlElement[] array, out bool isFound)
        {
            isFound = false;
            Regex NotEmpty = new(@"\S+");
            List<string> texts = [];
            foreach(ControlElement element in array)
            {
                TextField? textField = element as TextField;
                if (textField == null)
                    continue;
                if (textField.Hint != null && NotEmpty.IsMatch(textField.Hint) && textField.Text != null && NotEmpty.IsMatch(textField.Text))
                {
                    texts.Add(textField.Text);
                    isFound = true;
                }
            }
            return texts.Count > 0 ? texts.ToArray() : ["Нед удовлетворяющих запросу текстовых полей"];
        }

        private static string[] AllElementsAtXPos(ControlElement[] array, out bool isFound)
        {
            isFound = false;
            uint x = (uint)GetIntegerAnswer("Введите x для поиска >>> ", 0, 1980);
            List<string> savedInfo = [];
            foreach (ControlElement element in array)
            {
                if (element.X == x)
                {
                    savedInfo.Add(element.ToString());
                    isFound = true;
                }
            }
            return savedInfo.Count > 0 ? savedInfo.ToArray() : ["Нет элементов по заданной координате X"];
        }
    }
}
