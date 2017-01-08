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
            var currentProduct = new Product();
            currentProduct.ProductId = 1;
            currentProduct.ProductName = "Knife";
            currentProduct.Description = "Useful to cut something";
            var expected = "HelloKnife(1)Useful to cut something";

            //Act
            var actual = currentProduct.SayHello();

            //Assert
            Assert.AreEqual(expected, actual);
        }
    }
}