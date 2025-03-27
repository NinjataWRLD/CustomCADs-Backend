using CustomCADs.UnitTests.Catalog.Application.Products.Queries.Internal.Shared.GetCadUrl.Put;

namespace CustomCADs.UnitTests.Catalog.Application.Products.Queries.Internal.Shared.GetCadUrl.Put.Data;

public class GetProductCadPresignedUrlPutInvalidFileNameData : GetProductCadPresignedUrlPutData
{
    public GetProductCadPresignedUrlPutInvalidFileNameData()
    {
        Add("model/gltf-binary", null!);
        Add("model/gltf+json", string.Empty);
    }
}
