namespace CustomCADs.Customs.Endpoints.Customs.Endpoints.Client.Get.Recent;

public sealed record RecentCustomsResponse(
    Guid Id,
    string Name,
    string OrderedAt,
    string? DesignerName
);
