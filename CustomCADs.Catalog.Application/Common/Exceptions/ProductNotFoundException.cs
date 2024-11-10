namespace CustomCADs.Catalog.Application.Common.Exceptions;

public class ProductNotFoundException : Exception
{
    private ProductNotFoundException(string message, Exception? inner) : base(message, inner) { }

    public static ProductNotFoundException General(Exception? inner = default)
        => new("The requested Product does not exist.", inner);

    public static ProductNotFoundException ById(Guid id, Exception? inner = default)
        => new($"The Product with id: {id} does not exist.", inner);

    public static ProductNotFoundException Custom(string message, Exception? inner = default)
        => new(message, inner);
}
