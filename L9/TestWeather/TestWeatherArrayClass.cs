namespace TestWeather
{
    [TestClass]
    public class TestWeatherArrayClass
    {
        [TestMethod]
        public void TestCopyInit()
        {
            //Arrange
            WeatherArray expectedArray = new();

            //Act
            WeatherArray actualArray = new(expectedArray);
            Assert.AreEqual(expectedArray, actualArray);
            
            expectedArray[0] = new Weather();

            //Assert
            Assert.AreNotEqual(expectedArray, actualArray);

        }

        [TestMethod]
        public void TestIndex()
        {
            //Arrange
            WeatherArray expectedArray = new(2);
            Weather expectedFirstEl = new(expectedArray[0]);
            Weather expectedSecondEl = new(expectedArray[1]);

            //Act
            WeatherArray actualArray = new(2);
            actualArray[0] = new(expectedFirstEl);
            actualArray[1] = new(expectedSecondEl);

            //Assert
            Assert.AreEqual(expectedArray, actualArray);
            Assert.ThrowsException<IndexOutOfRangeException>(() => expectedArray[-1]);
            Assert.ThrowsException<IndexOutOfRangeException>(() => expectedArray[-1] = new());
        }
    }
}
