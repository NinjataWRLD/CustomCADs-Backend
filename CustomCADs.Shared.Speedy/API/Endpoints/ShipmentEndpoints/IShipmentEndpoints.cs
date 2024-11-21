using Refit;

namespace CustomCADs.Shared.Speedy.API.Endpoints.ShipmentEndpoints;

using AddParcel;
using BarcodeInformation;
using CancelShipment;
using CreateShipment;
using FinalizePendingShipment;
using FindParcelsByRef;
using HandoverToCourier;
using HandoverToMidwayCarrier;
using SecondaryShipment;
using ShipmentInfo;
using UpdateShipment;
using UpdateShipmentProperties;

public interface IShipmentEndpoints
{
    [Post("")]
    Task<CreateShipmentResponse> CreateShipmentAsync(CreateShipmentRequest request, CancellationToken ct = default);

    [Delete("")]
    Task<CancelShipmentResponse> CancelShipmentAsync(CancelShipmentRequest request, CancellationToken ct = default);

    [Post("add_parcel")]
    Task<AddParcelResponse> AddParcelShipmentAsync(AddParcelRequest request, CancellationToken ct = default);

    [Post("finalize")]
    Task<FinalizePendingShipmentResponse> FinalizePendingShipmentAsync(FinalizePendingShipmentRequest request, CancellationToken ct = default);

    [Post("info")]
    Task<ShipmentInfoResponse> ShipmentInfoAsync(ShipmentInfoRequest request, CancellationToken ct = default);

    [Post("{shipmentId}/secondary")]
    Task<SecondaryShipmentResponse> SecondaryShipmentAsync(string shipmentId, SecondaryShipmentRequest request, CancellationToken ct = default);

    [Post("update")]
    Task<UpdateShipmentResponse> UpdateShipmentAsync(UpdateShipmentRequest request, CancellationToken ct = default);

    [Post("update/properties")]
    Task<UpdateShipmentPropertiesResponse> UpdateShipmentPropertiesAsync(UpdateShipmentPropertiesRequest request, CancellationToken ct = default);

    [Post("search")]
    Task<FindParcelsByRefResponse> FindParcelsByRefAsync(FindParcelsByRefRequest request, CancellationToken ct = default);

    [Post("handover")]
    Task<HandoverToCourierResponse> HandoverToCourierAsync(HandoverToCourierRequest request, CancellationToken ct = default);

    [Post("handover-to-midway-carrier")]
    Task<HandoverToMidwayCarrierResponse> HandoverToMidwayCarrierAsync(HandoverToMidwayCarrierRequest request, CancellationToken ct = default);

    [Post("barcode-information")]
    Task<BarcodeInformationResponse> BarcodeInformationAsync(BarcodeInformationRequest request, CancellationToken ct = default);
}
