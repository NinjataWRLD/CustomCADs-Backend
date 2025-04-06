namespace CustomCADs.Customs.Endpoints.Customs.Endpoints.Customs.Post.Create;

public sealed record PostCustomResponse(
    Guid Id,
    string Name,
    string Description,
    string OrderedAt,
    string Status,
    bool ForDelivery
);
