namespace CustomCADs.Account.Application.Roles.Queries.GetByName;

public record GetRoleByNameQuery(string Name) : IQuery<RoleReadDto>;