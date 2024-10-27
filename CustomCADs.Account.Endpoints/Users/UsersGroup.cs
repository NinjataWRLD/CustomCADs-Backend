using FastEndpoints;
using Microsoft.AspNetCore.Http;
using static CustomCADs.Shared.Domain.Constants;

namespace CustomCADs.Account.Endpoints.Users;

using static StatusCodes;

public class UsersGroup : Group
{
    public UsersGroup()
    {
        Configure("Users", ep => 
        {
            ep.Roles(Admin);
            ep.Description(opt =>
            {
                opt.WithTags("Users");
                opt.ProducesProblem(Status401Unauthorized);
                opt.ProducesProblem(Status403Forbidden);
                opt.ProducesProblem(Status500InternalServerError);
            });
        });
    }
}
