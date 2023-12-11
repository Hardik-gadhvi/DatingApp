using Dating_App_Main.Data;
using Dating_App_Main.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Dating_App_Main.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly DataContext _context;

        public UsersController(DataContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserModel>>> GetUsers() { 
            var users = await _context.Users.ToListAsync();
            return users;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<UserModel>> GetUser(int id) 
        {
            return await _context.Users.FindAsync(id);
        }
        [HttpPost]
        public async Task<ActionResult> AddUsers(UserModel user)
        {
                await _context.Users.AddAsync(user);
                _context.SaveChanges();
                return Ok();
        }
    }
}
