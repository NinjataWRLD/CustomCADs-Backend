namespace CustomCADs.Customs.Endpoints.Customs.Endpoints.Customs.Put;

public sealed record PutCustomRequest(
    Guid Id,
    string Name,
    string Description
);
