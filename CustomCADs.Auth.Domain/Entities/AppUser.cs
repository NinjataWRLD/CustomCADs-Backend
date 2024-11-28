using CustomCADs.Shared.Core.Common.TypedIds.Account;
using Microsoft.AspNetCore.Identity;

namespace CustomCADs.Auth.Domain.Entities;

public class AppUser : IdentityUser<Guid>
{
    public AppUser() : base() { }

    public AppUser(string username, string email, UserId accountId)
        : base(username)
    {
        Email = email;
        AccountId = accountId;
    }

    public UserId AccountId { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenEndDate { get; set; }
}
