using CustomCADs.Shared.Domain.TypedIds.Files;

namespace CustomCADs.UnitTests.Files.Domain.Images;

using static ImagesData;

public class ImagesBaseUnitTests
{
	protected static Image CreateImage(string key = ValidKey, string contentType = ValidContentType)
		=> Image.Create(key, contentType);

	protected static Image CreateImageWithId(ImageId? id = null, string key = ValidKey, string contentType = ValidContentType)
		=> Image.CreateWithId(id ?? ImageId.New(), key, contentType);
}
