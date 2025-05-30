using CustomCADs.Customs.Application.Customs.Dtos;
using CustomCADs.Customs.Domain.Customs.Enums;

namespace CustomCADs.Customs.Application.Customs.Queries.Internal.Customers.GetById;

public record CustomerGetCustomByIdDto(
	CustomId Id,
	string Name,
	string Description,
	bool ForDelivery,
	CustomStatus CustomStatus,
	DateTimeOffset OrderedAt,
	AcceptedCustomDto? AcceptedCustom,
	FinishedCustomDto? FinishedCustom,
	CompletedCustomDto? CompletedCustom
);
