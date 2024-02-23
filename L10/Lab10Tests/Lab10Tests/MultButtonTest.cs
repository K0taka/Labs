namespace Lab10Tests
{
    [TestClass]
    public class MultButtonTest
    {
        [TestMethod]
        public void CorrectCreationTest()
        {
            //Arrange
            uint x = 100;
            uint y = 110;
            string text = "Button text";
            bool isEnable = true;

            //Act
            MultButton mltButton = new(x, y, text, isEnable);

            //Assert
            Assert.AreEqual(x, mltButton.X);
            Assert.AreEqual(y, mltButton.Y);
            Assert.AreEqual(text, mltButton.Text);
            Assert.AreEqual(isEnable, mltButton.IsEnabled);
        }

        [TestMethod]
        public void CorrectUpdateTest()
        {
            //Arrange
            uint x = 1000, y = 1000;
            string text = "Changed text";
            MultButton elem = new();

            //Act
            elem.X = x;
            elem.Y = y;
            elem.Text = text;
            elem.IsEnabled = true;

            //Assert
            Assert.AreEqual(x, elem.X);
            Assert.AreEqual(y, elem.Y);
            Assert.AreEqual(text, elem.Text);
            Assert.IsTrue(elem.IsEnabled);
        }

        [TestMethod]
        public void AttemptToIncorrectXModificateTest()
        {
            //Arrange
            uint incorrectX = 2000;
            MultButton elem = new();

            //Assert
            Assert.ThrowsException<InvalidOperationException>(() => elem.X = incorrectX);
        }

        [TestMethod]
        public void AttemptToIncorrectYModificateTest()
        {
            //Arrange
            uint incorrectY = 2000;
            MultButton elem = new();

            //Assert
            Assert.ThrowsException<InvalidOperationException>(() => elem.Y = incorrectY);
        }

        [TestMethod]
        public void EmptyTextTest()
        {
            //Arrange
            MultButton button = new();

            //Assert
            Assert.ThrowsException<ArgumentNullException>(() => button.Text = "   \t  ");
        }

        [TestMethod]
        public void LongTextTest()
        {
            //Arrange
            MultButton button = new();

            //Assert
            Assert.ThrowsException<NotSupportedException>(() => button.Text = "123456789 123456789 123456789 123456789 123456789" +
                                                                              " 123456789 123456789 123456789 123456789 123456789 123456789 ");
        }

        [TestMethod]
        public void CompareLessTest()
        {
            //Arrange
            MultButton elem1 = new();
            MultButton elem2 = new();
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
            MultButton elem1 = new();
            MultButton elem2 = new();
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
            MultButton elem1 = new(1000, 100, "Text", true);
            MultButton elem2 = new(1000, 100, "Text", true);

            //Assert
            Assert.AreEqual(elem1, elem2);
        }

        [TestMethod]
        public void NotEqualsTest()
        {
            //Arrange
            MultButton elem1 = new(100, 100, "Text", true);
            MultButton elem2 = new(100, 100, "Test", false);

            //Assert
            Assert.AreNotEqual(elem1, elem2);
        }

        [TestMethod]
        public void CloneCreationTest()
        {
            //Arrange
            MultButton elem = new(100, 100, "Text", true);

            //Act
            MultButton clone = (MultButton)elem.Clone();

            //Assert
            Assert.AreEqual(elem, clone);
            Assert.AreNotEqual(elem.Id, clone.Id);
        }

        [TestMethod]
        public void CloneChangingTest()
        {
            //Arrange
            MultButton elem = new(100, 100, "Text", true);
            MultButton clone = (MultButton)elem.Clone();

            //Act
            clone.IsEnabled = false;

            //Assert
            Assert.AreNotEqual(elem, clone);
        }

        [TestMethod]
        public void ShallowCopyCreationTest()
        {
            //Arrange
            MultButton elem = new(100, 100, "Text", true);

            //Act
            MultButton copy = (MultButton)elem.ShallowCopy();

            //Assert
            Assert.AreEqual(elem, copy);
            Assert.AreEqual(elem.Id, copy.Id);
        }

        [TestMethod]
        public void ShallowCopyChangingTest()
        {
            //Arrange
            MultButton elem = new(100, 100, "Text", true);
            MultButton copy = (MultButton)elem.ShallowCopy();

            //Act
            copy.Y = 1000;

            //Assert
            Assert.AreEqual(elem, copy);
        }
    }
}
