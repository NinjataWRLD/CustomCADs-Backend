using Microsoft.AspNetCore.Identity;

namespace CustomCADs.Identity.Domain.Entities;

public class AppRole(string name) : IdentityRole<Guid>(name);