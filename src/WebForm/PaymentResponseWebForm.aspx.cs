using System;


namespace Checkout
{
    public partial class PaymentResponseWebForm : System.Web.UI.Page
    {
        private string merchantSecretKey = "SAIPPUAKAUPPIAS";

        protected void Page_Load(object sender, EventArgs e)
        {
            PaymentResponse paymentResponse = new PaymentResponse();
            paymentResponse.SetValues(merchantSecretKey, Request.Params);

            //if response valid (ok data)
            if (paymentResponse.IsValid())
            {
                this.paymentResponseMessage.Text =
                    PaymentUtils.GetPaymentResponseStatusMessage(paymentResponse.Status);
            }
            else //invalid response (corrupt data)
            {
                Response.Redirect("~/PaymentErrorWebForm.aspx?error=response invalid");
            }

            //save values ready for payment status query 
            //Session used just for testign       
            Session["Stamp"] = paymentResponse.Stamp;
            Session["Reference"] = paymentResponse.Reference;
        }
    }
}