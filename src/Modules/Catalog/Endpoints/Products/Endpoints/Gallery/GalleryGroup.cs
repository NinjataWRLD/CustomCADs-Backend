namespace CustomCADs.Catalog.Endpoints.Products.Endpoints.Gallery;

public class GalleryGroup : Group
{
    public GalleryGroup()
    {
        Configure("products/gallery", ep =>
        {
            ep.AllowAnonymous();
            ep.Description(d => d.WithTags("02. Gallery"));
        });
    }
}
