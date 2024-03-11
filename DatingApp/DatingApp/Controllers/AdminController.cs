using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.Controllers
{
    public class AdminController : BaseApiController
    {
        [Authorize(Policy ="RequredAdminRole")]
        [HttpGet("Users-with-roles")]

        public ActionResult GetUsersWithRoles()
        {
            return Ok("Only admins can see this");
        }

        [Authorize(Policy = "ModeratePhotoRole")]
        [HttpGet("Pgotos-to-moderate")]
        public ActionResult GetPhotosForModeration() 
        {
            return Ok("Admins or moderators can see this");
        }
    }
}
