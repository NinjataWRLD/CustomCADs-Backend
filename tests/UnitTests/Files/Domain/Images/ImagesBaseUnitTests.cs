namespace CustomCADs.UnitTests.Files.Domain.Images;

using static CadsData;

public class ImagesBaseUnitTests
{
	protected static Image CreateImage(string key = ValidKey1, string contentType = ValidContentType1)
		=> Image.Create(key, contentType);
}
