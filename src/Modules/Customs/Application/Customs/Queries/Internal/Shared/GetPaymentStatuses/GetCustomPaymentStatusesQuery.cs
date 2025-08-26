using CustomCADs.Shared.Application.Abstractions.Requests.Attributes;

namespace CustomCADs.Customs.Application.Customs.Queries.Internal.Shared.GetPaymentStatuses;

[AddRequestCaching(ExpirationType.Absolute)]
public record GetCustomPaymentStatusesQuery : IQuery<string[]>;
