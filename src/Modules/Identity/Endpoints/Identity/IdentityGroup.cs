namespace CustomCADs.Identity.Endpoints.Identity;

using static EndpointsConstants;

public class IdentityGroup : Group
{
	public IdentityGroup()
	{
		Configure(Paths.Identity, ep =>
		{
			ep.AllowAnonymous();
			ep.Description(opt => opt.WithTags(Tags[Paths.Identity]));
		});
	}
}
