using System;
using System.Xml.Linq;
using System.Net;
using System.Web;


namespace Checkout
{
    public partial class QueryWebForm : System.Web.UI.Page
    {
        private string merchantId = "375917";
        private string merchantSecretKey = "SAIPPUAKAUPPIAS";

        protected void Page_Load(object sender, EventArgs e)
        {

            //testing query functionality 
            Query query = new Query();
            query.InitializeValues((string)Session["Stamp"], (string)Session["Reference"],
                this.merchantId, (string)Session["Amount"], this.merchantSecretKey);

            //send Query to Checkout Finland
            string queryResponse = "";
            try
            {
                queryResponse = CheckoutClient.postQueryData(query.FormData());
            }
            catch (WebException ex)
            {
                //exception handling for testing
                string errorMessage = ex.ToString();
                errorMessage = (errorMessage.Length > 1000) ? errorMessage.Substring(0, 1000) : errorMessage;
                Response.Redirect("~/PaymentErrorWebForm.aspx?error=" + HttpUtility.UrlEncode(errorMessage));
            }

            XDocument xmlDoc = XDocument.Parse(queryResponse);
            XElement statusNode = (XElement)xmlDoc.FirstNode;
            string statusValue = statusNode.Value;
            this.queryResponseMessage.Text =
                PaymentUtils.GetPaymentResponseStatusMessage(statusValue);
            XElement tradeElem = (XElement)xmlDoc.FirstNode;
            XElement statusElem = (XElement)tradeElem.FirstNode;

            this.tradeBegin.Text = tradeElem.Name.ToString();
            this.tradeEnd.Text = tradeElem.Name.ToString();
            this.statusBegin.Text = statusElem.Name.ToString();
            this.statusEnd.Text = statusElem.Name.ToString();
            this.statusValue.Text = statusElem.Value;

        }
    }
}