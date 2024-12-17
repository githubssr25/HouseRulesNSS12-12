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
[Authorize]
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
    [Authorize]
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
[Authorize]
public IActionResult UpdateChore(int id, [FromBody] UpdateChoreDTO choreDto)
{
    if (choreDto == null)
    {
        return BadRequest("Chore data is required.");
    }

    // Find the chore by ID
    var chore = _dbContext.Chores.FirstOrDefault(c => c.Id == id);

    if (chore == null)
    {
        return NotFound($"Chore with ID {id} not found.");
    }

    // Update the chore properties
    _mapper.Map(choreDto, chore);

    // Save changes to the database
    _dbContext.SaveChanges();

    return NoContent(); // Returns 204 No Content
}

[HttpPut("{id}/complete")]
[Authorize]
public IActionResult CompleteChore(int id)
{
    // Find the chore by its ID
    var chore = _dbContext.Chores.FirstOrDefault(c => c.Id == id);

    if (chore == null)
    {
        return NotFound($"Chore with ID {id} not found.");
    }

    // Create a new ChoreCompletion entry if applicable
    var choreCompletion = new ChoreCompletion
    {
        ChoreId = id,
        UserProfileId = int.Parse(User.FindFirst("UserId").Value), // Assuming UserId is in claims
        CompletedOn = DateTime.UtcNow
    };

    _dbContext.ChoreCompletions.Add(choreCompletion);
    _dbContext.SaveChanges();

    return NoContent(); // Returns 204 No Content
}


[HttpDelete("{id}")]
[Authorize]
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




}