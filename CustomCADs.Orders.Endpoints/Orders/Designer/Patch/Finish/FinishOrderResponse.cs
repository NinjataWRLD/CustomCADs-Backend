namespace CustomCADs.Orders.Endpoints.Orders.Designer.Patch.Finish;

public record FinishOrderResponse(
    string PresignedKey,
    string GeneratedUrl
);