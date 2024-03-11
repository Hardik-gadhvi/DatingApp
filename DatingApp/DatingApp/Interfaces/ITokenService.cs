using DatingApp.Entities;

namespace DatingApp.Interfaces
{
    public interface ITokenService
    {
        Task<string> createToken(AppUser user);
    }
}
