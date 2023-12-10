using System;

namespace ver1
{
    internal class ArrayLogic
    {
        private Array _userArray;
        private readonly string[] _types = new string[] {"Int32[]", "Int32[,]", "Int32[][]"};
        private readonly Random _rand = new Random();

        public Array GetArray() { return _userArray; }

        public void CreateSingleDimArray(int len)
        {
            _userArray = new int[len];
        }

        public void CreateMultiDimArray(int rows, int coloums)
        {
            _userArray = new int[rows,coloums];
        }

        public void CreateMultiDimArray(int rows)
        {
            _userArray = new int[rows][];
        }

        public void SetElement(int elem, int index)
        {
            _userArray.SetValue(elem, index);
        }

        public void SetElement(int elem, int row, int coloum)
        {
            _userArray.SetValue(elem, row, coloum);
        }

        public void SetElement(int[] elem, int row)
        {
            _userArray.SetValue(elem, row);
        }

        public void RandomFillArray() { RandomFillArray(_userArray.GetType()); }

        private void RandomFillArray(Type type)
        {
            switch(Array.IndexOf(_types, type.Name))
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

        private void RandomFillSingleDimArray()
        {
            for (int index = 0; index < _userArray.GetLength(0); index++)
            {
                _userArray.SetValue(_rand.Next(-128, 128), index);
            }
        }

        private void RandomFillMultiDimArray()
        {
            for (int row = 0; row < _userArray.GetLength(0); row++)
            {
                for (int coloum = 0; coloum < _userArray.GetLength(1); coloum++)
                {
                    _userArray.SetValue(_rand.Next(-128, 128), row, coloum);
                }
            }
        }

        private void RandomFillJaggedArray()
        {
            int[] GenerateArray()
            {
                int[] result = new int[_rand.Next(1, 10)];
                for (int index = 0; index <  result.Length; index++)
                {
                    result[index] = _rand.Next(-128, 128);
                }
                return result;
            }

            for (int row = 0; row < _userArray.GetLength(0); row++)
            {
                _userArray.SetValue(GenerateArray(), row);
            }
        }


        public void DoTask(int[,] extention = null) { DoTask(_userArray.GetType(), extention); }

        private void DoTask(Type type, int[,] extention)
        {
            if (!IsCorrectArray() && Array.IndexOf(_types, _userArray.GetType().Name) != 1)
                return;
            switch (Array.IndexOf(_types, type.Name))
            {
                case 0:
                    _userArray = DoTask(new int[_userArray.GetLength(0) - _userArray.GetLength(0) / 2]);
                    return;
                case 1:
                    if (extention != null)
                        _userArray = DoTask(new int[_userArray.GetLength(0), _userArray.GetLength(1) + extention.GetLength(1)], extention);
                    return;
                case 2:
                    _userArray = DoTask(new int[_userArray.GetLength(0) - _userArray.GetLength(0) / 2][]);
                    return;
            }
        }

        private int[] DoTask(int[] result)
        {
            for (int newIndex = 0, oldIndex = 0; newIndex < result.Length; newIndex++, oldIndex += 2)
            {
                result[newIndex] = (int)_userArray.GetValue(oldIndex);
            }
            return result;
        }

        private int[,] DoTask(int[,] result, int[,] extention)
        {
            for (int row = 0; row < extention.GetLength(0); row++)
            {
                for (int coloumn = 0; coloumn < extention.GetLength(1); coloumn++)
                {
                    result[row, coloumn] = extention[row,coloumn];
                }
            }
            for (int row = 0; row < result.GetLength(0); row++)
            {
                for (int resCol = extention.GetLength(1), usCol = 0;  resCol < result.GetLength(1); resCol++, usCol++)
                {
                    result[row, resCol] = (int)_userArray.GetValue(row, usCol);
                }
            }
            return result;
        }

        private int[][] DoTask(int[][] result)
        {
            for (int oldIndex = 0, newIndex = 0; newIndex < result.GetLength(0); oldIndex+=2, newIndex++)
            {
                result[newIndex] = (int[])_userArray.GetValue(oldIndex);
            }
            return result;
        }

        public void PrintArray() { PrintArray(_userArray.GetType()); }

        private void PrintArray(Type type)
        {
            if (!IsCorrectArray())
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
            switch(Array.IndexOf(_types, type.Name))
            {
                case 0:
                    Console.WriteLine("Одномерный массив в данный момент выглядит так:");
                    PrintSingleDimArray((int[])_userArray);
                    Console.WriteLine();
                    break;
                case 1:
                    Console.WriteLine("Двумерный массив в данный момент выглядит так:");
                    PrintMultiDimArray();
                    Console.WriteLine();
                    break;
                case 2:
                    Console.WriteLine("Рваный массив в данный момент выглядит так:");
                    PrintJaggedArray();
                    Console.WriteLine();
                    break;
            }
        }

        private void PrintSingleDimArray(int[] array)
        {
            foreach(int elem in array)
            {
                Console.Write($"{elem} ");
            }
            Console.WriteLine();
        }

        private void PrintMultiDimArray()
        {
            for (int row = 0; row < _userArray.GetLength(0); row++)
            {
                for (int col = 0; col < _userArray.GetLength(1); col++)
                {
                    Console.Write($"{_userArray.GetValue(row, col)} ");
                }
                Console.Write('\n');
            }
        }

        private void PrintJaggedArray()
        {
            for (int row = 0; row < _userArray.GetLength(0); row++)
            {
                int[] currLine = (int[])_userArray.GetValue(row);
                if (currLine.Length == 0 )
                    Console.WriteLine("<Здесь находится массив длины 0>");
                else
                    PrintSingleDimArray(currLine);
            }
        }
        
        public bool IsCorrectArray()
        {
            return _userArray != null && _userArray.Length != 0;
        }
    }
}
