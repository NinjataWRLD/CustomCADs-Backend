namespace CustomCADs.Identity.Endpoints.Info;

public class InfoGroup : Group
{
    public InfoGroup()
    {
        Configure("identity/info", ep =>
        {
            ep.AllowAnonymous();
            ep.Description(opt => opt.WithTags("01. Auth Information"));
        });
    }
}
