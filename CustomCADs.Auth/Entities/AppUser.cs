using Microsoft.AspNetCore.Identity;

namespace CustomCADs.Auth.Entities;

public class AppUser : IdentityUser<Guid>
{
    public AppUser(string username, string email)
        : base(username)
    {
         this.Email = email;
    }
}
