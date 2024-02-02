namespace ver1
{
    public class WeatherArray
    {
        private static int nCreated = 0;
        private static readonly Random rnd = new Random();

        private Weather[] arr;
        private readonly int length;

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
                    double temperature = ConsoleIO.GetUserAnswer($"[{i + 1}] Укажите температуру >>> ", lower: -100.0, upper: 100.0);
                    int humidity = (int)ConsoleIO.GetUserAnswer($"[{i + 1}] Укажите влажность >>> ", lower: 0, upper: 100);
                    int pressure = (int)ConsoleIO.GetUserAnswer($"[{i + 1}] Укажите давление >>> ", lower: 550, upper: 850);
                    arr[i] = new Weather(temperature, humidity, pressure);
                }
            }
        }

        public WeatherArray(WeatherArray weatherArray)
        {
            nCreated++;
            arr = new Weather[weatherArray.Length];
            length = arr.Length;
            for (int i = 0; i < weatherArray.Length; i++)
            {
                arr[i] = new Weather(weatherArray[i]);
            }
        }

        public int Length { get => length; }

        public static int Created { get => nCreated; }

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

        public override bool Equals(object? obj)
        {
            if (obj == null)
                return false;
            if (obj is not WeatherArray)
                return false;
            if (((WeatherArray)obj).Length != length)
                return false;
            for (int i = 0; i < Length; i++)
            {
                if (!this[i].Equals(((WeatherArray)obj)[i]))
                    return false;
            }
            return true;
        }
    }
}
