using CustomCADs.Shared.Core;
using CustomCADs.Shared.Core.Bases.Exceptions;
using CustomCADs.Shared.Core.Common.TypedIds.Files;

namespace CustomCADs.Files.Application.Common.Exceptions;

using static Constants.ExceptionMessages;

public class ImageNotFoundException : BaseException
{
    private ImageNotFoundException(string message, Exception? inner) : base(message, inner) { }

    public static ImageNotFoundException General(Exception? inner = default)
        => new(string.Format(NotFound, "Image"), inner);

    public static ImageNotFoundException ById(ImageId id, Exception? inner = default)
        => new(string.Format(NotFoundByProp, "Image", nameof(id), id), inner);

    public static ImageNotFoundException Custom(string message, Exception? inner = default)
        => new(message, inner);
}