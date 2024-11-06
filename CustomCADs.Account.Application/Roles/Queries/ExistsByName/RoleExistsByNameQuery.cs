namespace CustomCADs.Account.Application.Roles.Queries.ExistsByName;

public record RoleExistsByNameQuery(string Name) : IQuery<bool>;
