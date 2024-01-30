using AutoMapper;
using AutoMapper.QueryableExtensions;
using DatingApp.DTOs;
using DatingApp.Entities;
using DatingApp.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public UserRepository(DataContext context, IMapper mapper) { 

            _context = context;
            _mapper = mapper;
        }
        public async Task<AppUser> GetUserById(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<AppUser> GetUserByUserNameAsync(string username)
        {
            return await _context.Users
                .Include(p =>p.Photos)
                .SingleOrDefaultAsync(x => x.UserName == username);
        }

        public async Task<IEnumerable<AppUser>> GetUsersAsync()
        {
           return await _context.Users
                .Include(p => p.Photos)
                .ToListAsync(); 
        }

        public async Task<bool> SaveAllAsync()
        {
           return await _context.SaveChangesAsync() > 0;
        }

        public void Update(AppUser user)
        {
            _context.Entry(user).State = EntityState.Modified;
        }

        public async Task<MemberDto> GetMemberAsync(string username)
        {
            return await _context.Users.Where(x => x.UserName == username)
                .ProjectTo<MemberDto>(_mapper.ConfigurationProvider).SingleOrDefaultAsync();
        }

        async Task<IEnumerable<MemberDto>> IUserRepository.GetMembersAsync()
        {
            return await _context.Users.ProjectTo<MemberDto>(_mapper.ConfigurationProvider).ToListAsync();
        }
    }
}
