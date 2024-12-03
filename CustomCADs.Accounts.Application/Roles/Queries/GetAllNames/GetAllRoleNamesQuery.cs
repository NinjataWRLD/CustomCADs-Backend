namespace CustomCADs.Accounts.Application.Roles.Queries.GetAllNames;

public sealed record GetAllRoleNamesQuery
    : IQuery<IEnumerable<string>>;