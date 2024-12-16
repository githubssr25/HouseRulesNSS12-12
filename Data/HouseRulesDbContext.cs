using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using HouseRules.Models;
using Microsoft.AspNetCore.Identity;

namespace HouseRules.Data;
public class HouseRulesDbContext : IdentityDbContext<IdentityUser>
{
    private readonly IConfiguration _configuration;
    public DbSet<UserProfile> UserProfiles { get; set; }
    public DbSet<Chore> Chores {get ; set; }

    public DbSet<ChoreAssignment> ChoreAssignments { get; set;}

    public DbSet<ChoreCompletion> ChoreCompletions { get; set;}

    public HouseRulesDbContext(DbContextOptions<HouseRulesDbContext> context, IConfiguration config) : base(context)
    {
        _configuration = config;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
        {
            Id = "c3aaeb97-d2ba-4a53-a521-4eea61e59b35",
            Name = "Admin",
            NormalizedName = "admin"
        });

        modelBuilder.Entity<IdentityUser>().HasData(new IdentityUser
        {
            Id = "dbc40bc6-0829-4ac5-a3ed-180f5e916a5f",
            UserName = "Administrator",
            Email = "admina@strator.comx",
            PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(null, "AdminPassword123!")
        });

        modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
        {
            RoleId = "c3aaeb97-d2ba-4a53-a521-4eea61e59b35",
            UserId = "dbc40bc6-0829-4ac5-a3ed-180f5e916a5f"
        });

        modelBuilder.Entity<UserProfile>().HasData(new UserProfile
        {
            Id = 1,
            IdentityUserId = "dbc40bc6-0829-4ac5-a3ed-180f5e916a5f",
            FirstName = "Admina",
            LastName = "Strator",
            Address = "101 Main Street",
            Email = "admin.strator123@gmail.com"
        });

        // // Generate a GUID for the user ID
        // var user2Id = Guid.NewGuid().ToString();

        // modelBuilder.Entity<IdentityUser>().HasData(new IdentityUser
        // {
        //     Id = user2Id, // Use the generated GUID
        //     UserName = "secondadmin",
        //     Email = "secondadmin@example.com",
        //     PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(null, "SecondAdminPassword1234!")
        // });

        // modelBuilder.Entity<UserProfile>().HasData(new UserProfile
        // {
        //     Id = 3,
        //     IdentityUserId = user2Id, // Match the IdentityUser Id here
        //     FirstName = "Second",
        //     LastName = "Admin",
        //     Address = "300 Admin Street",
        //     Email = "secondadmin@example.com"
        // });


        // modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
        // {
        //     RoleId = "c3aaeb97-d2ba-4a53-a521-4eea61e59b35", // Admin RoleId
        //     UserId = user2Id // New Admin UserId
        // });


          // Add Chores
    modelBuilder.Entity<Chore>().HasData(
        new Chore { Id = 1, Name = "Mow the Lawn", Difficulty = 4, ChoreFrequencyDays = 14 },
        new Chore { Id = 2, Name = "Clean the Kitchen", Difficulty = 2, ChoreFrequencyDays = 7 },
        new Chore { Id = 3, Name = "Take Out Trash", Difficulty = 1, ChoreFrequencyDays = 1 },
        new Chore { Id = 4, Name = "Vacuum the House", Difficulty = 3, ChoreFrequencyDays = 7 },
        new Chore { Id = 5, Name = "Wash the Car", Difficulty = 5, ChoreFrequencyDays = 30 },
        new Chore { Id = 6, Name = "Water the Plants", Difficulty = 2, ChoreFrequencyDays = 2 },
        new Chore { Id = 7, Name = "Clean the Bathroom", Difficulty = 4, ChoreFrequencyDays = 10 },
        new Chore { Id = 8, Name = "Dust the Shelves", Difficulty = 2, ChoreFrequencyDays = 14 },
        new Chore { Id = 9, Name = "Organize the Garage", Difficulty = 5, ChoreFrequencyDays = 60 },
        new Chore { Id = 10, Name = "Cook Dinner", Difficulty = 3, ChoreFrequencyDays = 1 }
    );

    // Add ChoreAssignments
    modelBuilder.Entity<ChoreAssignment>().HasData(
        new ChoreAssignment { Id = 1, UserProfileId = 1, ChoreId = 1 },
        new ChoreAssignment { Id = 2, UserProfileId = 1, ChoreId = 2 },
        new ChoreAssignment { Id = 3, UserProfileId = 1, ChoreId = 3 },
        new ChoreAssignment { Id = 4, UserProfileId = 1, ChoreId = 4 }
    );

    // Add ChoreCompletions
    modelBuilder.Entity<ChoreCompletion>().HasData(
        new ChoreCompletion { Id = 1, UserProfileId = 1, ChoreId = 1, CompletedOn = DateTime.Now.AddDays(-14) },
        new ChoreCompletion { Id = 2, UserProfileId = 1, ChoreId = 2, CompletedOn = DateTime.Now.AddDays(-7) },
        new ChoreCompletion { Id = 3, UserProfileId = 1, ChoreId = 3, CompletedOn = DateTime.Now.AddDays(-1) },
        new ChoreCompletion { Id = 4, UserProfileId = 1, ChoreId = 4, CompletedOn = DateTime.Now.AddDays(-7) }
    );

       
    }
}