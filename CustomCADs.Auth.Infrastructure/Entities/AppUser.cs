using CustomCADs.Shared.Core.Common.TypedIds.Account;
using Microsoft.AspNetCore.Identity;

namespace CustomCADs.Auth.Infrastructure.Entities;

public class AppUser : IdentityUser<Guid>
{
    public AppUser() : base() { }

    public AppUser(string username, string email)
        : base(username)
    {
        Email = email;
    }

    public UserId? AccountId { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenEndDate { get; set; }
}
