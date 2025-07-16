using CustomCADs.Shared.Core.Common.TypedIds.Files;

namespace CustomCADs.UnitTests.Files.Data;

public class ImagesData
{
	public const string ValidKey = "key-to-image";
	public const string InvalidKey = "";

	public const string ValidContentType = "image/jpeg";
	public const string InvalidContentType = "";

	public static readonly ImageId ValidId = ImageId.New();
}
