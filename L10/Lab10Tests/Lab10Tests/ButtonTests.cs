namespace Lab10Tests
{
    [TestClass]
    public class ButtonTests
    {
        [TestMethod]
        public void CorrectCreationTest()
        {
            //Arrange
            uint x = 100;
            uint y = 110;
            string text = "Button text";

            //Act
            Button button = new(x, y, text);

            //Assert
            Assert.AreEqual(x, button.X);
            Assert.AreEqual(y, button.Y);
            Assert.AreEqual(text, button.Text);
        }

        [TestMethod]
        public void CorrectUpdateTest()
        {
            //Arrange
            uint x = 1000, y = 1000;
            string text = "Changed text";
            Button elem = new();

            //Act
            elem.X = x;
            elem.Y = y;
            elem.Text = text;

            //Assert
            Assert.AreEqual(x, elem.X);
            Assert.AreEqual(y, elem.Y);
            Assert.AreEqual(text, elem.Text);
        }

        [TestMethod]
        public void AttemptToIncorrectXModificateTest()
        {
            //Arrange
            uint incorrectX = 2000;
            Button elem = new();

            //Assert
            Assert.ThrowsException<InvalidOperationException>(() => elem.X = incorrectX);
        }

        [TestMethod]
        public void AttemptToIncorrectYModificateTest()
        {
            //Arrange
            uint incorrectY = 2000;
            Button elem = new();

            //Assert
            Assert.ThrowsException<InvalidOperationException>(() => elem.Y = incorrectY);
        }

        [TestMethod]
        public void EmptyTextTest()
        {
            //Arrange
            Button button = new();

            //Assert
            Assert.ThrowsException<ArgumentNullException>(() => button.Text = "   \t  ");
        }

        [TestMethod]
        public void LongTextTest()
        {
            //Arrange
            Button button = new();

            //Assert
            Assert.ThrowsException<NotSupportedException>(() => button.Text = "123456789 123456789 123456789 123456789 123456789" +
                                                                              " 123456789 123456789 123456789 123456789 123456789 123456789 ");
        }

        [TestMethod]
        public void CompareLessTest()
        {
            //Arrange
            Button elem1 = new();
            Button elem2 = new();
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
            Button elem1 = new();
            Button elem2 = new();
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
            Button elem1 = new(1000, 100, "Text");
            Button elem2 = new(1000, 100, "Text");

            //Assert
            Assert.AreEqual(elem1, elem2);
        }

        [TestMethod]
        public void NotEqualsTest()
        {
            //Arrange
            Button elem1 = new(100, 100, "Text");
            Button elem2 = new(100, 100, "Test");

            //Assert
            Assert.AreNotEqual(elem1, elem2);
        }

        [TestMethod]
        public void CloneCreationTest()
        {
            //Arrange
            Button elem = new(100, 100, "Text");

            //Act
            Button clone = (Button)elem.Clone();

            //Assert
            Assert.AreEqual(elem, clone);
            Assert.AreNotEqual(elem.Id, clone.Id);
        }

        [TestMethod]
        public void CloneChangingTest()
        {
            //Arrange
            Button elem = new(100, 100, "Text");
            Button clone = (Button)elem.Clone();

            //Act
            clone.Text = "Test";

            //Assert
            Assert.AreNotEqual(elem, clone);
        }

        [TestMethod]
        public void ShallowCopyCreationTest()
        {
            //Arrange
            Button elem = new(100, 100, "Text");

            //Act
            Button copy = (Button)elem.ShallowCopy();

            //Assert
            Assert.AreEqual(elem, copy);
            Assert.AreEqual(elem.Id, copy.Id);
        }

        [TestMethod]
        public void ShallowCopyChangingTest()
        {
            //Arrange
            Button elem = new(100, 100, "Text");
            Button copy = (Button)elem.ShallowCopy();

            //Act
            copy.Y = 1000;

            //Assert
            Assert.AreEqual(elem, copy);
        }

    }
}
