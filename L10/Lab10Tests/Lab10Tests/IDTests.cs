namespace Lab10Tests
{
    [TestClass]
    public class IDTests
    {
        [TestMethod]
        public void EqualsTest()
        {
            //Arrange
            ID id1 = new ID();
            ID id2 = new ID();

            //Assert
            Assert.IsFalse(id1.Equals(id2));
        }

        [TestMethod]
        public void CompareGreaterTest() 
        {
            //Arrange
            ID id1 = new ID();
            ID id2 = new ID();
            int expected = -1;

            //Act
            int actual = id1.CompareTo(id2);

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CompareLessTest()
        {
            //Arrange
            ID id1 = new ID();
            ID id2 = new ID();
            int expected = 1;

            //Act
            int actual = id2.CompareTo(id1);

            //Assert
            Assert.AreEqual(expected, actual);
        }
    }
}
