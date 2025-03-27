using CustomCADs.UnitTests.Catalog.Application.Products.Queries.Internal.Shared.GetCadUrl.Put;

namespace CustomCADs.UnitTests.Catalog.Application.Products.Queries.Internal.Shared.GetCadUrl.Put.Data;

public class GetProductCadPresignedUrlPutInvalidContentTypeData : GetProductCadPresignedUrlPutData
{
    public GetProductCadPresignedUrlPutInvalidContentTypeData()
    {
        Add(null!, "Hand.glb");
        Add(string.Empty, "Chair.gltf");
    }
}
