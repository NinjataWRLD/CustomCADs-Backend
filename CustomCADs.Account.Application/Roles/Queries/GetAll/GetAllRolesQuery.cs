namespace CustomCADs.Account.Application.Roles.Queries.GetAll;

public record GetAllRolesQuery(
    string? Name = null,
    string? Description = null,
    string Sorting = "");
