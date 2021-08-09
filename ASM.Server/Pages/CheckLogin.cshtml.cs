using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using ASM.Share.Interfaces;
using ASM.Share.Models;
using ASM.Share.Models.ViewModels;
using ASM.Share.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.DependencyInjection;


namespace ASM.Server.Pages
{
    [AllowAnonymous]
    public class CheckLoginModel : PageModel
    {
        [Inject]
        public INguoiDungService _nguoiDungService { get; set; }
        public CheckLoginModel(INguoiDungService nguoiDungService)
        {
            _nguoiDungService = nguoiDungService;
        }
        public string returnUrl { get; set; }

        public async Task<IActionResult> OnGetAsync(string paramUserName, string paramPassword)
        {
            string returnUrl = Url.Content("~/");
            try
            {
                // cleare the existing external cookie
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            }
            catch { }

            bool flagLogin = false;
            var viewlogin = new ViewLogin() { UserName = paramUserName, Password = paramPassword };
            NguoiDung nguoiDung = _nguoiDungService.Login(viewlogin);
            if (nguoiDung != null)
            {
                flagLogin = true;
            }
            if (flagLogin) //login success
            {
                var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, paramUserName),
                    new Claim(ClaimTypes.Role, "Admin")
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true,
                    RedirectUri = this.Request.Host.Value
                };

                try
                {
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, 
                        new ClaimsPrincipal(claimsIdentity), authProperties);
                }
                catch (Exception ex )
                {
                    string error = ex.Message;
                }
            }
            else
            {

            }

            return LocalRedirect(returnUrl);
        }
    }
}
