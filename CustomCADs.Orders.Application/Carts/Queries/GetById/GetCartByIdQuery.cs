namespace CustomCADs.Orders.Application.Carts.Queries.GetById;

public record GetCartByIdQuery(CartId Id) : IQuery<GetCartByIdDto>;
