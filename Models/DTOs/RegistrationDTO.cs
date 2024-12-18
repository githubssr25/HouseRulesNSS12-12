namespace HouseRules.Models.DTOs;
using System.ComponentModel.DataAnnotations;
public class RegistrationDTO
{
    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Password is required.")]
    public string Password { get; set; }

    [Required(ErrorMessage = "Username is required.")]
    [MaxLength(50, ErrorMessage = "Username cannot exceed 50 characters.")]
    public string UserName { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Address { get; set; }

}