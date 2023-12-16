namespace ver1
{
    internal class Menu(string[] menuObj)
    {
        private readonly string[] _menuObj = menuObj; //поле для объектов меню
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
                Console.WriteLine($"{menuNum}. {_menuObj[menuNum - 1]}");
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
                Console.Write("Введите номер желаемой задачи >>> ");
                isCorrect = byte.TryParse(Console.ReadLine(), out _userAnswer); //получаем ответ от пользователя
                if (_userAnswer < 1 | _userAnswer > _menuObj.Length) //ответ д. находиться в диапазоне исп. меню
                {
                    isCorrect = false;
                }
                if (!isCorrect)
                {
                    Console.WriteLine("Вы ввели элемент, не относящийся к текущему меню");
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
                Console.WriteLine("Работы программы прекращена!");
                Environment.Exit(0);
            }
        }
    }
}
