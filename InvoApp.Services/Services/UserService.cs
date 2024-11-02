using InvoApp.Models.DtoModels;
using InvoApp.Models.Entities;
using InvoApp.Services.Data;
using InvoApp.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace InvoApp.Services.Services
{
    public class UserService : IUserService
    {
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpCoontextAccessor;

        public UserService(DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpCoontextAccessor = httpContextAccessor;
        }

        public async Task<DataResponse<string>> CreateUser(CreateUserDTO request)
        {
            DataResponse<string> userResponse = new();
            var user = await _context.Users.AnyAsync(u => u.Email ==  request.Email);

            PasswordHashGenerator(request.Password!,
                out byte[] passwordHash,
                out byte[] passwordSalt);

            try
            {
                var userData = new User
                {
                    Email = request.Email,
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    CompanyName = request.CompanyName,
                    CompanyAddress = request.CompanyAddress
                };

                if (user)
                {
                    userResponse.Status = false;
                    userResponse.StatusMessage = "User Already Exists!";
                    return userResponse;
                }

                await _context.AddAsync(userData);
                await _context.SaveChangesAsync();

                userResponse.Status = true;
                userResponse.StatusMessage = "Successful";
                return userResponse;
            }
            catch(SqlException ex)
            {
                userResponse.Status = false;
                userResponse.StatusMessage = "Unsuccessful, An Error Occurred; It isn't your Fault!";
                userResponse.Data = $"{ex.Message} ||| {ex.StackTrace} ||| {DateTime.Now.ToString()}";
                return userResponse;
            }
            catch (DbUpdateException ex)
            {
                userResponse.Status = false;
                userResponse.StatusMessage = "Unsuccessful, An Error Occurred; It isn't your Fault!";
                userResponse.Data = $"{ex.Message} ||| {ex.StackTrace} ||| {DateTime.Now.ToString()}";
                return userResponse;
            }
            catch (Exception ex)
            {
                userResponse.Status = false;
                userResponse.StatusMessage = "Unsuccessful, An Error Occurred; It isn't your Fault!";
                userResponse.Data = $"{ex.Message} ||| {ex.StackTrace} ||| {DateTime.Now.ToString()}";
                return userResponse;
            }
        }

        public void PasswordHashGenerator(string password,
            out byte[] passwordHash,
            out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
    }
}
