using CustomCADs.Shared.Core;
using CustomCADs.Shared.Core.Bases.Exceptions;

namespace CustomCADs.Files.Domain.Images.Exceptions;

using static Constants.ExceptionMessages;

public class ImageValidationException : BaseException
{
    private ImageValidationException(string message, Exception? inner) : base(message, inner) { }

    public static ImageValidationException General(Exception? inner = default)
        => new(string.Format(Validation, "an", "Image"), inner);

    public static ImageValidationException NotNull(string property, Exception? inner = default)
        => new(string.Format(NonNullValidation, "An", "Image", property), inner);

    public static ImageValidationException Range(string property, int max, int min, Exception? inner = default)
        => new(string.Format(RangeValidation, "An", "Image", property, min, max), inner);

    public static ImageValidationException Custom(string message, Exception? inner = default)
        => new(message, inner);
}
