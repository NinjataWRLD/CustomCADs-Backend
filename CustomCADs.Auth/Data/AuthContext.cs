using CustomCADs.Auth.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CustomCADs.Auth.Data;

public class AuthContext(DbContextOptions opt) : IdentityDbContext<AppUser, AppRole, Guid>(opt);
