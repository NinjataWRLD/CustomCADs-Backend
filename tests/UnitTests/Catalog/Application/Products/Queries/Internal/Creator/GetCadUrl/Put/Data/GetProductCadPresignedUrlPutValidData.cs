namespace CustomCADs.UnitTests.Catalog.Application.Products.Queries.Internal.Creator.GetCadUrl.Put.Data;

public class GetProductCadPresignedUrlPutValidData : GetProductCadPresignedUrlPutData
{
    public GetProductCadPresignedUrlPutValidData()
    {
        Add(new("model/gltf-binary", "Hand.glb"));
        Add(new("model/gltf+json", "Chair.gltf"));
    }
}
