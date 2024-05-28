using ForTelegram.Models;

namespace ForTelegram.Services.UserRepo
{
    public interface IUserRepos
    {
        public Task AddUser(TeleUser user);
        public Task<List<TeleUser>> GetAllUsers();
    }
}
