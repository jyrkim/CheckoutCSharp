using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;


namespace Checkout
{
    /// <summary>
    /// Cryptography methods for MD5 Hash and HMAC SHA256.
    /// General utilities for translations and messages.
    /// Most translations are identical to the ones used by Checkout Finland.
    /// Feel free to modify translations for your organisation's specific needs.
    /// For example if your organisation is a society, not a company (business organisation),
    /// then you can change 'titleMerchantName' key value from "Merchant's Name"
    /// to "Society's Name" or similar. 
    /// </summary>
    public class PaymentUtils
    {
        /// <summary>
        /// Translations for Finnish, Swedish and English languages
        /// that can be used with XML response.
        /// The usage of this method for translations is optional.
        /// </summary>
        /// <returns>Dictionary of Dictionary classes that each have translations</returns>
        public static Dictionary<string, Dictionary<string, string>> TranslationsForXml = new Dictionary<string, Dictionary<string, string>>
        {
            { "FI", FinnishTranslations() },
            { "SE", SwedishTranslations() },
            { "EN", EnglishTranslations() }
        };

        /// <summary>
        /// Has Finnish translations that can be used with XML response.
        /// The usage of this method and translations is optional.
        /// </summary>
        /// <returns>Dictionary with translations</returns>
        public static Dictionary<string, string> FinnishTranslations()
        {

            Dictionary<string, string> labels = new Dictionary<string, string>();

            //titles
            labels.Add("titlePaymentReceiver", "Maksun saaja");
            labels.Add("titleMerchantName", "Markkinointinimi");
            labels.Add("titleOrderInformation", "Tilauksen tiedot");
            labels.Add("titleBuyer", "Maksaja");
            labels.Add("titleOrder", "Tilaus");

            //merchant labels
            labels.Add("merchantName", "Yritys:");
            labels.Add("merchantAddress", "Osoite:");
            labels.Add("merchantEmail", "Email:");
            labels.Add("merchantHelpdeskNumber", "Puhelin:");

            //buyer labels
            labels.Add("buyerName", "Nimi:");
            labels.Add("buyerAddress", "Osoite:");

            //order labels
            labels.Add("orderAmount", "Summa:");
            labels.Add("orderDescription", "Kuvaus:");

            labels.Add("cancelLink", "Peruuta maksaminen");

            labels.Add("checkoutFinlandInfo", "Checkout maksunvälityspalvelu välittää " +
                "maksusi kauppiaalle turvallisesti. Toimintamme on maksulaitoslain " +
                "siirtymäsäännöksen nojalla merkitty Finanssivalvonnan ylläpitämään maksulaitosrekisteriin.");

            labels.Add("checkoutFinlandContact", "Checkout Finland Oy • PL 322 33101 Tampere • Y-tunnus 2196606-6 • Checkout on osa OP Ryhmää.");

            return labels;
        }

        /// <summary>
        /// Has Swedish translations that can be used with XML response.
        /// The usage of this method and translations is optional.
        /// </summary>
        /// <returns>Dictionary with translations</returns>
        public static Dictionary<string, string> SwedishTranslations()
        {

            Dictionary<string, string> labels = new Dictionary<string, string>();

            //titles
            labels.Add("titlePaymentReceiver", "Betalningsmottagare");
            labels.Add("titleMerchantName", "Organisation");
            labels.Add("titleOrderInformation", "Beställningsinformation");
            labels.Add("titleBuyer", "Betalare");
            labels.Add("titleOrder", "Beställningen");

            //merchant labels
            labels.Add("merchantName", "Företag:");
            labels.Add("merchantAddress", "Adress:");
            labels.Add("merchantEmail", "E-post:");
            labels.Add("merchantHelpdeskNumber", "Telefonnummer:");

            //buyer labels
            labels.Add("buyerName", "Namn:");
            labels.Add("buyerAddress", "Adress:");

            //order labels
            labels.Add("orderAmount", "Totalbelopp:");
            labels.Add("orderDescription", "Beskrivning:");

            labels.Add("cancelLink", "Avbryt betalningen");

            labels.Add("checkoutFinlandInfo", "Checkout överför din betalning på ett säkert sätt till handlaren.");

            labels.Add("checkoutFinlandContact", "Checkout Finland Oy • PL 322 33101 Tammerfors • FO-nummer 2196606-6 • Checkout on osa OP Ryhmää.");

            return labels;
        }

        /// <summary>
        /// Has English translations that can be used with XML response.
        /// The usage of this method and translations is optional.
        /// </summary>
        /// <returns>Dictionary with translations</returns>
        public static Dictionary<string, string> EnglishTranslations()
        {

            Dictionary<string, string> labels = new Dictionary<string, string>();

            //titles
            labels.Add("titlePaymentReceiver", "Payment receiver");
            labels.Add("titleMerchantName", "Merchant's Name");
            labels.Add("titleOrderInformation", "Order information");
            labels.Add("titleBuyer", "Buyer");
            labels.Add("titleOrder", "Order");

            //merchant labels
            labels.Add("merchantName", "Company:");
            labels.Add("merchantAddress", "Address:");
            labels.Add("merchantEmail", "Email:");
            labels.Add("merchantHelpdeskNumber", "Phone:");

            //buyer labels
            labels.Add("buyerName", "Name:");
            labels.Add("buyerAddress", "Address:");

            //order labels
            labels.Add("orderAmount", "Total amount:");
            labels.Add("orderDescription", "Description:");

            labels.Add("cancelLink", "Cancel Payment");

            labels.Add("checkoutFinlandInfo", "Checkout transfers your payment safely to the merchant.");

            labels.Add("checkoutFinlandContact", "Checkout Finland Oy • PL 322 33101 Tampere • Reg.number FI21966066 • Checkout on osa OP Ryhmää.");

            return labels;
        }

        /// <summary>
        /// This returns payment status for PaymentResponse.
        /// Messages are based on Checkout Findland's doc:
        /// CTD-Tekninenrajapintakuvaus-210415-1146-170.pdf (page 5),
        /// "Verkkokauppaohjelmisto voi hyväksyä maksun suoritetuksi 
        /// kun maksun tila on 2,5,6,7,8,9 tai 10."
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        public static string GetPaymentResponseStatusMessage(string status)
        {
            string statusMessage = "";

            int statusInt = Convert.ToInt32(status);

            //if status 2, or between 5 and 10 then paid (6,7,8,9 are all paid)
            if ((PaymentResponse.Payment_Paid_I == statusInt) ||
               ((PaymentResponse.Payment_Paid_II <= statusInt) &&
                (PaymentResponse.Payment_Paid_VII >= statusInt)))
            {
                statusMessage = "Paid";
            }
            else if (PaymentResponse.Payment_In_Process == statusInt)
            {
                statusMessage = "In process/not complete";
            }
            else if ((PaymentResponse.Payment_Delayed_I == statusInt) ||
                     (PaymentResponse.Payment_Delayed_II == statusInt))
            {
                statusMessage = "Delayed";
            }
            else if (PaymentResponse.Payment_Cancellation_Proposed_By_Buyer
                == statusInt)
            {
                statusMessage = "Cancelled by buyer";
            }
            else if (PaymentResponse.Payment_Cancelled_By_System
              == statusInt)
            {
                statusMessage = "Cancelled by system";
            }
            else if (PaymentResponse.Payment_Timed_Out
                == statusInt)
            {
                statusMessage = "Timed out";
            }
            else if (PaymentResponse.Payment_Not_Found
                == statusInt)
            {
                statusMessage = "Not Found";
            }
            else if (PaymentResponse.Payment_Refunded
                == statusInt)
            {
                statusMessage = "Refunded";
            }

            return statusMessage;
        }

        /// <summary>
        /// Calculates MD5 hash from string and returns it in hex string format
        /// </summary>
        /// <param name="input">string</param>
        /// <returns>hash</returns>
        public static string CalculateMD5HashUTF8(string input) //UTF-8 encoding used
        {
            // step 1, calculate MD5 hash from input

            MD5 md5 = System.Security.Cryptography.MD5.Create();

            byte[] inputBytes = System.Text.Encoding.UTF8.GetBytes(input);

            byte[] hash = md5.ComputeHash(inputBytes);

            // step 2, convert byte array to hex string

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }

            return sb.ToString();
        }

        /// <summary>
        /// Calculates HMACSHA256
        /// </summary>
        /// <param name="key">key used for calculation</param>
        /// <param name="message">hash value for calculation</param>
        /// <returns>computed hash value in string format</returns>
        public static string CalculateHashHMAC(byte[] key, byte[] message)
        {
            var hash = new HMACSHA256(key);
            return BitConverter.ToString(hash.ComputeHash(message)).Replace("-", "");
        }

        /// <summary>
        /// removes dot or comma separator from amount,
        /// for testing (not production) 
        /// </summary>
        /// <param name="amount">amount in string format</param>
        /// <returns>modified string amount</returns>
        public static string ModifyAmount(string amount)
        {
            string modifiedAmount = amount;

            if (amount.IndexOf(",") > -1)
            {
                modifiedAmount = amount.Replace(",", "");
            }
            else if (amount.IndexOf(".") > -1)
            {
                modifiedAmount = amount.Replace(".", "");
            }

            return modifiedAmount;
        }
    }
}
