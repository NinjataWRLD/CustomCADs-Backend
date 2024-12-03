namespace CustomCADs.Orders.Endpoints.Designer.Patch.Finish;

public sealed record FinishOrderRequest(
    Guid Id,
    string? CadKey,
    string? CadContentType
);