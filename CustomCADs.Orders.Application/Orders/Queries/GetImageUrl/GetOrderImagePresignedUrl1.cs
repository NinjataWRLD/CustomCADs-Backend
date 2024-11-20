namespace CustomCADs.Orders.Application.Orders.Queries.GetImageUrl;

public record GetOrderImagePresignedUrlQuery(
    string OrderName,
    string ContentType,
    string FileName
) : IQuery<GetOrderImagePresignedUrlDto>;