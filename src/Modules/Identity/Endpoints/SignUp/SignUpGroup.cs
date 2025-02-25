namespace CustomCADs.Identity.Endpoints.SignUp;

public class SignUpGroup : Group
{
    public SignUpGroup()
    {
        Configure("identity/signup", ep =>
        {
            ep.AllowAnonymous();
            ep.Description(d => d.WithTags("02. Sign-up Operations"));
        });
    }
}
