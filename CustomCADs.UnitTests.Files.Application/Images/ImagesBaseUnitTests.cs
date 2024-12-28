using CustomCADs.Shared.Core.Common.TypedIds.Files;

namespace CustomCADs.UnitTests.Files.Application.Images;

public class ImagesBaseUnitTests
{
    protected const string ValidKey1 = "ProductName1_gibberish";
    protected const string ValidKey2 = "ProductName2_gibberish";
    protected const string InvalidKey = "";

    protected const string ValidContentType1 = "image/jpeg";
    protected const string ValidContentType2 = "image/png";
    protected const string InvalidContentType = "";

    protected static readonly ImageId id = new(Guid.Parse("e3f5e3f5-e3f5-e3f5-e3f5-e3f5e3f5e3f5"));
    protected static readonly CancellationToken ct = CancellationToken.None;

    protected static Image CreateImage(string key = ValidKey1, string contentType = ValidContentType1)
        => Image.Create(key, contentType);
}
