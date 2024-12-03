namespace CustomCADs.Orders.Endpoints.Orders.Designer.Patch.Finish;

public sealed record FinishOrderRequest(
    Guid Id,
    string? CadKey,
    string? CadContentType
);