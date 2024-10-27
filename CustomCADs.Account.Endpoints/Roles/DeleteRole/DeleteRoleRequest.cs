using FastEndpoints;

namespace CustomCADs.Account.Endpoints.Roles.DeleteRole;

public class DeleteRoleRequest
{
    [BindFrom("name")]
    public required string Name { get; set; }
}
