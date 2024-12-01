namespace CustomCADs.Gallery.Endpoints.Carts.Get.Count;

public record GetCartsCountResponse(
    int TotalCount,
    Dictionary<Guid, int> Counts
);
