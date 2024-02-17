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
                    buttonsText.Add(btn.Text);
            }
            return buttonsText.ToArray();
        }

        private static string[] ExistTextWithExistHint(ControlElement[] array)
        {
            Regex NotEmpty = new(@"\S+");
            List<string> texts = [];
            foreach(ControlElement element in array)
            {
                if (element is not TextField textField)
                    continue;
                if(textField.Hint != null && NotEmpty.IsMatch(textField.Hint) && textField.Text != null && NotEmpty.IsMatch(textField.Text))
                    texts.Add(textField.Text);
            }
            return texts.ToArray();
        }

        private static string[] AllElementsAtXPos(ControlElement[] array)
        {

        }
    }
}
