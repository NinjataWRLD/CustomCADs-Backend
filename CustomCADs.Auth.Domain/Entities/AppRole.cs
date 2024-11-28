using Microsoft.AspNetCore.Identity;

namespace CustomCADs.Auth.Domain.Entities;

public class AppRole(string name) : IdentityRole<Guid>(name);