namespace CustomCADs.UnitTests.Catalog.Application.Products.Queries.Internal.Creator.GetCadUrl.Post.Data;

public class GetProductCadPresignedUrlPostInvalidProductNameData : GetProductCadPresignedUrlPostData
{
    public GetProductCadPresignedUrlPostInvalidProductNameData()
    {
        Add(null!, new("model/gltf-binary", "Hand.glb"));
        Add(string.Empty, new("model/gltf+json", "Chair.gltf"));
    }
}
