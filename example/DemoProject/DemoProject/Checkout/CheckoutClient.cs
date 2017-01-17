using System.Text;
using System.Net;
using System.Collections.Specialized;

namespace Checkout
{
    /// <summary>
    /// Class used for sending Payment and Query data to Checkout Finland
    /// </summary>
    public class CheckoutClient
    {
        public static string paymentUrl = "https://payment.checkout.fi";
        public static string queryUrl = "https://rpcapi.checkout.fi/poll";

        /// <summary>
        /// Sends data using POST method for Payment
        /// </summary>
        /// <param name="formData">form data for <see cref="Payment.FormData()"/></param>
        /// <returns>response in string format</returns>
        public static string postPaymentData(NameValueCollection formData)
        {
            using (WebClient client = new WebClient())
            {
                byte[] responsebytes = client.UploadValues(paymentUrl, "POST", formData);
                string responsebody = Encoding.UTF8.GetString(responsebytes);
                return responsebody;
            }
        }

        /// <summary>
        /// Sends data using POST method for Query
        /// </summary>
        /// <param name="formData">form data for <see cref="Query.FormData()"/></param>
        /// <returns>response in string format</returns>
        public static string postQueryData(NameValueCollection formData)
        {
            using (WebClient client = new WebClient())
            {
                byte[] responsebytes = client.UploadValues(queryUrl, "POST", formData);
                string responsebody = Encoding.UTF8.GetString(responsebytes);
                return responsebody;
            }
        }

    }
}
