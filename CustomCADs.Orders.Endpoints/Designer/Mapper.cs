using CustomCADs.Orders.Endpoints.Designer.Patch.Finish;

namespace CustomCADs.Orders.Endpoints.Designer;

public static class Mapper
{
    public static (string Key, string ContentType)? ToCadDto(this FinishOrderRequest req)
        => req.CadKey is not null && req.CadContentType is not null ?
            (Key: req.CadKey, ContentType: req.CadContentType)
            : null;
}
