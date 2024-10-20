namespace CustomCADs.Catalog.Presentation.Products.Endpoints.PatchProduct;

public class PatchCadRequest
{
    public Guid Id { get; set; }
    public required string Type { get; set; }
    public required CoordinatesDto Coordinates { get; set; }
}
