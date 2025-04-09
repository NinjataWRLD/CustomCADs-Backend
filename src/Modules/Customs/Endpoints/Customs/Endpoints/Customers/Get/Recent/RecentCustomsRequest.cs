namespace CustomCADs.Customs.Endpoints.Customs.Endpoints.Customers.Get.Recent;

public sealed record RecentCustomsRequest(
    int Limit = 5
);
