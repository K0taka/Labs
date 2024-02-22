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

        public static string[] SendRequest(ControlElement[] array, Request req)
        {
            return req switch
            {
                Request.EnableMultButtonText => EnableMultButtonText(array),
                Request.ExistTextWithExistHint => ExistTextWithExistHint(array),
                Request.AllElementsAtXPos => AllElementsAtXPos(array),
                _ => throw new KeyNotFoundException("Request do not exist")
            };
        }

        private static string[] EnableMultButtonText(ControlElement[] array)
        {
            List<string> buttonsText = [];
            foreach (ControlElement element in array)
            {
                if (element is not MultButton btn)
                    continue;
                if (btn.IsEnabled)
                {
                    buttonsText.Add(btn.Text);
                }
            }
            return buttonsText.Count > 0 ?  buttonsText.ToArray() : [];
        }

        private static string[] ExistTextWithExistHint(ControlElement[] array)
        {
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
                }
            }
            return texts.Count > 0 ? texts.ToArray() : [];
        }

        private static string[] AllElementsAtXPos(ControlElement[] array)
        {
            uint x = (uint)GetIntegerAnswer("Введите x для поиска >>> ", 0, 1980);
            List<string> savedInfo = [];
            foreach (ControlElement element in array)
            {
                if (element.X == x)
                {
                    savedInfo.Add(element.ToString());
                }
            }
            return savedInfo.Count > 0 ? savedInfo.ToArray() : [];
        }
    }
}
