namespace CustomCADs.Orders.Endpoints.Orders.GetAll;

public record GetOrdersResponse(
    int Count,
    ICollection<GetOrdersDto> Orders
);
