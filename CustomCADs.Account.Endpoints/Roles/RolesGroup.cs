﻿namespace CustomCADs.Account.Endpoints.Roles;

using static Constants.Roles;
using static StatusCodes;

public class RolesGroup : Group
{
    public RolesGroup()
    {
        Configure("roles", ep =>
        {
            ep.Roles(Admin);
            ep.Description(opt =>
            {
                opt.WithTags("Roles");
                opt.ProducesProblem(Status401Unauthorized);
                opt.ProducesProblem(Status403Forbidden);
                opt.ProducesProblem(Status500InternalServerError);
            });
        });
    }
}
