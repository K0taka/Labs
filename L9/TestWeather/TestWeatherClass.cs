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
            Weather expectedMin = new();
            Weather expectedMax = new();

            //Act
            expectedMin.Temperature = -100;
            expectedMin.Pressure = 550;
            expectedMin.Humidity = 0;

            expectedMax.Temperature = 100;
            expectedMax.Pressure = 850;
            expectedMax.Humidity = 100;

            Weather actualMin = new(-100, 0, 550);
            Weather actualMax = new(100, 100, 850);
            
            //Assert
            Assert.AreEqual(expectedMax, actualMax);
            Assert.AreEqual(expectedMin, actualMin);
            Assert.ThrowsException<ArgumentException>(() => new Weather(110,0,550));
            Assert.ThrowsException<ArgumentException>(() => new Weather(10, -50, 550));
            Assert.ThrowsException<ArgumentException>(() => new Weather(10, 0, 250));
        }
        [TestMethod]
        public void TestDewPoint()
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
        public void TestOperators()
        {
            //Arrange
            double temperature = 20.54;
            int pressure = 678;
            int humidity = 21;

            Weather expectedNegateWeather = new(-20.54, 21, 678);
            bool expectedNotWeather = humidity > 80;
            bool expectedExpBoolWeather = pressure > 760;
            double expectedImpDoubleWeather = Math.Round(0.5 * (temperature + 61.0 + ((temperature - 68.0) * 1.2) + (humidity * 0.094)), 2);
            Weather expectedMinusWeather = new(temperature - 10, humidity, pressure);
            Weather expectedProdWeather = new(temperature * 1.2, humidity + (int)(humidity * 0.2), pressure + (int)(pressure * 0.2));

            //Act
            Weather expected = new(temperature, humidity, pressure);
            Weather actualNegateWeather = -expected;
            bool actualNotWeather = !expected;
            bool actualExplBoolWeather = (bool)expected;
            double actualImpDoubleWeather = expected;
            Weather actualMinusWeather = expected - 10;
            Weather actualProdWeather = expected * 20;

            //Assert
            Assert.AreEqual(expectedNegateWeather.Temperature, actualNegateWeather.Temperature);
            Assert.AreEqual(expectedNotWeather, actualNotWeather);
            Assert.AreEqual(expectedExpBoolWeather, actualExplBoolWeather);
            Assert.AreEqual(expectedImpDoubleWeather, actualImpDoubleWeather);
            Assert.AreEqual(expectedMinusWeather, actualMinusWeather);
            Assert.AreEqual(expectedProdWeather, actualProdWeather);
            Assert.ThrowsException<ArgumentException>(() => expected * 150);

        }
    }
}