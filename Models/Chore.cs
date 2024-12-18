
namespace HouseRules.Models;
using System.ComponentModel.DataAnnotations; // For [MaxLength] and [Range]

public class Chore
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Chore name is required.")]
        [MaxLength(100, ErrorMessage = "Chore name cannot exceed 100 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Difficulty is required.")]
        [Range(1, 5, ErrorMessage = "Difficulty must be between 1 and 5.")]
        public int Difficulty { get; set; }

        [Required(ErrorMessage = "Chore frequency is required.")]
        [Range(1, 14, ErrorMessage = "Chore frequency must be between 1 and 14.")]
        public int ChoreFrequencyDays { get; set; }

    // Relationships
    public List<ChoreAssignment> ChoreAssignments { get; set; } = new List<ChoreAssignment>();
    public List<ChoreCompletion> ChoreCompletions { get; set; } = new List<ChoreCompletion>();
}
