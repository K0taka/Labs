namespace Lab10Tests
{
    [TestClass]
    public class ControlElementTests
    {
        [TestMethod]
        public void CorrectCreationTest()
        {
            //Arrange
            uint x = 1000, y = 1000;

            //Act
            ControlElement elem = new(x, y);

            //Assert
            Assert.IsNotNull(elem);
            Assert.AreEqual(x, elem.X);
            Assert.AreEqual(y, elem.Y);
        }

        [TestMethod]
        public void CorrectModificationTest()
        {
            //Arrange
            uint x = 1000, y = 1000;
            ControlElement elem = new();

            //Act
            elem.X = x;
            elem.Y = y;

            //Assert
            Assert.AreEqual(x, elem.X);
            Assert.AreEqual(y, elem.Y);
        }

        [TestMethod]
        public void AttemptToIncorrectXModificateTest()
        {
            //Arrange
            uint incorrectX = 2000;
            ControlElement elem = new();

            //Assert
            Assert.ThrowsException<InvalidOperationException>( () => elem.X = incorrectX);
        }

        [TestMethod]
        public void AttemptToIncorrectYModificateTest()
        {
            //Arrange
            uint incorrectY = 2000;
            ControlElement elem = new();

            //Assert
            Assert.ThrowsException<InvalidOperationException>(() => elem.Y = incorrectY);
        }

        [TestMethod]
        public void CompareLessTest()
        {
            //Arrange
            ControlElement elem1 = new();
            ControlElement elem2 = new();
            int expectedResult = -1;

            //Act
            int actualResult = elem1.CompareTo(elem2);

            //Assert
            Assert.AreEqual(expectedResult, actualResult);

        }

        [TestMethod]
        public void CompareGreaterTest()
        {
            //Arrange
            ControlElement elem1 = new();
            ControlElement elem2 = new();
            int expectedResult = 1;

            //Act
            int actualResult = elem2.CompareTo(elem1);

            //Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void EqualsTest()
        {
            //Arrange
            ControlElement elem1 = new(1000, 100);
            ControlElement elem2 = new(1000, 100);

            //Assert
            Assert.AreEqual(elem1, elem2);
        }

        [TestMethod]
        public void NotEqualsTest()
        {
            //Arrange
            ControlElement elem1 = new(1000, 100);
            ControlElement elem2 = new(100, 1000);

            //Assert
            Assert.AreNotEqual(elem1, elem2);
        }
    }
}
