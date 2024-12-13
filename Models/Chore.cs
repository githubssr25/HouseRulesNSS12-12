
namespace HouseRules.Models;
public class Chore
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Difficulty { get; set; }
    public int ChoreFrequencyDays { get; set; }

    // Relationships
    public List<ChoreAssignment> ChoreAssignments { get; set; } = new List<ChoreAssignment>();
    public List<ChoreCompletion> ChoreCompletions { get; set; } = new List<ChoreCompletion>();
}
