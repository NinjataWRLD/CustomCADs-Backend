namespace CustomCADs.Orders.Application.Orders.Queries.GetImageUrlPost;

public record GetOrderImagePresignedUrlPostQuery(
    string OrderName,
    string ContentType,
    string FileName
) : IQuery<GetOrderImagePresignedUrlPostDto>;