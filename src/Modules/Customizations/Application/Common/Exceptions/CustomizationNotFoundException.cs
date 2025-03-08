using CustomCADs.Shared.Core.Bases.Exceptions;

namespace CustomCADs.Customizations.Application.Common.Exceptions;

using static Constants.ExceptionMessages;

public class CustomizationNotFoundException : BaseException
{
    private CustomizationNotFoundException(string message, Exception? inner) : base(message, inner) { }

    public static CustomizationNotFoundException General(Exception? inner = default)
        => new(string.Format(NotFound, "Customization"), inner);

    public static CustomizationNotFoundException ById(CustomizationId id, Exception? inner = default)
        => new(string.Format(NotFoundByProp, "Customization", nameof(id), id), inner);

    public static CustomizationNotFoundException Custom(string message, Exception? inner = default)
        => new(message, inner);
}
