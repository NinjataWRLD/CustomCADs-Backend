namespace CustomCADs.Catalog.Endpoints.Products.Gallery.Patch.RemoveTag;

public record RemoveProductTagRequest(
    Guid Id,
    Guid TagId
);
