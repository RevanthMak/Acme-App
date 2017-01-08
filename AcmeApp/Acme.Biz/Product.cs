namespace Acme.Biz
{
    /// <summary>
    /// Manage products in the inventory
    /// </summary>
    public class Product
    {
        private  string productName;

        public  string ProductName
        {
            get { return productName; }
            set { productName = value; }
        }

        private string description;

        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        private int productId;

        public int ProductId
        {
            get { return productId; }
            set { productId = value; }
        }

        public string SayHello()
        {
            return "Hello" + ProductName + "(" + ProductId + ")" + Description;
        }


    }
}
