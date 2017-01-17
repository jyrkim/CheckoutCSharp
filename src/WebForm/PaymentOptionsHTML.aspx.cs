using System;

namespace Checkout
{
    public partial class PaymentOptionsHTML : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string bankPaymentOptions = PreviousPage.BankPaymentOptions;
            this.Literal1.Text = bankPaymentOptions;
        }
    }
}