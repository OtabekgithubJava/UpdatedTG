using ForTelegram.Infrastructure;
using ForTelegram.Models;
using Microsoft.EntityFrameworkCore;

namespace ForTelegram.Services.UserRepo
{
    public class UserRepos : IUserRepos
    {
        public readonly AppDbContext _context;

        public UserRepos(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddUser(TeleUser user)
        {
            var isExists= await _context.Users.FirstOrDefaultAsync(x=> x.Id==user.Id);
            
            if (isExists != null)
            {
                return;
            }
            else
            {
                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();
            }

        }

        public async Task<List<TeleUser>> GetAllUsers()
        {
            return await _context.Users.ToListAsync();
        }
    }
}
