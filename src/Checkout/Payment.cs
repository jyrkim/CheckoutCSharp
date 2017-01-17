using System;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations.Schema;
using Checkout.Exceptions;

namespace Checkout
{
    /// <summary>
    /// Class used for creating payment to Checkout Finland
    /// </summary>
    public class Payment
    {
        /// <summary>
        /// 1. Default value for Version, currently always 0001 
        /// </summary>
        public static string Version_Default = "0001";

        /// <summary>
        /// 6. Default value for Language
        /// </summary>
        public static string Language_Default = "FI";

        /// <summary>
        /// 12. Country default value
        /// </summary>
        public static string Country_Default = "FIN";

        /// <summary>
        /// 13. Currency default value
        /// </summary>
        public static string Currency_Default = "EUR";

        /// <summary>
        /// 14. Device default value
        /// </summary>
        public static string Device_Default = "10";  // 10 = Xml (ei Html)

        /// <summary>
        /// 14. static member for HTML device
        /// </summary>
        public static string Device_HTML = "1";

        /// <summary>
        /// 14. static member for XML device
        /// </summary>
        public static string Device_XML = "10";

        /// <summary>
        /// 15. Content default value
        /// </summary>
        public static string Content_Default = "1";  // 1 = normaali (ei aikuisviihde)

        /// <summary>
        /// 15. static member for normal content 
        /// </summary>
        public static string Content_Normal = "1";

        /// <summary>
        /// 15. static member for adult entertainment content 
        /// </summary>
        public static string Content_Adult_Entertainment = "2";

        /// <summary>
        /// 16. Type default value
        /// </summary>
        public static string Type_Default = "0";

        /// <summary>
        /// 17. Algorithm default value
        /// </summary>
        public static string Algorithm_Default = "3";

        /**
         * P = pakollinen, required field
         * V = vapaaehtoinen, optional field
         */

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
         * 3. Maksun määrä, P
         * Amount of payment in cents (10 Euros == 1000) (N 8)
         */
        public string Amount { get; set; }

        /**
         * 4. Maksun viite, P
         * Reference number for the payment, recommended to be unique but not forced (AN 20)
         */
        public string Reference { get; set; }

        /**
         * 5. Maksun viesti, V
         * Message/Description for the buyer (AN 1000)
         */
        public string Message { get; set; }

        /**
         * 6. Maksun kieli, (required)
         * Language of the payment selection page/bank interface if supported. 
         * Currently supported languages include Finnish FI, Swedish SE and English EN (AN  2)
         */
        public string Language { get; set; }

        /**
         * 7. Myyjän tunniste, P
         * Merchant id (AN 20)
         */
        public string MerchantId { get; set; }

        /**
         * 8. Paluu-linkki, P
         * Url called when returning successfully (AN 300)
         */
        public string ReturnUrl { get; set; }

        /**
         * 9. Peruutus-linkki, P
         * Url called when user cancelled payment (AN 300)
         */
        public string CancelUrl { get; set; }

        /**
         * 10. Hylätty-linkki, V
         * Url called when payment was rejected (No credit on credit card etc) (AN 300)
         */
        public string RejectUrl { get; set; }

        /**
         * 11. Viivästytetty-linkki, V
         * Url called when payment is initially successful but not yet confirmed (AN 300)
         */
        public string DelayedUrl { get; set; }

        /**
         * 12. Maa, (required)
         * Country of the buyer, affects available payment methods (AN 3)
         */
        public string Country { get; set; }

        /**
         * 13. Valuutta, P
         * Currency used in payment. Currently only EUR is supported (AN 3)
         */
        public string Currency { get; set; }

        /**
         * 14. Maksupääte, P                                                                                                            
         * device or method used when creating new transaction. Affects how Checkout servers respond to posting the new payment 1 = HTML 10 = XML (N 2)
         */
        public string Device { get; set; }

        /**
         * 15. Maksun tyyppi, P
         * Payment type or content of purchase. Used to differentiate between adult entertainment and everything else. 1 = normal, 2 = adult entertainment (N 2)
         */
        public string Content { get; set; }

        /**
         * 16. Maksutavat, P
         * Type, currently always 0 (N 1)
         */
        public string Type { get; set; }

        /**
         * 17. Algoritmi, 3 käytössä, P
         * Algorithm used when calculating mac, currently = 3. 1 and 2 are still available but deprecated (N 1)
         */
        public string Algorithm { get; set; }

        /**
         * 18. Toimituspäivä,  muotoa: VVVVKKPP, P
         * Expected delivery date (N 8) 
         */
        public string DeliveryDate { get; set; }

        /**
         * 19. Tilaajan etunimi, V
         * First name of customer (AN 40)
         */
        public string FirstName { get; set; }

        /**
         * 20. Tilaajan sukunimi, V
         * Last name of customer (AN 40)
         */
        public string FamilyName { get; set; }

        /**
         * 21. Toimitusosoite, V
         * Street address of customer (AN 40)
         */
        public string Address { get; set; }

        /**
         * 22. Postinumero, V
         * Postcode of customer (AN 14)
         */
        public string Postcode { get; set; }

        /**
         * 23. Toimituspostitoimipaikka, V
         * Post office of customer (AN 18)
         */
        public string PostOffice { get; set; }

        /**
        * 24. turvatarkiste, P  
        * MAC
        */
        public string MAC { get; set; }

        /**
         * Myyjän turva-avain, P (salainen, ei lähetetä, käytetään turvatarkisteen laskentaan)
         * Secret merchant key (not sent, used for calculating MAC)
         */
        [NotMapped]
        public string MerchantSecretKey { get; set; }


        /// <summary>
        /// Initialize method to be used for setting Payment Payment with two parameters.
        /// This method calls 
        /// <see cref="InitializeValues(string, string, string, string, string, string, string, string, string)"/>
        /// method.
        /// </summary>
        /// <param name="merchantId">7. Myyjän tunniste, P, Merchant id (AN 20)</param>
        /// <param name="merchantSecretKey">Myyjän turva-avain (ei lähetetä), P, Secret merchant key</param>
        public void InitializeValues(string merchantId, string merchantSecretKey)
        {
            InitializeValues("", "", "", "", merchantId, "", "", "", merchantSecretKey);
        }

        /// <summary>
        /// Initialize method to be used for setting Payment with nine parameters.
        /// These nine parameters are all required and don't have default values.
        /// This method calls 
        /// <see cref="InitializeValues(string, string, string, string, string, string, string, string, string, string, string, string, string, string, string, string)"/>
        /// method, and also sets seven required parameters with default values.
        /// </summary>
        /// <param name="stamp">2. Maksun tunnus, P, Unique identifier for the payment (AN 20)</param>
        /// <param name="amount">3. Maksun määrä, P, Amount of payment in cents (10 Euros == 1000) (N 8)</param>
        /// <param name="reference">4. Maksun viite, P, Reference number for the payment (AN 20)</param>
        /// <param name="language">6. Maksun kieli, Language of the payment selection page/bank interface if supported.</param>
        /// <param name="merchantId">7. Myyjän tunniste, P, Merchant id (AN 20)</param>
        /// <param name="returnUrl">8. Paluu-linkki, P, Url called when returning successfully (AN 300)</param>
        /// <param name="cancelUrl">9. Peruutus-linkki, P, Url called when user cancelled payment (AN 300)</param>
        /// <param name="deliveryDate">18. Toimituspäivä,  muotoa: VVVVKKPP, P, Expected delivery date (N 8) </param>
        /// <param name="merchantSecretKey">Myyjän turva-avain (ei lähetetä), P, Secret merchant key (not sent, used for calculating MAC)</param>
        public void InitializeValues(string stamp, string amount, string reference, string language,
                string merchantId, string returnUrl, string cancelUrl, string deliveryDate, string merchantSecretKey)
        {
            InitializeValues(Payment.Version_Default, stamp, amount, language, reference, merchantId,
                returnUrl, cancelUrl, Payment.Country_Default, Payment.Currency_Default, Payment.Device_Default,
                Payment.Content_Default, Payment.Type_Default, Payment.Algorithm_Default, deliveryDate, merchantSecretKey);
        }

        /// <summary>
        /// Initialize method to be used for setting Payment with sixteen parameters.
        /// These sixteen parameters are all required, of which seven have default values available.
        /// </summary>
        /// <param name="version">1. Maksun versio, P, Payment version, currently always '0001' (AN 4)</param>
        /// <param name="stamp">2. Maksun tunnus, P, Unique identifier for the payment (AN 20)</param>
        /// <param name="amount">3. Maksun määrä, P, Amount of payment in cents (10 Euros == 1000) (N 8)</param>
        /// <param name="reference">4. Maksun viite, P, Reference number for the payment (AN 20)</param>
        /// <param name="language">6. Maksun kieli, Language of the payment selection page/bank interface if supported.</param>
        /// <param name="merchantId">7. Myyjän tunniste, P, Merchant id (AN 20)</param>
        /// <param name="returnUrl">8. Paluu-linkki, P, Url called when returning successfully (AN 300)</param>
        /// <param name="cancelUrl">9. Peruutus-linkki, P, Url called when user cancelled payment (AN 300)</param>
        /// <param name="country">12. Maa, (required) Country of the buyer, affects available payment methods (AN 3)</param>
        /// <param name="currency">13. Valuutta, P, Currency used in payment. Currently only EUR is supported (AN 3)</param>
        /// <param name="device">14. Maksupääte, P, device or method used for the transaction. 1 = HTML 10 = XML (N 2)</param>
        /// <param name="content">15. Maksun tyyppi, P, Payment type or content of purchase. Used to differentiate between adult entertainment and everything else. 1 = normal, 2 = adult entertainment (N 2)</param>
        /// <param name="type">16. Maksutavat, P, Type, currently always 0 (N 1)</param>
        /// <param name="algorithm"> 17. Algoritmi, 3 käytössä, P, Algorithm used when calculating mac, currently = 3. 1 and 2 are still available but deprecated(N 1)</param>
        /// <param name="deliveryDate">18. Toimituspäivä,  muotoa: VVVVKKPP, P, Expected delivery date (N 8) </param>
        /// <param name="merchantSecretKey">Myyjän turva-avain (ei lähetetä), P, Secret merchant key (not sent, used for calculating MAC)</param>
        public void InitializeValues(string version, string stamp, string amount, string reference,
            string language, string merchantId, string returnUrl, string cancelUrl, string country,
            string currency, string device, string content, string type, string algorithm, string deliveryDate,
            string merchantSecretKey)
        {

            //set default values & initialize properties 
            this.Version = version;  //1. P "0001"
            this.Stamp = stamp; // 2. P
            this.Amount = amount; // 3. P
            this.Reference = reference; // 4. P
            this.Message = "";  // 5. V
            this.Language = "";  // 6. V 
            this.MerchantId = merchantId; // 7. P
            this.ReturnUrl = returnUrl;  // 8. P
            this.CancelUrl = cancelUrl;  // 9. P
            this.RejectUrl = "";  // 10. V
            this.DelayedUrl = "";  // 11. V
            this.Country = ""; // 12. V
            this.Currency = currency; // 13. P
            this.Device = device; // 14. P
            this.Content = content; // 15. P
            this.Type = type; // 16. P
            this.Algorithm = algorithm; // 17. P
            this.DeliveryDate = deliveryDate; // 18. P
            this.FirstName = ""; // 19. V
            this.FamilyName = ""; // 20. V
            this.Address = ""; // 21. V
            this.Postcode = ""; // 22. V
            this.PostOffice = ""; // 23. V
            this.MerchantSecretKey = merchantSecretKey;
        }

        /// <summary>
        /// 24. Calculates MAC, MD5-turvatarkiste lasketaan.
        /// This method uses
        /// <see cref="PaymentUtils.CalculateMD5HashUTF8(string)"/>
        /// </summary>
        /// <returns>MAC</returns>
        public string CalculateMac()
        {
            string macString = "";

            string macDelimiter = "+";

            macString = this.Version + macDelimiter +  //1. P 
            this.Stamp + macDelimiter + // 2. P
            this.Amount + macDelimiter + // 3. P
            this.Reference + macDelimiter + // 4. P
            this.Message + macDelimiter + // 5. V
            this.Language + macDelimiter + // 6. V
            this.MerchantId + macDelimiter + // 7. P
            this.ReturnUrl + macDelimiter +  // 8. P
            this.CancelUrl + macDelimiter + // 9. P
            this.RejectUrl + macDelimiter + // 10. V
            this.DelayedUrl + macDelimiter + // 11. V
            this.Country + macDelimiter + // 12. V
            this.Currency + macDelimiter + // 13. P
            this.Device + macDelimiter + // 14. P
            this.Content + macDelimiter + // 15. P
            this.Type + macDelimiter + // 16. P
            this.Algorithm + macDelimiter + // 17. P
            this.DeliveryDate + macDelimiter + // 18. P
            this.FirstName + macDelimiter + // 19. V
            this.FamilyName + macDelimiter + // 20. V
            this.Address + macDelimiter + // 21. V
            this.Postcode + macDelimiter + // 22. V
            this.PostOffice + macDelimiter + // 23. V
            this.MerchantSecretKey;

            macString = PaymentUtils.CalculateMD5HashUTF8(macString); //24.

            this.MAC = macString;

            return macString;
        }

        /// <summary>
        /// Returns form data that is sent to Checkout Finland
        /// This method uses
        /// <see cref="CalculateMac()"/>
        /// </summary>
        /// <returns>NameValueCollection</returns>
        public NameValueCollection FormData()
        {
            var reqparm = new NameValueCollection();
            reqparm.Add("VERSION", this.Version); //1.
            reqparm.Add("STAMP", this.Stamp);
            reqparm.Add("AMOUNT", this.Amount);
            reqparm.Add("REFERENCE", this.Reference);
            reqparm.Add("MESSAGE", this.Message);
            reqparm.Add("LANGUAGE", this.Language);
            reqparm.Add("MERCHANT", this.MerchantId);
            reqparm.Add("RETURN", this.ReturnUrl);
            reqparm.Add("CANCEL", this.CancelUrl);
            reqparm.Add("REJECT", this.RejectUrl);
            reqparm.Add("DELAYED", this.DelayedUrl);
            reqparm.Add("COUNTRY", this.Country);
            reqparm.Add("CURRENCY", this.Currency);
            reqparm.Add("DEVICE", this.Device);
            reqparm.Add("CONTENT", this.Content);
            reqparm.Add("TYPE", this.Type);
            reqparm.Add("ALGORITHM", this.Algorithm);
            reqparm.Add("DELIVERY_DATE", this.DeliveryDate);
            reqparm.Add("FIRSTNAME", this.FirstName);
            reqparm.Add("FAMILYNAME", this.FamilyName);
            reqparm.Add("ADDRESS", this.Address);
            reqparm.Add("POSTCODE", this.Postcode);
            reqparm.Add("POSTOFFICE", this.PostOffice);
            reqparm.Add("MAC", this.CalculateMac());  // 24. 
            return reqparm;
        }

        /// <summary>
        /// Validates forms parameters sent to Checkout Finland.
        /// The usage of this method is optional.
        /// This method is not used internally by Payment class.
        /// </summary>
        /// <exception cref="RequiredParameterMissingException">
        /// Thrown when required parameter is missing</exception>
        /// <exception cref="InvalidParameterException">
        /// Thrown when parameter is unsuitable</exception>
        public void Validate()
        {
            //1. VERSION
            if (string.IsNullOrEmpty(this.Version))
            {
                throw new RequiredParameterMissingException("VERSION value is missing");
            }
            else if (!this.Version.Equals("0001"))
            {
                throw new InvalidParameterException("VERSION value is not valid: " + this.Version);
            }

            //2. STAMP
            if (string.IsNullOrEmpty(this.Stamp))
            {
                throw new RequiredParameterMissingException("STAMP value is missing");
            }
            else if (this.Stamp.Length > 20)
            {
                throw new InvalidParameterException("STAMP value is too long: " + this.Stamp);
            }

            //3. AMOUNT
            double amountValue;

            if (Double.TryParse(this.Amount, out amountValue))
            {
                //amount has to be at least 100 cents (1 Euro)
                if (amountValue < 100)
                    throw new InvalidParameterException("AMOUNT value is too small: " + this.Amount);
                //amount can't equal or exceed 100 000 000 Cents (= 1 000 000 Euros) (Max 999 999 Euros and 99 cents)
                else if (amountValue >= 100000000)
                    throw new InvalidParameterException("AMOUNT value is too high: " + this.Amount);
            }
            else
            {
                throw new InvalidParameterException("AMOUNT value is not valid");
            }

            //4. REFERENCE
            if (string.IsNullOrEmpty(this.Reference))
            {
                throw new RequiredParameterMissingException("REFERENCE value is missing");
            }
            else if (this.Reference.Length > 20)
            {
                throw new InvalidParameterException("REFERENCE value is too long: " + this.Reference);
            }

            //5. MESSAGE
            if ((!string.IsNullOrEmpty(this.Message) &&
                  this.Message.Length > 1000))
                throw new InvalidParameterException("MESSAGE value is too long");

            //6. LANGUAGE
            if (string.IsNullOrEmpty(this.Language))
            {
                throw new RequiredParameterMissingException("LANGUAGE Url value is missing");
            }
            else if (!(this.Language.Equals("FI")
                      || this.Language.Equals("SE")
                      || this.Language.Equals("EN")))
            {
                throw new InvalidParameterException("LANGUAGE value needs to be FI, SE or EN");
            }

            //7. MERCHANT
            if (this.MerchantId.Length > 20)
                throw new InvalidParameterException("MERCHANT value is too long: " + this.Reference);

            //8. RETURN
            if (string.IsNullOrEmpty(this.ReturnUrl))
            {
                throw new RequiredParameterMissingException("RETURN Url value is missing");
            }
            else if (this.ReturnUrl.Length > 300)
            {
                throw new InvalidParameterException("RETURN Url value is too long");
            }

            //9. CANCEL
            if (string.IsNullOrEmpty(this.CancelUrl))
            {
                throw new RequiredParameterMissingException("CANCEL Url value is missing");
            }
            else if (this.CancelUrl.Length > 300)
            {
                throw new InvalidParameterException("CANCEL Url value is too long");
            }

            //10. REJECT
            if ((!string.IsNullOrEmpty(this.RejectUrl) &&
                this.RejectUrl.Length > 300))
            {
                throw new InvalidParameterException("REJECT Url value is too long");
            }

            //11. DELAYED
            if ((!string.IsNullOrEmpty(this.DelayedUrl) &&
                this.DelayedUrl.Length > 300))
            {
                throw new InvalidParameterException("DELAYED Url value is too long");
            }

            //12. COUNTRY
            if (string.IsNullOrEmpty(this.Country))
            {
                throw new RequiredParameterMissingException("COUNTRY value is missing");
            }
            else if (!this.Country.Equals("FIN"))
            {
                throw new InvalidParameterException("COUNTRY value needs to be FIN");
            }

            //13. CURRENCY
            if (string.IsNullOrEmpty(this.Currency))
            {
                throw new RequiredParameterMissingException("CURRENCY value is missing");
            }
            else if (!this.Currency.Equals("EUR"))
            {
                throw new InvalidParameterException("CURRENCY value needs to be EUR");
            }

            //14. DEVICE
            if (string.IsNullOrEmpty(this.Device))
            {
                throw new RequiredParameterMissingException("DEVICE value is missing");
            }
            else if (!(this.Device.Equals("1")
                      || this.Device.Equals("10")))
            {
                throw new InvalidParameterException("DEVICE value needs to be either 1 or 10");
            }

            //15. CONTENT
            if (string.IsNullOrEmpty(this.Content))
            {
                throw new RequiredParameterMissingException("CONTENT value is missing");
            }
            else if (!(this.Content.Equals("1")
                      || this.Content.Equals("2")))
            {
                throw new InvalidParameterException("CONTENT value needs to be either 1 or 2");
            }

            //16. TYPE
            if (string.IsNullOrEmpty(this.Type))
            {
                throw new RequiredParameterMissingException("TYPE value is missing");
            }
            else if (!this.Type.Equals("0"))
            {
                throw new InvalidParameterException("TYPE value needs to be 0");
            }

            //17. ALGORITHM
            if (string.IsNullOrEmpty(this.Algorithm))
            {
                throw new RequiredParameterMissingException("ALGORITHM value is missing");
            }
            else if (!(this.Algorithm.Equals("2")
                                 || this.Algorithm.Equals("3")))
            {
                throw new InvalidParameterException("ALGORITHM value needs to be either 2 or 3");
            }


            //18. DELIVERY_DATE
            int dateNumbers;

            if (string.IsNullOrEmpty(this.DeliveryDate))
            {
                throw new RequiredParameterMissingException("DELIVERY_DATE value is missing");
            }
            else if (this.DeliveryDate.Length < 8)
            {
                throw new InvalidParameterException("DELIVERY_DATE value is too short");
            }
            else if (this.DeliveryDate.Length > 8)
            {
                throw new InvalidParameterException("DELIVERY_DATE value is too long");
            }
            else if (!int.TryParse(this.DeliveryDate, out dateNumbers))
            {
                throw new InvalidParameterException("DELIVERY_DATE value is not numeric");
            }

            //19. FIRSTNAME
            if ((!string.IsNullOrEmpty(this.FirstName) &&
                this.FirstName.Length > 40))
            {
                throw new InvalidParameterException("FIRSTNAME value is too long");
            }

            //20. FAMILYNAME
            if ((!string.IsNullOrEmpty(this.FamilyName) &&
                this.FamilyName.Length > 40))
            {
                throw new InvalidParameterException("FAMILYNAME value is too long");
            }

            //21. ADDRESS
            if ((!string.IsNullOrEmpty(this.Address) &&
                this.Address.Length > 40))
            {
                throw new InvalidParameterException("ADDRESS value is too long");
            }

            //22. POSTCODE
            if ((!string.IsNullOrEmpty(this.Postcode) &&
                this.Postcode.Length > 14))
            {
                throw new InvalidParameterException("POSTCODE value is too long");
            }

            //23. POSTOFFICE
            if ((!string.IsNullOrEmpty(this.PostOffice) &&
                this.PostOffice.Length > 18))
            {
                throw new InvalidParameterException("POSTOFFICE value is too long");
            }
        }
    }
}
