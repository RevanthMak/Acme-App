using Acme.Common;
using System;

namespace Acme.Biz
{
    /// <summary>
    /// Manage different product details
    /// </summary>
    public class Product
    {
        public const double InchesPeMeter = 39.37;
        public readonly decimal MinimumPrice;

        #region constructors
        public Product()
        {
            System.Console.WriteLine("Product instance has been created");
            //this.ProductVendor = new Vendor();
            this.MinimumPrice = .9m;
            this.Category = "Tools"; 
        }

        public Product(int ProductId, string ProductName, string Description) : this()
        {
            this.description = Description;
            this.productId = ProductId;
            this.productName = ProductName;
            if(productName.StartsWith("Bulk"))
            {
                this.MinimumPrice = 9.99m; 
            }

            System.Console.WriteLine("Hello(1) Used to cut something");
        }
        #endregion

        #region Properties 
        private DateTime? accessibilityDate;

        public DateTime? AccessibilityDate
        {
            get { return accessibilityDate; }
            set { accessibilityDate = value; }
        }

        private string ProductName;

        public string productName
        {
            get {
                var formattedValue = ProductName?.Trim();
                return formattedValue;
            }
            set {
                if(value.Length < 3)
                {
                    ValidationMessage = "Product Name must be too short";
                }
                else if(value.Length > 20)
                {
                    ValidationMessage = "Product Name is too long"; 
                }
                else
                {
                    ProductName = value;
                }
                
            }
        }

        private string Description;

        public string description
        {
            get { return Description; }
            set { Description = value; }
        }

        private int ProductId;

        public int productId
        {
            get { return ProductId; }
            set { ProductId = value; }
        }

        private Vendor productVendor;

        public Vendor ProductVendor
        {
            get {
                if(ProductVendor == null)
                {
                    ProductVendor = new Vendor();
                }
                return productVendor;
            }
            set { productVendor = value; }
        }

        public string ValidationMessage { get; private set; }

        public string Category { get; set; }
        public int SequenceId { get; set; } = 1;

        public string ProductCode => this.Category + "-" + this.SequenceId;

        public decimal cost { get; set; }

        #endregion

        /// <summary>
        /// Calculates the suggested prcie
        /// </summary>
        /// <param name="markupPercent"></param>
        /// <returns></returns>
        public decimal CalculateSuggestedPrice(decimal markupPercent)
        {
            return this.cost + (this.cost * markupPercent / 100);
        }

        public string SayHello()
        {
            //var vendor = new Vendor();
            //vendor.SendWelcomeEmail("Message from product");

            var emailService = new EmailService();
            emailService.SendMessage("New Product", this.productName, "sales@abc.com");

            var result = LoggingService.LogAction("Saying Hello");

            return "Hello" + productName + "(" + productId + ")" + description + AccessibilityDate?.ToShortDateString();
        }

        public override string ToString()
        {
            return this.productName + "(" + this.productId + ")";
        }


    }
}
