namespace CustomCADs.Orders.Endpoints.Orders.Designer.Patch.PresignedUrl;

public sealed record GetOrderPatchPresignedCadUrlResponse(
    string GeneratedCadKey,
    string PresignedCadUrl
);
