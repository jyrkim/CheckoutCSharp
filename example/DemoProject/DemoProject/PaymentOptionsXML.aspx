<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PaymentOptionsXML.aspx.cs" Inherits="Checkout.PaymentOptionsXML" %>
<%@ Import Namespace="System.Web.Optimization" %>
<%@ PreviousPageType VirtualPath="~/PaymentWebForm.aspx" %>
<%@ Register Src="~/User_Controls/BankPaymentOptions.ascx" TagPrefix="uc1" TagName="BankPaymentOptions" %>
<%@ Register Src="~/User_Controls/MerchantAndBuyerInfo.ascx" TagPrefix="uc1" TagName="MerchantAndBuyerInfo" %>
<%@ Register Src="~/User_Controls/CheckoutFinlandInfo.ascx" TagPrefix="uc1" TagName="CheckoutFinlandInfo" %>
<%@ Register Src="~/User_Controls/PaymentURL.ascx" TagPrefix="uc1" TagName="PaymentURL" %>

<!DOCTYPE html>
<!-- HTML elements and CSS (Checkout.css) follow closely the example at Checkout Finland's Web page:
    http://demo1.checkout.fi/xml2.php -->

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width" />
    <title>BankPaymentOptionsXML</title>
    <%:Styles.Render("~/Content/Checkout.css") %>
</head>
<body>
       <div id="container">
        
        <div id="top">
        </div>

        <div id="middle">
            <div id="center">
                <div id="content">
                    <br>
                    <center>
                        <div style="width: 95%; text-align: left;">

                            <!-- optional control for merchant and buyer info -->
                            <uc1:MerchantAndBuyerInfo runat="server" id="MerchantAndBuyerInfo" />

                            <br>
                            <!-- bank payment options control -->
                            <uc1:BankPaymentOptions runat="server" id="BankPaymentOptions" />
                            
                            <!-- depreciated payment url -->
                            <!-- <uc1:PaymentURL runat="server" id="PaymentURL" />  -->

                            <hr style="clear: both;">

                            <!-- optional control for Checkout Finland info -->
                            <uc1:CheckoutFinlandInfo runat="server" id="CheckoutFinlandInfo" />

                        </div>

                    </center>
                </div>
            </div><!-- eof center -->
        </div> <!-- eof middle -->

        <div id="bottom">
            <div id="bottomcenter">
                <p class="kesk"><asp:Literal ID="checkoutFinlandContact" Text=""  runat="server">  </asp:Literal></p>
            </div>
        </div> <!-- eof bottom -->
    </div> <!-- eof container -->
</body>
</html>
