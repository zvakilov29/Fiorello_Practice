using Microsoft.AspNetCore.Identity;

namespace ZV_Fiorello.Models
{
    public class AppUser : IdentityUser
    {
        public string? Fullname { get; set; } 
    }
}
