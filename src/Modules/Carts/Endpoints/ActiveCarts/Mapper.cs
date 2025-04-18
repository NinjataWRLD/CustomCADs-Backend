﻿using CustomCADs.Carts.Endpoints.ActiveCarts.Endpoints.Get.CalculateShipment;
using CustomCADs.Shared.Core.Common.Dtos;

namespace CustomCADs.Carts.Endpoints.ActiveCarts;

using static Constants.DateTimes;

internal static class Mapper
{
    internal static ActiveCartItemResponse ToResponse(this ActiveCartItemDto item)
        => new(
            Quantity: item.Quantity,
            ForDelivery: item.ForDelivery,
            AddedAt: item.AddedAt,
            ProductId: item.ProductId.Value,
            CustomizationId: item.CustomizationId?.Value
        );

    internal static CalculateActiveCartShipmentResponse ToResponse(this CalculateShipmentDto calculation)
        => new(
            Service: calculation.Service,
            Total: calculation.Total,
            Currency: calculation.Currency,
            PickupDate: calculation.PickupDate.ToString(SpeedyDateFormatString),
            DeliveryDeadline: calculation.DeliveryDeadline.ToString(SpeedyDateFormatString)
        );
}
