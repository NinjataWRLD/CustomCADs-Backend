using CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Orders;

namespace CustomCADs.Orders.Endpoints.Orders.Get.PresignedUrl;

public record GetOrderGetPresignedUrlRequest(Guid Id);
