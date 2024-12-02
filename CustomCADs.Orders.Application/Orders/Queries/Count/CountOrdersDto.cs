namespace CustomCADs.Orders.Application.Orders.Queries.Count;

public record CountOrdersDto(
    int Pending,
    int Accepted,
    int Begun,
    int Finished,
    int Completed,
    int Reported,
    int Removed
);