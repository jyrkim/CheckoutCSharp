using System;
using System.Collections.Generic;


namespace Checkout
{
    public partial class PaymentOptionsXML : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            //Initialize User Control member values

            //XML
            string bankPaymentOptions = PreviousPage.BankPaymentOptions;
            BankPaymentOptions.XML = bankPaymentOptions;
            PaymentURL.XML = bankPaymentOptions; //optional

            //Translations
            Dictionary<string, string> translations =
                PaymentUtils.TranslationsForXml[PreviousPage.Payment.Language];

            MerchantAndBuyerInfo.Translations = translations;
            BankPaymentOptions.Translations = translations;
            CheckoutFinlandInfo.Translations = translations;

            //Payment
            MerchantAndBuyerInfo.payment = PreviousPage.Payment;

            //Checkout Finland contact Literal control
            this.checkoutFinlandContact.Text = translations["checkoutFinlandContact"];
        }
    }
}