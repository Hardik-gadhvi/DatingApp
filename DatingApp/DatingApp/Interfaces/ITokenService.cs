using DatingApp.Entities;

namespace DatingApp.Interfaces
{
    public interface ITokenService
    {
        string createToken(AppUser user);
    }
}
