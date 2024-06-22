using Microsoft.EntityFrameworkCore;
using Car_shering.Dtos;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace Car_shering.Entity
{
    public class CarSheringDbContext: IdentityDbContext<AppUser>
    {
        public DbSet<Car> Cars { get; set; }
        public DbSet<Rental> Rentals { get; set; }
        public DbSet<CarLocalization> CarLocalizations { get; set; }


        public CarSheringDbContext(DbContextOptions<CarSheringDbContext> options):base(options)
        {
                
        }      
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            var admin = new IdentityRole("admin");
            admin.NormalizedName = "admin";

            var user = new IdentityRole("user");
            user.NormalizedName = "user";

            modelBuilder.Entity<IdentityRole>().HasData(admin, user);
            var hasher = new PasswordHasher<AppUser>();
            var adminPasswordHash = hasher.HashPassword(null, "admin123321");
            var user1 = new AppUser
            {
                Id = "1",
                Name = "admin",
                UserName = "admin@admin.pl",
                NormalizedUserName = "admin@admin.pl",
                Email = "admin@admin.pl",
                PasswordHash = adminPasswordHash,
                
            };
            var userRole = new IdentityUserRole<string>
            {
                UserId = user1.Id.ToString(),
                RoleId = admin.Id, 
            };
            modelBuilder.Entity<AppUser>().HasData(user1);
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(userRole);
        }
        public DbSet<Car_shering.Dtos.CarLocalizationDto> CarLocalizationDto { get; set; } = default!;
        public DbSet<Car_shering.Dtos.RentalDto> RentalDto { get; set; } = default!;
    }
}
