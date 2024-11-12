using CustomCADs.Shared.Core.Common.Exceptions;

namespace CustomCADs.Account.Domain.Common.Exceptions;

public class RoleValidationException : BaseException
{
    private RoleValidationException(string message, Exception? inner) : base(message, inner) { }

    public static RoleValidationException General(Exception? inner = default)
        => new("There was a validation error while working with a Role.", inner);
    
    public static RoleValidationException NotNull(string property, Exception? inner = default)
        => new($"A Role must have a non-null {property}.", inner);
    
    public static RoleValidationException Length(string property, int max, int min, Exception? inner = default)
        => new($"A Role's {property} must be shorter than {min} and longer than {max} characters.", inner);

    public static RoleValidationException Custom(string message, Exception? inner = default)
        => new(message, inner);
}
