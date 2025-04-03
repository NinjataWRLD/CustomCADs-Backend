namespace CustomCADs.Customs.Endpoints.Customs.Endpoints.Client.Put;

public sealed record PutCustomRequest(
    Guid Id,
    string Name,
    string Description
);
