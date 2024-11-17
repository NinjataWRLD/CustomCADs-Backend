using CustomCADs.Shared.Core;
using CustomCADs.Shared.Core.Common.Exceptions;

namespace CustomCADs.Catalog.Domain.Common.Exceptions.Products;

using static Constants.ExceptionMessages;

public class ProductValidationException : BaseException
{
    private ProductValidationException(string message, Exception? inner) : base(message, inner) { }

    public static ProductValidationException General(Exception? inner = default)
        => new(string.Format(Validation, "a", "Product"), inner);

    public static ProductValidationException NotNull(string property, Exception? inner = default)
        => new(string.Format(NonNullValidation, "a", "Product", property), inner);
    
    public static ProductValidationException Length(string property, int max, int min, Exception? inner = default)
        => new(string.Format(LengthValidation, "a", "Product", property, min, max), inner);

    public static ProductValidationException Range<TType>(string property, TType max, TType min, Exception? inner = default) where TType : struct
        => new(string.Format(RangeValidation, "a", "Product", property, min, max), inner);

    public static ProductValidationException Custom(string message, Exception? inner = default)
        => new(message, inner);

    public static ProductValidationException CadNotNull(ProductId id, Exception? inner = default)
        => Custom($"The Product with id: {id} has a null CadId Foreign Key.", inner);

    public static ProductValidationException InvalidStatus(ProductId id, string status, Exception? inner = default)
        => Custom($"The Product with id: {id} cannot have a status: {status}.", inner);
}
