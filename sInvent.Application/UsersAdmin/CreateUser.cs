using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace sInvent.Application.UsersAdmin
{
    public class CreateUser
    {
        private readonly UserManager<IdentityUser> _userManager;

        public CreateUser(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public class Seek
        {
            public string UserName { get; set; }
        }
        public async Task<bool> Execute(Seek seek)
        {
            var adminUser = new IdentityUser()
            {
                UserName = seek.UserName
            };
            await _userManager.CreateAsync(adminUser,"password");

            var adminClaim = new Claim("Role","Admin");
            await _userManager.AddClaimAsync(adminUser,adminClaim);
            
            return true;
        }
    }
}
