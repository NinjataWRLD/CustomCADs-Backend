using FastEndpoints;

namespace CustomCADs.Auth.Endpoints.Auth;

using static StatusCodes;

public class AuthGroup : Group
{
    public AuthGroup()
    {
        Configure("Auth", ep =>
        {
            ep.AllowAnonymous();
            ep.Description(opt =>
            {
                opt.WithTags("Auth");
            });
        });
    }
}
