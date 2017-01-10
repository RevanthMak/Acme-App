using Acme.Biz;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Acme.Biz.Tests
{
    [TestClass()]
    public class ProductTests
    {
        [TestMethod()]
        public void SayHelloTest()
        {
            //Arrange
            var currentproduct = new Product();
            currentproduct.productName = "Knife";
            currentproduct.productId = 1;
            currentproduct.description = "Used to cut something";
            currentproduct.ProductVendor.CompanyName = "ABC Corp";

            var expected = "HelloKnife(1)Used to cut something" + "Available on";

            //Act
            var actual = currentproduct.SayHello();

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void Product_ParameterizedConstructor()
        {
            //Arrange
            var currentproduct = new Product(1, "Knife", "Used to cut something");
            var expected = "HelloKnife(1)Used to cut something";//+ "Available on";

            //Act
            var actual = currentproduct.SayHello();

            //Assert
            Assert.AreEqual(expected, actual);
        }

        public void Product_Null()
        {
            //Arrange
            Product CurrentProduct = null;
            var CompanyName = CurrentProduct?.ProductVendor?.CompanyName;
            string expected = null;

            //Act
            var actual = CompanyName;

            //Assert
            Assert.AreEqual(expected, actual);


        }

        [TestMethod()]
        public void ConvertMetersToInchesTest()
        {
            //Arrange
            var expected = 78.74;

            //Act
            var actual = 2 * Product.InchesPeMeter;

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void MinimumPriceTest_Default()
        {
            //Arrange
            var CurrentProduct = new Product();
            var expected = .9m;


            //Act
            var actual = CurrentProduct.MinimumPrice;

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void MinimumPriceTest_BulkValue()
        {
            //Arrange
            var CurrentProduct = new Product(1, "Bulk", "Used to cut something");
            var expected = 9.99m;


            //Act
            var actual = CurrentProduct.MinimumPrice;

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void ProductName_Format()
        {
            //Arrange
            var currentProduct = new Product();
            currentProduct.productName = " Steel Hammer    ";
            var expected = "Steel Hammer";

            //Act
            var actual = currentProduct.productName;

            //Assert
            Assert.AreEqual(expected, actual);

        }

        [TestMethod()]
        public void ProductName_TooShort()
        {
            //Arrange
            var CurrentProduct = new Product();
            CurrentProduct.productName = "aw";

            string expected = null;
            string expectedMessage = "Product Name must be too short";

            //Act
            var actual = CurrentProduct.productName;
            var actualMessage = CurrentProduct.ValidationMessage;

            //Assert
            Assert.AreEqual(expected, actual);
            Assert.AreEqual(expectedMessage, actualMessage);


        }

        [TestMethod()]
        public void ProductName_TooLong()
        {
            //Arrange
            var currentProduct = new Product();
            currentProduct.productName = "ABC DEF GHI JKL MNO PQRS TUV WXYZ";

            string expected = null;
            string expectedMessage = "Product Name is too long";

            //Act 
            var actual = currentProduct.productName;
            var actualMessage = currentProduct.ValidationMessage;

            //Assert 
            Assert.AreEqual(expected, actual);
            Assert.AreEqual(expectedMessage, actualMessage);

        }

        [TestMethod()]
        public void ProductName_Actual()
        {
            //Arrange
            var currentProduct = new Product();
            currentProduct.productName = "Knife";

            var expected = "Knife";
            string expectedMessage = null;

            //Act
            var actual = currentProduct.productName;
            var actualMessage = currentProduct.ValidationMessage;

            //Assert
            Assert.AreEqual(expected, actual);
            Assert.AreEqual(expectedMessage, actualMessage);

        }

        [TestMethod]
        public void ProductCode_DefaultValue()
        {
            //Arrange
            var currentProduct = new Product();
            var expected = "Tools-1";

            //Act 
            var actual = currentProduct.ProductCode;

            //Assert
            Assert.AreEqual(expected, actual);

        }

        [TestMethod()]
        public void CalculateSuggestedPriceTest()
        {
            //Arrange 
            var currentProduct = new Product();
            currentProduct.cost = 50m;
            var expected = 55m;

            //Act
            var actual = currentProduct.CalculateSuggestedPrice(10m);
            //Assert
            Assert.AreEqual(expected,actual);
        }
    }
}