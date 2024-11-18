namespace CustomCADs.Auth.Endpoints.SignIn;

public class SignInGroup : Group
{
    public SignInGroup()
    {
        Configure("auth/signin", ep =>
        {
            ep.AllowAnonymous();
            ep.Description(d => d.WithTags("Auth SignIn"));
        });
    }
}
