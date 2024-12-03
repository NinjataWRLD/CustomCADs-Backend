namespace CustomCADs.Orders.Endpoints.Designer.Patch.PresignedUrl;

public sealed record GetOrderPatchPresignedCadUrlResponse(
    string GeneratedCadKey,
    string PresignedCadUrl
);
