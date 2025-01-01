namespace CustomCADs.UnitTests.Files.Application.Images.SharedQueries.GetPresignedUrlPut.Data;

using static ImagesData;

public class GetImagePresignedUrlPutByIdHandlerValidData : GetImagePresignedUrlPutByIdHandlerData
{
    public GetImagePresignedUrlPutByIdHandlerValidData()
    {
        Add(ValidContentType1, ValidFileName1);
        Add(ValidContentType2, ValidFileName2);
    }
}
