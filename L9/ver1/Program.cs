using static ver1.ConsoleIO;
namespace ver1
{
    internal class Program
    {
        static void Main()
        {
            //внимание, обработка исключений для всех частей вынесена в отдельный блок Excp
            //goto Excp; //используется для более удобной демонстрации на паре, передвигаясь по именованым участкам
        Init:
            {
                //Создание объектов класса Weather
                //Случайное заполнение полей
                Weather rndWeather = new();
                WriteLine(rndWeather.ToString());
                WriteLine($"Количество созданных объектов: {Weather.Created}");
                WaitAnyButton();

                //Заполнение полей вручную
                double temperature = GetUserAnswer("Укажите температуру >>> ", lower: -100.0, upper: 100.0);
                int humidity = (int)GetUserAnswer("Укажите влажность >>> ", lower: 0, upper: 100);
                int pressure = (int)GetUserAnswer("Укажите давление >>> ", lower: 550, upper: 850);
                Weather myWeather = new(temperature, humidity, pressure);
                WriteLine(myWeather.ToString());
                WriteLine($"Количество созданных объектов: {Weather.Created}");
                WaitAnyButton();

                //Копирование
                Weather copyWeather = new(rndWeather);
                WriteLine(rndWeather.ToString());
                WriteLine(copyWeather.ToString());
                WriteLine($"Количество созданных объектов: {Weather.Created}");
                WaitAnyButton();

                //Работа с полями
                WriteLine($" rndWeather: Temperature {rndWeather.Temperature}");
                WriteLine($"copyWeather: Temperature {copyWeather.Temperature}");

                rndWeather.Temperature = 100;

                WriteLine("Температура rndWeather была устанновлена 100");
                WriteLine($" rndWeather: Temperature {rndWeather.Temperature}");
                WriteLine($"copyWeather: Temperature {copyWeather.Temperature}");
                WaitAnyButton();

            }

        DewPoint:
            {
                //отличия использования static-функции от метода класса
                Weather currWeather = new();
                WriteLine(currWeather.ToString());
                WriteLine($"Определение точки погода как\n- метод {currWeather.DewPoint()}\n- статическая функция {Weather.DewPoint(currWeather)}");
                WaitAnyButton();
            }

        OverloadAndOperation:
            {
                //демонстрация работы перегруженных операторов
                Weather currWeather = new(15.6, 50, 600);
                Weather alterWeather = -currWeather;
                bool notWeather = !currWeather;
                bool boolWeather = (bool)currWeather;
                double doubleWeather = currWeather;
                Weather minusWeather = currWeather - 6;
                Weather prodWeather = currWeather * 10;
                WriteLine($"currWeather: {currWeather}");
                WriteLine($"alterWeather: {alterWeather}");
                WriteLine($"notWeather: {notWeather}");
                WriteLine($"boolWeather: {boolWeather}");
                WriteLine($"doubleWeather: {doubleWeather}");
                WriteLine($"minusWeather: {minusWeather}");
                WriteLine($"prodWeather: {prodWeather}");
                WaitAnyButton();
            }

        WArray:
            {
                //демонстрация работы с коллекцией

                int nSaved = Weather.Created; //для подсчета созданных на последующем этапе объектов сохраним текущее значение
                static double Amp(WeatherArray array)
                {
                    if (array == null || array.Length == 0)
                        return 0;
                    double min = array[0].Temperature, max = array[0].Temperature;
                    for (int i = 1;  i < array.Length; i++)
                    {
                        //обновляем min max если необходимо
                        (min, max) = (min > array[i].Temperature ? array[i].Temperature : min, max < array[i].Temperature ? array[i] : max);
                    }
                    return max - min; //возвращаем амплитуду
                }

                WeatherArray rndArr = new();
                WeatherArray lenArr = new((int)GetUserAnswer("Введите длину массива >>> ", lower: 1, upper: 10));
                WeatherArray usrArr = new((int)GetUserAnswer("Введите длину массива >>> ", lower: 1, upper: 10), random: false);
                WeatherArray copy = new(usrArr);


                WriteLine("Создаем несколько коллекций");
                WriteLine($"rndArr: {rndArr}");
                WriteLine($"lenArr: {lenArr}");
                WriteLine($"usrArr: {usrArr}");
                WriteLine($"  copy: {copy}");
                WriteLine("---------------------------------------------");

                WriteLine("Демонстрируем работу глубокого копирования:");
                usrArr[0].Temperature = -usrArr[0].Temperature;
                WriteLine($"usrArr: {usrArr}");
                WriteLine($"  copy: {copy}");
                WriteLine("---------------------------------------------");

                WriteLine("Демонстрация работы индексаторов (здесь только верные)");
                WeatherArray indArr = new(2);
                WriteLine($"indArr до изменений: {indArr}");
                WriteLine($"indArr[0] = {indArr[0]}");
                indArr[1] = new Weather();
                WriteLine($"indArr измененный, по индексу 1: {indArr}");

                WriteLine("---------------------------------------------");
                WeatherArray temperatureArr = new(5);
                WriteLine($"temperatureArr: {temperatureArr}");
                WriteLine($"Функция: {Amp(temperatureArr)}");
                WriteLine($"Было создано объектов-списков: {WeatherArray.Created}");
                WriteLine($"Было создано объектов-погоды в ходе работы со списками: {Weather.Created - nSaved}");
                WriteLine($"Всего создано объектов погоды: {Weather.Created}");

                WaitAnyButton();
            }
        Excp:
            {
                try
                {
                    Weather cool = new(-50, 50, 400); //инициализация объекта погоды с неверными показателями
                }
                catch (Exception)
                {
                    ReturnError(ErrorCodes.IncorrectWeather);
                }

                try
                {
                    WeatherArray arr = new();
                    WriteLine(arr[-5].ToString()); //доступ по несуществующему индексу
                }
                catch (Exception)
                {
                    ReturnError(ErrorCodes.NumberIsOutOfBounds);
                }

                try
                {
                    WeatherArray arr = new();
                    arr[100] = new Weather(); //попытка изменить несуществующий индекс
                }
                catch (Exception)
                {
                    ReturnError(ErrorCodes.NumberIsOutOfBounds);
                }
            }

        }
    }
}
