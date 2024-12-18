using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using HouseRules.Data;
using HouseRules.Models.DTOs;
using Microsoft.EntityFrameworkCore;
using HouseRules.Models;
using Microsoft.AspNetCore.Identity;
using AutoMapper;
using Mapping.MappingProfile;


namespace HouseRules.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ChoreController : ControllerBase
{
    private HouseRulesDbContext _dbContext;
    private readonly IMapper _mapper;

    public ChoreController(HouseRulesDbContext context, IMapper mapper)
    {
        _dbContext = context;
        _mapper = mapper;
    }

       [HttpGet]
    // [Authorize]
    ///api/chore
    // for ChoresList/// 
    public IActionResult Get()
    {
          List<Chore> ourChores = _dbContext.Chores.ToList();
        var mappedChores = _mapper.Map<List<ChoreDTO>>(ourChores);

          // Manually set ChoresDue on each mapped ChoreDTO
    for (int i = 0; i < mappedChores.Count; i++)
    {
        var originalChore = ourChores[i];
        mappedChores[i].ChoresDue = originalChore.ChoresDue; // Calculate the ChoresDue property
    }

        return Ok(mappedChores);

// said dont do the includes its not part of what were mapping so mapper might fail as result 
        // List<Chore> ourChores = _dbContext.Chores
        //                     .Include(c => c.ChoreAssignments)
        //                     .Include(c => c.ChoreCompletions)
        //                     .ToList();
        // var mappedChores = _mapper.Map<List<ChoreDTO>>(ourChores);
        // return Ok(mappedChores);
    }

    [HttpGet("{id}")]
    // [Authorize]
    public IActionResult GetById(int id)
    {
          var chore = _dbContext.Chores
        .Include(c => c.ChoreAssignments)
            .ThenInclude(ca => ca.UserProfile) // Ensure UserProfile is eagerly loaded
        .Include(c => c.ChoreCompletions)
            .ThenInclude(cc => cc.UserProfile) // Ensure UserProfile is eagerly loaded
        .FirstOrDefault(c => c.Id == id);

        if (chore == null)
        {
            return NotFound($"Chore with ID {id} not found.");
        }
        var mappedChore = new {
            ChoreId = chore.Id,
            Name = chore.Name,
            Difficulty = chore.Difficulty,
            ChoreFrequency = chore.ChoreFrequencyDays,
            ChoresDue = chore.ChoresDue,
            ChoreAssignment = chore.ChoreAssignments.Select( eachCA => new {
            ChoreAssignmentId = eachCA.Id,
            UserProfileId = eachCA.UserProfileId,
            FirstName = eachCA.UserProfile.FirstName,
            LastName = eachCA.UserProfile.LastName,
            }).ToList(),
            ChoreCompletions = chore.ChoreCompletions
            .OrderBy(eachCC => eachCC.CompletedOn)
            .Take(1)
            .Select(eachCC => new {
                ChoreId = eachCC.ChoreId,
                ChoreCompletionId = eachCC.Id,
                DateCompleted = eachCC.CompletedOn,
                UserProfileId = eachCC.UserProfileId,
                FirstName = eachCC.UserProfile.FirstName,
                LastName = eachCC.UserProfile.LastName,
            }).ToList()
            };

        return Ok(mappedChore);
    }

[HttpPost("{choreId}/complete")]
// [Authorize]
public IActionResult CreateChoreCompletion(int choreId, [FromQuery] int userId)
{
    var user = _dbContext.UserProfiles
                .Include(u => u.ChoreAssignments)
                .ThenInclude(ca => ca.Chore)
                .Include(u => u.ChoreCompletions)
                .ThenInclude(cc => cc.Chore)
                .FirstOrDefault(u => u.Id == userId);

   if (user == null)
    {
        return NotFound(new { message = $"User with ID {userId} not found" });
    }

      // Check if the user is assigned to the specific chore
    var assignedChore = user.ChoreAssignments
        .FirstOrDefault(choreAssign => choreAssign.ChoreId == choreId);

    if (assignedChore == null)
    {
        return BadRequest(new { message = $"User with ID {userId} is not assigned to Chore ID {choreId}" });
    }

 var newChoreCompletion = new ChoreCompletion
    {
        CompletedOn = DateTime.Now,
        UserProfileId = userId,      // We have userId from the query parameter
        ChoreId = choreId            // Use the choreId from the URL
    };

//as long as you set UserProfileId and ChoreId, Entity Framework will automatically associate UserProfile and Chore through navigation properties when you query for the ChoreCompletion later


   _dbContext.ChoreCompletions.Add(newChoreCompletion);
   //    user.ChoreCompletions.Add(newChoreCompletion); dont needto automatically added to join table 


   _dbContext.SaveChanges();

    return NoContent();




// probably inefficient 
//    ChoreCompletion newChoreCompletion = new ChoreCompletion{

//     CompletedOn = DateTime.Now,
//     UserProfileId = user.Id,
//     UserProfile = _dbContext.UserProfiles.FirstOrDefault(up => up.Id == id),
//     ChoreId = user.ChoreAssignments.Where(choreAssign => choreAssign.UserProfileId == user.Id)
//                                    .Select(choreAssign => choreAssign.ChoreId)
//                                    .FirstOrDefault(),
//     Chore = user.ChoreAssignments.Where(choreAssign => choreAssign.UserProfileId == user.Id)
//                                  .Select(choreAssign => choreAssign.Chore)
//                                  .FirstOrDefault(),
//    };


}

  [HttpPost]
    // [Authorize]
    public IActionResult CreateChore([FromBody] CreateChoreDTO choreDto)
    {
        if (choreDto == null)
        {
            return BadRequest("Chore data is required.");
        }

        var newChore = _mapper.Map<Chore>(choreDto);
        _dbContext.Chores.Add(newChore);
        _dbContext.SaveChanges();

    return Ok(newChore);
    }


  [HttpPut("{id}")]
// [Authorize]
public IActionResult UpdateChore(int id, [FromBody] UpdateChoreDTO choreDto)
{
    if (choreDto == null)
    {
        return BadRequest("Chore data is required.");
    }

    // Find the chore by ID
    var chore = _dbContext.Chores.FirstOrDefault(c => c.Id == id);

    chore.Name = choreDto.Name;
    chore.Difficulty = choreDto.Difficulty;
    chore.ChoreFrequencyDays = choreDto.ChoreFrequencyDays;

    if (chore == null)
    {
        return NotFound($"Chore with ID {id} not found.");
    }

    // Update the chore properties
    _mapper.Map(choreDto, chore);

    // Save changes to the database
    _dbContext.SaveChanges();

    return Ok(chore);// Returns 204 No Content
}

[HttpPut("{choreId}/complete")]
// [Authorize]

public IActionResult CompleteChore(int choreId, [FromQuery] int userId)
{
    // Find the chore by its ID
    var chore = _dbContext.Chores.FirstOrDefault(c => c.Id == choreId);

    if (chore == null)
    {
        return NotFound($"Chore with ID {choreId} not found.");
    }

     // Check if a completion for this user and this chore already exists (optional)
    var existingCompletion = _dbContext.ChoreCompletions
        .FirstOrDefault(cc => cc.ChoreId == choreId && cc.UserProfileId == userId);

    if (existingCompletion != null)
    {
        return BadRequest("Chore already completed by this user.");
    }

    // Create a new ChoreCompletion entry if applicable
    var choreCompletion = new ChoreCompletion
    {
        ChoreId = choreId,
        UserProfileId = userId, // Assuming UserId is in claims
        CompletedOn = DateTime.UtcNow
        //DateTime.UtcNow will match the "universal" time concept of a "timestamp without time zone" field.
    };

    _dbContext.ChoreCompletions.Add(choreCompletion);
    _dbContext.SaveChanges();

    return Ok(new 
    {
        Message = "Chore Completed Successfully",
        ChoreCompletionId = choreCompletion.Id,
        ChoreId = choreCompletion.ChoreId,
        CompletedOn = choreCompletion.CompletedOn
    });
}


[HttpDelete("{id}")]
// [Authorize]
public IActionResult DeleteChore(int id)
{

     var chore = _dbContext.Chores
            .Include(c => c.ChoreAssignments)
            .Include(c => c.ChoreCompletions)
            .FirstOrDefault(c => c.Id == id);

        if (chore == null)
        {
            return NotFound($"Chore with ID {id} not found.");
        }

    var assignments = _dbContext.ChoreAssignments.Where(ca => ca.ChoreId == id).ToList();

    var completions = _dbContext.ChoreCompletions.Where(cc => cc.ChoreId == id).ToList();
    _dbContext.ChoreCompletions.RemoveRange(completions);
//ex how can be multiple values 
// ChoreCompletions Table:
// Id	UserProfileId	ChoreId	CompletedOn
// 1	1	1	2023-12-01 10:00 AM
// 2	2	1	2023-12-02 12:00 PM
// 3	1	1	2023-12-03 09:00 AM
// 4	3	2	2023-12-04 08:30 AM
_dbContext.Chores.Remove(chore);

_dbContext.SaveChanges();

return NoContent();

}

  [HttpPost("{choreId}/assign")]
    // [Authorize]
    public IActionResult AssignChore(int choreId, [FromQuery] int userId)
    {

    var chore = _dbContext.Chores
            .Include(c => c.ChoreAssignments)
            .Include(c => c.ChoreCompletions)
            .FirstOrDefault(c => c.Id == choreId);

        if (chore == null)
        {
            return NotFound($"Chore with ID {choreId} not found.");
        }

    var user = _dbContext.UserProfiles
                .Include(u => u.ChoreAssignments)
                .ThenInclude(ca => ca.Chore)
                .Include(u => u.ChoreCompletions)
                .ThenInclude(cc => cc.Chore)
                .FirstOrDefault(u => u.Id == userId);
      // Check if the assignment already exists to avoid duplicates
    var existingAssignment = _dbContext.ChoreAssignments
        .FirstOrDefault(ca => ca.ChoreId == choreId && ca.UserProfileId == userId);

    if (existingAssignment != null)
    {
        return BadRequest($"Chore with ID {choreId} is already assigned to User with ID {userId}.");
    }


    ChoreAssignment choreAssignment = new ChoreAssignment{
        UserProfileId = user.Id,
        ChoreId = chore.Id
    };
// dont need to do this nor did i need to use include tbh if add will automatically add the navigation properties

    _dbContext.ChoreAssignments.Add(choreAssignment);

    chore.ChoreAssignments.Add(choreAssignment);

    user.ChoreAssignments.Add(choreAssignment);

    _dbContext.SaveChanges();
        return NoContent();
    }


[HttpPost("{choreId}/unassign")]
// [Authorize]
public IActionResult UnassignChore(int choreId, [FromQuery] int userId)
{
    // Check if the chore exists
    var chore = _dbContext.Chores.FirstOrDefault(c => c.Id == choreId);
    if (chore == null)
    {
        return NotFound(new { message = $"Chore with ID {choreId} not found." });
    }

    // Check if the user exists
    var user = _dbContext.UserProfiles.FirstOrDefault(u => u.Id == userId);
    if (user == null)
    {
        return NotFound(new { message = $"User with ID {userId} not found." });
    }

    // Find the specific ChoreAssignment for the given user and chore
    var choreAssignment = _dbContext.ChoreAssignments
        .FirstOrDefault(ca => ca.ChoreId == choreId && ca.UserProfileId == userId);

    if (choreAssignment == null)
    {
        return NotFound(new { message = $"No assignment found for Chore ID {choreId} and User ID {userId}." });
    }

    // Remove the assignment from the database
    _dbContext.ChoreAssignments.Remove(choreAssignment);
    _dbContext.SaveChanges();

    return NoContent();
}


    [HttpGet("{id}/overdue")]
    // [Authorize]
    public IActionResult IsChoreOverdueById(int id)
    {
          var chore = _dbContext.Chores
        .Include(c => c.ChoreCompletions)
            .ThenInclude(cc => cc.UserProfile) // Ensure UserProfile is eagerly loaded
        .FirstOrDefault(c => c.Id == id);

        if (chore == null)
        {
            return NotFound($"Chore with ID {id} not found.");
        }
        var mappedChore = new {
            ChoreId = chore.Id,
            Name = chore.Name,
            Difficulty = chore.Difficulty,
            ChoreFrequency = chore.ChoreFrequencyDays,
            ChoreCompletions = chore.ChoreCompletions
            .OrderBy(eachCC => eachCC.CompletedOn)
            .Take(1)
            .Select(eachCC => new {
                ChoreCompletionId = eachCC.Id,
                DateCompleted = eachCC.CompletedOn,
                UserProfileId = eachCC.UserProfileId,
                FirstName = eachCC.UserProfile.FirstName,
                LastName = eachCC.UserProfile.LastName,
            }).FirstOrDefault()
            };

DateTime lastCompletion = mappedChore.ChoreCompletions?.DateCompleted ?? DateTime.MinValue;
//f no completions exist, it will return DateTime.MinValue (which is 0001-01-01 00:00:00)

DateTime currentTime = DateTime.Now;




        return Ok(mappedChore);
    }


// get asigned chores given a userId 
[HttpGet("assigned/{userId}")]
//[Authorize]
public IActionResult GetAssignedDueChores(int userId)
{

   var chores = _dbContext.Chores
        .Include(c => c.ChoreAssignments)
            .ThenInclude(ca => ca.UserProfile) // Ensure UserProfile is eagerly loaded
        .Include(c => c.ChoreCompletions)
            .ThenInclude(cc => cc.UserProfile) // Ensure UserProfile is eagerly loaded
        .Where(c => c.ChoreAssignments.Any(ca => ca.UserProfileId == userId)) //acts ON CHORES SO GETS ANY CHORE THAT HAS A CHORE ASSIGNMENT THAT INCLUDES THIS USERID 
        //BUT IF CHOREASSIGNMENT HAS 4 CHORES AND ONLY 1 IS FOR OUR USERID IT INCLUDES ALL THE CHORE ASSIGNMENTS BEYOND JUST OUR USERID AS PART OF THIS CHORE
        //filters through each chore and includs any chore assignment for it why all the choreassignments are for choreID 1 
        //REASON WE ARE ONLY SEEING HCOREID 1 BELOW IS BECUASE  ChoreId = 1 is the only chore that has a ChoreAssignment with the UserProfileId that matches the parameter you passed in.
        .ToList()
         //BRING DATA INTO MEMORY 
        .Where(c => c.ChoresDue)
        .ToList();
  //The ChoresDue property is a custom C# property that exists only in C# memory. EF Core can only translate properties that are part of the database schema (mapped properties) into SQL. Since ChoresDue is not stored in the database, it cannot be translated into SQL. 
  //Instead, EF Core tries to convert it into an SQL query, but since it doesn’t exist in SQL, it throws this exception.      
// solution is load data into memory using ToList first before doing the .Where 


//current structure is below make it cleaner 
// 1.	Array(1)
// 1.	choreAssignments: Array(4)
// 1.	0: {id: 5, userProfileId: 2, userProfile: {…}, choreId: 1, chore: null} CAN SEE ALL CHORE ASSIGNMENTS NOT JUST OUR USER ID 
// 2.	1: {id: 6, userProfileId: 2, userProfile: {…}, choreId: 1, chore: null}
// 3.	2: {id: 25, userProfileId: 1, userProfile: {…}, choreId: 1, chore: null}
// 4.	3: {id: 27, userProfileId: 3, userProfile: {…}, choreId: 1, chore: null}
// 5.	length: 4
// 2.	choreCompletions: Array(1)
// 1.	0: {id: 1, completedOn: '2024-12-02T13:21:23.422156Z', userProfileId: 1, userProfile: {…}, choreId: 1, …}
// 2.	length: 1
// 3.	choreFrequencyDays: 2
// 4.	choresDue: true
// 5.	difficulty: 4
// 6.	id: 1
// 7.	name: "Mow the Lawn update 1"

var choresToReturn = chores.Select(c => new {

    choreId = c.Id,
    choreName = c.Name,
    choreFrequency = c.ChoreFrequencyDays,
    difficulty = c.Difficulty,
    choreDue = c.ChoresDue,
    choreAssignmentIds = c.ChoreAssignments.Where(ca => ca.UserProfileId == userId).Select(ca => ca.Id).ToList(),
    LastCompletedDate = c.ChoreCompletions.Any() 
    ? c.ChoreCompletions.Max(eachCC => eachCC.CompletedOn) 
    : (DateTime?)null //WHAT IF THERE IS NO COMPLETIONS WOULD GIVE NULL ERROR SO GET AROUND IT 
}).ToList();

return Ok(choresToReturn);


}


// ex
// Chore Table
// ChoreId	Chore Name	ChoreFrequency
// 1	Mow the Lawn	14
// 2	Clean the Kitchen	7
// 3	Wash the Car	30
// 4	Water the Plants	3
// 5	Clean the Bathroom	10

// ChoreAssignment Table
// ChoreId	UserProfileId
// 1	2
// 2	2
// 3	3
// 4	2
// 5	4
   // if you dont use where  lets say our userProfileId 2 is 2 well it will just retrieve the first choreAssignment where userId is 2 
   // when in reality there are 2 so that is why we cant use firstOrDefualt 



}

