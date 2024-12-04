using InvoApp.Common.Constants;
using InvoApp.Models.DtoModels;
using InvoApp.Models.Entities;
using InvoApp.Services.Data;
using InvoApp.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace InvoApp.Services.Services
{
    public class AuthService : IAuthService
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;

        public AuthService(DataContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;

        }

        public async Task<DataResponse<LoginTokenDTO>> Login(UserLoginDTO request)
        {
            DataResponse<LoginTokenDTO> loginResponse = new();

            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Email ==  request.Email);

                if(user == null)
                {
                    loginResponse.Status = false;
                    loginResponse.StatusMessage = Messages.ErrorMessage.UserNotFound;
                    return loginResponse;
                }

                bool isPasswordValid = PasswordHashVerifier(request.Password, user.PasswordHash, user.PasswordSalt);

                if(!isPasswordValid)
                {
                    loginResponse.Status = false;
                    loginResponse.StatusMessage = Messages.ErrorMessage.IncorrectPassword;
                    return loginResponse;
                }

                string token = CreateToken(user);
                user.VerificationToken = token;
                await _context.SaveChangesAsync();

                LoginTokenDTO loginData = new()
                {
                    Email = request.Email,
                    VerificationToken = token,
                };


                loginResponse.Status = true;
                loginResponse.StatusMessage = Messages.SuccessMessage.BaseSuccess;
                loginResponse.Data = loginData;
                return loginResponse;
            }
            catch (Exception ex) when (ex is SqlException)
            {
                loginResponse.Status = false;
                loginResponse.StatusMessage = Messages.ErrorMessage.BaseError;
                loginResponse.ErrorMessage = $"{ex.Message} ||| {ex.StackTrace} ||| {DateTime.UtcNow}";
            }

            return loginResponse;
        }

        public bool PasswordHashVerifier(string password,
            byte[] passwordHash,
            byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }
        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Email!)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                            _configuration.GetSection("AppSettings:Token").Value!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }
    }
}
