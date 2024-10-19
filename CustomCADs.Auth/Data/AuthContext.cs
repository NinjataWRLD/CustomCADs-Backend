using CustomCADs.Auth.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace CustomCADs.Auth.Data;

public class AuthContext : IdentityDbContext<AppUser, AppRole, Guid>;
