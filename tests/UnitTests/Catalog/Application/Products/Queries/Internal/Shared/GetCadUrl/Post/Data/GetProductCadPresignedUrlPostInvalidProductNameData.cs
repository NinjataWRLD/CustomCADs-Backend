using CustomCADs.UnitTests.Catalog.Application.Products.Queries.Internal.Shared.GetCadUrl.Post;

namespace CustomCADs.UnitTests.Catalog.Application.Products.Queries.Internal.Shared.GetCadUrl.Post.Data;

public class GetProductCadPresignedUrlPostInvalidProductNameData : GetProductCadPresignedUrlPostData
{
    public GetProductCadPresignedUrlPostInvalidProductNameData()
    {
        Add(null!, "model/gltf-binary", "Hand.glb");
        Add(string.Empty, "model/gltf+json", "Chair.gltf");
    }
}
