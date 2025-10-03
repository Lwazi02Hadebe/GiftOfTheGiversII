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
        public DbSet<ResourceDonation> ResourceDonations { get; set; }
        public DbSet<DonationCenter> DonationCenters { get; set; }
        public DbSet<AcceptedItem> AcceptedItems { get; set; }
        public DbSet<Volunteer> Volunteers { get; set; }
        public DbSet<VolunteerTask> VolunteerTasks { get; set; }
        public DbSet<VolunteerAssignment> VolunteerAssignments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Seed initial data - MAKE SURE EACH ENTITY IS SEEDED ONLY ONCE

            // Inventory Items
            modelBuilder.Entity<InventoryItem>().HasData(
                new InventoryItem { ItemID = 1, ItemName = "Bottled Water 500ml", Description = "Clean drinking water", Category = "Food", Unit = "Bottle" },
                new InventoryItem { ItemID = 2, ItemName = "Emergency Blanket", Description = "Thermal emergency blankets", Category = "Shelter", Unit = "Piece" },
                new InventoryItem { ItemID = 3, ItemName = "First Aid Kit", Description = "Basic medical supplies", Category = "Medical", Unit = "Kit" },
                new InventoryItem { ItemID = 4, ItemName = "Canned Food", Description = "Non-perishable food items", Category = "Food", Unit = "Can" }
            );

            // Relief Projects
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

            // Project Needs
            modelBuilder.Entity<ProjectNeed>().HasData(
                new ProjectNeed { ProjectNeedID = 1, ProjectID = 1, ItemID = 1, QuantityNeeded = 1000, QuantityFulfilled = 450 },
                new ProjectNeed { ProjectNeedID = 2, ProjectID = 1, ItemID = 2, QuantityNeeded = 500, QuantityFulfilled = 200 },
                new ProjectNeed { ProjectNeedID = 3, ProjectID = 2, ItemID = 3, QuantityNeeded = 200, QuantityFulfilled = 75 },
                new ProjectNeed { ProjectNeedID = 4, ProjectID = 2, ItemID = 4, QuantityNeeded = 800, QuantityFulfilled = 300 }
            );

            // Donation Centers
            modelBuilder.Entity<DonationCenter>().HasData(
                new DonationCenter
                {
                    CenterID = 1,
                    Name = "Central Relief Warehouse",
                    Address = "123 Main Street",
                    City = "Johannesburg",
                    ZipCode = "2000",
                    Phone = "+27-11-123-4567",
                    Email = "warehouse@relief.org",
                    OperatingHours = "Mon-Sun 8:00-18:00"
                },
                new DonationCenter
                {
                    CenterID = 2,
                    Name = "Coastal Distribution Center",
                    Address = "456 Beach Road",
                    City = "Durban",
                    ZipCode = "4000",
                    Phone = "+27-31-765-4321",
                    OperatingHours = "Mon-Fri 9:00-17:00, Sat 9:00-13:00"
                }
            );

            // Accepted Items - MAKE SURE THESE ARE UNIQUE
            modelBuilder.Entity<AcceptedItem>().HasData(
                new AcceptedItem { AcceptedItemID = 1, CenterID = 1, ItemID = 1, SpecialInstructions = "Sealed bottles only" },
                new AcceptedItem { AcceptedItemID = 2, CenterID = 1, ItemID = 2, SpecialInstructions = "New or gently used" },
                new AcceptedItem { AcceptedItemID = 3, CenterID = 1, ItemID = 3 },
                new AcceptedItem { AcceptedItemID = 4, CenterID = 1, ItemID = 4, SpecialInstructions = "Non-expired items only" },
                new AcceptedItem { AcceptedItemID = 5, CenterID = 2, ItemID = 1 },
                new AcceptedItem { AcceptedItemID = 6, CenterID = 2, ItemID = 3 },
                new AcceptedItem { AcceptedItemID = 7, CenterID = 2, ItemID = 4 }
            );

            // Volunteer Tasks
            modelBuilder.Entity<VolunteerTask>().HasData(
                new VolunteerTask
                {
                    TaskID = 1,
                    Title = "Food Distribution Assistance",
                    Description = "Help distribute food packages to affected families in coastal areas.",
                    ProjectID = 1,
                    TaskType = "Distribution",
                    Location = "Coastal Region Community Center",
                    StartDate = DateTime.UtcNow.AddDays(2),
                    EndDate = DateTime.UtcNow.AddDays(2).AddHours(6),
                    VolunteersNeeded = 10,
                    Status = "Open",
                    Requirements = "Physical fitness, friendly demeanor"
                },
                new VolunteerTask
                {
                    TaskID = 2,
                    Title = "Medical Camp Support",
                    Description = "Assist medical professionals in setting up and running temporary medical camps.",
                    ProjectID = 2,
                    TaskType = "Medical",
                    Location = "Mountain Area Temporary Clinic",
                    StartDate = DateTime.UtcNow.AddDays(3),
                    EndDate = DateTime.UtcNow.AddDays(3).AddHours(8),
                    VolunteersNeeded = 5,
                    Status = "Open",
                    Requirements = "First aid knowledge preferred"
                },
                new VolunteerTask
                {
                    TaskID = 3,
                    Title = "Warehouse Organization",
                    Description = "Help organize and sort donated items in the central warehouse.",
                    ProjectID = 1,
                    TaskType = "Logistics",
                    Location = "Central Relief Warehouse",
                    StartDate = DateTime.UtcNow.AddDays(1),
                    EndDate = DateTime.UtcNow.AddDays(1).AddHours(4),
                    VolunteersNeeded = 8,
                    Status = "Open"
                }
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