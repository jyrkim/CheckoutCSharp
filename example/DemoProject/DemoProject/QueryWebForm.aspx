<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="QueryWebForm.aspx.cs" Inherits="Checkout.QueryWebForm" %>
<%@ PreviousPageType VirtualPath="~/PaymentResponseWebForm.aspx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <br />
    <br />

    <h4>Query response</h4>

    <hr />

    Payment status: <b><asp:Literal ID="queryResponseMessage" Text=""  runat="server">  </asp:Literal></b>
    <br />
    <br />
    Query response XML:

    <br />

    <br />
    <span style="color: green">
        &lt;<asp:Literal ID="tradeBegin" Text=""  runat="server" Mode="PassThrough">  </asp:Literal>&gt;
    </span>
    <br />
    <span style="color: blue; margin-left:10px">
        &lt;<asp:Literal ID="statusBegin" Text=""  runat="server" Mode="PassThrough">  </asp:Literal>&gt;
    </span>

    <span>
        <asp:Literal ID="statusValue" Text=""  runat="server" Mode="PassThrough">  </asp:Literal>
    </span>

    <span style="color: blue">
        &lt;/<asp:Literal ID="statusEnd" Text=""  runat="server" Mode="PassThrough">  </asp:Literal>>&gt;
    </span>

    <br />
    <span style="color: green">
        &lt;/<asp:Literal ID="tradeEnd" Text=""  runat="server" Mode="PassThrough">  </asp:Literal>>&gt;
    </span>

    <br />
    <br />
    <asp:HyperLink NavigateUrl="~/PaymentWebForm.aspx" runat="server">New payment</asp:HyperLink>


</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptSection" runat="server">
</asp:Content>
