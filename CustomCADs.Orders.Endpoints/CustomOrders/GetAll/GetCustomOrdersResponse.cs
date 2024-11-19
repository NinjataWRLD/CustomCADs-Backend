namespace CustomCADs.Orders.Endpoints.CustomOrders.GetAll;

public record GetCustomOrdersResponse(
    int Count,
    ICollection<GetCustomOrdersDto> CustomOrders
);
