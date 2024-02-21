namespace Lab10Lib
{
    public class Weather: IInit
    {
        private static int numCreated = 0;
        private static readonly Random rnd = new();

        private double temperature;
        private int humidity;
        private int pressure;

        /// <summary>
        /// Объект "Погода" со случайными атрибутами
        /// </summary>
        public Weather()
        {
            //не через свойства так как здесь уже указаны границы, зачем еще раз проверять
            temperature = 25.5;
            humidity = 60;
            pressure = 750;
            numCreated++;
        }

        /// <summary>
        /// Объект "Погода" с заданными атрибутами
        /// </summary>
        /// <param name="temperature">Температура окружающей среды</param>
        /// <param name="humidity">Влажность воздуха</param>
        /// <param name="pressure">Атмосферное давление мм. рт. ст.</param>
        public Weather(double temperature, int humidity, int pressure)
        {
            //а вот тут через свойства, так как может быть неизвестно что
            Temperature = temperature;
            Humidity = humidity;
            Pressure = pressure;
            numCreated++;
        }

        /// <summary>
        /// Глубокая копия погоды
        /// </summary>
        /// <param name="w">Объект для копирования</param>
        public Weather(Weather w)
        {
            //так как копируем из класса то зачем проверять все еще раз
            temperature = w.Temperature;
            humidity = w.Humidity;
            pressure = w.Pressure;
            numCreated++;
        }

        /// <summary>
        /// Свойства атрибута температура
        /// </summary>
        public double Temperature
        {
            get { return temperature; }
            set
            {
                if (value >= -100 && value <= 100)
                    temperature = value;
                else
                    throw new ArgumentException("Incorrect temperature", paramName: "Temperature");
            }
        }

        /// <summary>
        /// Свойства аттрибута влажность
        /// </summary>
        public int Humidity
        {
            get { return humidity; }
            set
            {
                if (value >= 0 && value <= 100)
                    humidity = value;
                else
                    throw new ArgumentException("Incorrect humidity", paramName: "Humidity");
            }
        }

        /// <summary>
        /// Свойства атрибута давление
        /// </summary>
        public int Pressure
        {
            get { return pressure; }
            set
            {
                if (value >= 550 && value <= 850)
                    pressure = value;
                else
                    throw new ArgumentException("Incorrect pressure", paramName: "Pressure");
            }
        }

        /// <summary>
        /// Геттер для количества созданных элементов
        /// </summary>
        public static int Created { get { return numCreated; } }

        /// <summary>
        /// Метод для расчета точки росы
        /// </summary>
        /// <returns>Double значение точки росы</returns>
        public double DewPoint()
        {
            const double a = 17.27;
            const double b = 237.7;

            double f = (a * temperature) / (b + temperature) + (Math.Log(humidity / 100.0));

            double dPoint = (b * f) / (a - f);

            return Math.Round(dPoint, 4);
        }

        /// <summary>
        /// Статик-функция для расчета точки росы
        /// </summary>
        /// <param name="weather">Принимает объект, для которого необходимо расчитать точку росы</param>
        /// <returns>Double значение точки росы</returns>
        public static double DewPoint(Weather weather)
        {
            const double a = 17.27;
            const double b = 237.7;

            double f = (a * weather.Temperature) / (b + weather.Temperature) + Math.Log(weather.Humidity / 100.0);

            double dPoint = (b * f) / (a - f);//можно все это упростить до записи return weather.DewPoint(), но необходимо показать разницу.
            //А конкретно: обращение происходит к объекту, а не к полям непосредственно.

            return Math.Round(dPoint, 4);
        }

        /// <summary>
        /// Переопределение ToString() для строкого представления объекта
        /// </summary>
        /// <returns>Строковое представление объекта</returns>
        public override string ToString() => $"Погода: температура {Temperature}, влажность {Humidity}, давление {Pressure}";

        /// <summary>
        /// Переопределение для сравнения объектов на равенство
        /// </summary>
        /// <param name="obj">Объект, с которым сравнивается</param>
        /// <returns>bool равенство объектов</returns>
        public override bool Equals(object? obj)
        {
            if (obj == null)
                return false;
            if (obj is not Weather)
                return false;
            return this.Temperature == ((Weather)obj).Temperature && this.Humidity == ((Weather)obj).Humidity && this.Pressure == ((Weather)obj).Pressure;
        }

        public override int GetHashCode()
        {
            return Pressure.GetHashCode()*17 + Temperature.GetHashCode()*19 + Humidity.GetHashCode()*23;
        }

        /// <summary>
        /// перегрузка оператора -
        /// </summary>
        /// <param name="weather">операнд</param>
        /// <returns>новый объект с температурой, обратной исходному</returns>
        public static Weather operator -(Weather weather)
        {
            Weather newWeather = new(weather);
            newWeather.Temperature = -newWeather.Temperature;
            return newWeather;
        }

        /// <summary>
        /// Перегрузка операции !
        /// </summary>
        /// <param name="weather">Операнд</param>
        /// <returns>bool влажность выше 80%</returns>
        public static bool operator !(Weather weather)
        {
            return weather.Humidity > 80;
        }

        /// <summary>
        /// Явное приведение типа bool, true при давлении выше 760
        /// </summary>
        /// <param name="weather">Операнд</param>
        public static explicit operator bool(Weather weather)
        {
            return weather.Pressure > 760;
        }

        /// <summary>
        /// Неявное преобразование типа в double
        /// </summary>
        /// <param name="weather">Операнд</param>
        public static implicit operator double(Weather weather)
        {
            return Math.Round(0.5 * (weather.Temperature + 61.0 + ((weather.Temperature - 68.0) * 1.2) + (weather.Humidity * 0.094)), 2);
        }

        /// <summary>
        /// Правосторонее вычитание
        /// </summary>
        /// <param name="weather">Операнд</param>
        /// <param name="delta">Изменение</param>
        /// <returns>Новый объект со значением градусов, измененным на delta</returns>
        public static Weather operator -(Weather weather, double delta)
        {
            Weather newWeather = new(weather);
            newWeather.Temperature -= delta;
            return newWeather;
        }

        /// <summary>
        /// Правосторонее умножение
        /// </summary>
        /// <param name="weather">Операнд, погода</param>
        /// <param name="percent">Процент увеличения атрибутов</param>
        /// <returns>Новый объект погоды с увеличенными на указанный процент атрибутов</returns>
        /// <exception cref="ArgumentException">Некорректное значение процентов</exception>
        public static Weather operator *(Weather weather, double percent)
        {
            if (percent < 0 || percent > 100)
                throw new ArgumentException("Incorrect percent value");
            Weather newWeather = new(weather);
            newWeather.Temperature *= (1+ percent / 100);
            newWeather.Humidity += (int)(newWeather.Humidity * percent / 100);
            newWeather.Pressure += (int)(newWeather.Pressure * percent / 100);
            return newWeather;
        }

        public void Init()
        {
            Temperature = GetDoubleAnswer("Укажите температуру >>> ", -100.0, 100.0);
            Humidity = (int)GetIntegerAnswer("Укажите влажность >>> ", 0, 100);
            Pressure = (int)GetIntegerAnswer("Укажите влажность >>> ", 550, 850);
        }

        public void RandomInit()
        {
            Temperature = Math.Round(rnd.Next(-100, 71) + rnd.NextDouble(), 2);
            Humidity = rnd.Next(0, 100);
            Pressure = rnd.Next(550, 851);
        }
    }
}
