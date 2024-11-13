using CustomCADs.Shared.Core.Common.Exceptions;

namespace CustomCADs.Orders.Domain.Common.Exceptions.Cads;

public class CadValidationException : BaseException
{
    private CadValidationException(string message, Exception? inner) : base(message, inner) { }

    public static CadValidationException General(Exception? inner = default)
        => new("There was a validation error while working with a Cad.", inner);

    public static CadValidationException Custom(string message, Exception? inner = default)
        => new(message, inner);
}
