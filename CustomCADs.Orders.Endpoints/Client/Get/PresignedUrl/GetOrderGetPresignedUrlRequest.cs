using CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Orders;

namespace CustomCADs.Orders.Endpoints.Client.Get.PresignedUrl;

public record GetOrderGetPresignedUrlRequest(Guid Id);
