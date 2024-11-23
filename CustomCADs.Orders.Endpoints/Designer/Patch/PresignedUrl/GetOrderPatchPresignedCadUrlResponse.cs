namespace CustomCADs.Orders.Endpoints.Designer.Patch.PresignedUrl;

public record GetOrderPatchPresignedCadUrlResponse(
    string GeneratedCadKey,
    string PresignedCadUrl
);
