<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MerchantAndBuyerInfo.ascx.cs" Inherits="Checkout.User_Controls.MerchantAndBuyerInfo" %>

<div class="Tables-Container">

    <table width="100%" border="0" cellspacing="0">
        <tr> 
            <td with="30%" valign="top">
                <h2><asp:Literal ID="titlePaymentReceiver" Text=""  runat="server">  </asp:Literal></h2>
            </td>
            <td colspan="2" width="35%" valign="top">
                <h2><asp:Literal ID="titleOrderInformation" Text=""  runat="server">  </asp:Literal></h2>
            </td>
        </tr>
        <tr>
            <td valign="top">
                <h3><asp:Literal ID="titleMerchantName" Text=""  runat="server">  </asp:Literal></h3>
                <table width="100%" cellspacing="0" cellpadding="0" border="0" style='border-collapse:collapse;'>
                    <tr>
                        <td valign="top" width="70"><asp:Literal ID="merchantName" Text=""  runat="server">  </asp:Literal></td>
                        <td>
                            <asp:Literal ID="merchantNameAndVatId" Text=""  runat="server">  </asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top"><asp:Literal ID="merchantEmail" Text=""  runat="server">  </asp:Literal></td>
                        <td><asp:Literal ID="merchantEmailValue" Text=""  runat="server">  </asp:Literal></td>
                    </tr>
                    <tr>
                        <td valign="top"><asp:Literal ID="merchantAddress" Text=""  runat="server">  </asp:Literal></td>
                        <td>
                            <asp:Literal ID="merchantAddressValue" Text=""  runat="server">  </asp:Literal>
                            <br />
                            <asp:Literal ID="merchantPostCodeAndPostOffice" Text=""  runat="server">  </asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top"><asp:Literal ID="merchantHelpdeskNumber" Text=""  runat="server">  </asp:Literal></td>
                        <td><asp:Literal ID="merchantPhone" Text=""  runat="server">  </asp:Literal></td>
                    </tr>
                </table>
            </td>
            <td valign="top">
                <h3><asp:Literal ID="titleBuyer" Text=""  runat="server">  </asp:Literal></h3>
                <table width="100%" cellspacing="0" cellpadding="0" border="0" style='border-collapse:collapse;'>
                    <tr>
                        <td valign="top" width="58"><asp:Literal ID="buyerName" Text=""  runat="server">  </asp:Literal></td>
                        <td><asp:Literal ID="buyerNameValue" Text=""  runat="server">  </asp:Literal></td>
                    </tr>
                    <tr>
                        <td valign="top"><asp:Literal ID="buyerAddress" Text=""  runat="server">  </asp:Literal></td>
                        <td>
                            <asp:Literal ID="buyerAddressValue" Text=""  runat="server">  </asp:Literal><br />
                            <asp:Literal ID="buyerPostCodeAndPostOfficeValue" Text=""  runat="server">  </asp:Literal>
                        </td>
                    </tr>
                </table>
            </td>
            <td width="35%" valign="top">
                <h3><asp:Literal ID="titleOrder" Text=""  runat="server">  </asp:Literal></h3>
                <table width="100%" cellspacing="0" cellpadding="0" border="0" style='border-collapse:collapse;'>
                    <tr>
                        <td valign="top" width="82"><asp:Literal ID="orderAmount" Text=""  runat="server">  </asp:Literal></td>
                        <td><asp:Literal ID="orderAmountValue" Text=""  runat="server"></asp:Literal> &euro;</td>
                    </tr>
                    <tr>
                        <td valign="top"><asp:Literal ID="orderDescription" Text=""  runat="server">  </asp:Literal></td>
                        <td>
                            <asp:Literal ID="orderDescriptionValue" Text=""  runat="server">  </asp:Literal>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>

</div>

