using Microsoft.AspNetCore.Identity;

namespace CustomCADs.Auth.Data.Entities;

public class AppUser : IdentityUser<Guid>
{
    public AppUser(string username, string email)
        : base(username)
    {
        Email = email;
    }

    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenEndDate { get; set; }
}
