namespace CustomCADs.UnitTests.Catalog.Application.Products.Queries.Internal.Creator.GetImageUrl.Post.Data;

using static ProductsData;

public class CreatorGetProductImagePresignedUrlPostInvalidContentTypeData : CreatorGetProductImagePresignedUrlPostData
{
    public CreatorGetProductImagePresignedUrlPostInvalidContentTypeData()
    {
        Add(ValidName1, new(null!, "Hand.jpg"));
        Add(ValidName2, new(string.Empty, "Chair.png"));
    }
}
