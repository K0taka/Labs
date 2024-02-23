namespace Lab10Tests
{
    [TestClass]
    public class TextFieldTest
    {
        [TestMethod]
        public void CorrectCreationTest()
        {
            //Arrange
            uint x = 1000, y = 1000;
            string hint = "Hint", text = "Text";

            //Act
            TextField elem = new(x, y, hint, text);

            //Assert
            Assert.IsNotNull(elem);
            Assert.AreEqual(x, elem.X);
            Assert.AreEqual(y, elem.Y);
            Assert.AreEqual(hint, elem.Hint);
            Assert.AreEqual(text, elem.Text);
        }

        [TestMethod]
        public void CorrectUpdateTest()
        {
            //Arrange
            uint x = 1000, y = 1000;
            string text = "Text", hint = "Hint";
            TextField elem = new();

            //Act
            elem.X = x;
            elem.Y = y;
            elem.Hint = hint;
            elem.Text = text;

            //Assert
            Assert.AreEqual(x, elem.X);
            Assert.AreEqual(y, elem.Y);
            Assert.AreEqual(hint, elem.Hint);
            Assert.AreEqual(text, elem.Text);
        }

        [TestMethod]
        public void AttemptToIncorrectXModificateTest()
        {
            //Arrange
            uint incorrectX = 2000;
            TextField elem = new();

            //Assert
            Assert.ThrowsException<InvalidOperationException>(() => elem.X = incorrectX);
        }

        [TestMethod]
        public void AttemptToIncorrectYModificateTest()
        {
            //Arrange
            uint incorrectY = 2000;
            TextField elem = new();

            //Assert
            Assert.ThrowsException<InvalidOperationException>(() => elem.Y = incorrectY);
        }

        [TestMethod]
        public void CompareLessTest()
        {
            //Arrange
            TextField elem1 = new();
            TextField elem2 = new();
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
            TextField elem1 = new();
            TextField elem2 = new();
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
            TextField elem1 = new(1000, 100, "Hint", "Text");
            TextField elem2 = new(1000, 100, "Hint", "Text");

            //Assert
            Assert.AreEqual(elem1, elem2);
        }

        [TestMethod]
        public void NotEqualsTest()
        {
            //Arrange
            TextField elem1 = new(1000, 100, "Hint", "Text");
            TextField elem2 = new(100, 1000, "Text", "Hint");

            //Assert
            Assert.AreNotEqual(elem1, elem2);
        }

        [TestMethod]
        public void CloneCreationTest()
        {
            //Arrange
            TextField elem = new(100, 100, "Hint", "Text");

            //Act
            TextField clone = (TextField)elem.Clone();

            //Assert
            Assert.AreEqual(elem, clone);
            Assert.AreNotEqual(elem.Id, clone.Id);
        }

        [TestMethod]
        public void CloneChangingTest()
        {
            //Arrange
            TextField elem = new(100, 100, "Hint", "Text");
            TextField clone = (TextField)elem.Clone();

            //Act
            clone.X = 1000;

            //Assert
            Assert.AreNotEqual(elem, clone);
        }

        [TestMethod]
        public void ShallowCopyCreationTest()
        {
            //Arrange
            TextField elem = new(100, 100, "Text", "Hint");

            //Act
            TextField copy = (TextField)elem.ShallowCopy();

            //Assert
            Assert.AreEqual(elem, copy);
            Assert.AreEqual(elem.Id, copy.Id);
        }

        [TestMethod]
        public void ShallowCopyChangingTest()
        {
            //Arrange
            TextField elem = new(100, 100, "Hint", "Text");
            TextField copy = (TextField)elem.ShallowCopy();

            //Act
            copy.X = 1000;

            //Assert
            Assert.AreEqual(elem, copy);
        }
    }
}
