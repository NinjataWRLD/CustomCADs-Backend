namespace CustomCADs.Catalog.Application.Products.Common.Exceptions;

public class ProductStatusException : Exception
{
    public ProductStatusException() : base("The provided Product cannot perform the requested action.") { }
    public ProductStatusException(Guid id, string action) : base($"The Product with id: {id} cannot perform the action: {action}.") { }
    public ProductStatusException(string message) : base(message) { }
    public ProductStatusException(string message, Exception inner) : base(message, inner) { }
}
