using Microsoft.AspNetCore.Identity;

namespace CustomCADs.Identity.Persistence.ShadowEntities;

public class AppRole(string name) : IdentityRole<Guid>(name);
