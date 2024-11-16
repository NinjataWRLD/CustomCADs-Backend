using Refit;

namespace CustomCADs.Shared.Speedy.Services.ShipmentService;

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

public interface IShipmentService
{
    [Post("")]
    Task<CreateShipmentResponse> CreateShipmentAsync(CreateShipmentRequest request, CancellationToken ct = default);

    [Delete("")]
    Task<CancelShipmentResponse> CancelShipmentAsync(CancelShipmentRequest request, CancellationToken ct = default);

    [Post("add_parcel")]
    Task<AddParcelResponse> AddParcelShipmentAsync(AddParcelRequest request, CancellationToken ct = default);

    [Post("finalize")]
    Task<FinalizePendingShipmentRequest> FinalizePendingShipmentAsync(FinalizePendingShipmentResponse request, CancellationToken ct = default);

    [Get("info")]
    [Headers("Content-Type: application/x-www-form-urlencoded")]
    Task<ShipmentInfoResponse> ShipmentInfoAsync(ShipmentInfoRequest request, CancellationToken ct = default);

    [Get("{shipmentId}/secondary")]
    [Headers("Content-Type: application/x-www-form-urlencoded")]
    Task<SecondaryShipmentResponse> SecondaryShipmentAsync([AliasAs("shipmentId")] string id, SecondaryShipmentRequest request, CancellationToken ct = default);

    [Post("update")]
    Task<UpdateShipmentResponse> UpdateShipmentAsync(UpdateShipmentRequest request, CancellationToken ct = default);

    [Post("update/properties")]
    Task<UpdateShipmentPropertiesResponse> UpdateShipmentPropertiesAsync(UpdateShipmentPropertiesRequest request, CancellationToken ct = default);

    [Get("search")]
    [Headers("Content-Type: application/x-www-form-urlencoded")]
    Task<FindParcelsByRefResponse> FindParcelsByRefAsync(FindParcelsByRefRequest request, CancellationToken ct = default);

    [Post("handover")]
    Task<HandoverToCourierResponse> HandoverToCourierAsync(HandoverToCourierRequest request, CancellationToken ct = default);

    [Post("handover-to-midway-carrier")]
    Task<HandoverToMidwayCarrierResponse> HandoverToMidwayCarrierAsync(HandoverToMidwayCarrierRequest request, CancellationToken ct = default);

    [Get("barcode-information")]
    [Headers("Content-Type: application/x-www-form-urlencoded")]
    Task<BarcodeInformationResponse> BarcodeInformationAsync(BarcodeInformationRequest request, CancellationToken ct = default);
}
