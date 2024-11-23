namespace CustomCADs.Orders.Application.Orders.Queries.GetImageUrlGet;

public record GetOrderImagePresignedUrlGetQuery(OrderId Id)
 : IQuery<GetOrderImagePresignedUrlGetDto>;
