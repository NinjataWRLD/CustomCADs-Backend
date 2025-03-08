namespace CustomCADs.Customizations.Endpoints.Customizations;

public class CustomizationsGroup : Group
{
    public CustomizationsGroup()
    {
        Configure("customizations", ep =>
        {
            ep.AllowAnonymous();
            ep.Description(opt => opt.WithTags("06. Customizations"));
        });
    }
}
