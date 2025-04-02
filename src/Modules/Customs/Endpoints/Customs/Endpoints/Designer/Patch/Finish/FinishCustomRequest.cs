namespace CustomCADs.Customs.Endpoints.Customs.Endpoints.Designer.Patch.Finish;

public sealed record FinishCustomRequest(
    Guid Id,
    decimal Price,
    string CadKey,
    string CadContentType,
    decimal CadVolume
);