using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace SAMLTEST.Pages.SP
{
    /// <summary>
    /// This is the Assertion Consumer Page Model
    /// This page will be posted to from outside this application
    /// thuys the Ignore Anti Forgery Token below
    /// </summary>
    [IgnoreAntiforgeryToken(Order = 1001)]
    public class AssertionConsumerModel : PageModel
    {

        public String SAMLResponse { get; private set; }
        public Dictionary<string, string> attrsandvals { get; private set; }

        public IActionResult OnPost(string SAMLResponse)
        {

            byte[] ENcSAMLByteArray = Convert.FromBase64String(SAMLResponse);
            String sml = System.Text.ASCIIEncoding.ASCII.GetString(ENcSAMLByteArray);
            XmlDocument doc = new XmlDocument();
            XmlNamespaceManager nsmgr = new XmlNamespaceManager(doc.NameTable);
            nsmgr.AddNamespace("saml", "urn:oasis:names:tc:SAML:2.0:assertion");
            nsmgr.AddNamespace("samlp", "urn:oasis:names:tc:SAML:2.0:protocol");
            doc.LoadXml(sml);
            XmlElement root = doc.DocumentElement;

            string statusCode = root.SelectSingleNode("/samlp:Response/samlp:Status/samlp:StatusCode/@Value", nsmgr).Value;
            if (statusCode.Trim() != "urn:oasis:names:tc:SAML:2.0:status:Success")
            {
                string statusMessage = root.SelectSingleNode("/samlp:Response/samlp:Status/samlp:StatusMessage", nsmgr).InnerText;
                return Redirect("/Error?ErrorMessage=" + statusMessage);
            }

            XmlNodeList nodes = root.SelectNodes("/samlp:Response/saml:Assertion/saml:AttributeStatement/saml:Attribute", nsmgr); 
            this.attrsandvals = new Dictionary<string, string>();
            foreach (XmlNode node in nodes)
            {
                String attrname = node.Attributes["Name"].Value;
                String val = "";
                if (node.HasChildNodes && node.ChildNodes.Count > 1)
                {
                    var values = node.ChildNodes.Cast<XmlNode>()
                               .Select(item => item.InnerText).ToList();
                    val = string.Join("<br>", values);
                }
                else
                {
                    val = node.InnerText;
                }
                this.attrsandvals.Add(attrname, val);
            }
            this.SAMLResponse = sml;
            return Page();
            
        }
    }
}