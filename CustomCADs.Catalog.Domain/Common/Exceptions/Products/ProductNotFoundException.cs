using CustomCADs.Shared.Core.Common.Exceptions;
using CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Cads;

namespace CustomCADs.Catalog.Domain.Common.Exceptions.Products;

public class ProductCadException : BaseException
{
    private ProductCadException(string message, Exception? inner) : base(message, inner) { }

    public static ProductCadException General(Exception? inner = default)
        => new("There was an error with requested Product's Cad.", inner);

    public static ProductCadException Null(ProductId id, Exception? inner = default)
        => new($"The Product with id: {id} has a null CadId Foreign Key.", inner);

    public static ProductCadException Custom(string message, Exception? inner = default)
        => new(message, inner);
}
