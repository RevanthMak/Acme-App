using Acme.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acme.Biz
{
    /// <summary>
    /// Manages the vendors from whom we purchase our inventory.
    /// </summary>
    public class Vendor 
    {
        public int VendorId { get; set; }
        public string CompanyName { get; set; }
        public string Email { get; set; }


        /// <summary>
        /// Sends product order to the vendor
        /// </summary>
        /// <param name="product">product to order</param>
        /// <param name="quantity">quantity to order </param>
        /// <returns></returns>
        public OperationResult PlaceOrder(Product product, int quantity)
        {
            return PlaceOrder(product, quantity, null, null);

        }

        /// <summary>
        /// Method overload with deliverby parameter
        /// </summary>
        /// <param name="product">product to order</param>
        /// <param name="quantity">quantity to order</param>
        /// <param name="deliverBy">Time for deliver instructions</param>
        /// <returns></returns>
        public OperationResult PlaceOrder(Product product, int quantity, DateTimeOffset? deliverBy)
        {
            return PlaceOrder(product, quantity, deliverBy, null);

        }

        /// <summary>
        /// Method overloaded with instructions parameter 
        /// </summary>
        /// <param name="product">product to order</param>
        /// <param name="quantity">quantity to order</param>
        /// <param name="deliverBy">Time for deliver instructions</param>
        /// <param name="instructions"></param>
        /// <returns></returns>
        public OperationResult PlaceOrder(Product product, int quantity, DateTimeOffset? deliverBy, string instructions)
        {
            //Gaurd classes
            if (product == null) throw new ArgumentNullException(nameof(product));
            if (quantity <= 0) throw new ArgumentOutOfRangeException(nameof(quantity));
            if (deliverBy <= DateTimeOffset.Now) throw new ArgumentOutOfRangeException(nameof(deliverBy));

            var success = false;
            var OrderText = "Order from Acme, INC" + System.Environment.NewLine + "Product:" + product.ProductCode
                + System.Environment.NewLine + "Quantity:" + quantity;

            if (deliverBy.HasValue)
            {
                OrderText += System.Environment.NewLine + "Deliver by: " + deliverBy.Value;

            }

            if(!string.IsNullOrWhiteSpace(instructions))
            {
                OrderText += System.Environment.NewLine + "Instruction: " + instructions;

            }

            var emailService = new EmailService();
            var Confirmation = emailService.SendMessage("New Order", OrderText, Email);
            if (Confirmation.StartsWith("Message sent: "))
            {
                success = true;
            }
            var opResult = new OperationResult(success, OrderText);
            return opResult;


        }


        /// <summary>
        /// Sends an email to welcome a new vendor.
        /// </summary>
        /// <returns></returns>
        public string SendWelcomeEmail(string message)
        {
            var emailService = new EmailService();
            var subject = "Hello" + this.CompanyName;
            var confirmation = emailService.SendMessage(subject,
                                                        message, 
                                                        this.Email);
            return confirmation;
        }
    }
}
