using Microsoft.AspNetCore.Identity;

namespace FIRSTPROJECT.Infrastructure.Identity;

public class ApplicationUser : IdentityUser
{
    public string FullName { get; set; } = string.Empty;
}