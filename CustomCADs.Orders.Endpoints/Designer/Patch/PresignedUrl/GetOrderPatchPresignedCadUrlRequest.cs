namespace CustomCADs.Orders.Endpoints.Designer.Patch.PresignedUrl;

public record GetOrderPatchPresignedCadUrlRequest(
    string OrderName,
    string ContentType,
    string FileName
);
