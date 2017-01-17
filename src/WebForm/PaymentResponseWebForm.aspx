<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="PaymentResponseWebForm.aspx.cs" Inherits="Checkout.PaymentResponseWebForm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
<br />
<br />

<h4>Payment Response</h4>

<hr />

Payment status: <b><asp:Literal ID="paymentResponseMessage" Text=""  runat="server" Mode="PassThrough">  </asp:Literal></b>
<p>
   <br />
    <asp:button id="Button"
          text="Query previous payment" 
          postbackurl="~/QueryWebForm.aspx" 
          CssClass="btn btn-default"
          runat="Server">
    </asp:button>
 
</p>

<br />
<br />

<asp:HyperLink NavigateUrl="~/PaymentWebForm.aspx" runat="server">New payment</asp:HyperLink>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptSection" runat="server">
</asp:Content>
