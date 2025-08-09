using Microsoft.AspNetCore.Identity;

namespace CustomCADs.Identity.Infrastructure.Identity.ShadowEntities;

public class AppRole(string name) : IdentityRole<Guid>(name);
