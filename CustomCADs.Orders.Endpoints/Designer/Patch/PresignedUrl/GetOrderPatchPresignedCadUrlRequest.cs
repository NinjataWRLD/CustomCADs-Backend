namespace CustomCADs.Orders.Endpoints.Designer.Patch.PresignedUrl;

public sealed record GetOrderPatchPresignedCadUrlRequest(
    string OrderName,
    string ContentType,
    string FileName
);
