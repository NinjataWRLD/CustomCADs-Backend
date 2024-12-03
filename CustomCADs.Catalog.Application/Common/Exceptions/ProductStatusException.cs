using CustomCADs.Shared.Core.Bases.Exceptions;

namespace CustomCADs.Catalog.Application.Common.Exceptions;

public class ProductStatusException : BaseException
{
    private ProductStatusException(string message, Exception? inner) : base(message, inner) { }

    public static ProductStatusException General(Exception? inner = null)
        => new("An error occurred while processing the product status.", inner);

    public static ProductStatusException MustBeValidated(ProductId id, Exception? inner = null)
        => new($"The product with id: {id} must have a validated status to execute this operation.", inner);

    public static ProductStatusException Custom(string message, Exception? inner = null)
        => new(message, inner);
}
