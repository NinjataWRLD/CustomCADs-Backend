﻿namespace CustomCADs.Account.Endpoints.Users;

using static Constants.Roles;
using static StatusCodes;

public class UsersGroup : Group
{
    public UsersGroup()
    {
        Configure("users", ep =>
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
