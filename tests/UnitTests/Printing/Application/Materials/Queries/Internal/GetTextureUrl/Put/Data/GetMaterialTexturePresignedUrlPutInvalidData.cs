using CustomCADs.Shared.Application.Dtos.Files;

namespace CustomCADs.UnitTests.Printing.Application.Materials.Queries.Internal.GetTextureUrl.Put.Data;

public class GetMaterialTexturePresignedUrlPutInvalidData : TheoryData<UploadFileRequest>
{
	private const string ValidContentType = "content-type";
	private const string ValidFileName = "file-name";

	public GetMaterialTexturePresignedUrlPutInvalidData()
	{
		// Content Type
		Add(new(string.Empty, ValidFileName));

		// File Name
		Add(new(ValidContentType, string.Empty));
	}
}
