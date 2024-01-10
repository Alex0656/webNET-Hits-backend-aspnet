using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace lab1_backend.Models
{
    public class UserDbContext : IdentityDbContext<IdentityUser>
    {
        //public DbSet<Genres> Genres{ get; set; }
        public UserDbContext(DbContextOptions<UserDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
