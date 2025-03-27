namespace CustomCADs.Orders.Application.OngoingOrders.Queries.Internal.Count;

public record CountOngoingOrdersDto(
    int Pending,
    int Accepted,
    int Begun,
    int Finished,
    int Reported,
    int Removed
);