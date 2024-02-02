using System;

namespace ver2
{
    internal class ArrayLogic
    {
        private int[] _userArray;
        private readonly Random _rand = new Random();

        public int Length { get { return _userArray.Length; } }

        public void ManualArrayInit(int arrayLen)
        {
            _userArray = new int[arrayLen];
        }
        
        public void SetElement(int index, int element)
        {
            _userArray[index] = element;
        }

        public void RandomArrayWithN(int arrayLen)
        {
            _userArray = new int[arrayLen];
            for (int index = 0; index < arrayLen; index++)
            {
                _userArray[index] = _rand.Next(-128, 128);
            }
        }

        public void RandomArrayWithoutN()
        {
            _userArray = new int[_rand.Next(1,256)];
            for (int index = 0; index < _userArray.Length; index++)
            {
                _userArray[index] = _rand.Next(-128, 128);
            }
        }

        public int FindMaxElements(out int numMax)
        {
            int maxN = _userArray[0];//назначаем максимумом первый элемент массива
            numMax = 0;//количество максимумов
            foreach (int elem in _userArray)//перебор всех элементов массива
            {
                if (maxN < elem)//сравнение с максимумом, если найден элемент больший текущего максимума - обновим счетчик и значение максимума
                {
                    numMax = 1;
                    maxN = elem;
                }
                else if (maxN == elem)//если найденный элемент равен максимуму - увеличим счетчик
                {
                    numMax += 1;
                }
            }
            return maxN;
        }

        public void DeleteMaxElements(int maxN, int numMax)
        {
            int[] answerArray = new int[_userArray.Length - numMax];//создаем массив с размерностью меньше на количество максимумов
            int index = 0;//запоминаем индекс для продолжения копирования массива
            for (int newElem = 0; newElem < _userArray.Length - numMax; newElem++)
            {
                for (int elem = index; elem < _userArray.Length; elem++)
                {
                    if (_userArray[elem] != maxN)
                    {
                        answerArray[newElem] = _userArray[elem];//копируем значение если оно не равно максимуму
                        index = elem + 1;//запоминаем место для продолжения
                        break;//переходим на следующую ячейку нового массива
                    }
                }
            }
            _userArray = answerArray;
        }

        public void ExtendArray(int extension)
        {
            int[] tempArray = new int[_userArray.Length + extension];
            for (int index = 0; index < _userArray.Length; index++)
            {
                tempArray[index] = _userArray[index];
            }
            _userArray = tempArray;
        }

        public void MoveLeft(int slide)
        {
            int[] tempArray = new int[slide];//массив для хранения сдвига
            for (int elem = 0; elem < slide; elem++)
            {
                tempArray[elem] = _userArray[elem];//сохраняем сдвиг в вспомагательном массиве
            }
            for (int elem = slide; elem < _userArray.Length; elem++)
            {
                _userArray[elem - slide] = _userArray[elem];//передвижение всех элементов
            }
            for (int elem = _userArray.Length - slide; elem < _userArray.Length; elem++)
            {
                _userArray[elem] = tempArray[slide - _userArray.Length + elem];//возвращаем на последние позиции элементы из начала
            }
        }

        public void FindFirstNegative()
        {
            int comps = 0;//количество сравнений
            bool isFound = false;
            foreach (int elem in _userArray)//цикл по всем элементам массива подряд
            {
                comps++;//увеличиваем количество сравнений
                if (elem < 0) //при нахождении - выведем элемент на экран
                {
                    Console.WriteLine($"Первый отрицательный элемент массива: {elem}");
                    isFound = true;//меняем флаг о нахождении
                    break;
                }
            }
            if (!isFound)//если ничего не нашли напишем об этом
            {
                Console.WriteLine("В массиве нет отрицательных элементов");
            }
            Console.WriteLine($"Операция сравнения была применена {comps} раз");//выведем количество совершенных сравнений
        }

        public void BubbleSorting()
        {
            int length = _userArray.Length;//текущая длина сортируемой последовательности
            while (length > 0)//выполняем до тех пор пока не пройдем по всей длине
            {
                int maxIndex = 0;//текущее положение максимального элемента
                for (int rPointer = 1; rPointer < length; rPointer++)//проходить будем парами, поэтому первый элемент единица
                {
                    if (_userArray[rPointer - 1] > _userArray[rPointer])//меняем элементы местами, если предыдущий больше текущего
                    {
                        (_userArray[rPointer - 1], _userArray[rPointer]) = (_userArray[rPointer], _userArray[rPointer - 1]);//для смены порядка следования используем кортеж
                        maxIndex = rPointer;//обновляем индекс максимального (это будет самое левое значение отсортированной части)
                    }
                }
                length = maxIndex; //обновляем длину рассматриваемой части
            }
        }

        public void FastSortingInit(int sPos, int ePos)
        {
            if (sPos >= ePos)//прерываем рекурсию при если позиция начала и позиция конца совпали
                return;
            int newPos = FastSortingIteration(sPos, ePos); //получаем новую позицию - положение отсортированных относительно друг друга подмассивов
            FastSortingInit(sPos, newPos - 1);//рекурсивно вызываем инициализацию сортировка для каждого из подмассивов пока не придем к тому
            FastSortingInit(newPos, ePos);//что сортируемая последовательность будет из 1 элемента или пуста
        }

        private int FastSortingIteration(int lPointer, int rPointer)
        {
            int pillar = _userArray[(lPointer + rPointer) / 2]; //находим опорное значение для сравнения
            while (lPointer <= rPointer) //в цикле идем пока указатели не встретятся
            {
                while (_userArray[lPointer] < pillar) //ставим левый указатель на значение >= опорному
                    lPointer++;
                while (_userArray[rPointer] > pillar)//а правый на значение <= опорному
                    rPointer--;
                if (lPointer <= rPointer)//если указатели не сравнялись - меняем местами значения под указателями
                {
                    (_userArray[lPointer], _userArray[rPointer]) = (_userArray[rPointer], _userArray[lPointer]);
                    lPointer++;//сдвигаем левый указатель вправо
                    rPointer--;//а правый указатель влево
                }
            }
            return lPointer; //после выхода из цикла левый указатель обозначает начало подпоследовательности, в которой элементы >= опорного. Вернем его
        }

        public bool SortCheck()
        {
            bool isSorted = true;//начальное значение отсортированности - true
            for (int index = 1; index < _userArray.Length; index++)
            {
                if (_userArray[index] < _userArray[index - 1])
                {
                    isSorted = false;//как только находим пару предыдущее > текущее - массив не отсортирован
                    break;//уже можно прервать проверку
                }
            }
            return isSorted;
        }

        public int BinSearch(int searchKey, out int compares)
        {
            int lPointer = 0, rPointer = _userArray.Length - 1; //установка левого и правого указателей в начальные позиции
            compares = 0;//количество сравнений
            while (lPointer <= rPointer)
            {
                compares += 1;//увеличиваем количество сравнений
                int midPointer = (lPointer + rPointer) / 2;//находим текущее центральное положение
                if (_userArray[midPointer] == searchKey)//определяем равны ли цетральный элемент и искомый
                    return midPointer + 1;
                else if (_userArray[midPointer] < searchKey)//смотрим в какую сторону от центрального необходимо сдвинуть указатели
                    lPointer = midPointer + 1;
                else
                    rPointer = midPointer - 1;
            }
            return -1;//если не нашли искомый вернем -1 как невозможну позицию
        }

        public void PrintArray()
        {
            if (_userArray == null || _userArray.Length == 0)
            {
                Console.WriteLine("Массив в данный момент пуст!");
                return;
            }
            Console.WriteLine($"В данный момент массив из {_userArray.Length} эелемнтов выглядит так: ");
            foreach (int elem in _userArray)//цикл по каждому элементу
            {
                Console.Write($"{elem} ");//печатаем его с пробелом после
            }
            Console.WriteLine();
        }

        public bool IsEmptyArray()
        {
            if (_userArray == null || _userArray.Length == 0)
            {
                Console.WriteLine("Операция недоступна, так как массив пуст!");
                return true;
            }
            return false;
        }
    }
}
