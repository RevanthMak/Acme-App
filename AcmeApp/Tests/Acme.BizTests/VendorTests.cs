using Acme.Biz;
using Acme.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Acme.Biz.Tests
{
    [TestClass()]
    public class VendorTests
    {
        [TestMethod()]
        public void SendWelcomeEmail_ValidCompany_Success()
        {
            // Arrange
            var vendor = new Vendor();
            vendor.CompanyName = "ABC Corp";
            var expected = "Message sent: HelloABC Corp";

            // Act
            var actual = vendor.SendWelcomeEmail("Test Message");

            // Assert
            //This is the basic commit
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void SendWelcomeEmail_EmptyCompany_Success()
        {
            // Arrange
            var vendor = new Vendor();
            vendor.CompanyName = "";
            var expected = "Message sent: Hello";

            // Act
            var actual = vendor.SendWelcomeEmail("Test Message");

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void SendWelcomeEmail_NullCompany_Success()
        {
            // Arrange
            var vendor = new Vendor();
            vendor.CompanyName = null;
            var expected = "Message sent: Hello";

            // Act
            var actual = vendor.SendWelcomeEmail("Test Message");

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void PlaceOrderTest()
        {
            //Arrange 
            var vendor = new Vendor();
            var product = new Product(1, "Knife", "Placing an order");
            var produc = new Product(ProductId: 2, ProductName: "Knnn", Description: "XYZ");
            var expected = new OperationResult(true, "Order from Acme, INC\r\nProduct:Tools-1\r\nQuantity:12" +
                "\r\nInstruction: Standard delivery");

            //Act
            var actual = vendor.PlaceOrder(product, 12);

            //Assert 
            Assert.AreEqual(expected.Success, actual.Success);
            Assert.AreEqual(expected.Message, actual.Message);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void PlaceOrder_NullProduct_Exception()
        {
            //Arrange
            var vendor = new Vendor();

            //Act
            var expected = vendor.PlaceOrder(null, 12);

            //Assert
            //Expected Exception

        }

        [TestMethod()]
        public void PlaceOrderTest1()
        {
            //Arrange
            var currentOrder = new Vendor();
            var product = new Product(1, "Knife", "");
            var expected = new OperationResult(true, "Order from Acme, INC\r\nProduct:Tools-1\r\nQuantity:12"
                + "\r\nDeliver by:10/25/2015");

            //Act
            var actual = currentOrder.PlaceOrder(product, 12, new DateTimeOffset(2015, 10, 25, 0, 0, 0, new TimeSpan(-7, 0, 0)));

            //Assert
            Assert.AreEqual(expected.Success, actual.Success);
            Assert.AreEqual(expected.Message, actual.Message);
        }

        [TestMethod()]
        public void PlaceOrderTest_WithEnums()
        {
            //Arrange 
            var vendor = new Vendor();
            var product = new Product(1, "saw", "");
            var expected = new OperationResult(true, "Test With Address With Copy");

            //Act
            var actual = vendor.PlaceOrder(product, 12, Vendor.IncludeAddress.yes, Vendor.SendCopy.No);

            //Assert

            Assert.AreEqual(expected.Message, actual.Message);
            Assert.AreEqual(expected.Success, actual.Success);
        }
    }
}