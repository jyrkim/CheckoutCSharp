using System;
using System.Collections.Generic;


namespace Checkout.User_Controls
{
    public partial class MerchantAndBuyerInfo : System.Web.UI.UserControl
    {
        public Dictionary<string, string> Translations;
        public Payment payment;

        protected void Page_Load(object sender, EventArgs e)
        {
            this.titlePaymentReceiver.Text = Translations["titlePaymentReceiver"];
            this.titleOrderInformation.Text = Translations["titleOrderInformation"];
            this.titleMerchantName.Text = Translations["titleMerchantName"];

            //merchant info 
            //some of this data is also available
            //from Payment object and XML response
            string MerchantNameAndVatId = "Testi Oy (123456-7)";
            string MerchantEmail = "testi@checkout.fi";
            string MerchantAddress = "Testikuja 1";
            string MerchantPostCodeAndPostOffice = "12345 Testilä";
            string MerchantPhone = "012-345 678";

            this.merchantName.Text = Translations["merchantName"];
            this.merchantNameAndVatId.Text = MerchantNameAndVatId;
            this.merchantEmail.Text = Translations["merchantEmail"];
            this.merchantEmailValue.Text = MerchantEmail;

            this.merchantAddress.Text = Translations["merchantAddress"];
            this.merchantAddressValue.Text = MerchantAddress;
            this.merchantPostCodeAndPostOffice.Text = MerchantPostCodeAndPostOffice;
            this.merchantHelpdeskNumber.Text = Translations["merchantHelpdeskNumber"];
            this.merchantPhone.Text = MerchantPhone;

            this.titleBuyer.Text = Translations["titleBuyer"];
            this.buyerName.Text = Translations["buyerName"];
            this.buyerNameValue.Text = payment.FirstName + " " + payment.FamilyName;

            this.buyerAddress.Text = Translations["buyerAddress"];
            this.buyerAddressValue.Text = payment.Address;
            this.buyerPostCodeAndPostOfficeValue.Text = payment.Postcode + " " + payment.PostOffice;

            this.titleOrder.Text = Translations["titleOrder"];
            this.orderAmount.Text = Translations["orderAmount"];
            this.orderAmountValue.Text = payment.Amount.Insert((payment.Amount.Length - 2), ",");

            this.orderDescription.Text = Translations["orderDescription"];
            this.orderDescriptionValue.Text = payment.Message;

        }
    }
}