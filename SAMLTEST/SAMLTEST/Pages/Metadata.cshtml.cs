using Microsoft.AspNetCore.Mvc.RazorPages;
using System;

namespace SAMLTEST.Pages
{
    /// <summary>
    /// This is the Metadata Page Model
    /// </summary>
    public class MetadataModel : PageModel
    {
        public string ServerName { get; private set; }
        public Boolean ShowView { get; private set; } = false;

        public void OnGet(string showpage="false")
        {
            ShowView = showpage.Equals("true");

            string httpors = HttpContext.Request.IsHttps ? "https://" : "http://";
            ServerName = httpors + HttpContext.Request.Host.Value;
        }

    }
}