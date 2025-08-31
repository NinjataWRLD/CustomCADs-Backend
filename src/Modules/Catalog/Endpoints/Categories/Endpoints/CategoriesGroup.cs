namespace CustomCADs.Catalog.Endpoints.Categories.Endpoints;

using static Constants.Roles;
using static EndpointsConstants;

public class CategoriesGroup : Group
{
	public CategoriesGroup()
	{
		Configure(Paths.Categories, ep =>
		{
			ep.Roles(Admin);
			ep.Description(opt => opt.WithTags(Tags[Paths.Categories]));
		});
	}
}
