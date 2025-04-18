namespace CustomCADs.UnitTests.Catalog.Application.Products.Queries.Internal.Creator.GetCadUrl.Put.Data;

public class CreatorGetProductCadPresignedUrlPutValidData : CreatorGetProductCadPresignedUrlPutData
{
    public CreatorGetProductCadPresignedUrlPutValidData()
    {
        Add(new("model/gltf-binary", "Hand.glb"));
        Add(new("model/gltf+json", "Chair.gltf"));
    }
}
