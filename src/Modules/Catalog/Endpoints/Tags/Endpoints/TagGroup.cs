namespace CustomCADs.Catalog.Endpoints.Tags.Endpoints;

using static Constants.Roles;
using static EndpointsConstants;

public class TagGroup : Group
{
	public TagGroup()
	{
		Configure(Paths.Tags, ep =>
		{
			ep.Roles(Admin);
			ep.Description(d => d.WithTags(Tags[Paths.Tags]));
		});
	}
}
