using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ver1
{
    internal class Menu
    {
        private readonly string[] _menuObj;
        private byte _userAnswer;

        public Menu(string[] menuObj) { _menuObj = menuObj; }

        public string[] MenuObj { get { return _menuObj; } }

        public byte UserAnswer { get { return _userAnswer; } }

        public void ShowMenu()
        {
            for (byte menuNum = 1; menuNum <= _menuObj.Length; menuNum++)
            {
                Console.WriteLine($"{menuNum}. {_menuObj[menuNum - 1]}");
            }
        }

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

        public byte SetUserAnswer()
        {
            ReadUserAnswer();
            return _userAnswer;
        }

        private void IsExit()
        {
            if (_menuObj[_userAnswer - 1] == _menuObj[_menuObj.Length - 1])
            {
                Console.WriteLine("Работы программы прекращена!");
                Environment.Exit(0);
            }
        }
    }
}
