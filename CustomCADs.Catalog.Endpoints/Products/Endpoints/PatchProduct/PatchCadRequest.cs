namespace CustomCADs.Catalog.Endpoints.Products.Endpoints.PatchProduct;

public class PatchCadRequest
{
    public Guid Id { get; set; }
    public required string Type { get; set; }
    public required CoordinatesDto Coordinates { get; set; }
}
