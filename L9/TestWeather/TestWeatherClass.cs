namespace TestWeather
{
    [TestClass]
    public class TestWeatherClass
    {
        [TestMethod]
        public void TestCopyInit()
        {
            //Arrange
            Weather expected = new();
            //Act
            Weather actual = new(expected);
            //Assert
            Assert.AreEqual(expected, actual);

            expected.Temperature = 0;
            expected.Humidity = 0;
            expected.Pressure = 550;

            Assert.AreNotEqual(expected, actual);
        }

        [TestMethod]
        public void TestPreferences()
        {
            //Arrange
            double minTemp = -100, maxTemp = 100;
            int minPressure = 550, maxPressure = 850;
            int minHumidity = 0, maxHumidity = 100;

            //Act
            Weather actualMin = new(minTemp, minHumidity, minPressure);
            Weather actualMax = new(maxTemp, maxHumidity, maxPressure);
            
            //Assert
            Assert.AreEqual(minTemp, actualMin.Temperature);
            Assert.AreEqual(minHumidity, actualMin.Humidity);
            Assert.AreEqual(minPressure, actualMin.Pressure);

            Assert.AreEqual(maxTemp, actualMax.Temperature);
            Assert.AreEqual(maxHumidity, actualMax.Humidity);
            Assert.AreEqual(maxPressure, actualMax.Pressure);
        }

        [TestMethod]
        public void TestExcPreferences()
        {
            Assert.ThrowsException<ArgumentException>(() => new Weather(100.1, 0, 550));
            Assert.ThrowsException<ArgumentException>(() => new Weather(10, -1, 550));
            Assert.ThrowsException<ArgumentException>(() => new Weather(10, 0, 549));
        }

        [TestMethod]
        public void TestDewPoints()
        {
            //Arrange
            double temperature = 20.54;
            int pressure = 678;
            int humidity = 21;

            double a = 17.27;
            double b = 237.7;
            double f = (a * temperature) / (b + temperature) + Math.Log(humidity / 100.0);

            double dPoint = Math.Round((b * f) / (a - f), 4);

            //Act
            Weather expected = new(temperature, humidity, pressure);
            double staticDew = Weather.DewPoint(expected);
            double methodDew = expected.DewPoint();

            //Assert
            Assert.AreEqual(dPoint, methodDew);
            Assert.AreEqual(dPoint, staticDew);
        }

        [TestMethod]
        public void TestNegateWeather()
        {
            //Arrange
            double temperature = 20.54;
            int pressure = 678;
            int humidity = 21;
            Weather expectedNegateWeather = new(-20.54, 21, 678);

            //Act
            Weather expected = new(temperature, humidity, pressure);
            Weather actualNegateWeather = -expected;
            
            //Assert
            Assert.AreEqual(expectedNegateWeather.Temperature, actualNegateWeather.Temperature);
        }

        [TestMethod]
        public void TestNotWeather()
        {
            //Arrange
            double temperature = 20.54;
            int pressure = 678;
            int humidity = 21;
            bool expectedNotWeather = humidity > 80;

            //Act
            Weather expected = new(temperature, humidity, pressure);
            bool actualNotWeather = !expected;

            //Assert
            Assert.AreEqual(expectedNotWeather, actualNotWeather);
        }

        [TestMethod]
        public void TestExpBoolWeather()
        {
            //Arrange
            double temperature = 20.54;
            int pressure = 678;
            int humidity = 21;
            bool expectedExpBoolWeather = pressure > 760;

            //Act
            Weather expected = new(temperature, humidity, pressure);
            bool actualExplBoolWeather = (bool)expected;

            //Assert
            Assert.AreEqual(expectedExpBoolWeather, actualExplBoolWeather);
        }

        [TestMethod]
        public void TestImpDoubleWeather()
        {
            //Arrange
            double temperature = 20.54;
            int pressure = 678;
            int humidity = 21;
            double expectedImpDoubleWeather = Math.Round(0.5 * (temperature + 61.0 + ((temperature - 68.0) * 1.2) + (humidity * 0.094)), 2);

            //Act
            Weather expected = new(temperature, humidity, pressure);
            double actualImpDoubleWeather = expected;

            //Assert
            Assert.AreEqual(expectedImpDoubleWeather, actualImpDoubleWeather);
        }

        [TestMethod]
        public void TestMinusWeather()
        {
            //Arrange
            double temperature = 20.54;
            int pressure = 678;
            int humidity = 21;
            Weather expectedMinusWeather = new(temperature - 10, humidity, pressure);

            //Act
            Weather expected = new(temperature, humidity, pressure);
            Weather actualMinusWeather = expected - 10;

            //Assert
            Assert.AreEqual(expectedMinusWeather, actualMinusWeather);

        }

        [TestMethod]
        public void TestProdWeather()
        {
            //Arrange
            double temperature = 20.54;
            int pressure = 678;
            int humidity = 21;
            Weather expectedProdWeather = new(temperature * 1.2, humidity + (int)(humidity * 0.2), pressure + (int)(pressure * 0.2));

            //Act
            Weather expected = new(temperature, humidity, pressure);
            Weather actualProdWeather = expected * 20;

            //Assert
            Assert.AreEqual(expectedProdWeather, actualProdWeather);
            Assert.ThrowsException<ArgumentException>(() => expected * 150);
        }
    }
}