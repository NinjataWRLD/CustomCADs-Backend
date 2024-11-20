namespace CustomCADs.Gallery.Application.Carts.Queries.GetItems;

public record GetCartItemsByIdCommand(CartId Id) : IQuery<ICollection<GetCartItemsByIdDto>>;
