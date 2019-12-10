using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace sInvent.UI.Pages.Accounts
{
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public RegisterModel(
            SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        
        [BindProperty]
        public SignInViewModel Input { get; set; }

        public void OnGet(string returnUrl = null)
        {
            ViewData["roles"] = _roleManager.Roles.ToList();
        }

        public async Task<IActionResult> OnPost()
        {
            var role = _roleManager.FindByNameAsync(Input.Email);
            var user = new IdentityUser { UserName = Input.Email, Email = Input.Email};
            var result = await _userManager.CreateAsync(user,Input.Password);
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user,isPersistent:false);
                return RedirectToPage("/Index");
            }
            else
            {
                foreach (var errors in result.Errors)
                {
                    ModelState.AddModelError(string.Empty,errors.Description);
                }
            }
            return Page();
        }

        public class SignInViewModel
        {
            [Required]
            [EmailAddress]
            [Display(Name="Email")]
            public string Email { get; set; }

            [Required]
            [DataType(DataType.Password,ErrorMessage ="Please try again")]
            [Display(Name="Password")]
            public string Password { get; set; }
            
            [DataType(DataType.Password)]
            [Display(Name="Confirm Password")]
            [Compare("Password",ErrorMessage ="Passwords do not match. Please Try again")]
            public string ConfirmPassword { get; set; }

        }
    }
}
