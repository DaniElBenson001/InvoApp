using InvoApp.Common.Constants;
using InvoApp.Models.DtoModels;
using InvoApp.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InvoApp.Controllers
{
    [Route("/")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost(ApiRoutes.Version + ApiRoutes.Auth.Base + ApiRoutes.Auth.Login)]
        public async Task<IActionResult> Login(UserLoginDTO request)
        {
            var res = await _authService.Login(request);
            return Ok(res);
        }
    }
}
