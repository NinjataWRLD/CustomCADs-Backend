namespace CustomCADs.UnitTests.Files.Application.Images.SharedQueries.GetPresignedUrlPut.Data;

using static ImagesData;

public class GetImagePresignedUrlPutByIdValidData : GetImagePresignedUrlPutByIdData
{
    public GetImagePresignedUrlPutByIdValidData()
    {
        Add(ValidContentType1, ValidFileName1);
        Add(ValidContentType2, ValidFileName2);
    }
}
