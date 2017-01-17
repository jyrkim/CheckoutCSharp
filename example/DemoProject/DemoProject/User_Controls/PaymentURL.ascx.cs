using System;
using System.Linq;
using System.Xml.Linq;

namespace Checkout.User_Controls
{
    public partial class PaymentURL : System.Web.UI.UserControl
    {
        //BankPaymentOptions.ascx sets XML value
        public string XML = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            XDocument xmlDoc = XDocument.Parse(this.XML);
            XText paymentURLText = (XText)xmlDoc.Descendants("paymentURL").Nodes().First();

            this.Literal1.Text =
                "<a href='" + paymentURLText.Value + "'> Depreciated Payment Link</a>";

        }
    }
}