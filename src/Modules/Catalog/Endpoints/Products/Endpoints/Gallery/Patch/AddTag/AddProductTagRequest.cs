namespace CustomCADs.Catalog.Endpoints.Products.Endpoints.Gallery.Patch.AddTag;

public record AddProductTagRequest(
    Guid Id,
    Guid TagId
);
