using System;

namespace T1
{
    internal class Program
    {
        static readonly string[][] allMenues = //создадим зубчатый массив для хранения всех консольных окон,
                                               //по своей сути это хранилище ссылок на 4 массива,
            {
                new string[]//меню 1, оно же - главное меню. Содержит все доступные операции над массивом
                {
                    "Найти и удалить максимальные элементы массива",
                    "Добавить K элементов в конец массива",
                    "Сдвинуть циклически элементы массива на M элементов влево",
                    "Найти первый отрицатальный элемент",
                    "Сортировка массива с помощью простого обмена",
                    "Сортировка массива с помощью быстрой сортировки по алгоритму Хоара",
                    "Выход"
                },

                new string[]//меню 2, оно же - меню настройки массива. Содержит все доступные варианты задания массива
                {
                    "Ввести массив вручную",
                    "Использовать случайный массив элементов [-100, 100] заданной длины",
                    "Использовать случайный массив элементов [-100, 100] случайной длины из диапазона [1, 100]",
                    "Отменить и вернуться в главное меню",
                    "Выход"
                },

                new string[] //меню 3, используется после выполнения операции над массивом, исп. если полученный массив не пустой
                {
                    "Найти в этом массиве элемент K (бинарный поиск)",
                    "Вернуться в главное меню",
                    "Выход"
                },

                new string[] //меню 4, исп. если полученный массив пустой, либо после меню 2
                {
                    "Вернуться в главное меню",
                    "Выход"
                }
            };
        static readonly Random randElement = new Random(); //инициализируем ДСЧ для использования при создании массивов

        /// <summary>
        /// Функция для получения от пользователя опции в предлагаемом меню
        /// </summary>
        /// <param name="currMenu">Текущий номер меню</param>
        /// <param name="uAnswer">Выход: ответ пользователя</param>
        /// <returns>Возвращает номер выбранного пункта в меню</returns>
        static byte GetAnswerMenu(byte currMenu)
        {
            byte uAnswer;
            bool isCorrect;//флаг для повторного запроса по необходимости
            do
            {
                Console.Write("Введите номер желаемой задачи >>> ");
                isCorrect = byte.TryParse(Console.ReadLine(), out uAnswer); //получаем ответ от пользователя
                if (uAnswer < 1 | uAnswer > allMenues[currMenu].Length) //ответ д. находиться в диапазоне исп. меню
                {
                    isCorrect = false;
                }
                if (!isCorrect)
                {
                    Console.WriteLine("Вы ввели элемент, не относящийся к текущему меню");
                }
            } while (!isCorrect);
            ExitOption(currMenu, uAnswer); //завершении программы при выборе соотв. опции
            return uAnswer;
        }

        /// <summary>
        /// Функция для вывода пунктов меню на экран
        /// </summary>
        /// <param name="menuNames">Требует массив строк, который необходимо вывести</param>
        static void PrintMenu(string[] menuNames)
        {
            for (byte menuNum = 1; menuNum <= menuNames.Length; menuNum++)
            {
                Console.WriteLine($"{menuNum}. {menuNames[menuNum - 1]}");
            }
        }

        /// <summary>
        /// Функция для смены меню после выбора пользователя
        /// </summary>
        /// <param name="wantMenu">Номер следующего меню</param>
        /// <param name="firstPageAns">Необязательный: ответ на предыдущем пункте меню </param>
        /// <param name="userArray">Необязательный: массив чисел, полученный в ходе работы программы</param>
        static void ControlMenu(byte wantMenu, byte firstPageAns = 0, int[] userArray = null)
        {
            switch (wantMenu)
            {
                case 0:
                    Console.Clear();//Очистка окна консоли
                    Console.WriteLine("Добро пожаловать! Это лабораторная работа #4");
                    Console.WriteLine("Вы можете использовать одну из следующих функций:");
                    PrintMenu(allMenues[0]);//Напечатать текущее меню
                    firstPageAns = GetAnswerMenu(0);//Получить от пользователя задачу
                    ControlMenu(1, firstPageAns);//Перейте на следующее меню, выбор массива. Передаем текущий выбор пользователя
                    break;
                case 1:
                    Console.Clear();
                    Console.WriteLine($"Вы выбрали: {allMenues[0][firstPageAns - 1]}");
                    Console.WriteLine("Укажите желаемый массив:");
                    PrintMenu(allMenues[1]);//Печать текущего меню
                    SecondMenu(firstPageAns);//Логика обработки данного меню
                    break;
                case 2:
                    PrintMenu(allMenues[2]);//Печать текущего меню
                    byte thirdPageAnswer = GetAnswerMenu(2);
                    if (MainPageOption(2, thirdPageAnswer))//если пользователь выбрал вернуться на главную - возвращаемся
                        return;
                    Console.Clear();
                    Console.WriteLine("Текущая операция: бинарный поиск элемента в массиве");
                    Console.WriteLine(BinSearchInit(userArray, out int compares));//тк в данном меню всего три пункта, бин. поиск - последний из возможных. Инициализируем его
                    Console.WriteLine($"Количество сравнений, потребовавшееся для нахождения указанного элемента: {compares}");
                    ControlMenu(3);//переход к 4 меню
                    break;
                case 3:
                    PrintMenu(allMenues[3]);//печать текущего меню
                    GetAnswerMenu(3);//внутри проверки ответа проверяется опция выхода, потому если программа не завершена,
                                     //то возврат на главный экран, так как всего 2 опции: либо на главную, либо выход 
                    break;
            }
        }

        /// <summary>
        /// Логика обработки второго меню
        /// </summary>
        /// <param name="firstPageAns">Принимает ответ пользователя из первого меню</param>
        static void SecondMenu(byte firstPageAns)
        {
            byte secondPageAns = GetAnswerMenu(1);//выбор в меню 2
            if (MainPageOption(1, secondPageAns))//опция вернуться на главную
                return;
            int[] userArray = MenuCreateArray(secondPageAns, firstPageAns);//получаем созданный массив
            PrintArray(userArray);//вывод его на экран по заданию
            int[] answerArray = ChooseTask(firstPageAns, userArray, out bool isArrayFlag);//обрабатываем массив согласно выбору пользователя
            if (answerArray.Length != 0 && isArrayFlag)//проверка ответа: получен ли массив, тк в некоторых задач ответом является число
            {
                PrintArray(answerArray);//печатаем его
                ControlMenu(2, userArray: answerArray);//переключаем на меню 3
            }
            else if (isArrayFlag)//если получен пустой массив - оповестим об этом
            {
                Console.WriteLine("После выполнения операции массив пуст");
                ControlMenu(3);//в пустом массиве невозможно выполнить бинарный поиск, поэтому переключаем на меню 4
            }
            else
                ControlMenu(2, userArray: answerArray);//ответом на задачу является число, массив не менялся, переключаем на меню 2
        }

        /// <summary>
        /// Функция для выполнения соответствующей выбору пользоваетя задачи по обработке массива
        /// </summary>
        /// <param name="firstPageAns">Выбранная пользователем задача по обработке массива</param>
        /// <param name="userArray">Целочисленный массив введенный пользователем</param>
        /// <param name="isArrayFlag">Флаг указывающий на то, является ответом на задачу массив или нет</param>
        /// <returns>Возвращает массив целых чисел полученный путем обработки переданного массива, и флаг обозначения результата</returns>
        static int[] ChooseTask(byte firstPageAns, int[] userArray, out bool isArrayFlag)
        {
            //в первых двух кейсах функции изменяют длину массива, поэтому создается новый массив внутри функции и возвращается,
            //в случае же остальных кейсов меняется порядок следования элементов, а так как внутрь функции передается не копия массива, а ссылка
            //то и элементы, переставленные внутри функции в массиве, будут переставлены и вне функции, так как ссылка ведет на одно и то же место
            switch (firstPageAns)//выбираем метод обработки согласно ответу пользователя
            {
                case 1:
                    int maxN = FindMaxElements(userArray, out int numMax);//нахождение максимального элемента
                    isArrayFlag = true;//выход - массив
                    return DeleteMaxElements(userArray, numMax, maxN);//удаление максимумов
                case 2:
                    isArrayFlag = true;//выход - массив
                    return AddElementToArray(userArray);//возвращаем массив с добавленными элементами
                case 3:
                    isArrayFlag = true;//выход - массив
                    MovingLeft(userArray);
                    return userArray;//возвращаем массив со сдвинутыми элементами
                case 4:
                    isArrayFlag = false;//выход - число
                    FindFirstNegative(userArray);//в функции ищем и выводим первый отрицательный
                    return userArray;//возвращаем массив для дальнейшей обработки
                case 5:
                    isArrayFlag = true;//выход - массив
                    BubbleSorting(userArray);//Выполнить сортировку простым обменом
                    return userArray;//вернуть массив осортированный методом простого обмена
                case 6:
                    isArrayFlag = true;//выход - массив
                    FastSortingInit(userArray, 0, userArray.Length - 1);//отсортировать массив используя алгоритм быстрой сортировки Хоара
                    return userArray;//вернуть этот массив
                default:
                    isArrayFlag = false;
                    return null;
            }
        }

        /// <summary>
        /// Инициализирует создание массива согласно выбору пользователя
        /// </summary>
        /// <param name="createMethod">Выбранный пользователем способ задания массива</param>
        /// <param name="firstPageAns">Выбранная пользователем операция над массивом</param>
        /// <returns>Возвращает созданный массив целых чисел</returns>
        static int[] MenuCreateArray(byte createMethod, byte firstPageAns)
        {
            Console.Clear();//Очистка консоли
            Console.WriteLine($"Вы выбрали: {allMenues[0][firstPageAns - 1]}, необходимо {allMenues[1][createMethod - 1].ToLower()}");
            switch (createMethod)//Переключение от выбанного способа задания массива
            {
                case 1:
                    return ManualArray();//вручную

                case 2:
                    return RandomArrayN();//массив случайных элементов от -100 до 100 заданной длины

                case 3:
                    return RandomArrayNoN();//массив случайных элементов от -100 до 100 случайной длины от 1 до 100

                default:
                    return null;
            }
        }

        /// <summary>
        /// Функция для создания массива случайных элементов в диапазоне от -100 до 100 длины, которую задает пользователь
        /// </summary>
        /// <returns>Возвращает сформированный массив</returns>
        static int[] RandomArrayN()
        {
            Console.Write("Введите длину желаемого массива >>> ");
            int length = GetArrayLength("Массив не может быть пустым! Повторите ввод >>> ");//получение длины массива
            int[] userArray = new int[length];//инициализация массива указанной длины
            for (int index = 0; index < length; index++)//заполнение массива по индексам
            {
                userArray[index] = randElement.Next(-100, 101);//случайный элемент от -100 до 100
            }
            return userArray;
        }

        /// <summary>
        /// Функция для создания массива случайных элементов в диапазоне от -100 до 100 случайной длины в диапазоне от -100 до 100
        /// </summary>
        /// <returns>Возвращает сформированный массив</returns>
        static int[] RandomArrayNoN()
        {
            int[] userArray = new int[randElement.Next(1, 101)]; //задаем массив случайной длины
            for (int index = 0; (index < userArray.Length); index++)//по индексу заполняем его
            {
                userArray[index] = randElement.Next(-100, 101);//элементы от -100 до 100
            }
            return userArray;
        }

        /// <summary>
        /// Функция для формирования массива заданной длины из чисел, вводимых вручную пользователем
        /// </summary>
        /// <returns>Возвращает сформированный массив</returns>
        static int[] ManualArray()
        {
            Console.Write("Введите длину желаемого массива >>> ");
            int length = GetArrayLength("Массив не может быть пустым! Повторите ввод >>> ");//Получаем длину массива
            int[] userArray = new int[length];//создаем массив заданной длины
            for (int index = 0; index < length; index++)//запоняем этот массив по индексу
            {
                Console.Write($"Введите элемент последовательности #{index + 1} >>> ");
                userArray[index] = GetCorrectEl();//заполняем корректным элементом
            }
            return userArray;
        }

        /// <summary>
        /// Функция для получения длины массива/последовательности
        /// </summary>
        /// <param name="zerotext">Необязательно: текст для реакции на встреченный 0</param>
        /// <param name="removeZero">Необязательно: флаг для необходимости отреагировать на 0</param>
        /// <returns>Возвращает целое число - длину последовательности</returns>
        static int GetArrayLength(string zerotext = "", bool removeZero = true)
        {
            int length;
            bool isCorrect;
            do
            {
                isCorrect = int.TryParse(Console.ReadLine(), out length);
                if (!isCorrect)
                {
                    Console.Write("Вы ввели значение, которые не может быть использовано для задания длины массива. Повторите ввод >>> ");
                }
                if (removeZero && isCorrect && length == 0) //если необходимо отреагировать на встреченный ноль
                {
                    Console.Write(zerotext);
                    isCorrect = false;
                }
                if (isCorrect && length < 0) //реакция на отрицательную длину
                {
                    isCorrect = false;
                    Console.Write("Длина не может быть отрицательной! Повторите ввод! >>> ");
                }
            } while (!isCorrect);
            return length;
        }

        /// <summary>
        /// Получение от пользователя корректного элемента
        /// </summary>
        /// <returns>Возвращает целое число - элемент последовательности</returns>
        static int GetCorrectEl()
        {
            int elem;
            bool isCorrect;
            do
            {
                isCorrect = int.TryParse(Console.ReadLine(), out elem);
                if (!isCorrect)
                {
                    Console.Write("Введите корректный элемент-целое число >>> ");
                }
            } while (!isCorrect);
            return elem;
        }

        /// <summary>
        /// Функция для вывода массива на экран
        /// </summary>
        /// <param name="userArray">Получает массив который надо вывести</param>
        static void PrintArray(int[] userArray)
        {
            Console.WriteLine($"В данный момент массив из {userArray.Length} эелемнтов выглядит так: ");
            foreach (int elem in userArray)//цикл по каждому элементу
            {
                Console.Write($"{elem} ");//печатаем его с пробелом после
            }
            Console.WriteLine();
        }

        /// <summary>
        /// Функция для поиска максимального значения в массиве
        /// </summary>
        /// <param name="userArray">Получает массив для поиска</param>
        /// <param name="numMax">Выходное значение - количество максимумов</param>
        /// <returns>Возвращает значение максимального элемента и их количество</returns>
        static int FindMaxElements(int[] userArray, out int numMax)
        {
            int maxN = userArray[0];//назначаем максимумом первый элемент массива
            numMax = 0;//количество максимумов
            foreach (int elem in userArray)//перебор всех элементов массива
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

        /// <summary>
        /// Функция для удаления всех максимумов из массива чисел
        /// </summary>
        /// <param name="userArray">Обрабатываемый массив</param>
        /// <param name="numMax">количество максимумов</param>
        /// <param name="maxN">Значение максимума</param>
        /// <returns>Возвращает новый массив после удаления максимумов</returns>
        static int[] DeleteMaxElements(int[] userArray, int numMax, int maxN)
        {
            int[] answerArray = new int[userArray.Length - numMax];//создаем массив с размерностью меньше на количество максимумов
            int index = 0;//запоминаем индекс для продолжения копирования массива
            for (int newElem = 0; newElem < userArray.Length - numMax; newElem++)
            {
                for (int elem = index; elem < userArray.Length; elem++)
                {
                    if (userArray[elem] != maxN)
                    {
                        answerArray[newElem] = userArray[elem];//копируем значение если оно не равно максимуму
                        index = elem + 1;//запоминаем место для продолжения
                        break;//переходим на следующую ячейку нового массива
                    }
                }
            }
            return answerArray;
        }

        /// <summary>
        /// Функция добавления элементов в конец массива
        /// </summary>
        /// <param name="userArray">Массив для добавления элементов</param>
        /// <returns>Возвращаем массив с добавленными элементами</returns>
        static int[] AddElementToArray(int[] userArray)
        {
            Console.Write("Введите количество элементов >>>> ");
            int length = GetArrayLength(removeZero: false);//получаем длину последовательности
            if (length != 0)
            {
                int[] answerArray = new int[userArray.Length + length];//увеличим массив
                for (int elem = 0; elem < userArray.Length; elem++)//копирование значений из предыдущего массива
                {
                    answerArray[elem] = userArray[elem];
                }
                for (int elem = userArray.Length; elem < answerArray.Length; elem++)//добавление новых элементов
                {
                    Console.Write($"Введите элемент #{elem + 1 - userArray.Length} >>> ");
                    answerArray[elem] = GetCorrectEl();//вызываем функцию для получения элемента
                }
                return answerArray;//вернем новый массив
            }
            return userArray;//если длина 0 вернем массив без изменений
        }

        /// <summary>
        /// Функция для перемещения элементов внутри массива влево на число элементов указанное пользователем
        /// </summary>
        /// <param name="userArray">Обрабатываемый массив</param>
        static void MovingLeft(int[] userArray)
        {
            Console.Write("Введите значение M, на которое необходимо сдвинуть влево >>> ");
            int slide = GetArrayLength(removeZero: false);//получаем сдвиг, может быть нулем
            slide %= userArray.Length;//обновляем сдвиг вычитая "лишние круги" в сдвиге
            if (slide == 0)//при свдвиге 0 массив не трубется менять
                return;
            int[] tempArray = new int[slide];//массив для хранения сдвига
            for (int elem = 0; elem < slide; elem++)
            {
                tempArray[elem] = userArray[elem];//сохраняем сдвиг в вспомагательном массиве
            }
            for (int elem = slide; elem < userArray.Length; elem++)
            {
                userArray[elem - slide] = userArray[elem];//передвижение всех элементов
            }
            for (int elem = userArray.Length - slide; elem < userArray.Length; elem++)
            {
                userArray[elem] = tempArray[slide - userArray.Length + elem];//возвращаем на последние позиции элементы из начала
            }
        }

        /// <summary>
        /// Функция для нахождения первого отрицательного элемента
        /// </summary>
        /// <param name="userArray">Получает массив в котором производится поиск</param>
        static void FindFirstNegative(int[] userArray)
        {
            int comps = 0;//количество сравнений
            bool isFound = false;
            foreach (int elem in userArray)//цикл по всем элементам массива подряд
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

        /// <summary>
        /// Функция для сортировки методом простого обмена (aka сортировка пузырьком)
        /// </summary>
        /// <param name="userArray">получает массив который необходимо отсортировать</param>
        static void BubbleSorting(int[] userArray)
        {
            int length = userArray.Length;//текущая длина сортируемой последовательности
            while (length > 0)//выполняем до тех пор пока не пройдем по всей длине
            {
                int maxIndex = 0;//текущее положение максимального элемента
                for (int rPointer = 1; rPointer < length; rPointer++)//проходить будем парами, поэтому первый элемент единица
                {
                    if (userArray[rPointer - 1] > userArray[rPointer])//меняем элементы местами, если предыдущий больше текущего
                    {
                        (userArray[rPointer - 1], userArray[rPointer]) = (userArray[rPointer], userArray[rPointer - 1]);//для смены порядка следования используем кортеж
                        maxIndex = rPointer;//обновляем индекс максимального (это будет самое левое значение отсортированной части)
                    }
                }
                length = maxIndex; //обновляем длину рассматриваемой части
            }
        }

        /// <summary>
        /// Функция производящая одну итерацию алгоритма быстрой сортировки по методике Хоара
        /// </summary>
        /// <param name="userArray">Получает текущий массив</param>
        /// <param name="lPointer">Получает текущее значение левого указателя</param>
        /// <param name="rPointer">Получает текущее значение правого указателя</param>
        /// <returns>Возвращает число - значение левого указателя</returns>
        static int FastSortingIteration(int[] userArray, int lPointer, int rPointer)
        {
            int pillar = userArray[(lPointer + rPointer) / 2]; //находим опорное значение для сравнения
            while (lPointer <= rPointer) //в цикле идем пока указатели не встретятся
            {
                while (userArray[lPointer] < pillar) //ставим левый указатель на значение >= опорному
                    lPointer++;
                while (userArray[rPointer] > pillar)//а правый на значение <= опорному
                    rPointer--;
                if (lPointer <= rPointer)//если указатели не сравнялись - меняем местами значения под указателями
                {
                    (userArray[lPointer], userArray[rPointer]) = (userArray[rPointer], userArray[lPointer]);
                    lPointer++;//сдвигаем левый указатель вправо
                    rPointer--;//а правый указатель влево
                }
            }
            return lPointer; //после выхода из цикла левый указатель обозначает начало подпоследовательности, в которой элементы >= опорного. Вернем его
        }

        /// <summary>
        /// Функция инициализирующая быструю сортировку. Работает рекурсивно.
        /// </summary>
        /// <param name="userArray">Получает сортируемый массив</param>
        /// <param name="sPos">Позицию начала</param>
        /// <param name="ePos">И позицию конца</param>
        static void FastSortingInit(int[] userArray, int sPos, int ePos)
        {
            if (sPos >= ePos)//прерываем рекурсию при если позиция начала и позиция конца совпали
                return;
            int newPos = FastSortingIteration(userArray, sPos, ePos); //получаем новую позицию - положение отсортированных относительно друг друга подмассивов
            FastSortingInit(userArray, sPos, newPos - 1);//рекурсивно вызываем инициализацию сортировка для каждого из подмассивов пока не придем к тому
            FastSortingInit(userArray, newPos, ePos);//что сортируемая последовательность будет из 1 элемента или пуста
        }

        /// <summary>
        /// Инициализирует бинарный поиск
        /// </summary>
        /// <param name="userArray">Принимает массив для поиска</param>
        /// <param name="compares">Выходное значение: количество сравнений</param>
        /// <returns>Возвращает строку - результат бинарного поиска</returns>
        static string BinSearchInit(int[] userArray, out int compares)
        {
            bool isSorted = SortCheck(userArray);//проверка на необходимость сортировки
            if (!isSorted)//в том случае, если необходима - проводится быстрая сортировка
            {
                Console.WriteLine("Для бинарного поиска требуется отсортированный массив. Будет проведена быстрая сортировка по алгоритму Хоара");
                FastSortingInit(userArray, 0, userArray.Length - 1);//запускаем быструю сортировку
                PrintArray(userArray);//печатает отсортированный массив
                Console.WriteLine();
            }
            else
            {
                PrintArray(userArray);//печатаем массив для наглядности
                Console.WriteLine();
            }
            Console.Write("Введите искомый элемент >>> ");
            int searchKey = GetCorrectEl();//получение элемента который нужно найти
            int keyIndex = BinSearch(userArray, searchKey, out compares);//запускаем бинарный поиск
            if (keyIndex == -1)//если не найдено - вернем сообщение об этом, иначе вернем обратное сообщение
                return $"В последовательности нет искомого элемента {searchKey}";
            return $"Искомый элемент {searchKey} находится на позиции {keyIndex}";
        }

        /// <summary>
        /// Функция алгоритма бинарного поиска
        /// </summary>
        /// <param name="userArray">Принимает сортируемый массив</param>
        /// <param name="searchKey">Искомое значение</param>
        /// <param name="compares">Отдает количество сравнений</param>
        /// <returns>Возвращает число - позицию искомого элемента</returns>
        static int BinSearch(int[] userArray, int searchKey, out int compares)
        {
            int lPointer = 0, rPointer = userArray.Length - 1; //установка левого и правого указателей в начальные позиции
            compares = 0;//количество сравнений
            while (lPointer <= rPointer)
            {
                compares += 1;//увеличиваем количество сравнений
                int midPointer = (lPointer + rPointer) / 2;//находим текущее центральное положение
                if (userArray[midPointer] == searchKey)//определяем равны ли цетральный элемент и искомый
                    return midPointer + 1;
                else if (userArray[midPointer] < searchKey)//смотрим в какую сторону от центрального необходимо сдвинуть указатели
                    lPointer = midPointer + 1;
                else
                    rPointer = midPointer - 1;
            }
            return -1;//если не нашли искомый вернем -1 как невозможну позицию
        }

        /// <summary>
        /// Функция для проверки отсортирован ли массив
        /// </summary>
        /// <param name="userArray">Получает массив для проверки</param>
        /// <returns>Возвращает логическое значение о том отсортирован ли массив</returns>
        static bool SortCheck(int[] userArray)
        {
            bool isSorted = true;//начальное значение отсортированности - true
            for (int index = 1; index < userArray.Length; index++)
            {
                if (userArray[index] < userArray[index - 1])
                {
                    isSorted = false;//как только находим пару предыдущее > текущее - массив не отсортирован
                    break;//уже можно прервать проверку
                }
            }
            return isSorted;
        }

        /// <summary>
        /// Функция для закрытия программы
        /// </summary>
        /// <param name="currMenu">Получает номер текущего меню</param>
        /// <param name="option">Выбранное пользователем значение</param>
        static void ExitOption(byte currMenu, byte option)
        {
            if (allMenues[currMenu][option - 1] == allMenues[currMenu][allMenues[currMenu].Length - 1])//если выбран последний пункт в текущем меню
            {
                Console.WriteLine("Работы программы завершена!");
                Environment.Exit(0);//программа завершается
            }
        }

        /// <summary>
        /// Функция для возвращения на главную страницу
        /// </summary>
        /// <param name="currMenu">Номер текущего меню</param>
        /// <param name="option">Выбор пользователя</param>
        /// <returns>Возвращает логическое - true, если необходимо вернуться на главную</returns>
        static bool MainPageOption(byte currMenu, byte option)
        {
            if (currMenu != 0 && (allMenues[currMenu][option - 1] == allMenues[currMenu][allMenues[currMenu].Length - 2]))
            {
                return true;
            }
            return false;
        }

        static void Main()
        {
            while (true)//бесконечный вызов меню. Все действия происходят в нем. Дабы избежать рекурсии
                        //программа возвращается на данный этап и запускает меню вновь
            {
                ControlMenu(0);
            }
        }
    }
}
