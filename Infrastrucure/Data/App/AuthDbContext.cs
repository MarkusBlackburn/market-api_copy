using Core.Models.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.App
{
    public class AuthDbContext : IdentityDbContext<User>
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var readerRole = "9fe09393-2fad-48fd-b5b2-b1335adc5599";
            var writerRole = "27b9fe2e-e8bd-458a-9dcf-379993956ae3";

            var roles = new List<IdentityRole>
            {
                new()
                {
                    Id = readerRole,
                    Name = "Viewer",
                    NormalizedName = "Viewer".ToUpper(),
                },

                new()
                {
                    Id = writerRole,
                    Name = "Admin",
                    NormalizedName = "Admin".ToUpper(),
                }
            };

            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}
