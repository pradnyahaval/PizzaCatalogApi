using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace PizzaCatalog.WebApi.Data
{
    public class AuthenticationDBContext : IdentityDbContext
    {
        public AuthenticationDBContext(DbContextOptions<AuthenticationDBContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var readerId = "cb3d64e8-edb3-4a00-9433-9856a977e453"; //Guid.NewGuid()
            var writerId = "10be995c-4d7e-468a-8c09-a46d19b62fb2"; //Guid.NewGuid()

            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id = readerId,
                    ConcurrencyStamp = readerId,
                    Name = "Reader",
                    NormalizedName = "Reader".ToUpper()
                },    
                new IdentityRole
                {
                    Id = writerId,
                    ConcurrencyStamp = writerId,
                    Name = "Writer",
                    NormalizedName = "Writer".ToUpper()
                }
            };

            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}
