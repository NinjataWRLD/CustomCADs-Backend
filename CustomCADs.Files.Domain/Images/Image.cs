using CustomCADs.Files.Domain.Images.Validation;
using CustomCADs.Shared.Core.Bases.Entities;

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

    public static Image Create(
        string key,
        string contentType
    ) => new Image(key, contentType)
        .ValidateKey();

    public Image SetKey(string key)
    {
        Key = key;

        return this;
    }

    public Image SetContentType(string contentType)
    {
        Key = contentType;

        return this;
    }
}
