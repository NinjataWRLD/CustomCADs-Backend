namespace CustomCADs.UnitTests.Files.Application.Images.Queries.Shared.GetPresignedUrlPut.Data;

using CustomCADs.UnitTests.Files.Application.Images.Queries.Shared.GetPresignedUrlPut;
using static ImagesData;

public class GetImagePresignedUrlPutByIdValidData : GetImagePresignedUrlPutByIdData
{
    public GetImagePresignedUrlPutByIdValidData()
    {
        Add(ValidContentType1, ValidFileName1);
        Add(ValidContentType2, ValidFileName2);
    }
}
