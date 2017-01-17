using System;
using System.Collections.Generic;

namespace Checkout.User_Controls
{
    public partial class CheckoutFinlandInfo : System.Web.UI.UserControl
    {
        public Dictionary<string, string> Translations;

        protected void Page_Load(object sender, EventArgs e)
        {
            this.checkoutFinlandInfo.Text = Translations["checkoutFinlandInfo"];
        }
    }
}