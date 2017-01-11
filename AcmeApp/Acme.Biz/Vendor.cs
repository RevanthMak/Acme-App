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

        public enum IncludeAddress { yes, no};
        public enum SendCopy { yes, No};

        public int VendorId { get; set; }
        public string CompanyName { get; set; }
        public string Email { get; set; }

        /// <summary>
        /// Method overloaded with instructions parameter 
        /// </summary>
        /// <param name="product">product to order</param>
        /// <param name="quantity">quantity to order</param>
        /// <param name="deliverBy">Time for deliver instructions</param>
        /// <param name="instructions"></param>
        /// <returns></returns>
        public OperationResult PlaceOrder(Product product, int quantity, DateTimeOffset? deliverBy = null, string instructions = "Standard delivery")
        {
            //Gaurd classes
            if (product == null) throw new ArgumentNullException(nameof(product));
            if (quantity <= 0) throw new ArgumentOutOfRangeException(nameof(quantity));
            if (deliverBy <= DateTimeOffset.Now) throw new ArgumentOutOfRangeException(nameof(deliverBy));

            var success = false;
            var OrderText = new StringBuilder("Order from Acme, INC" + System.Environment.NewLine + "Product:" + product.ProductCode
                + System.Environment.NewLine + "Quantity:" + quantity);

            if (deliverBy.HasValue)
            {
                OrderText.Append(System.Environment.NewLine + "Deliver by:" + deliverBy.Value.ToString("d"));

            }

            if(!string.IsNullOrWhiteSpace(instructions))
            {
                OrderText.Append( System.Environment.NewLine + "Instruction: " + instructions);

            }

            var emailService = new EmailService();
            var Confirmation = emailService.SendMessage("New Order", OrderText.ToString(), Email);
            if (Confirmation.StartsWith("Message sent: "))
            {
                success = true;
            }
            var opResult = new OperationResult(success, OrderText.ToString());
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

        public OperationResult PlaceOrder(Product product, int quantity, IncludeAddress includeaddress, SendCopy sendcopy)
        {
            var orderText = "Test";
            if (includeaddress == IncludeAddress.yes) orderText += " With Address";
            if (sendcopy == SendCopy.No ) orderText += " With Copy";

            var operationResult = new OperationResult(true, orderText);
            return operationResult;

        }
    }
}
