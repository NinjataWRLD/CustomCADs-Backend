namespace CustomCADs.Orders.Application.Orders.Queries.GetCadUrlGet;

public record GetOrderCadPresignedUrlGetQuery(OrderId Id)
    : IQuery<GetOrderCadPresignedUrlGetDto>;
