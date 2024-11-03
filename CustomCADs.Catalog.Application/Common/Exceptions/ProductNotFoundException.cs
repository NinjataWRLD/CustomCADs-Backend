namespace CustomCADs.Catalog.Application.Common.Exceptions;

public class ProductNotFoundException : Exception
{
    public ProductNotFoundException() : base("The requested Product does not exist.") { }
    public ProductNotFoundException(Guid id) : base($"The Product with id: {id} does not exist.") { }
    public ProductNotFoundException(string message) : base(message) { }
    public ProductNotFoundException(string message, Exception inner) : base(message, inner) { }
}
