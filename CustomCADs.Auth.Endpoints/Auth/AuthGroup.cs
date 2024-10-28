using FastEndpoints;
using Microsoft.AspNetCore.Http;

namespace CustomCADs.Auth.Endpoints.Auth;

public class AuthGroup : Group
{
    public AuthGroup()
    {
        Configure("Auth", ep =>
        {
            ep.Description(opt =>
            {
                opt.WithTags("Auth");
            });
        });
    }
}
