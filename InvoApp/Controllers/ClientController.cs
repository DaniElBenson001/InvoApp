using InvoApp.Common.Constants;
using InvoApp.Models.DtoModels;
using InvoApp.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InvoApp.Controllers
{
    [Route("/")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _clientService;
        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }

        [HttpPost(ApiRoutes.Version + ApiRoutes.Clients.Base + ApiRoutes.Clients.Create), Authorize]
        public async Task<IActionResult> CreateClient(InputClientDTO request)
        {
            var res = await _clientService.CreateClient(request);
            return Ok(res);
        }
        [HttpGet(ApiRoutes.Version + ApiRoutes.Clients.Base + ApiRoutes.Clients.ReadAll), Authorize]
        public async Task<IActionResult> GetAllClients()
        {
            var res = await _clientService.GetAllClients();
            return Ok(res);
        }
        [HttpPut(ApiRoutes.Version + ApiRoutes.Clients.Base + ApiRoutes.Clients.Update), Authorize]
        public async Task<IActionResult> UpdateClient(InputClientDTO request, int id)
        {
            var res = await _clientService.UpdateClient(request, id);
            return Ok(res);
        }
        [HttpPut(ApiRoutes.Version + ApiRoutes.Clients.Base + ApiRoutes.Clients.Delete), Authorize]
        public async Task<IActionResult> DeleteClient(int id)
        {
            var res = await _clientService.DeleteClient(id);
            return Ok(res);
        }
    }
}
