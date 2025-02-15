namespace CustomCADs.Catalog.Endpoints.Products.Gallery.Patch.AddTag;

public record AddProductTagRequest(
    Guid Id,
    Guid TagId
);
