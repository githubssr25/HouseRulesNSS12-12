using Microsoft.AspNetCore.Identity;

namespace HouseRules.Models;


public class UserProfile
{
    public int Id { get; set; }
    public string? FirstName { get; set; }

    public string? LastName { get; set; }
    public string? Address { get; set; }

    public string? IdentityUserId { get; set; }

    public IdentityUser? IdentityUser { get; set; }

     public string? Email { get; set; }

    // Relationships
    public List<ChoreAssignment> ChoreAssignments { get; set; } = new List<ChoreAssignment>();
    public List<ChoreCompletion> ChoreCompletions { get; set; } = new List<ChoreCompletion>();

}