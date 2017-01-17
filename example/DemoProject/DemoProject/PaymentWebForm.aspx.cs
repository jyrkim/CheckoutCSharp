using System;
using System.Net;
using System.Web;


namespace Checkout
{
    public partial class PaymentWebForm : System.Web.UI.Page
    {
        private string merchantId = "375917";
        private string merchantSecretKey = "SAIPPUAKAUPPIAS";
        private string bankPaymentOptions = "";
        private Payment payment;

        protected void Page_Load(object sender, EventArgs e)
        {

            if (IsPostBack)
            {

                //the below creates for example
                //URL: http://localhost:53416/PaymentResponseWebForm.aspx";
                string urlBeginning = this.TestUrl();
                string returnURL = urlBeginning + "/PaymentResponseWebForm.aspx";
                string cancelURL = urlBeginning + "/PaymentResponseWebForm.aspx";
                string rejectURL = urlBeginning + "/PaymentResponseWebForm.aspx";
                string delayedURL = urlBeginning + "/PaymentResponseWebForm.aspx";

                //Create Payment
                Payment payment = new Payment();

                //set merchant data (required)
                payment.InitializeValues(merchantId, merchantSecretKey);

                //other required parameters
                payment.Stamp = DateTime.Now.ToString("MMddyyyyhhmmssfff");
                string modifiedAmount = PaymentUtils.ModifyAmount(this.AmountTextBox.Text); // remove comma or dot
                payment.Amount = modifiedAmount; // 1 Euro minimum purchase (100 cents)
                Session["Amount"] = modifiedAmount; //store Amount in Session for Query (just for testing)
                payment.Reference = "123456";
                payment.ReturnUrl = returnURL;
                payment.CancelUrl = cancelURL;
                payment.DeliveryDate = DateTime.Now.ToString("yyyyMMdd");

                payment.Language = this.LanguageDropDownList.SelectedValue; // (required param)
                payment.Country = "FIN"; // (required)

                payment.Device = this.DeviceDropDownList.SelectedValue;  //HTML or XML 

                //Optional
                payment.RejectUrl = rejectURL;
                payment.DelayedUrl = delayedURL;
                payment.FirstName = this.FirstNameTextBox.Text;
                payment.FamilyName = this.FamilyNameTextBox.Text;
                payment.Address = this.AddressTextBox.Text;
                payment.Postcode = this.PostcodeTextBox.Text;
                payment.PostOffice = this.PostcodeTextBox.Text;
                payment.Message = this.MessageTextBox.Text;

                payment.Validate(); //optional validation (Checkout Finland validates data too)

                //Send payment data to Checkout Finland,
                //then make received payment options available 
                //to the Web Form
                try
                {
                    this.BankPaymentOptions = CheckoutClient.postPaymentData(payment.FormData());
                }
                catch (WebException ex)
                {
                    //exception handling for testing
                    string errorMessage = ex.ToString();
                    errorMessage = (errorMessage.Length > 1000) ? errorMessage.Substring(0, 1000) : errorMessage;
                    Response.Redirect("~/PaymentErrorWebForm.aspx?error=" + HttpUtility.UrlEncode(errorMessage));
                }

                //if device HTML (1), then use WebForm for HTML page
                if (payment.Device.Equals(Checkout.Payment.Device_HTML))
                {
                    Server.Transfer("PaymentOptionsHTML.aspx");
                }
                else //WebForm for XML format 
                {
                    this.Payment = payment;

                    Server.Transfer("PaymentOptionsXML.aspx");
                }

            }

        }

        public String BankPaymentOptions
        {
            get
            {
                return bankPaymentOptions;
            }
            set
            {
                bankPaymentOptions = value;
            }
        }

        public Payment Payment
        {
            get
            {
                return payment;
            }
            set
            {
                payment = value;
            }
        }

        /// <summary>
        /// Method creates test URLs sent to Checkout Finland.
        /// This method is for test/development environment,
        /// because host and http(s) ports might be different
        /// for Visual Studio projects.
        /// This method returns for example
        /// URL: http://localhost:53416
        /// </summary>
        /// <returns>URL string</returns>
        private string TestUrl()
        {
            string urlBeginning;

            if (System.Web.HttpContext.Current.Request.IsSecureConnection)
            {
                urlBeginning = "https://";
            }
            else
            {
                urlBeginning = "http://";
            }

            urlBeginning = urlBeginning + System.Web.HttpContext.Current.Request.Url.Host +
                ":" + System.Web.HttpContext.Current.Request.Url.Port;

            return urlBeginning;
        }


    }
}