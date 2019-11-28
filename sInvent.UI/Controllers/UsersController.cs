using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using sInvent.Application.UsersAdmin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sInvent.UI.Controllers
{
    [Route("[controller]")]
    [Authorize(Policy="Admin")]
    public class UsersController : Controller
    {
        private readonly CreateUser _createUser;

        public UsersController(CreateUser createUser)
        {
            _createUser = createUser;
        }
        public async Task<IActionResult> CreateUser([FromBody] CreateUser.Seek seek)
        {
            await _createUser.Execute(seek);
            return Ok();
        }


    }
}
