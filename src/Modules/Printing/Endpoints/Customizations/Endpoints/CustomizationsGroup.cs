namespace CustomCADs.Printing.Endpoints.Customizations.Endpoints;

using static EndpointsConstants;

public class CustomizationsGroup : Group
{
	public CustomizationsGroup()
	{
		Configure(Paths.Customizations, ep =>
		{
			ep.AllowAnonymous();
			ep.Description(opt => opt.WithTags(Tags[Paths.Customizations]));
		});
	}
}
