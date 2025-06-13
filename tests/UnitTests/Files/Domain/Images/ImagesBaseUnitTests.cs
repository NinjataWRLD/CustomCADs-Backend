namespace CustomCADs.UnitTests.Files.Domain.Images;

using static CadsData;

public class ImagesBaseUnitTests
{
	protected static Image CreateImage(string key = ValidKey, string contentType = ValidContentType)
		=> Image.Create(key, contentType);
}
