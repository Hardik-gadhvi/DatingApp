using DatingApp.Data;
using DatingApp.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.Controllers
{
    [Authorize]
    public class UserController : BaseApiController
    {
        private readonly DataContext _dataContext;

        public UserController(DataContext dataContext) {

            _dataContext = dataContext; 
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
        {
            var users = await _dataContext.Users.ToListAsync();
            return users;
        }
   
        [HttpGet("{id}")]
        public async Task<ActionResult<AppUser>> GetUser(int id)
        {
            var user = await _dataContext.Users.FirstOrDefaultAsync(x => x.Id == id);
           
            return user;
        }
    }
}
