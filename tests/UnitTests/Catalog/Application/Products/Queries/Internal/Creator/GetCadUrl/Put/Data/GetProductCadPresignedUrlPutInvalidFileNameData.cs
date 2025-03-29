using CustomCADs.UnitTests.Catalog.Application.Products.Queries.Internal.Creator.GetCadUrl.Put;

namespace CustomCADs.UnitTests.Catalog.Application.Products.Queries.Internal.Creator.GetCadUrl.Put.Data;

public class GetProductCadPresignedUrlPutInvalidFileNameData : GetProductCadPresignedUrlPutData
{
    public GetProductCadPresignedUrlPutInvalidFileNameData()
    {
        Add("model/gltf-binary", null!);
        Add("model/gltf+json", string.Empty);
    }
}
