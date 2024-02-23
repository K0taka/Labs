using System.Net.Http.Headers;

namespace Lab10Tests
{
    [TestClass]
    public class RequestTests
    {
        [TestMethod]
        public void EnableMltButtonTextTest()
        {
            //Arrange
            ControlElement[] arr =
            {
                new ControlElement(),
                new Button(),
                new MultButton(),
                new MultButton(),
                new MultButton(0,0,"1123", true),
                new TextField()
            };
            int expected = 1;

            //Act
            int actual = Requests.SendRequest(arr, Requests.Request.EnableMultButtonText).Length;

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ExistTextWithExistHintTest()
        {
            //Arrange
            ControlElement[] arr =
            {
                new ControlElement(),
                new Button(),
                new TextField(0,0,"","12"),
                new TextField(0,0,"",""),
                new TextField(0,0,"1123",""),
                new MultButton(),
                new TextField(0,0,"123","1243")
            };
            int expected = 1;

            //Act
            int actual = Requests.SendRequest(arr, Requests.Request.ExistTextWithExistHint).Length;

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void AllElmsAtXTest()
        {
            //Arrange
            ControlElement[] arr =
            {
                new ControlElement(),
                new Button(),
                new TextField(5,0,"","12"),
                new TextField(112,0,"",""),
                new TextField(30,0,"1123",""),
                new MultButton(),
                new TextField(0,0,"123","1243")
            };
            int expected = 4;

            //Act
            int actual = Requests.SendRequest(arr, Requests.Request.AllElementsAtXPos, 0).Length;

            //Assert
            Assert.AreEqual(expected, actual);
        }
    }
}