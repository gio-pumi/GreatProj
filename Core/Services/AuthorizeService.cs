using AutoMapper;
using GreatProj.Core.Interfaces;
using GreatProj.Core.Models;
using GreatProj.Core.Models.User;
using GreatProj.Core.Repository_Interfaces;
using GreatProj.Domain.DbEntities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace GreatProj.Core.Services
{
    public class AuthorizeService : IAuthorizeService
    {
        private readonly IAuthorizeRepository _authorizeRepository;
        private readonly IConfiguration _configuration;

        public AuthorizeService(IAuthorizeRepository authorizeRepository,
                                IConfiguration configuration)
        {
            _authorizeRepository = authorizeRepository;
            _configuration = configuration;
        }
        public async Task<ServiceResponse<long>> Register(UserDto userDto, string password)
        {
            var response = new ServiceResponse<long>();
            var Id = await _authorizeRepository.UserExists(userDto.UserName);
            if (Id != 0)
            {
                response.Success = false;
                response.Message = "User already exists";
                response.Data = Id;
                return response;
            }
            CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);
            response.Data = await _authorizeRepository.AddUser(userDto, passwordHash, passwordSalt);
            return response;
        }
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
        public async Task<ServiceResponse<string>> login(string userName, string password)
        {
            var response = new ServiceResponse<string>();
            var user = await _authorizeRepository.CheckCredentials(userName);
            if (user == null)
            {
                response.Success = false;
                response.Message = "User not found.";
            }
            else if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            {
                response.Success = false;
                response.Message = "Wrong password.";
            }
            else
            {
                response.Data = GenerateAccessToken(user);
            }
            return response;
        }
        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computeHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computeHash.SequenceEqual(passwordHash);
            }
        }
        private string GenerateAccessToken(User user)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim("UserId", user.Id.ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.UserName)
            };
            SymmetricSecurityKey key = new SymmetricSecurityKey(System.Text.Encoding.UTF8
                .GetBytes(_configuration.GetSection("AppSettings:Token").Value));

            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
