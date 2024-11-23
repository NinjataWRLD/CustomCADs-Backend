namespace CustomCADs.Orders.Endpoints.Designer.Patch.Finish;

public record FinishOrderRequest(
    Guid Id,
    string? CadKey,
    string? CadContentType
);