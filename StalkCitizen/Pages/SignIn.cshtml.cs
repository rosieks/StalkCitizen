using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace StalkCitizen.Pages
{
    [AllowAnonymous]
    public class SignIn : PageModel
    {
        public IActionResult OnGet()
        {
            var redirectUrl = Url.Page("Index");

            return Challenge(
                new AuthenticationProperties { RedirectUri = redirectUrl },
                OpenIdConnectDefaults.AuthenticationScheme);
        }
    }
}