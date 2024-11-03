using CustomCADs.Account.Application.Common.Contracts;

namespace CustomCADs.Account.Application.Roles.Queries.GetAllNames;

public record GetAllRoleNamesQuery : IQuery<IEnumerable<string>>;