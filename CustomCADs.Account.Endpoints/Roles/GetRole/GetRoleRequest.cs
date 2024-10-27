using FastEndpoints;

namespace CustomCADs.Account.Endpoints.Roles.GetRole;

public class GetRoleRequest
{
    [BindFrom("name")]
    public required string Name { get; set; }
}
