namespace HouseRules.Models;

public class ChoreAssignment
{
    public int Id { get; set; }

    // Foreign Keys
    public int UserProfileId { get; set; }
    public UserProfile UserProfile { get; set; }

    public int ChoreId { get; set; }
    public Chore Chore { get; set; }
}
