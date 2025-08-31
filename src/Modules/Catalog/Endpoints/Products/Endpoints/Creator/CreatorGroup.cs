namespace CustomCADs.Catalog.Endpoints.Products.Endpoints.Creator;

using static Constants.Roles;
using static EndpointsConstants;

public class CreatorGroup : Group
{
	public CreatorGroup()
	{
		Configure(Paths.ProductsCreator, ep =>
		{
			ep.Roles(Contributor, Designer);
			ep.Description(d => d.WithTags(Tags[Paths.ProductsCreator]));
		});
	}
}
