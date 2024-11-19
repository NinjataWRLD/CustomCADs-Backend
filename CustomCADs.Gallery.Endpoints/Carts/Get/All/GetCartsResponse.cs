namespace CustomCADs.Gallery.Endpoints.Carts.Get.All;

public record GetCartsResponse(
    int Count,
    ICollection<GetCartsDto> Carts
);
