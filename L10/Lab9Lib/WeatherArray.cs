using static IOLib.IO;
namespace Lab9Lib
{
    public class WeatherArray
    {
        private static int nCreated = 0;
        private static readonly Random rnd = new();

        private readonly Weather[] arr;
        private readonly int length;

        /// <summary>
        /// Создание объекта со случайными данными
        /// </summary>
        public WeatherArray()
        {
            nCreated++;
            length = rnd.Next(1, 10);
            arr = new Weather[length];
            for (int i = 0; i < length; i++)
            {
                arr[i] = new Weather();
            }
        }

        /// <summary>
        /// Создает массив заданной длины
        /// </summary>
        /// <param name="length">Длина массива</param>
        /// <param name="random">Необходимость генерации случайных элеиентов для заполнения. По-умолчанию true</param>
        public WeatherArray(int length, bool random = true)
        {
            nCreated++;
            this.length = length;
            arr = new Weather[length];
            if (random)
            {
                for (int i = 0; i < length; i++)
                {
                    arr[i] = new Weather();
                }
            }
            else
            {
                for (int i = 0; i < length; i++)
                {
                    double temperature = GetDoubleAnswer($"[{i + 1}] Укажите температуру >>> ", lower: -100.0, upper: 100.0);
                    int humidity = (int)GetIntegerAnswer($"[{i + 1}] Укажите влажность >>> ", lower: 0, upper: 100);
                    int pressure = (int)GetIntegerAnswer($"[{i + 1}] Укажите давление >>> ", lower: 550, upper: 850);
                    arr[i] = new Weather(temperature, humidity, pressure);
                }
            }
        }

        /// <summary>
        /// Конструктор копирования
        /// </summary>
        /// <param name="weatherArray">Объект для копирования</param>
        public WeatherArray(WeatherArray weatherArray)
        {
            nCreated++;
            arr = new Weather[weatherArray.Length]; 
            length = arr.Length;
            for (int i = 0; i < weatherArray.Length; i++)
            {
                arr[i] = new Weather(weatherArray[i]); //необходимо создать глубокую копию, потому создаем новый объект
            }
        }

        /// <summary>
        /// Возвращает длину массива
        /// </summary>
        public int Length { get => length; }

        /// <summary>
        /// Возвращает количество созданнызх коллекций
        /// </summary>
        public static int Created { get => nCreated; }

        /// <summary>
        /// Индексатор для коллекции
        /// </summary>
        /// <param name="index">Индекс элемента</param>
        /// <returns>Элемент на указанной позиции</returns>
        /// <exception cref="IndexOutOfRangeException">Выход за пределы массива</exception>
        public Weather this[int index]
        {
            get
            {
                if (index >= 0 && index < length)
                    return arr[index];
                else
                    throw new IndexOutOfRangeException("Index is out of range");
            }

            set
            {
                if (index >= 0 && index < length)
                    arr[index] = value;
                else
                    throw new IndexOutOfRangeException("Index is out of range");
            }
        }

        /// <summary>
        /// Переопределение для вывода на экран
        /// </summary>
        /// <returns>Сторокове предтавление коллекции</returns>
        public override string ToString()
        {
            string answer = "";
            string spase = "".PadRight(23, ' ');
            for (int index = 0; index < length; index++)
            {
                answer += $"\n{spase}{index}: {arr[index]}";
            }
            return answer;
        }

        /// <summary>
        /// Сравнение на равенство
        /// </summary>
        /// <param name="obj">Сравниваемый объект</param>
        /// <returns>bool результат равенства объектов</returns>
        public override bool Equals(object? obj)
        {
            if (obj == null)
                return false;
            if (obj is not WeatherArray)
                return false;
            if (((WeatherArray)obj).Length != length) //если у obj длина >, то без этой проверки они м.б. равны при равенстве первых Length эл.
                return false;
            for (int i = 0; i < Length; i++)
            {
                if (!this[i].Equals(((WeatherArray)obj)[i]))
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Вычисляешь хэш-код
        /// </summary>
        /// <returns>Возвращает вычисленный хэш-код</returns>
        public override int GetHashCode()
        {
            return Length.GetHashCode() * 3 + arr.GetHashCode() * 5;
        }
    }
}
