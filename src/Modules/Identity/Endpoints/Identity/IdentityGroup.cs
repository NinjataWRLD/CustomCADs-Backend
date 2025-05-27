namespace CustomCADs.Identity.Endpoints.Identity;

public class IdentityGroup : Group
{
	public IdentityGroup()
	{
		Configure("identity", ep =>
		{
			ep.AllowAnonymous();
			ep.Description(opt => opt.WithTags("01. Identity"));
		});
	}
}
