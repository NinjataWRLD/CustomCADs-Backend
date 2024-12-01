using CustomCADs.Shared.Core.Bases.Exceptions;

namespace CustomCADs.Inventory.Application.Products.Exceptions;

public class ProductAuthorizationException : BaseException
{
    private ProductAuthorizationException(string message, Exception? inner) : base(message, inner) { }

    public static ProductAuthorizationException General(Exception? inner = null)
        => new("Cannot modify another Creator's Products.", inner);

    public static ProductAuthorizationException ByProductId(ProductId id, Exception? inner = null)
        => new($"Cannot modify another Creator's Product: {id}.", inner);

    public static ProductAuthorizationException AlreadyChecked(Exception? inner = null)
        => new("A Designer has already checked this product", inner);

    public static ProductAuthorizationException Custom(string message, Exception? inner = null)
        => new(message, inner);
}
