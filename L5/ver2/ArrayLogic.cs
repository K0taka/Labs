namespace ver2
{
    internal class ArrayLogic
    {
        private Array _userArray = Array.Empty<int>(); //поле для хранения массива
        private readonly Type[] _types = [typeof(int[]), typeof(int[,]), typeof(int[][])];//поддерживаемые типы для массива
        private readonly Random _rand = new(); //инициализируем генератор случайных чисел

        /// <summary>
        /// Геттер для поля массива
        /// </summary>
        /// <returns>Текущее значение массива</returns>
        public Array GetArray() => _userArray;

        /// <summary>
        /// Инициализирует в поле одномерный массив
        /// </summary>
        /// <param name="len"> Длина одномерного массива</param>
        public void CreateSingleDimArray(int len) => _userArray = new int[len];

        /// <summary>
        /// Инициализирует двумерный массив в поле
        /// </summary>
        /// <param name="rows">Количество рядов в массиве</param>
        /// <param name="coloums">Количество колонок в массиве</param>
        public void CreateMultiDimArray(int rows, int coloums) => _userArray = new int[rows, coloums];

        /// <summary>
        /// Инициализирует рваный массив в поле
        /// </summary>
        /// <param name="rows">Количество рядов в массиве</param>
        public void CreateMultiDimArray(int rows) => _userArray = new int[rows][];

        /// <summary>
        /// Устанавливает элемент на позицию в одномерном массиве
        /// </summary>
        /// <param name="elem">Элемент для установки</param>
        /// <param name="index">Место для установки</param>
        public void SetElement(int elem, int index) => _userArray.SetValue(elem, index);

        /// <summary>
        /// Устанавливает значение на место в двумерном массиве
        /// </summary>
        /// <param name="elem">Элемент который необходимо установить</param>
        /// <param name="row">Ряд в котором необходимо установить</param>
        /// <param name="coloum">Колонка в которой необходимо установить</param>
        public void SetElement(int elem, int row, int coloum) => _userArray.SetValue(elem, row, coloum);

        /// <summary>
        /// Устанавилвает ссылку на переданный массив на позицию в рваном массиве
        /// </summary>
        /// <param name="elem">Массив, ссылку на который нужно установить ссылку</param>
        /// <param name="row">Место ссылки</param>
        public void SetElement(int[] elem, int row) => _userArray.SetValue(elem, row);

        /// <summary>
        /// Инициализирует случайное заполнение массиве
        /// </summary>
        public void RandomFillArray() => RandomFillArray(_userArray.GetType());

        /// <summary>
        /// Определяет как именно заполнить массив согласно его типу
        /// </summary>
        /// <param name="type">Принимает текущий тип массива</param>
        private void RandomFillArray(Type type)
        {
            switch (Array.IndexOf(_types, type))//получаем индекс текущего типа, согласно ему заполняем массив
            {
                case 0:
                    RandomFillSingleDimArray();
                    break;
                case 1:
                    RandomFillMultiDimArray();
                    break;
                case 2:
                    RandomFillJaggedArray();
                    break;
            }
        }

        /// <summary>
        /// Заполняем случайно одномерный массив
        /// </summary>
        private void RandomFillSingleDimArray()
        {
            for (int index = 0; index < _userArray.GetLength(0); index++)
            {
                _userArray.SetValue(_rand.Next(-999, 10000), index);//случайное значение от -128 до 127 включ. на позиии index
            }
        }

        /// <summary>
        /// Заполняем случайно двумерный массив
        /// </summary>
        private void RandomFillMultiDimArray()
        {
            for (int row = 0; row < _userArray.GetLength(0); row++)
            {
                for (int coloum = 0; coloum < _userArray.GetLength(1); coloum++)
                {
                    _userArray.SetValue(_rand.Next(-999, 10000), row, coloum);//случайное значение от -128 до 127 включ. на позиии ROW x COLOUMN
                }
            }
        }

        /// <summary>
        /// Случайным образом заполняет рваный массив
        /// </summary>
        private void RandomFillJaggedArray()
        {
            int[] GenerateArray()
            {
                int[] result = new int[_rand.Next(0, 17)];//длина случайного массива от 1 до 10
                for (int index = 0; index <  result.Length; index++)
                {
                    result[index] = _rand.Next(-999, 10000);//заполняем этот массив случайными элементами
                }
                return result;
            }

            for (int row = 0; row < _userArray.GetLength(0); row++)
            {
                _userArray.SetValue(GenerateArray(), row);//устанавливает ссылку на случайно сгенерированный массив
            }
        }

        /// <summary>
        /// Инициализирует выполнение задачи
        /// </summary>
        /// <param name="extention">Расширение для двумерного массива</param>
        public void DoTask(int[,]? extention = null) => DoTask(_userArray.GetType(), extention);

        /// <summary>
        /// Выбирает соотв. задачу согласно типу массива
        /// </summary>
        /// <param name="type">Тип массива</param>
        /// <param name="extention">Расширение для двумерного массива</param>
        private void DoTask(Type type, int[,]? extention)
        {
            if (!IsCorrectArray() && Array.IndexOf(_types, _userArray.GetType()) != 1)
                return;//если массив не инициализирован, то прекратить
            switch (Array.IndexOf(_types, type))
            {
                case 0:
                    _userArray = DoTask(new int[_userArray.GetLength(0) - _userArray.GetLength(0) / 2]);//передаем массив для записи ответа
                    return;
                case 1:
                    if (extention != null)
                        _userArray = DoTask(new int[_userArray.GetLength(0), _userArray.GetLength(1) + extention.GetLength(1)], extention);//передаем массив для записи ответа
                    return;
                case 2:
                    _userArray = DoTask(new int[_userArray.GetLength(0) - _userArray.GetLength(0) / 2][]);//передаем массив для записи ответа
                    return;
            }
        }

        /// <summary>
        /// Выполнение задачи для одномерного массива
        /// </summary>
        /// <param name="result">Массив для записи результата</param>
        /// <returns>Возвращает заполненный массив результата</returns>
        private int[] DoTask(int[] result)
        {
            for (int newIndex = 0, oldIndex = 0; newIndex < result.Length; newIndex++, oldIndex += 2)
            {
                result[newIndex] = (int)_userArray.GetValue(oldIndex)!;//копируем нужные элементы в новый массив
            }
            return result;
        }

        /// <summary>
        /// Выполнение задачи для двумерного массива
        /// </summary>
        /// <param name="result">Массив для записи результата</param>
        /// <param name="extention">Расширение массива</param>
        /// <returns>Заполненный массив результата</returns>
        private int[,] DoTask(int[,] result, int[,] extention)
        {
            for (int row = 0; row < extention.GetLength(0); row++)
            {
                for (int coloumn = 0; coloumn < extention.GetLength(1); coloumn++)
                {
                    result[row, coloumn] = extention[row,coloumn];//заполняем первые столбцы расширением
                }
            }
            for (int row = 0; row < result.GetLength(0); row++)
            {
                for (int resCol = extention.GetLength(1), usCol = 0;  resCol < result.GetLength(1); resCol++, usCol++)
                {
                    result[row, resCol] = (int)_userArray.GetValue(row, usCol)!;//копируем оставшийся массив
                }
            }
            return result;
        }

        /// <summary>
        /// Задача для рваного массива
        /// </summary>
        /// <param name="result">Массив для записи результата</param>
        /// <returns>Возвращает заполненный массив</returns>
        private int[][] DoTask(int[][] result)
        {
            for (int oldIndex = 0, newIndex = 0; newIndex < result.GetLength(0); oldIndex+=2, newIndex++)
            {
                int[] array = (int[])_userArray.GetValue(oldIndex)!;
                int[] newArray = new int[array.Length];
                array.CopyTo(newArray, 0);
                result[newIndex] = newArray;//копируем необходимые элементы
            }
            return result;
        }

        /// <summary>
        /// Функция для иницилизации вывода массива
        /// </summary>
        public void PrintArray() => PrintArray(_userArray.GetType());

        /// <summary>
        /// Выбор функции печати согласно типу массива
        /// </summary>
        /// <param name="type">Тип текущего массива</param>
        private void PrintArray(Type type)
        {
            if (!IsCorrectArray())//Вывод на не пустой или не инициализированный массив
            {
                Console.WriteLine("Массив пуст!");
                if (type == typeof(int[,]))
                {
                    Console.WriteLine($"И содержит в себе {_userArray.GetLength(0)} строчек, {_userArray.GetLength(1)} колонок");
                    if (_userArray.GetLength(0) == 0)
                        Console.WriteLine("Дальйшее добавление колонок бессмысленно с точки зрения условия, массив останется пуст, так как по заданию увеличиваются колонки без изменения количества строк");
                }
                return;
            }
            switch(Array.IndexOf(_types, type))
            {
                case 0:
                    Console.WriteLine("Одномерный массив в данный момент выглядит так:");
                    PrintSingleDimArray((int[])_userArray);//печатаем одномерный массив
                    Console.WriteLine();
                    break;
                case 1:
                    Console.WriteLine("Двумерный массив в данный момент выглядит так:");
                    PrintMultiDimArray();//печатаем двумерный массив
                    Console.WriteLine();
                    break;
                case 2:
                    Console.WriteLine("Рваный массив в данный момент выглядит так:");
                    PrintJaggedArray();//печатаем рваный массив
                    Console.WriteLine();
                    break;
            }
        }

        /// <summary>
        /// Печать одномерного массива
        /// </summary>
        /// <param name="array">Одномерный массив для печати</param>
        private static void PrintSingleDimArray(int[] array)
        {
            int len = array.Length * 7 + 1;
            string[] separator = new string[len];
            Array.Fill(separator, "_");
            PrintSeparator(separator);
            Console.Write("\n|");
            foreach (int elem in array)
            {
                Console.Write($" {elem, 4} |");
            }
            Console.WriteLine();
            Array.Fill(separator, "\u00AF");
            PrintSeparator(separator);
            Console.WriteLine();
        }

        /// <summary>
        /// Выводить разделитеь на экран
        /// </summary>
        /// <param name="sep">Массив с элементами разделителя</param>
        static void PrintSeparator(string[] sep)
        {
            foreach (string el in sep)
            {
                Console.Write(el);
            }
        }

        /// <summary>
        /// Печать двумерного массива
        /// </summary>
        private void PrintMultiDimArray()
        {
            int len = _userArray.GetLength(1) * 7 - 1;
            string[] separator = new string[len];
            Array.Fill(separator, "-");
            Console.Write("|");
            PrintSeparator(separator);
            Console.Write("|");
            for (int row = 0; row < _userArray.GetLength(0); row++)
            {
                Console.Write("\n|");
                for (int col = 0; col < _userArray.GetLength(1); col++)
                {
                    Console.Write($" {_userArray.GetValue(row, col), 4} |");
                }
                Console.Write("\n|");
                PrintSeparator(separator);
                Console.Write("|");
            }
            Console.WriteLine();
        }

        /// <summary>
        /// Печать рваного массива
        /// </summary>
        private void PrintJaggedArray()
        {
            for (int row = 0; row < _userArray.GetLength(0); row++)
            {
                int[] currLine = (int[])_userArray.GetValue(row)!;
                if (currLine.Length == 0 )
                    Console.WriteLine("<Здесь находится массив длины 0>");
                else
                    PrintSingleDimArray(currLine);//печатаем одномерный массив по ссылке из рваного если он не пустой
            }
        }
        
        /// <summary>
        /// Проверка на инициализацию и заполненность массива
        /// </summary>
        /// <returns>Логическое значение о состоянии массива</returns>
        public bool IsCorrectArray()
        {
            return _userArray != null && _userArray.Length != 0;
        }
    }
}
