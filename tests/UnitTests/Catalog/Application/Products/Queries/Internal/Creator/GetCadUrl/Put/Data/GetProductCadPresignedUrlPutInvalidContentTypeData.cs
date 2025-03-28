using CustomCADs.UnitTests.Catalog.Application.Products.Queries.Internal.Creator.GetCadUrl.Put;

namespace CustomCADs.UnitTests.Catalog.Application.Products.Queries.Internal.Creator.GetCadUrl.Put.Data;

public class GetProductCadPresignedUrlPutInvalidContentTypeData : GetProductCadPresignedUrlPutData
{
    public GetProductCadPresignedUrlPutInvalidContentTypeData()
    {
        Add(null!, "Hand.glb");
        Add(string.Empty, "Chair.gltf");
    }
}
