using CustomCADs.Shared.Core.Common.Exceptions;
using CustomCADs.Shared.Core.Domain.ValueObjects.Ids;

namespace CustomCADs.Catalog.Domain.Common.Exceptions;

public class CategoryNotFoundException : BaseException
{
    private CategoryNotFoundException(string message, Exception? inner) : base(message, inner) { }

    public static CategoryNotFoundException General(Exception? inner = default)
        => new("The requested Category does not exist.", inner);

    public static CategoryNotFoundException ById(CategoryId id, Exception? inner = default)
        => new($"The Category with id: {id} does not exist.", inner);

    public static CategoryNotFoundException ByName(string name, Exception? inner = default)
        => new($"The Category with name: {name} does not exist.", inner);

    public static CategoryNotFoundException Custom(string message, Exception? inner = default)
        => new(message, inner);
}
