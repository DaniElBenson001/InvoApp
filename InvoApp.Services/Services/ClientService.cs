using InvoApp.Common.Constants;
using InvoApp.Models.DtoModels;
using InvoApp.Models.Entities;
using InvoApp.Services.Data;
using InvoApp.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace InvoApp.Services.Services
{
    public class ClientService : IClientService
    {
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ClientService(IHttpContextAccessor httpContextAccessor, DataContext context)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<DataResponse<string>> CreateClient(InputClientDTO request)
        {
            DataResponse<string> clientResponse = new();

            int userID;
            var userHttpAccessor = _httpContextAccessor.HttpContext;



            try
            {
                userID = Convert.ToInt32(userHttpAccessor!.User.FindFirstValue(ClaimTypes.NameIdentifier));

                if(userHttpAccessor == null)
                {
                    clientResponse.Status = false;
                    clientResponse.StatusMessage = Messages.ErrorMessage.UserNotFound;
                    return clientResponse;
                }

                var client = await _context.Clients.AnyAsync(c => c.Email == request.Email && c.IsDeleted == false);

                if (client)
                {
                    clientResponse.Status = false;
                    clientResponse.StatusMessage = Messages.ErrorMessage.ClientAlreadyExists;
                    return clientResponse;

                }

                Client clientData = new()
                {
                    UserId = userID,
                    CompanyName = request.CompanyName,
                    ContactName = request.ContactName,
                    Email = request.Email,
                    PhoneNumber = request.PhoneNumber,
                    BillingAddress = request.BillingAddress,
                    ShippingAddress = request.ShippingAddress,
                    Currency = request.Currency,
                    PaymentTerms = request.PaymentTerms,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow

                };

                await _context.AddAsync(clientData);
                await _context.SaveChangesAsync();

                clientResponse.Status = true;
                clientResponse.StatusMessage = Messages.SuccessMessage.ClientAddedSuccess;
                return clientResponse;
            }
            catch (Exception ex) when (ex is SqlException || ex is DbUpdateException)
            {
                clientResponse.Status = false;
                clientResponse.StatusMessage = Messages.ErrorMessage.BaseError;
                clientResponse.ErrorMessage = $"{ex.Message} ||| {ex.StackTrace} ||| {DateTime.UtcNow}";
            }
            return clientResponse;
        }
        public async Task<DataResponse<List<ClientDTO>>> GetAllClients()
        {
            DataResponse<List<ClientDTO>> clientResponse = new()
            {
                Data = new List<ClientDTO>()
            };

            List<ClientDTO> getClients = new();

            var userHttpAccessor = _httpContextAccessor.HttpContext;
            int userID;

            try
            {
                userID = Convert.ToInt32(userHttpAccessor!.User.FindFirstValue(ClaimTypes.NameIdentifier));

                var userClientData = await _context.Clients.Where(c => c.UserId  == userID && c.IsDeleted == false).ToListAsync();

                foreach(var clients in userClientData)
                {
                    getClients.Add(new ClientDTO
                    {
                        ClientId = clients.Id,
                        CompanyName = clients.CompanyName,
                        ContactName = clients.ContactName,
                        Email = clients.Email,
                        PhoneNumber = clients.PhoneNumber,
                        BillingAddress = clients.BillingAddress,
                        ShippingAddress = clients.ShippingAddress,
                        Currency = clients.Currency,
                        PaymentTerms = clients.PaymentTerms,
                        CreatedAt = clients.CreatedAt,
                        UpdatedAt = clients.UpdatedAt
                    });
                }

                clientResponse.Status = true;
                clientResponse.StatusMessage = Messages.SuccessMessage.ClientListSuccess;
                clientResponse.Data = getClients;
            }
            catch (Exception ex) when (ex is SqlException || ex is DbUpdateException)
            {
                clientResponse.Status = false;
                clientResponse.StatusMessage = Messages.ErrorMessage.BaseError;
                clientResponse.ErrorMessage = $"{ex.Message} ||| {ex.StackTrace} ||| {DateTime.UtcNow}";
            }

            return clientResponse;
        }

        public async Task<DataResponse<string>> UpdateClient(InputClientDTO request, int id)
        {
            DataResponse<string> clientResponse = new();
            int userID;
            var userHttpAccessor = _httpContextAccessor.HttpContext;

            try
            {
                userID = Convert.ToInt32(userHttpAccessor!.User.FindFirstValue(ClaimTypes.NameIdentifier));

                if(userHttpAccessor == null)
                {
                    clientResponse.Status = false;
                    clientResponse.StatusMessage = Messages.ErrorMessage.UserNotFound;
                    return clientResponse;
                }

                var client = await _context.Clients
                    .FirstOrDefaultAsync(c => c.Id == id && c.UserId == userID);

                if(client == null)
                {
                    clientResponse.Status = false;
                    clientResponse.StatusMessage = Messages.ErrorMessage.ClientNotFound;
                    return clientResponse;
                }

                client.CompanyName = !string.IsNullOrWhiteSpace(request.CompanyName) ? request.CompanyName : client.CompanyName;
                client.ContactName = !string.IsNullOrWhiteSpace(request.ContactName) ? request.ContactName : client.ContactName;

                client.Email = !string.IsNullOrWhiteSpace(request.Email) ? request.Email : client.Email;
                client.PhoneNumber = !string.IsNullOrWhiteSpace(request.PhoneNumber) ? request.PhoneNumber : client.PhoneNumber;

                client.BillingAddress = !string.IsNullOrWhiteSpace(request.BillingAddress) ? request.BillingAddress : client.BillingAddress;
                client.ShippingAddress = !string.IsNullOrWhiteSpace(request.ShippingAddress) ? request.ShippingAddress : client.ShippingAddress;
                client.PaymentTerms = !string.IsNullOrWhiteSpace(request.PaymentTerms) ? request.PaymentTerms : client.PaymentTerms;

                client.UpdatedAt = DateTime.UtcNow;

                _context.Clients.Update(client);
                await _context.SaveChangesAsync();

                clientResponse.Status = true;
                clientResponse.StatusMessage = Messages.SuccessMessage.ClientUpdatedSuccess;
                return clientResponse;
            }
            catch(Exception ex) when (ex is SqlException || ex is DbUpdateException)
            {
                clientResponse.Status = false;
                clientResponse.StatusMessage = Messages.ErrorMessage.BaseError;
                clientResponse.ErrorMessage = $"{ex.Message} ||| {ex.StackTrace} ||| {DateTime.UtcNow}";
                return clientResponse;
            }
        }

        public async Task<DataResponse<string>> DeleteClient(int id)
        {
            DataResponse<string> clientResponse = new();

            var userHttpAccessor = _httpContextAccessor.HttpContext;

            try
            {
                if (userHttpAccessor == null ||
             !int.TryParse(userHttpAccessor.User.FindFirstValue(ClaimTypes.NameIdentifier), out int userID) ||
             userID == 0)
                {
                    clientResponse.Status = false;
                    clientResponse.StatusMessage = Messages.ErrorMessage.UserNotFound;
                    return clientResponse;
                }

                var client = await _context.Clients
                    .FirstOrDefaultAsync(c => c.UserId == userID && c.Id == id);

                if (client == null)
                {
                    clientResponse.Status = false;
                    clientResponse.StatusMessage = Messages.ErrorMessage.ClientNotFound;
                    return clientResponse;
                }

                if (client.IsDeleted)
                {
                    clientResponse.Status = false;
                    clientResponse.StatusMessage = Messages.ErrorMessage.ClientAlreadyDeleted;
                    return clientResponse;
                }

                client.IsDeleted = true;

                _context.Clients.Update(client);
                await _context.SaveChangesAsync();

                clientResponse.Status = true;
                clientResponse.StatusMessage = $"{client.ContactName ?? "Client"}" + Messages.SuccessMessage.ClientDeletedSuccess;
                return clientResponse;

            }
            catch (Exception ex) when (ex is SqlException || ex is DbUpdateException)
            {
                clientResponse.Status = false;
                clientResponse.StatusMessage = Messages.ErrorMessage.BaseError;
                clientResponse.ErrorMessage = $"{ex.GetType().Name}: {ex.Message} ||| {ex.StackTrace} ||| {DateTime.UtcNow}";
            }
            return clientResponse;
        }
     }
}
