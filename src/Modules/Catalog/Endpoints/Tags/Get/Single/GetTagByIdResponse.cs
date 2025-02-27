namespace CustomCADs.Catalog.Endpoints.Tags.Get.Single;

public record GetTagByIdResponse(
    Guid Id,
    string Name
);
