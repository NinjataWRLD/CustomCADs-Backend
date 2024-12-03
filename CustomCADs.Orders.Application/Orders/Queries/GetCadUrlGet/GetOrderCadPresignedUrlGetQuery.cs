namespace CustomCADs.Orders.Application.Orders.Queries.GetCadUrlGet;

public sealed record GetOrderCadPresignedUrlGetQuery(OrderId Id)
    : IQuery<GetOrderCadPresignedUrlGetDto>;
