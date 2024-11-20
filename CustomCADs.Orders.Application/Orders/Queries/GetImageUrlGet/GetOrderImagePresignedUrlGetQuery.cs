namespace CustomCADs.Orders.Application.Orders.Queries.GetImageUrlGet;

public record GetOrderImagePresignedUrlGetQuery(
    string ImageKey,
    string ImageContentType
) : IQuery<GetOrderImagePresignedUrlGetDto>;
