namespace CustomCADs.Orders.Endpoints.Orders.Designer.Patch.PresignedUrl;

public sealed record GetOrderPatchPresignedCadUrlRequest(
    string OrderName,
    string ContentType,
    string FileName
);
