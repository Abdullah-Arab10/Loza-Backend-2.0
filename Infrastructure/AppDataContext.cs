using Loza.Domain.Entities;
using Loza.Infrastructure.Configurations;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Loza.Infrastructure
{
    public class AppDataContext : DbContext
    {
       // private readonly Fakers _fakers;
   

        public AppDataContext(DbContextOptions<AppDataContext> dbContextOptions
            ) : base(dbContextOptions) {
        //    _fakers=fakers;
         
        }


        public DbSet<User> Users{ get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfiguration<User>(new UserConfig());

           base.OnModelCreating(modelBuilder);

          // modelBuilder.Entity<User>().HasData(_fakers.GenerateUsersProfile());

        }
    }
}
