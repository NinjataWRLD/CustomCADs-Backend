namespace CustomCADs.Orders.Endpoints.CustomOrders.Count;

public record CountCustomOrdersResponse(
    int Pending,
    int Accepted,
    int Begun,
    int Finished,
    int Reported,
    int Removed
);
