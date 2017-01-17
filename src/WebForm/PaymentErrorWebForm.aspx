<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="PaymentErrorWebForm.aspx.cs" Inherits="Checkout.PaymentErrorWebForm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <h4>Payment Response Error</h4>
    <br />
    <p class="text-danger">
       <asp:Literal ID="errorMessage" Text=""  runat="server" Mode="PassThrough">  </asp:Literal>
    </p>
    <br />
    <br />
    <asp:HyperLink NavigateUrl="~/PaymentWebForm.aspx" runat="server">New payment</asp:HyperLink>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptSection" runat="server">
</asp:Content>
