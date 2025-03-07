﻿using CustomCADs.Delivery.Application.Shipments.Queries.GetAll;
using CustomCADs.Delivery.Application.Shipments.Queries.GetStatus;
using CustomCADs.Delivery.Domain.Shipments.ValueObjects;
using CustomCADs.Delivery.Endpoints.Shipments.Get.Shipment;
using CustomCADs.Delivery.Endpoints.Shipments.Get.Track;

namespace CustomCADs.Delivery.Endpoints.Shipments;

using static Constants;

public static class Mapper
{
    public static GetShipmentsResponse ToGetShipmentsResponse(this GetAllShipmentsDto shipment)
        => new(
            Id: shipment.Id.Value,
            Address: shipment.Address.ToAddressDto(),
            BuyerName: shipment.BuyerName
        );

    public static Dictionary<string, TrackShipmentResponse> ToTrackShipmentResponse(this Dictionary<DateTime, GetShipmentTrackDto> tracks)
        => tracks.ToDictionary(
            x => x.Key.ToString(SpeedyDateTimeFormatString),
            x => new TrackShipmentResponse(x.Value.Message, x.Value.Place)
        );

    public static AddressDto ToAddressDto(this Address address)
        => new(
            Country: address.Country,
            City: address.City
        );
}
