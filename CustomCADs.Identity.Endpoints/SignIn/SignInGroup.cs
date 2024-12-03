namespace CustomCADs.Identity.Endpoints.SignIn;

public class SignInGroup : Group
{
    public SignInGroup()
    {
        Configure("identity/signin", ep =>
        {
            ep.AllowAnonymous();
            ep.Description(d => d.WithTags("03. Sign-in Operations"));
        });
    }
}
