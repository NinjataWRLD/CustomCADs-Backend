using CustomCADs.Speedy.API;
using CustomCADs.Speedy.API.Dtos.TrackedParcel;
using CustomCADs.Speedy.API.Dtos.TrackedParcel.TrackedParcelOperation;
using CustomCADs.Speedy.Services.Track.Models;

namespace CustomCADs.Speedy.Services.Track;

internal static class Mapper
{
	internal static TrackedParcelModel ToModel(this TrackedParcelDto dto)
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

	internal static TrackedParcelOperationModel ToModel(this TrackedParcelOperationDto dto)
		=> new(
			DateTime: dto.DateTime.ParseDateTime(),
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

	internal static TrackedParcelOperationAdditionalInfoModel ToModel(this TrackedParcelOperationAdditionalInfoDto dto)
		=> new(
			OfficeUrl: dto.OfficeURL,
			GeoPudoId: dto.GeoPUDOId,
			Predict: dto.Predict?.ToModel()
		);

	internal static TrackedParcelOperationAdditionalInfoPredictModel ToModel(this TrackedParcelOperationAdditionalInfoPredictDto dto)
		=> new(
			PredictedVisitDateTimeFrom: DateTime.Parse(dto.PredictedVisitDateTimeFrom),
			PredictedVisitDateTimeTo: DateTime.Parse(dto.PredictedVisitDateTimeTo),
			Canceled: dto.Canceled,
			IncludedDelayInMinutes: dto.IncludedDelayInMinutes
		);
}
