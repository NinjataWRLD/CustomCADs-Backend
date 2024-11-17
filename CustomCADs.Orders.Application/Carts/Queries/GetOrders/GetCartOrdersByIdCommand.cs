namespace CustomCADs.Orders.Application.Carts.Queries.GetOrders;

public record GetCartOrdersByIdCommand(CartId Id) : IQuery<ICollection<GetCartOrdersByIdDto>>;
