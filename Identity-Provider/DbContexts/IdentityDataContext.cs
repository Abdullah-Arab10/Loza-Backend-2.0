using IdentityProvider.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IdentityProvider.DbContexts
{
    public class IdentityDataContext : IdentityDbContext<IdentityUserModel>
    {

        public IdentityDataContext(
          DbContextOptions<IdentityDataContext> options)
        : base(options)
        {
        }
        public DbSet<IdentityUserModel> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<IdentityUserModel>().Ignore(c => c.AccessFailedCount)
                                                    .Ignore(c => c.LockoutEnd)
                                                    .Ignore(c => c.LockoutEnabled)
                                                    .Ignore(c => c.TwoFactorEnabled)
                                                    .Ignore(c => c.Email)
                                                    .Ignore(c => c.EmailConfirmed)
                                                    .Ignore(c => c.NormalizedEmail)
                                                    .Ignore(c => c.PhoneNumber)
                                                    .Ignore(c => c.PhoneNumberConfirmed)
                                                    .Ignore(c => c.UserName);

          
                base.OnModelCreating(modelBuilder);
           // modelBuilder.Ignore<IdentityUserRole<string>>();
            //modelBuilder.Ignore<IdentityUserLogin<string>>(); 
          //  modelBuilder.Entity<IdentityUserModel>().ToTable("Users");
            /*    modelBuilder.Entity<IdentityUserModel>()
               .HasIndex(u => u.IdentityId)
               .IsUnique();

               modelBuilder.Entity<IdentityUserModel>()
               .HasIndex(u => u.UserName)
               .IsUnique();

               modelBuilder.Entity<IdentityUserModel>().HasData(
                   new IdentityUserModel()
                   {
                       Id = 1,
                       Password = "password",
                       IdentityId = "d860efca-22d9-47fd-8249-791ba61b07c7",
                       UserName = "David",
                       Active = true
                   },
                   new IdentityUserModel()
                   {
                       Id = 2,
                       Password = "password",
                       IdentityId = "b7539694-97e7-4dfe-84da-b4256e1ff5c7",
                       UserName = "Emma",
                       Active = true
                   });

               modelBuilder.Entity<UserClaims>().HasData(
                new UserClaims()
                {
                    Id = 1,
                    UserId = 1,
                    Type = "given_name",
                    Value = "David"
                },
                new UserClaims()
                {
                    Id = 2,
                    UserId = 1,
                    Type = "family_name",
                    Value = "Flagg"
                },
                new UserClaims()
                {
                    Id = 3,
                    UserId = 1,
                    Type = "country",
                    Value = "nl"
                },
                new UserClaims()
                {
                    Id = 4,
                    UserId = 1,
                    Type = "role",
                    Value = "FreeUser"
                },
                new UserClaims()
                {
                    Id = 5,
                    UserId = 2,
                    Type = "given_name",
                    Value = "Emma"
                },
                new UserClaims()
                {
                    Id = 6,
                    UserId = 2,
                    Type = "family_name",
                    Value = "Flagg"
                },
                new UserClaims()
                {
                    Id = 7,
                    UserId = 2,
                    Type = "country",
                    Value = "be"
                },
                new UserClaims()
                {
                    Id = 8,
                    UserId = 2,
                    Type = "role",
                    Value = "PayingUser"
                });*/
        }

        /*        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
                {
                    // get updated entries
                    var updatedConcurrencyAwareEntries = ChangeTracker.Entries()
                            .Where(e => e.State == EntityState.Modified)
                            .OfType<IConcurrencyAware>();

                    foreach (var entry in updatedConcurrencyAwareEntries)
                    {
                        entry.ConcurrencyStamp = Guid.NewGuid().ToString();
                    }

                    return base.SaveChangesAsync(cancellationToken);
                }*/
    }
}
