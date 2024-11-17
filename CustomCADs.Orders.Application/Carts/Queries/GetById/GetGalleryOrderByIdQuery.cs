namespace CustomCADs.Orders.Application.Carts.Queries.GetById;

public record GetGalleryOrderByIdQuery(CartId Id) : IQuery<GetCartByIdDto>;
