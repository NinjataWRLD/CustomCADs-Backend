namespace CustomCADs.UnitTests.Catalog.Application.Products.Queries.Internal.Creator.GetCadUrl.Put.Data;

public class GetProductCadPresignedUrlPutInvalidContentTypeData : GetProductCadPresignedUrlPutData
{
    public GetProductCadPresignedUrlPutInvalidContentTypeData()
    {
        Add(new(null!, "Hand.glb"));
        Add(new(string.Empty, "Chair.gltf"));
    }
}
