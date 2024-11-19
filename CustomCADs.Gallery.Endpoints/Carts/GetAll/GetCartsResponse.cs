namespace CustomCADs.Gallery.Endpoints.Carts.GetAll;

public record GetCartsResponse(
    int Count,
    ICollection<GetCartsDto> Carts
);
