using static IOLib.IO;
using System.Text.RegularExpressions;

namespace Lab10Lib
{
    public class Requests
    {
        /// <summary>
        /// Доступные для выполнения запросы
        /// </summary>
        public enum Request
        {
            /// <summary>
            /// Запрос для получения текста от всех включенных мульти-кнопок
            /// </summary>
            EnableMultButtonText,

            /// <summary>
            /// Запрос для получения непустого текста у всех текстовых полей с непустой подсказкой
            /// </summary>
            ExistTextWithExistHint,

            /// <summary>
            /// Запрос для получения всех элементов управления на указанной позиции X
            /// </summary>
            AllElementsAtXPos
        }

        /// <summary>
        /// Выполнить указанный запрос в указанном массиве
        /// </summary>
        /// <param name="array">Массив в котором выполяется запрос</param>
        /// <param name="req">Необходимый запрос</param>
        /// <returns>Строковый массив с результатам выполнения запроса</returns>
        /// <exception cref="KeyNotFoundException">Запрос указан неверно</exception>
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

        /// <summary>
        /// Получает весь теест у включенных мульти-кнопок
        /// </summary>
        /// <param name="array">Массив в котором осуществляется поиск</param>
        /// <returns>Строковый массив найденнх результатов</returns>
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

        /// <summary>
        /// Получает весь непустой текст у текстовых полей с непустой подсказкой
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Получение всех элементов на позиции X
        /// </summary>
        /// <param name="array">Массив для поиска значений</param>
        /// <returns>Строковый массив найденных значений</returns>
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
