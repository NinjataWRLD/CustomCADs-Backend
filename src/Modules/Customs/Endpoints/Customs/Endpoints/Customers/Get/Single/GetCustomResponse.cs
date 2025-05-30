using CustomCADs.Customs.Endpoints.Customs.Dtos;

namespace CustomCADs.Customs.Endpoints.Customs.Endpoints.Customers.Get.Single;

public sealed record GetCustomResponse(
	Guid Id,
	string Name,
	string Description,
	DateTimeOffset OrderedAt,
	string Status,
	bool ForDelivery,
	AcceptedCustomResponse? AcceptedCustom,
	FinishedCustomResponse? FinishedCustom,
	CompletedCustomResponse? CompletedCustom
);
