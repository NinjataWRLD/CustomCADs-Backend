using CustomCADs.Shared.Speedy.API.Dtos.ShipmentParcels;
using CustomCADs.Shared.Speedy.API.Dtos.TrackedParcel;
using CustomCADs.Shared.Speedy.API.Dtos.TrackedParcel.TrackedParcelOperation;
using CustomCADs.Shared.Speedy.Services.Models.Shipment.Parcel;
using CustomCADs.Shared.Speedy.Services.Track.Models;

namespace CustomCADs.Shared.Speedy.Services.Track;

public static class Mapper
{
    public static TrackShipmentParcelRefDto ToDto(this (ShipmentParcelRefModel Parcel, string? Ref) model)
        => new(
            Ref: model.Ref,
            Id: model.Parcel.Id,
            ExternalCarrierParcelNumber: model.Parcel.ExternalCarrierParcelNumber,
            FullBarcode: model.Parcel.FullBarcode
        );

    public static TrackedParcelModel ToModel(this TrackedParcelDto dto)
        => new(
            ParcelId: dto.ParcelId,
            ExternalCarrierParcelNumbers: dto.ExternalCarrierParcelNumbers,
            Operations: [.. dto.Operations.Select(o => o.ToModel())],
            ExternalCarrierParcelNumbersDetails: dto.ExternalCarrierParcelNumbersDetails?.ToDictionary(
                    kv => kv.Key,
                    kv => (
                        kv.Value.ExternalCarrierId,
                        kv.Value.ExternalCarrierName,
                        kv.Value.TrackAndTraceUrl
                    )
                )
        );

    public static TrackedParcelOperationModel ToModel(this TrackedParcelOperationDto dto)
        => new(
            DateTime: DateTime.Parse(dto.DateTime),
            OperationCode: dto.OperationCode,
            Description: dto.Description,
            Place: dto.Place,
            Comment: dto.Comment,
            ExceptionCodes: dto.ExceptionCodes,
            ReturnShipmentId: dto.ReturnShipmentId,
            RedirectShipmentId: dto.RedirectShipmentId,
            Consignee: dto.Consignee,
            PodImageURL: dto.PodImageURL,
            AdditionalInfo: dto.AdditionalInfo?.ToModel()
        );

    public static TrackedParcelOperationAdditionalInfoModel ToModel(this TrackedParcelOperationAdditionalInfoDto dto)
        => new(
            OfficeUrl: dto.OfficeURL,
            GeoPudoId: dto.GeoPUDOId,
            Predict: dto.Predict?.ToModel()
        );

    public static TrackedParcelOperationAdditionalInfoPredictModel ToModel(this TrackedParcelOperationAdditionalInfoPredictDto dto)
        => new(
            PredictedVisitDateTimeFrom: DateTime.Parse(dto.PredictedVisitDateTimeFrom),
            PredictedVisitDateTimeTo: DateTime.Parse(dto.PredictedVisitDateTimeTo),
            Canceled: dto.Canceled,
            IncludedDelayInMinutes: dto.IncludedDelayInMinutes
        );
}
