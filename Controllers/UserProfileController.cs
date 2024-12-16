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
public class UserProfileController : ControllerBase
{
    private HouseRulesDbContext _dbContext;
    private readonly IMapper _mapper;

    public UserProfileController(HouseRulesDbContext context, IMapper mapper)
    {
        _dbContext = context;
        _mapper = mapper;
    }

    [HttpGet]
    // [Authorize]
    ///api/userprofile
    public IActionResult Get()
    {
        var userProfiles = _dbContext.UserProfiles
     .Include(up => up.IdentityUser)
     .Select(up => new UserProfileDTO
     {
         Id = up.Id,
         FirstName = up.FirstName,
         LastName = up.LastName,
         Address = up.Address,
         Email = up.IdentityUser.Email,
         UserName = up.IdentityUser.UserName, // Manually map UserName
         Roles = _dbContext.UserRoles
             .Where(ur => ur.UserId == up.IdentityUserId)
             .Join(_dbContext.Roles,
                   ur => ur.RoleId,
                   r => r.Id,
                   (ur, r) => r.Name)
             .ToList(), // This will populate the roles
         IdentityUserId = up.IdentityUserId,
         IdentityUser = up.IdentityUser // Explicitly include IdentityUser
     })
     .ToList();


        return Ok(userProfiles);
    }

[HttpGet("{id}/chores")]
    // [Authorize]
    public IActionResult GetById(int id)
    {
        UserProfile userProfile = _dbContext.UserProfiles.Include(up => up.IdentityUser)
                                .Include(up => up.ChoreAssignments)
                                .ThenInclude(ca => ca.Chore)
                                .Include(up => up.ChoreCompletions)
                                .ThenInclude(cc => cc.Chore)
                                .FirstOrDefault(up => up.Id == id);

        if (userProfile == null)
        {
            return NotFound();
        }

        // int userIdInt = int.Parse(id);
        //include assigned chores completed chores 

        // this is why we need to include where  .Where(ur => ur.UserId == userProfile.IdentityUserId) many users can have the same role 
        // UserId	RoleId
        // user-1	role-1
        // user-1	role-2
        // user-2	role-2
        // user-3	role-3
        // user-3	role-4
        // user-4	role-1
        // user-4	role-5

        var userRoleIds = _dbContext.UserRoles
            .Where(ur => ur.UserId == userProfile.IdentityUserId)
            .Select(ur => ur.RoleId)
            .ToList();

        var userChoreDTO = new
        {
            FirstName = userProfile.FirstName,
            LastName = userProfile.LastName,
            Address = userProfile.Address,
            Email = userProfile.IdentityUser?.Email,
            // Roles = _dbContext.UserRoles.Where(eachUR => eachUR.UserId == userProfile.IdentityUserId)
            // .Select(ourUR => _dbContext.Roles.FirstOrDefault(eachRole => userRoleIds.Contains(ourUR.RoleId))), my wrong way look alter why its wrong 
            Roles = _dbContext.Roles
                .Where(role => userRoleIds.Contains(role.Id))
                .Select(role => role.Name)
                .ToList(),
            IdentityUserId = userProfile.IdentityUser.Id,
            IdentityUser = userProfile.IdentityUser,
            AssignedChores = userProfile.ChoreAssignments.Select(ca => new
            {
                ChoreName = ca.Chore.Name,
                Difficulty = ca.Chore.Difficulty
            }).ToList(),
            CompletedChores = userProfile.ChoreCompletions.Select(cc => new
            {
                ChoreName = cc.Chore.Name,
                CompletedOn = cc.CompletedOn
            }).ToList()
        };



        return Ok(userChoreDTO);
    }




    [HttpGet("withroles")]
[Authorize(Roles = "Admin")]
public IActionResult GetWithRoles()
{
    return Ok(_dbContext.UserProfiles
    .Include(up => up.IdentityUser)
    .Select(up => new UserProfileDTO
    {
        Id = up.Id,
        FirstName = up.FirstName,
        LastName = up.LastName,
        Address = up.Address,
        Email = up.IdentityUser.Email,
        UserName = up.IdentityUser.UserName,
        IdentityUserId = up.IdentityUserId,
        Roles = _dbContext.UserRoles
        .Where(ur => ur.UserId == up.IdentityUserId)
        .Select(ur => _dbContext.Roles.SingleOrDefault(r => r.Id == ur.RoleId).Name)
        .ToList()
    }));
}

[HttpPost("promote/{id}")]
[Authorize(Roles = "Admin")]
public IActionResult Promote(string id)
{
    IdentityRole role = _dbContext.Roles.SingleOrDefault(r => r.Name == "Admin");
    // This will create a new row in the many-to-many UserRoles table.
    _dbContext.UserRoles.Add(new IdentityUserRole<string>
    {
        RoleId = role.Id,
        UserId = id
    });
    _dbContext.SaveChanges();
    return NoContent();
}

[HttpPost("demote/{id}")]
[Authorize(Roles = "Admin")]
public IActionResult Demote(string id)
{
    IdentityRole role = _dbContext.Roles
        .SingleOrDefault(r => r.Name == "Admin"); 
    IdentityUserRole<string> userRole = _dbContext
        .UserRoles
        .SingleOrDefault(ur =>
            ur.RoleId == role.Id &&
            ur.UserId == id);

    _dbContext.UserRoles.Remove(userRole);
    _dbContext.SaveChanges();
    return NoContent();
}
}