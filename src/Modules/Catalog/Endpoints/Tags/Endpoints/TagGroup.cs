namespace CustomCADs.Catalog.Endpoints.Tags.Endpoints;

using static Constants.Roles;

public class TagGroup : Group
{
	public TagGroup()
	{
		Configure("tags", ep =>
		{
			ep.Roles(Admin);
			ep.Description(d => d.WithTags("17. Tags Dashboard"));
		});
	}
}
