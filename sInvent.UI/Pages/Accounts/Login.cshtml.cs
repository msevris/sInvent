using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace sInvent.UI.Pages.Accounts
{
    public class LoginModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;

        public LoginModel(SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager;
        }
        [BindProperty]
        public LoginViewModel Input { get; set; }

        public async Task<IActionResult> OnPost()
        {
           var result = await _signInManager.PasswordSignInAsync(Input.Username,Input.Password,false,false);
            if (result.Succeeded)
            {
                return RedirectToAction("/Admin/Index");
            }
            else
            {
                return RedirectToPage("/Index");
            }
        }

        public void OnGet()
        {
        }

        public class LoginViewModel
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }

    }
}