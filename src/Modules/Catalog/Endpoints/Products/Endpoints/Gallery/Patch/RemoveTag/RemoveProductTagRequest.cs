namespace CustomCADs.Catalog.Endpoints.Products.Endpoints.Gallery.Patch.RemoveTag;

public record RemoveProductTagRequest(
    Guid Id,
    Guid TagId
);
