namespace CustomCADs.Orders.Application.Orders.Queries.GetCadUrlPost;

public sealed record GetOrderCadPresignedUrlPostQuery(
    string OrderName,
    string ContentType,
    string FileName
) : IQuery<GetOrderCadPresignedUrlPostDto>;
