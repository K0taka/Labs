﻿namespace Lab10Tests
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
        public void CorrectUpdateTest()
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

        [TestMethod]
        public void CloneCreationTest()
        {
            //Arrange
            ControlElement elem = new(100, 100);

            //Act
            ControlElement clone = (ControlElement)elem.Clone();

            //Assert
            Assert.AreEqual(elem, clone);
            Assert.AreNotEqual(elem.Id, clone.Id);
        }

        [TestMethod]
        public void CloneChangingTest()
        {
            //Arrange
            ControlElement elem = new(100, 100);
            ControlElement clone = (ControlElement)elem.Clone();

            //Act
            clone.X = 1000;
            
            //Assert
            Assert.AreNotEqual(elem, clone);
        }

        [TestMethod]
        public void ShallowCopyCreationTest()
        {
            //Arrange
            ControlElement elem = new(100, 100);

            //Act
            ControlElement copy = (ControlElement)elem.ShallowCopy();

            //Assert
            Assert.AreEqual(elem, copy);
            Assert.AreEqual(elem.Id, copy.Id);
        }

        [TestMethod]
        public void ShallowCopyChangingTest()
        {
            //Arrange
            ControlElement elem = new(100, 100);
            ControlElement copy = (ControlElement)elem.ShallowCopy();

            //Act
            copy.X = 1000;

            //Assert
            Assert.AreEqual(elem, copy);
        }
    }
}
