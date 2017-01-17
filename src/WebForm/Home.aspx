<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="Checkout.Home" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">

    <br />
    <br />

    <h4>Try Checkout Finland Payment using</h4>

    <br />
    <ul>
        <li>
            <asp:HyperLink NavigateUrl="~/PaymentWebForm.aspx" runat="server">ASP.NET WebForm</asp:HyperLink>
        </li>
        <li>
            <asp:HyperLink NavigateUrl="~/Checkout/Payment" runat="server">ASP.NET MVC</asp:HyperLink>
        </li>
    </ul>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptSection" runat="server">

        <script type="text/javascript">

            $(document).ready(function () {

                $('.navbar .nav').find('.active').removeClass('active');

            });
            </script>

</asp:Content>

