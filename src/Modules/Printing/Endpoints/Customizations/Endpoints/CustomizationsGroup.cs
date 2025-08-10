namespace CustomCADs.Printing.Endpoints.Customizations.Endpoints;

public class CustomizationsGroup : Group
{
	public CustomizationsGroup()
	{
		Configure("customizations", ep =>
		{
			ep.AllowAnonymous();
			ep.Description(opt => opt.WithTags("04. Customizations"));
		});
	}
}
