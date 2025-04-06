namespace CustomCADs.Customs.Endpoints.Customs.Endpoints.Customs.Post.Create;

public sealed record PostCustomRequest(
    string Name,
    string Description,
    bool ForDelivery
);
