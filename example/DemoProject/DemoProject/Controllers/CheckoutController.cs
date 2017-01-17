using System;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Net;
using ViewModel;
using Checkout;

namespace Controllers
{
    public class CheckoutController : Controller
    {
        private string merchantId = "375917";
        private string merchantSecretKey = "SAIPPUAKAUPPIAS";

        [HttpGet]
        public ActionResult Payment()
        {
            ViewBag.Language = this.LanguageListItems();
            ViewBag.Device = this.DeviceListItems();
            return View(new PaymentViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Payment(PaymentViewModel paymentViewModel)
        {
            //the below creates for example
            //URL: http://localhost:53416/Checkout/PaymentResponse
            string urlBeginning = this.TestUrl();
            string returnURL = urlBeginning + "/Checkout/PaymentResponse";
            string cancelURL = urlBeginning + "/Checkout/PaymentResponse";
            string rejectURL = urlBeginning + "/Checkout/PaymentResponse";
            string delayedURL = urlBeginning + "/Checkout/PaymentResponse";

            //Create Payment
            Payment payment = new Payment();

            //set merchant data (required)
            payment.InitializeValues(merchantId, merchantSecretKey);

            //other required parameters
            payment.Stamp = DateTime.Now.ToString("MMddyyyyhhmmssfff");
            string modifiedAmount = PaymentUtils.ModifyAmount(paymentViewModel.Amount); // remove comma or dot
            payment.Amount = modifiedAmount; // 1 Euro minimum purchase (100 cents)
            Session["Amount"] = modifiedAmount; //store Amount in Session for Query (just for testing)
            payment.Reference = "123456";
            payment.ReturnUrl = returnURL;
            payment.CancelUrl = cancelURL;
            payment.DeliveryDate = DateTime.Now.ToString("yyyyMMdd");

            payment.Language = paymentViewModel.Language; // (required param)
            payment.Country = "FIN"; // (required)

            payment.Device = paymentViewModel.Device;  //HTML or XML 

            //Optional
            payment.RejectUrl = rejectURL;
            payment.DelayedUrl = delayedURL;
            payment.FirstName = paymentViewModel.FirstName;
            payment.FamilyName = paymentViewModel.FamilyName;
            payment.Address = paymentViewModel.Address;
            payment.Postcode = paymentViewModel.Postcode;
            payment.PostOffice = paymentViewModel.PostOffice;
            payment.Message = paymentViewModel.Message;

            payment.Validate(); //optional validation (Checkout Finland validates data too)

            //Send payment data to Checkout Finland,
            //then make received payment options available 
            //to the view in ViewBag
            try
            {
                ViewBag.BankPaymentOptions = CheckoutClient.postPaymentData(payment.FormData());
            }
            catch (WebException ex)
            {
                //exception handling for testing
                ViewBag.PaymentResponseMessage = "error: " + ex.ToString();
                return View("PaymentResponseError");
            }

            //if device HTML (1), then use view for HTML page
            if (payment.Device.Equals(Checkout.Payment.Device_HTML))
            {
                return View("BankPaymentOptionsHtml");
            }
            else //parse XML format 
            {
                //pass merchant info for view (optional)
                //some of this data is also available
                //from Payment object and XML response
                ViewBag.MerchantNameAndVatId = "Testi Oy (123456-7)";
                ViewBag.MerchantEmail = "testi@checkout.fi";
                ViewBag.MerchantAddress = "Testikuja 1";
                ViewBag.MerchantPostCodeAndPostOffice = "12345 Testilä";
                ViewBag.MerchantPhone = "012-345 678";

                //translations for Xml view 
                ViewBag.Translations = PaymentUtils.TranslationsForXml[payment.Language];

                //payment for view
                ViewBag.Payment = payment;

                return View("BankPaymentOptionsXML");
            }
        }


        [HttpGet]
        public ActionResult PaymentResponse(
            String VERSION, String STAMP, String REFERENCE,
            String PAYMENT, String STATUS, String ALGORITHM, String MAC)
        {
            PaymentResponse paymentResponse = new PaymentResponse();
            paymentResponse.SetValues(merchantSecretKey, Request.Params);

            //if response valid (ok data)
            if (paymentResponse.IsValid())
            {
                ViewBag.PaymentResponseMessage =
                    PaymentUtils.GetPaymentResponseStatusMessage(paymentResponse.Status);
            }
            else //invalid response (corrupt data)
            {
                ViewBag.PaymentResponseMessage = "response invalid";
                return View("PaymentResponseError");
            }

            //set form values ready for payment status query
            QueryViewModel queryViewModel = new QueryViewModel();
            queryViewModel.Stamp = paymentResponse.Stamp;
            queryViewModel.Reference = paymentResponse.Reference;
            //Get Amount from Session, because it's not available
            //from PaymentResponse (for testing). 
            queryViewModel.Amount = (string)Session["Amount"];

            return View(queryViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Query(QueryViewModel queryViewModel)
        {
            //testing query functionality
            Query query = new Query();
            query.InitializeValues(queryViewModel.Stamp, queryViewModel.Reference,
                this.merchantId, queryViewModel.Amount, this.merchantSecretKey);

            //send Query to Checkout Finland
            try
            {
                ViewBag.QueryResponse = CheckoutClient.postQueryData(query.FormData());
            }
            catch (WebException ex)
            {
                //exception handling for testing
                ViewBag.PaymentResponseMessage = "error: " + ex.ToString();
                return View("PaymentResponseError");
            }

            //get status value
            XDocument xmlDoc = XDocument.Parse(ViewBag.QueryResponse);
            XElement statusNode = (XElement)xmlDoc.FirstNode;
            string statusValue = statusNode.Value;

            ViewBag.QueryResponseMessage =
                PaymentUtils.GetPaymentResponseStatusMessage(statusValue);

            return View();
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

        /// <summary>
        /// Used for creating Language list for DropDownList 
        /// </summary>
        /// <returns></returns>
        private List<SelectListItem> LanguageListItems()
        {
            List<SelectListItem> items = new List<SelectListItem>();

            items.Add(new SelectListItem { Text = "Suomi", Value = "FI" });

            items.Add(new SelectListItem { Text = "Svenska", Value = "SE" });

            items.Add(new SelectListItem { Text = "English", Value = "EN", Selected = true });

            return items;
        }

        /// <summary>
        /// Used for creating Device list for DropDownList 
        /// </summary>
        /// <returns></returns>
        private List<SelectListItem> DeviceListItems()
        {
            List<SelectListItem> items = new List<SelectListItem>();

            items.Add(new SelectListItem { Text = "HTML", Value = "1" });

            items.Add(new SelectListItem { Text = "XML", Value = "10", Selected = true });

            return items;

        }

    }
}