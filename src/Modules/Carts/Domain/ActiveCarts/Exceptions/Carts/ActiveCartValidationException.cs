using CustomCADs.Shared.Core;
using CustomCADs.Shared.Core.Bases.Exceptions;

namespace CustomCADs.Carts.Domain.ActiveCarts.Exceptions.Carts;

using static Constants.ExceptionMessages;

public class ActiveCartValidationException : BaseException
{
    private ActiveCartValidationException(string message, Exception? inner) : base(message, inner) { }

    public static ActiveCartValidationException General(Exception? inner = default)
        => new(string.Format(Validation, "an", "Active Cart"), inner);

    public static ActiveCartValidationException Range<TType>(string property, TType max, TType min, Exception? inner = default) where TType : struct
        => new(string.Format(RangeValidation, "An", "Active Cart", property, min, max), inner);

    public static ActiveCartValidationException Custom(string message, Exception? inner = default)
        => new(message, inner);
}
