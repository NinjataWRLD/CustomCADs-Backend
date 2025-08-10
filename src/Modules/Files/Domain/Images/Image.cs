using CustomCADs.Shared.Domain.Bases.Entities;

namespace CustomCADs.Files.Domain.Images;

public class Image : BaseAggregateRoot
{
	private Image() { }
	private Image(
		string key,
		string contentType
	)
	{
		Key = key;
		ContentType = contentType;
	}

	public ImageId Id { get; set; }
	public string Key { get; private set; } = string.Empty;
	public string ContentType { get; private set; } = string.Empty;

	public static Image Create(string key, string contentType
	) => new Image(key, contentType)
		.ValidateKey()
		.ValidateContentType();

	public static Image CreateWithId(ImageId id, string key, string contentType
	) => new Image(key, contentType)
	{
		Id = id
	}
	.ValidateKey()
	.ValidateContentType();

	public Image SetKey(string key)
	{
		Key = key;
		this.ValidateKey();

		return this;
	}

	public Image SetContentType(string contentType)
	{
		ContentType = contentType;
		this.ValidateContentType();

		return this;
	}
}
