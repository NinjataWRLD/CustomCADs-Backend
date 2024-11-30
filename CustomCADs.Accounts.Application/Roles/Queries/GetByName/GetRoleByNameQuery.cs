namespace CustomCADs.Accounts.Application.Roles.Queries.GetByName;

public record GetRoleByNameQuery(string Name) : IQuery<RoleReadDto>;