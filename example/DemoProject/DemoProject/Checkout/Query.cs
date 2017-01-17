using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Specialized;

namespace Checkout
{
    /// <summary>
    /// Class for making payment status query
    /// </summary>
    public class Query
    {
        /// <summary>
        /// 1. Default value for Version, currently always 0001 
        /// </summary>
        public static string Version_Default = "0001";

        /// <summary>
        /// 6. Currency default value
        /// </summary>
        public static string Currency_Default = "EUR";

        /// <summary>
        /// 7. Device default value
        /// </summary>
        public static string Format_Default = "1";  // 10 = Xml (ei Html)

        /// <summary>
        /// 8. Algorithm default value
        /// </summary>
        public static string Algorithm_Default = "1";

        /** 
         * 1. Maksun versio, P
         * Payment version, currently always '0001' (AN 4)
         */
        public string Version { get; set; }

        /** 
         * 2. Maksun tunnus, P
         * Unique identifier for the payment (AN 20)
         */
        public string Stamp { get; set; }

        /**
         * 3. Maksun viite, P
         * Reference number for the payment, recommended to be unique but not forced (AN 20)
         */
        public string Reference { get; set; }

        /**
         * 4. Myyjän tunniste, P
         * Merchant id (AN 20)
         */
        public string MerchantId { get; set; }

        /**
         * 5. Maksun määrä, P
         * Amount of payment in cents (10 Euros == 1000) (N 8)
         */
        public string Amount { get; set; }

        /**
         * 6. Valuutta, P
         * Currency used in payment. Currently only EUR is supported (AN 3)
         */
        public string Currency { get; set; }

        /**
         * 7. Paluu viestin muoto, P                                                                                                           
         * format, always 1 = XML 
         */
        public string Format { get; set; }

        /**
         * 8. Algoritmi, 1 käytössä, P
         * Algorithm used when calculating mac, currently always 1
         */
        public string Algorithm { get; set; }

        /**
        * 9. turvatarkiste, P  
        * MAC
        */
        public string MAC { get; set; }

        /**
         * Myyjän turva-avain, P  (salainen, ei lähetetä, käytetään turvatarkisteen laskentaan)
         * Secret merchant key (not sent, used for calculating MAC)
         */
        [NotMapped]
        public string MerchantSecretKey { get; set; }

        /// <summary>
        /// Initialize method to be used for setting Query with five parameters.
        /// </summary>
        /// <param name="stamp">2. Maksun tunnus, P, Unique identifier for the payment (AN 20)</param>
        /// <param name="reference">3. Maksun viite, P, Reference number for the payment (AN 20)</param>
        /// <param name="merchantId">4. Myyjän tunniste, P, Merchant id (AN 20)</param>
        /// <param name="amount">5. Maksun määrä, P, Amount of payment in cents (10 Euros == 1000) (N 8)</param>
        /// <param name="merchantSecretKey">Myyjän turva-avain (ei lähetetä), P, Secret merchant key (not sent, used for calculating MAC)</param>
        public void InitializeValues(string stamp, string reference,
                string merchantId, string amount, string merchantSecretKey)
        {
            //set default values & initialize properties 
            this.Version = Query.Version_Default;  //1. 
            this.Stamp = stamp; // 2.
            this.Reference = reference; // 3.
            this.MerchantId = merchantId; // 4.
            this.Amount = amount; // 5. P
            this.Currency = Query.Currency_Default; // 6.
            this.Format = Query.Format_Default; // 7. 
            this.Algorithm = Query.Algorithm_Default; // 8.
            this.MerchantSecretKey = merchantSecretKey;
        }

        /// <summary>
        /// 9. Calculates MAC, MD5-turvatarkiste lasketaan
        /// </summary>
        /// <returns>MAC</returns>
        public string CalculateMac()
        {
            string macString = "";

            string macDelimiter = "+";

            macString = this.Version + macDelimiter +  // 1. 
            this.Stamp + macDelimiter + // 2. 
            this.Reference + macDelimiter + // 3.
            this.MerchantId + macDelimiter + // 4.
            this.Amount + macDelimiter + // 5.            
            this.Currency + macDelimiter + // 6.
            this.Format + macDelimiter + // 7.
            this.Algorithm + macDelimiter + // 8.    
            this.MerchantSecretKey;

            macString = PaymentUtils.CalculateMD5HashUTF8(macString); //9.

            this.MAC = macString;

            return macString;
        }

        /// <summary>
        /// Returns form data that is sent to Checkout Finland
        /// </summary>
        /// <returns>NameValueCollection</returns>
        public NameValueCollection FormData()
        {
            var reqparm = new NameValueCollection();
            reqparm.Add("VERSION", this.Version); // 1.
            reqparm.Add("STAMP", this.Stamp);
            reqparm.Add("REFERENCE", this.Reference);
            reqparm.Add("MERCHANT", this.MerchantId);
            reqparm.Add("AMOUNT", this.Amount);
            reqparm.Add("CURRENCY", this.Currency);
            reqparm.Add("FORMAT", this.Format);
            reqparm.Add("ALGORITHM", this.Algorithm);
            reqparm.Add("MAC", this.CalculateMac());  // 9. 
            return reqparm;
        }
    }
}