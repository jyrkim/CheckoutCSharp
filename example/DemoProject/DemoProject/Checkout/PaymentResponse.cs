using System;
using System.Text;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations.Schema;

namespace Checkout
{
    /// <summary>
    /// Class is used for validating response from Checkout Finland
    /// </summary>
    public class PaymentResponse
    {

        //5. Payment statuses for payment response
        public static int Payment_Refunded = -10;
        public static int Payment_Not_Found = -4;
        public static int Payment_Timed_Out = -3;
        public static int Payment_Cancelled_By_System = -2;
        public static int Payment_Cancellation_Proposed_By_Buyer = -1;
        public static int Payment_In_Process = 1; //not complete
        public static int Payment_Paid_I = 2;
        public static int Payment_Delayed_I = 3;
        public static int Payment_Delayed_II = 4;
        public static int Payment_Paid_II = 5;
        public static int Payment_Paid_III = 6;
        public static int Payment_Paid_IV = 7;
        public static int Payment_Paid_V = 8;
        public static int Payment_Paid_VI = 9;
        public static int Payment_Paid_VII = 10;

        /** 
         * 1. Maksun versio
         * Payment version, currently always '0001' (AN 4)
         */
        public string Version { get; set; }

        /** 
         * 2. Maksun tunnus
         * Unique identifier for the payment (AN 20)
         */
        public string Stamp { get; set; }

        /**
         * 3. Maksun viite
         * Reference number for the payment, recommended to be unique but not forced (AN 20)
         */
        public string Reference { get; set; }

        /**
         * 4. Maksun arkistotunnus,
         * Payment's unique archive id created by Checkout
         */
        public string Payment { get; set; }

        /**
         * 5. Maksun tilatieto,
         * Status of payment set by Checkout. 
         * 2 = successful,
         * -1 = cancelled by user,
         * 1 = payment is still in process (unfinished). 
         * More details in the doc and above (static members).
         */
        public string Status { get; set; }

        /**
         * 6. Algoritmi, 3 käytössä
         * Algorithm used when calculating mac, currently = 3. 1 and 2 are still available but deprecated (N 1)
         */
        public string Algorithm { get; set; }

        /**
         * 7. Turvatarkiste
         * Mac
         */
        public string MAC { get; set; }

        /**
         * Myyjän turva-avain
         * Secret merchant key
         */
        [NotMapped]
        public string MerchantSecretKey { get; set; }

        /// <summary>
        /// Method used to initialize class members
        /// </summary>
        /// <param name="merchantSecretKey">Myyjän turva-avain, Secret merchant key</param>
        /// <param name="responseParams">data received from Checkout Finland</param>
        public void SetValues(string merchantSecretKey, NameValueCollection responseParams)
        {
            this.MerchantSecretKey = merchantSecretKey;
            this.Version = responseParams["VERSION"];  //1. "0001"
            this.Stamp = responseParams["STAMP"]; ; // 2.
            this.Reference = responseParams["REFERENCE"]; ; // 3. 
            this.Payment = responseParams["PAYMENT"]; // 4. 
            this.Status = responseParams["STATUS"];  // 5. 
            this.Algorithm = responseParams["ALGORITHM"]; // 6. 
            this.MAC = responseParams["MAC"];  // 7. 
        }

        /// <summary>
        /// Validates if is response valid.
        /// Algorithm value needs to be 3.
        /// It also uses <see cref="HashHMAC(byte[], byte[])"/> 
        /// method for received MAC validation 
        /// which need to equal.
        /// </summary>
        /// <returns>true if valid, false if not</returns>
        public Boolean IsValid()
        {
            if (!this.Algorithm.Equals("3"))
                return false;

            if (!this.HashHMAC().Equals(this.MAC))
                return false;

            return true;
        }

        /// <summary>
        /// Calculates HMACSHA256 from response.
        /// This method uses
        /// <see cref="PaymentUtils.CalculateHashHMAC(byte[], byte[])"/>
        /// </summary>
        /// <returns>computed hash value in string format</returns>
        public string HashHMAC()
        {
            string macString = "";

            string macDelimiter = "&";

            macString = this.Version + macDelimiter +  //1. 
            this.Stamp + macDelimiter + // 2.
            this.Reference + macDelimiter + // 3. 
            this.Payment + macDelimiter + // 4.
            this.Status + macDelimiter + // 5. 
            this.Algorithm;// 6. 

            string hashMac = PaymentUtils.CalculateHashHMAC(Encoding.UTF8.GetBytes(this.MerchantSecretKey),
                Encoding.UTF8.GetBytes(macString)); 

            this.MAC = hashMac;

            return hashMac;
        }
    }
}
