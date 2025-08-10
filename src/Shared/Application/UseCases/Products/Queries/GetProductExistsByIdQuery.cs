namespace CustomCADs.Shared.Application.UseCases.Products.Queries;

public record GetProductExistsByIdQuery(ProductId Id) : IQuery<bool>;
