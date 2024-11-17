namespace CustomCADs.Orders.Application.CustomOrders.Queries.GetById;

public record GetCustomOrderByIdQuery(CustomOrderId Id) :  IQuery<GetCustomOrderByIdDto>;
