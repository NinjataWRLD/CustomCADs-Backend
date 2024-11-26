namespace CustomCADs.Orders.Application.Orders.Queries.GetById;

public record GetOrderByIdQuery(OrderId Id) : IQuery<GetOrderByIdDto>;
