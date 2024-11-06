﻿using InvoApp.Models.DtoModels;
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
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace InvoApp.Services.Services
{
    public class UserService : IUserService
    {
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<DataResponse<string>> CreateUser(CreateUserDTO request)
        {
            DataResponse<string> userResponse = new();
            var user = await _context.Users.AnyAsync(u => u.Email == request.Email);

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
                    CompanyAddress = request.CompanyAddress,
                    IsTwoFactorEnabled = false,
                    CreatedAt = DateTime.UtcNow,
                    LastUpdatedAt = DateTime.UtcNow
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
            catch (SqlException ex)
            {
                userResponse.Status = false;
                userResponse.StatusMessage = "Unsuccessful, An Error Occurred; It isn't your Fault!";
                userResponse.ErrorMessage = $"{ex.Message} ||| {ex.StackTrace} ||| {DateTime.UtcNow.ToString()}";
                return userResponse;
            }
            catch (DbUpdateException ex)
            {
                userResponse.Status = false;
                userResponse.StatusMessage = "Unsuccessful, An Error Occurred; It isn't your Fault!";
                userResponse.ErrorMessage = $"{ex.Message} ||| {ex.StackTrace} ||| {DateTime.UtcNow.ToString()}";
                return userResponse;
            }
            catch (Exception ex)
            {
                userResponse.Status = false;
                userResponse.StatusMessage = "Unsuccessful, An Error Occurred; It isn't your Fault!";
                userResponse.ErrorMessage = $"{ex.Message} ||| {ex.StackTrace} ||| {DateTime.UtcNow.ToString()}";
                return userResponse;
            }
        }

        public async Task<DataResponse<UserInfoDTO>> GetUserInfo()
        {
            DataResponse<UserInfoDTO> infoResponse = new();

            try
            {
                var userID = Convert.ToInt32(_httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier));

                if (userID == 0 || _httpContextAccessor.HttpContext == null)
                {
                    infoResponse.Status = false;
                    infoResponse.StatusMessage = "User Not Found!";
                    return infoResponse;
                }

                var userData = await _context.Users
                    .Where(u => u.Id == userID)
                    .Select(u => new UserInfoDTO
                    {
                        Email = u.Email,
                        FirstName = u.FirstName,
                        LastName = u.LastName,
                    })
                    .FirstOrDefaultAsync();

                infoResponse.Status = true;
                infoResponse.StatusMessage = "Successful";
                infoResponse.Data = userData;
                return infoResponse;
            }
            catch (SqlException ex)
            {
                infoResponse.Status = false;
                infoResponse.StatusMessage = "Unsuccessful, An Error Occurred; It isn't your Fault!";
                infoResponse.ErrorMessage = $"{ex.Message} ||| {ex.StackTrace} ||| {DateTime.UtcNow.ToString()}";
                return infoResponse;
            }
            catch (Exception ex)
            {
                infoResponse.Status = false;
                infoResponse.StatusMessage = "Unsuccessful, An Error Occurred; It isn't your Fault!"
                infoResponse.ErrorMessage = $"{ex.Message} ||| {ex.StackTrace} ||| {DateTime.UtcNow.ToString()}";
                return infoResponse;
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
