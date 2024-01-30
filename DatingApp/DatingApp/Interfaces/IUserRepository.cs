using DatingApp.DTOs;
using DatingApp.Entities;

namespace DatingApp.Interfaces
{
    public interface IUserRepository
    {
        void Update(AppUser user);
        Task<bool> SaveAllAsync();

        Task<IEnumerable<AppUser>> GetUsersAsync();
        Task<AppUser> GetUserById(int id);
        Task<AppUser> GetUserByUserNameAsync(string username);
        Task<IEnumerable<MemberDto>> GetMembersAsync();
        Task<MemberDto> GetMemberAsync(String username);
    }
}
