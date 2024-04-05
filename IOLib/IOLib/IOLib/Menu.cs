namespace IOLib
{
    public class Menu(string[] menuObj)
    {
        private readonly string[] _menuObj = [..menuObj, "Выход"]; //поле для объектов меню
        private byte _userAnswer; //поле для ответа пользователя

        /// <summary>
        /// Геттер для поля MenuOnj
        /// </summary>
        public string[] MenuObj => _menuObj;

        /// <summary>
        /// Геттер для поля _userAnswer
        /// </summary>
        public byte UserAnswer => _userAnswer;

        /// <summary>
        /// Вывести объекты меню на экран
        /// </summary>
        public void ShowMenu()
        {
            for (byte menuNum = 1; menuNum <= _menuObj.Length; menuNum++)
            {
                IO.WriteLine($"{menuNum}. {_menuObj[menuNum - 1]}");
            }
        }

        /// <summary>
        /// Получить ответ от пользователя с номером желаемого пункта меню
        /// </summary>
        private void ReadUserAnswer()
        {
            bool isCorrect;//флаг для повторного запроса по необходимости
            do
            {
                isCorrect = byte.TryParse(IO.GetTextAnswer("Введите номер желаемой задачи >>> "), out _userAnswer); //получаем ответ от пользователя
                if (_userAnswer < 1 | _userAnswer > _menuObj.Length) //ответ д. находиться в диапазоне исп. меню
                {
                    isCorrect = false;
                }
                if (!isCorrect)
                {
                    IO.WriteLine("Вы ввели элемент, не относящийся к текущему меню");
                }
            } while (!isCorrect);
            IsExit();
        }

        /// <summary>
        /// Начать получение ответа от пользователя
        /// </summary>
        /// <returns>Вернуть ответ пользователя</returns>
        public byte SetUserAnswer()
        {
            ReadUserAnswer();
            return _userAnswer;
        }

        /// <summary>
        /// Проверка является ли ответ выходом из программы
        /// </summary>
        private void IsExit()
        {
            if (_menuObj[_userAnswer - 1] == _menuObj[^1])
            {
                IO.WriteLine("Работы программы прекращена!");
                Environment.Exit(0);
            }
        }
    }
}
