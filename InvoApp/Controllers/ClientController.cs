using InvoApp.Models.DtoModels;
using InvoApp.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InvoApp.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _clientService;
        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }

        [HttpPost("add-client"), Authorize]
        public async Task<IActionResult> CreateClient(CreateClientDTO request)
        {
            var res = await _clientService.CreateClient(request);
            return Ok(res);
        }
        [HttpGet("get-all-clients"), Authorize]
        public async Task<IActionResult> GetAllClients()
        {
            var res = await _clientService.GetAllClients();
            return Ok(res);
        }
        [HttpPut("delete-client"), Authorize]
        public async Task<IActionResult> DeleteClient(int id)
        {
            var res = await _clientService.DeleteClient(id);
            return Ok(res);
        }
    }
}
