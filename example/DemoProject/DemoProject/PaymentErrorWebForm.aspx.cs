using System;


namespace Checkout
{
    public partial class PaymentErrorWebForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.errorMessage.Text = Request.QueryString["error"];
        }
    }
}