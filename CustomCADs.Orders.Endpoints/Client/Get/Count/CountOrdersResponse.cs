namespace CustomCADs.Orders.Endpoints.Client.Get.Count;

public record CountOrdersResponse(
    int Pending,
    int Accepted,
    int Begun,
    int Finished,
    int Reported,
    int Removed
);
