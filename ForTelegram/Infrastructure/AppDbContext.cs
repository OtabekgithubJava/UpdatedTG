using ForTelegram.Models;
using Microsoft.EntityFrameworkCore;

namespace ForTelegram.Infrastructure
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        {
            Database.Migrate(); 
        }
        public DbSet<TeleUser> Users { get; set; }
    }
}
