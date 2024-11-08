using CustomCADs.Shared.Core.Entities;
using Microsoft.AspNetCore.Identity;

namespace CustomCADs.Auth.Infrastructure.Entities;

public class AppRole : IdentityRole<Guid>, IEntity;