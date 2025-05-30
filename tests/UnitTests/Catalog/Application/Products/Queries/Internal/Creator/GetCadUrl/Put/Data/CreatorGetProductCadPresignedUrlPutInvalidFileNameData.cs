namespace CustomCADs.UnitTests.Catalog.Application.Products.Queries.Internal.Creator.GetCadUrl.Put.Data;

public class CreatorGetProductCadPresignedUrlPutInvalidFileNameData : CreatorGetProductCadPresignedUrlPutData
{
    public CreatorGetProductCadPresignedUrlPutInvalidFileNameData()
    {
        Add(new("model/gltf-binary", null!));
        Add(new("model/gltf+json", string.Empty));
    }
}
