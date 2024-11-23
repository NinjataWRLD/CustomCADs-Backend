namespace CustomCADs.Orders.Application.Orders.Queries.GetCadUrlPost;

public record GetOrderCadPresignedUrlPostQuery(
    string OrderName,
    string ContentType,
    string FileName
) : IQuery<GetOrderCadPresignedUrlPostDto>;
