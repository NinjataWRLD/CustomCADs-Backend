namespace CustomCADs.Catalog.Endpoints.Tags.Put;

public record EditTagRequest(
    Guid Id,
    string Name
);
