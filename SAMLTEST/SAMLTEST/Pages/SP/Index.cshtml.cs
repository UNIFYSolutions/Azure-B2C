using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using SAMLTEST.SAMLObjects;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SAMLTEST.Pages.SP
{
    /// <summary>
    /// This is the Index Page Model for the Service Provider
    /// </summary>
    public class IndexModel : PageModel
    {

        [DisplayName("Tenant Name"), Required]
        public string Tenant { get; set; }
        [DisplayName("B2C Policy"),Required]
        public string Policy { get; set; } = "SAMLTEST";

        private readonly IConfiguration _configuration;

        /// <summary>
        /// This Constructor is used to retrieve the Appsettings data
        /// </summary>
        public IndexModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// This Post Action is used to Generate the AuthN Request and redirect to the B2C Login endpoint
        /// </summary>
        public IActionResult OnPost(string Tenant,string Policy)
        {

            string b2cloginurl = _configuration["SAMLTEST:b2cloginurl"];
            Policy = Policy.StartsWith("B2C_1A_") ? Policy : "B2C_1A_" + Policy;
            AuthnRequest AuthnReq = new AuthnRequest("https://" + b2cloginurl + "/te/" + Tenant + ".onmicrosoft.com/" + Policy + "/samlp/sso/login",SAMLHelper.GetThisURL(this));
            string cdoc = SAMLHelper.Compress(AuthnReq.ToString());
            string URL = "https://" + b2cloginurl + "/te/" + Tenant + ".onmicrosoft.com/" + Policy + "/samlp/sso/login?SAMLRequest=" + System.Web.HttpUtility.UrlEncode(cdoc);
       
            return Redirect(URL);
  
        }

    }
}