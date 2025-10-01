using Microsoft.EntityFrameworkCore;

namespace GiftOfTheGiversII.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Donation> Donations { get; set; }
        public DbSet<ReliefProject> ReliefProjects { get; set; }
        public DbSet<InventoryItem> InventoryItems { get; set; }
        public DbSet<ProjectNeed> ProjectNeeds { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Seed initial data
            modelBuilder.Entity<InventoryItem>().HasData(
                new InventoryItem { ItemID = 1, ItemName = "Bottled Water 500ml", Description = "Clean drinking water", Category = "Food", Unit = "Bottle" },
                new InventoryItem { ItemID = 2, ItemName = "Emergency Blanket", Description = "Thermal emergency blankets", Category = "Shelter", Unit = "Piece" },
                new InventoryItem { ItemID = 3, ItemName = "First Aid Kit", Description = "Basic medical supplies", Category = "Medical", Unit = "Kit" },
                new InventoryItem { ItemID = 4, ItemName = "Canned Food", Description = "Non-perishable food items", Category = "Food", Unit = "Can" }
            );

            modelBuilder.Entity<ReliefProject>().HasData(
                new ReliefProject
                {
                    ProjectID = 1,
                    ProjectName = "Flood Relief - Coastal Region",
                    Description = "Providing emergency aid to communities affected by recent flooding in coastal areas.",
                    Location = "Coastal Region",
                    StartDate = DateTime.UtcNow.AddDays(-30),
                    Status = "Active",
                    ImageUrl = "/images/flood-relief.jpg"
                },
                new ReliefProject
                {
                    ProjectID = 2,
                    ProjectName = "Earthquake Response - Mountain Area",
                    Description = "Supporting communities affected by the recent earthquake with shelter and medical aid.",
                    Location = "Mountain Area",
                    StartDate = DateTime.UtcNow.AddDays(-15),
                    Status = "Active",
                    ImageUrl = "/images/earthquake-relief.jpg"
                }
            );

            modelBuilder.Entity<ProjectNeed>().HasData(
                new ProjectNeed { ProjectNeedID = 1, ProjectID = 1, ItemID = 1, QuantityNeeded = 1000, QuantityFulfilled = 450 },
                new ProjectNeed { ProjectNeedID = 2, ProjectID = 1, ItemID = 2, QuantityNeeded = 500, QuantityFulfilled = 200 },
                new ProjectNeed { ProjectNeedID = 3, ProjectID = 2, ItemID = 3, QuantityNeeded = 200, QuantityFulfilled = 75 },
                new ProjectNeed { ProjectNeedID = 4, ProjectID = 2, ItemID = 4, QuantityNeeded = 800, QuantityFulfilled = 300 }
            );

            // Create default admin user
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    UserID = 1,
                    Email = "admin@relief.org",
                    FirstName = "System",
                    LastName = "Admin",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin123"),
                    UserType = "Admin",
                    DateRegistered = DateTime.UtcNow,
                    IsEmailVerified = true
                }
            );
        }
    }
}