namespace CustomCADs.Catalog.Endpoints.Products.Endpoints.Gallery;

using static EndpointsConstants;

public class GalleryGroup : Group
{
	public GalleryGroup()
	{
		Configure(Paths.ProductsGallery, ep =>
		{
			ep.AllowAnonymous();
			ep.Description(d => d.WithTags(Tags[Paths.ProductsGallery]));
		});
	}
}
