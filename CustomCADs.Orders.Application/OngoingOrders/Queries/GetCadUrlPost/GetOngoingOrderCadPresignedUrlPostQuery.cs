using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.Orders.Application.OngoingOrders.Queries.GetCadUrlPost;

public record GetOngoingOrderCadPresignedUrlPostQuery(
    OngoingOrderId Id,
    string ContentType,
    string FileName,
    AccountId DesignerId
) : IQuery<GetOngoingOrderCadPresignedUrlPostDto>;
