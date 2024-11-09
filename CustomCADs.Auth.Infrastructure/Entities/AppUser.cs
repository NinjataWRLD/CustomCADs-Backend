using CustomCADs.Shared.Core.Domain;
using Microsoft.AspNetCore.Identity;

namespace CustomCADs.Auth.Infrastructure.Entities;

public class AppUser : IdentityUser<Guid>, IEntity
{
    public AppUser() : base() { }

    public AppUser(string username, string email)
        : base(username)
    {
        Email = email;
    }

    public Guid? AccountId { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenEndDate { get; set; }
}
