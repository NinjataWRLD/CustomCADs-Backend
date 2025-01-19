namespace CustomCADs.UnitTests.Catalog.Application.Products.Queries.GetCadUrlPut.Data;

public class GetProductCadPresignedUrlPutInvalidFileNameData : GetProductCadPresignedUrlPutData
{
    public GetProductCadPresignedUrlPutInvalidFileNameData()
    {
        Add("model/gltf-binary", null!);
        Add("model/gltf+json", string.Empty);
    }
}
