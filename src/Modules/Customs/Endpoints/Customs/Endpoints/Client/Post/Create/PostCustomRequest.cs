namespace CustomCADs.Customs.Endpoints.Customs.Endpoints.Client.Post.Create;

public sealed record PostCustomRequest(
    string Name,
    string Description,
    bool ForDelivery
);
