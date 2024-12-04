using InvoApp.Models.DtoModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoApp.Services.IServices
{
    public interface IClientService
    {
        Task<DataResponse<string>> CreateClient(InputClientDTO request);
        Task<DataResponse<List<ClientDTO>>> GetAllClients();
        Task<DataResponse<string>> DeleteClient(int id);
        Task<DataResponse<string>> UpdateClient(InputClientDTO request, int id);
    }
}
