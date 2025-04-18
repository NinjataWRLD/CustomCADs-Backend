namespace CustomCADs.UnitTests.Catalog.Application.Products.Queries.Internal.Creator.GetImageUrl.Post.Data;

using static ProductsData;

public class CreatorGetProductImagePresignedUrlPostValidData : CreatorGetProductImagePresignedUrlPostData
{
    public CreatorGetProductImagePresignedUrlPostValidData()
    {
        Add(ValidName1, new("cad/jpeg", "Hand.jpg"));
        Add(ValidName2, new("cad/png", "Chair.png"));
    }
}
