using DatingApp.Data;
using DatingApp.DTOs;
using DatingApp.Entities;
using DatingApp.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace DatingApp.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly DataContext _dataContext;

        private readonly ITokenService _tokenService;
        public AccountController(DataContext context, ITokenService tokenService) {

            _dataContext = context;
            _tokenService = tokenService;
        }
        [HttpPost("register")]
        public async Task<ActionResult<UserDTO>> Register(RegisterDTO register)
        {
            if (await UserExists(register.Username))
                return BadRequest("User already taken!");
            using var hmac = new HMACSHA512();

            var user = new AppUser { 
            
                UserName = register.Username.ToLower(),
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(register.Password)),
                PasswordSalt = hmac.Key
            };
            _dataContext.Users.Add(user);
            await _dataContext.SaveChangesAsync();
            return new UserDTO {
                Username = user.UserName,
                Token = _tokenService.createToken(user),
            };

        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDTO>> Login(LoginDTO login)
        {
            var user = await _dataContext.Users.SingleOrDefaultAsync(x => x.UserName == login.Username);

            if (user == null)
                return Unauthorized("Invalid username!");
            using var hmac = new HMACSHA512(user.PasswordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(login.Password));

            for(int i = 0; i<computedHash.Length; i++)
            {
                if (computedHash[i] != user.PasswordHash[i])
                    return Unauthorized("Invalid Password!");
            }
            return new UserDTO
            {
                Username = user.UserName,
                Token = _tokenService.createToken(user),
            };
        }
        private async Task<bool> UserExists(string username)
        {
            return await _dataContext.Users.AnyAsync(x => x.UserName == username.ToLower());
        }
    }
}
