namespace ver1
{
    public class Weather
    {
        private static int numCreated = 0;
        private static readonly Random rnd = new();

        private double temperature;
        private int humidity;
        private int pressure;

        public Weather()
        {
            //не через свойства так как здесь уже указаны границы, зачем еще раз проверять
            temperature = Math.Round(rnd.Next(-100, 71) + rnd.NextDouble(), 2);
            humidity = rnd.Next(0, 100);
            pressure = rnd.Next(550, 851);
            numCreated++;
        }

        public Weather(double temperature, int humidity, int pressure)
        {
            //а вот тут через свойства, так как может быть неизвестно что
            Temperature = temperature;
            Humidity = humidity;
            Pressure = pressure;
            numCreated++;
        }

        public Weather(Weather w)
        {
            //так как копируем из класса то зачем проверять все еще раз
            temperature = w.Temperature;
            humidity = w.Humidity;
            pressure = w.Pressure;
            numCreated++;
        }

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

        public static int Created { get { return numCreated; } }

        public double DewPoint()
        {
            const double a = 17.27;
            const double b = 237.7;

            double f = (a * temperature) / (b + temperature) + (Math.Log(humidity / 100.0));

            double dPoint = (b * f) / (a - f);

            return Math.Round(dPoint, 4);
        }

        public static double DewPoint(Weather weather)
        {
            const double a = 17.27;
            const double b = 237.7;

            double f = (a * weather.Temperature) / (b + weather.Temperature) + Math.Log(weather.Humidity / 100.0);

            double dPoint = (b * f) / (a - f);//можно все это упростить до записи return weather.DewPoint(), но необходимо показать разницу.
            //А конкретно: обращение происходит к объекту, а не к полям непосредственно.

            return Math.Round(dPoint, 4);
        }

        public override string ToString() => $"Погода: температура {Temperature}, влажность {Humidity}, давление {Pressure}";

        public override bool Equals(object? obj)
        {
            if (obj == null)
                return false;
            if (obj is not Weather)
                return false;
            return this.Temperature == ((Weather)obj).Temperature && this.Humidity == ((Weather)obj).Humidity && this.Pressure == ((Weather)obj).Pressure;
        }

        public static Weather operator -(Weather weather)
        {
            Weather newWeather = new(weather);
            newWeather.Temperature = -newWeather.Temperature;
            return newWeather;
        }

        public static bool operator !(Weather weather)
        {
            return weather.Humidity > 80;
        }

        public static explicit operator bool(Weather weather)
        {
            return weather.Pressure > 760;
        }

        public static implicit operator double(Weather weather)
        {
            return Math.Round(0.5 * (weather.Temperature + 61.0 + ((weather.Temperature - 68.0) * 1.2) + (weather.Humidity * 0.094)), 2);
        }

        public static Weather operator -(Weather weather, double delta)
        {
            Weather newWeather = new(weather);
            newWeather.Temperature -= delta;
            return newWeather;
        }

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
    }
}
