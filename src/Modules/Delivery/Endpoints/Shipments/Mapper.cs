using CustomCADs.Delivery.Application.Shipments.Queries.Internal.GetAll;
using CustomCADs.Delivery.Application.Shipments.Queries.Internal.GetStatus;
using CustomCADs.Delivery.Domain.Shipments.ValueObjects;
using CustomCADs.Delivery.Endpoints.Shipments.Endpoints.Get.Shipment;
using CustomCADs.Delivery.Endpoints.Shipments.Endpoints.Get.Track;

namespace CustomCADs.Delivery.Endpoints.Shipments;

public static class Mapper
{
    public static GetShipmentsResponse ToResponse(this GetAllShipmentsDto shipment)
        => new(
            Id: shipment.Id.Value,
            Address: shipment.Address.ToResponse(),
            BuyerName: shipment.BuyerName
        );

    public static Dictionary<DateTimeOffset, TrackShipmentResponse> ToResponse(this Dictionary<DateTimeOffset, GetShipmentTrackDto> tracks)
        => tracks.ToDictionary(
            x => x.Key,
            x => new TrackShipmentResponse(x.Value.Message, x.Value.Place)
        );

    public static AddressResponse ToResponse(this Address address)
        => new(
            Country: address.Country,
            City: address.City
        );
}
