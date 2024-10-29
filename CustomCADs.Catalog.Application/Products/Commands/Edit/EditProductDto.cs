namespace CustomCADs.Catalog.Application.Products.Commands.Edit;

public record EditProductDto(
    string Name,
    string Description,
    int CategoryId,
    decimal Cost
);
