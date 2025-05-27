namespace CustomCADs.UnitTests.Files.Application.Images.Queries.Shared.GetPresignedUrlPut.Data;

using static ImagesData;

public class GetImagePresignedUrlPutByIdValidData : GetImagePresignedUrlPutByIdData
{
	public GetImagePresignedUrlPutByIdValidData()
	{
		Add(new(ValidContentType1, ValidFileName1));
		Add(new(ValidContentType2, ValidFileName2));
	}
}
