namespace CustomCADs.Customs.Endpoints.Customs.Endpoints.Client.Post.Create;

public sealed record PostCustomResponse(
    Guid Id,
    string Name,
    string Description,
    string OrderedAt,
    string Status,
    bool ForDelivery
);
