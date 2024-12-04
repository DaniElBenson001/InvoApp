using InvoApp.Common.Constants;
using InvoApp.Models.DtoModels;
using InvoApp.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InvoApp.Controllers
{
    [ApiController]
    [Route("/")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService; 
        }


        [HttpPost(ApiRoutes.Version + ApiRoutes.Users.Base + ApiRoutes.Users.Register)]
        public async Task<IActionResult> CreateUser(CreateUserDTO request)
        {
            var res = await _userService.CreateUser(request);
            return Ok(res);
        }

        [HttpGet(ApiRoutes.Version + ApiRoutes.Users.Base + ApiRoutes.Users.GetInfo), Authorize]
        public async Task<IActionResult> GetUserInfo()
        {
            var res = await _userService.GetUserInfo();
            return Ok(res);
        }
    }
}
