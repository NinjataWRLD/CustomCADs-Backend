using CustomCADs.Files.Domain.Common.Exceptions.Images;

namespace CustomCADs.Files.Domain.Images.Validation;

public static class ImageValidations
{
    public static Image ValidateKey(this Image image)
    {
        string property = "Key";
        string key = image.Key;

        if (string.IsNullOrEmpty(key))
        {
            throw ImageValidationException.NotNull(property);
        }

        return image;
    }

    public static Image ValidateContentType(this Image image)
    {
        string property = "ContentType";
        string contentType = image.ContentType;

        if (string.IsNullOrEmpty(contentType))
        {
            throw ImageValidationException.NotNull(property);
        }

        return image;
    }
}
