namespace CustomCADs.Orders.Endpoints.Carts.GetAll;

public record GetCartsResponse(
    int Count,
    ICollection<GetCartsDto> Carts
);
