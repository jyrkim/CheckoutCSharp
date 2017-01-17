using System;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Xml;

namespace Checkout.User_Controls
{
    public partial class BankPaymentOptions : System.Web.UI.UserControl
    {
        //BankPaymentOptions.ascx sets XML value
        public string XML = "";
        public Dictionary<string, string> Translations;

        protected void Page_Load(object sender, EventArgs e)
        {
            XDocument xmlDoc = XDocument.Parse(this.XML);
            IEnumerable<XNode> banks = xmlDoc.Descendants("banks").Nodes();
            var cancelURL = "";
            var html = "";

            foreach (XNode bank in banks)
            {
                if (bank.NodeType == XmlNodeType.Element)
                {
                    var bankElement = (XElement)bank;
                    var url = (string)bankElement.Attribute("url");
                    var icon = (string)bankElement.Attribute("icon");
                    var name = (string)bankElement.Attribute("name");

                    if (bankElement.Name.LocalName.Equals("tilisiirto"))
                    {
                        //Cancel url can be any url on your domain, 
                        //for example http://domain/home.
                        //Now Checkout Finland's cancel link is used
                        int position = url.LastIndexOf('/');
                        if (url.Length > position)
                        {
                            cancelURL = url.Substring(0, (position + 1)) + "cancel";
                        }
                    }

                    html += "<div class='C1' style='float: left; margin-right: 20px; min-height: 100px; text-align: center;'>";
                    html += "<form action='" + url + "' method='post'>";

                    foreach (XNode bankChildNode in ((XElement)bank).Nodes())
                    {
                        var bankElementHidden = (XElement)bankChildNode;
                        var hiddenElementName = bankElementHidden.Name.LocalName;
                        var hiddenElementValue = (bankElementHidden.Value == null) ? "" : bankElementHidden.Value;

                        html += "<input type='hidden' name='" + hiddenElementName + "' value='" + hiddenElementValue + "' />";
                    }
                    html += "<span><input type='image' src='" + icon + "' /> </span>";
                    html += "<div>" + name + "</div>";
                    html += "</form>";
                    html += "</div>";
                }

            }

            html += "<p style='text-align: left;clear: both;'>";
            html += "<a href='" + cancelURL + "'>" + Translations["cancelLink"] + "</a>";
            html += "</p>";


            //literal
            this.Literal1.Text = html;
        }
    }
}