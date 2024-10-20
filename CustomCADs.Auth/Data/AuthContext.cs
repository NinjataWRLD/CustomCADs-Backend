using CustomCADs.Auth.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CustomCADs.Auth.Data;

public class AuthContext(DbContextOptions opts) : IdentityDbContext<AppUser, AppRole, Guid>(opts);
