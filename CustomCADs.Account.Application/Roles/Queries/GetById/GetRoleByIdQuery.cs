namespace CustomCADs.Account.Application.Roles.Queries.GetById;

public record GetRoleByIdQuery(RoleId Id) : IQuery<RoleReadDto>;