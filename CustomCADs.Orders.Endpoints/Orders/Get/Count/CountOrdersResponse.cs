namespace CustomCADs.Orders.Endpoints.Orders.Get.Count;

public record CountOrdersResponse(
    int Pending,
    int Accepted,
    int Begun,
    int Finished,
    int Reported,
    int Removed
);
