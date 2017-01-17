<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="PaymentWebForm.aspx.cs" Inherits="Checkout.PaymentWebForm" %>
<%@ Import Namespace="System.Web.Optimization" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">

<br />
<br />
<h4>Payment</h4>

    <div class="form-horizontal">
        <hr />
         
        <div class="form-group">
            <asp:Label ID="FirstNameLabel" runat="server" Text="FirstName" 
                CssClass = "control-label col-md-2" AssociatedControlID="FirstNameTextBox"></asp:Label>
            <div class="col-md-10">
                <asp:TextBox ID="FirstNameTextBox" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
             <asp:Label ID="FamilyNameLabel" runat="server" Text="FamilyName" 
                CssClass = "control-label col-md-2" AssociatedControlID="FamilyNameTextBox"></asp:Label>
            <div class="col-md-10">
                <asp:TextBox ID="FamilyNameTextBox" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
             <asp:Label ID="AddressLabel" runat="server" Text="Address" 
                CssClass = "control-label col-md-2" AssociatedControlID="AddressTextBox"></asp:Label>
            <div class="col-md-10">
                <asp:TextBox ID="AddressTextBox" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
             <asp:Label ID="PostcodeLabel" runat="server" Text="Postcode" 
                CssClass = "control-label col-md-2" AssociatedControlID="PostcodeTextBox"></asp:Label>
            <div class="col-md-10">
                <asp:TextBox ID="PostcodeTextBox" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
             <asp:Label ID="PostOfficeLabel" runat="server" Text="PostOffice" 
                CssClass = "control-label col-md-2" AssociatedControlID="PostOfficeTextBox"></asp:Label>
            <div class="col-md-10">
                <asp:TextBox ID="PostOfficeTextBox" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <asp:Label ID="AmountLabel" runat="server" Text="Amount" 
                CssClass = "control-label col-md-2" AssociatedControlID="AmountTextBox"></asp:Label>
            <div class="col-md-10">
                <asp:TextBox ID="AmountTextBox" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">         
            <asp:Label ID="MessageLabel" runat="server" Text="Message" 
                CssClass = "control-label col-md-2" AssociatedControlID="MessageTextBox"></asp:Label>
            <div class="col-md-10"> 
                <asp:TextBox TextMode="MultiLine" ID="MessageTextBox" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <asp:Label ID="LanguageLabel" runat="server" Text="Language" 
                CssClass = "control-label col-md-2" AssociatedControlID="LanguageDropDownList"></asp:Label>
            <div class="col-md-10">
               <asp:DropDownList id="LanguageDropDownList" runat="server" CssClass="form-control">
                 <asp:ListItem Value="FI">Suomi</asp:ListItem>
                 <asp:ListItem Value="SE">Svenska</asp:ListItem>
                 <asp:ListItem Selected="True" Value="EN">English</asp:ListItem>
              </asp:DropDownList>
            </div>
        </div>
        <div class="form-group">
              <asp:Label ID="Label1" runat="server" Text="Device" 
                CssClass = "control-label col-md-2" AssociatedControlID="DeviceDropDownList"></asp:Label>
            <div class="col-md-10">
                <asp:DropDownList id="DeviceDropDownList" runat="server" CssClass="form-control">
                 <asp:ListItem Value="1">HTML</asp:ListItem>
                 <asp:ListItem Selected="True" Value="10">XML</asp:ListItem>
              </asp:DropDownList>
            </div>
        </div>
        <div class="form-group">
            <div class="col-md-offset-2 col-md-10">
                 <asp:Button id="Button1" Text="Pay" runat="server" 
                     CssClass="btn btn-default"/>
            </div>
        </div>
    </div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptSection" runat="server">

    <%:Scripts.Render("~/bundles/jqueryval") %>

    <!-- sets default values -->
    <script type="text/javascript">

        $(document).ready(function () {

            $("#MainContentPlaceHolder_FirstNameTextBox").val("Tero");
            $("#MainContentPlaceHolder_FamilyNameTextBox").val("Testaaja");
            $("#MainContentPlaceHolder_AddressTextBox").val("Ääkköstie 5 b3");
            $("#MainContentPlaceHolder_PostcodeTextBox").val("33100");
            $("#MainContentPlaceHolder_PostOfficeTextBox").val("Tampere");
            $("#MainContentPlaceHolder_AmountTextBox").val("10,00");
            $("#MainContentPlaceHolder_MessageTextBox").val("Huonekalutilaus. Paljon puita, lehtiä ja muttereita");

        });

    </script>
}

</asp:Content>
