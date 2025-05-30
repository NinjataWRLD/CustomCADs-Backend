namespace CustomCADs.UnitTests.Catalog.Application.Products.Queries.Internal.Creator.GetCadUrl.Post.Data;

using static ProductsData;

public class CreatorGetProductCadPresignedUrlPostInvalidContentTypeData : CreatorGetProductCadPresignedUrlPostData
{
    public CreatorGetProductCadPresignedUrlPostInvalidContentTypeData()
    {
        Add(ValidName1, new(null!, "Hand.glb"));
        Add(ValidName2, new(string.Empty, "Chair.gltf"));
    }
}
