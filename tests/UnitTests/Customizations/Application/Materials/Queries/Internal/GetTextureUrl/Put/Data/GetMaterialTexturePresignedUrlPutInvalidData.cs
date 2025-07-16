using CustomCADs.Shared.Core.Common.Dtos;

namespace CustomCADs.UnitTests.Customizations.Application.Materials.Queries.Internal.GetTextureUrl.Put.Data;

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
