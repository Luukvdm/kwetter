using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Kwetter.Services.Identity.Api.Models
{
    public class ApplicationUser : IdentityUser
    {
       [Required] public string Name { get; set; }
       [Required] public string Username { get; set; }
    }
}
