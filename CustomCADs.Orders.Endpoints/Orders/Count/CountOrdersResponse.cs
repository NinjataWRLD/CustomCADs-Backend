namespace CustomCADs.Orders.Endpoints.Orders.Count;

public record CountOrdersResponse(
    int Pending,
    int Accepted,
    int Begun,
    int Finished,
    int Reported,
    int Removed
);
