using CustomCADs.Speedy.Http.Dtos.CalculationContent;
using CustomCADs.Speedy.Http.Dtos.CalculationRecipient;
using CustomCADs.Speedy.Http.Dtos.CalculationResult;
using CustomCADs.Speedy.Http.Dtos.CalculationSender;
using CustomCADs.Speedy.Http.Dtos.CalculationService;
using CustomCADs.Speedy.Http.Dtos.ShipmentContent.ShipmentParcel;
using CustomCADs.Speedy.Http;
using CustomCADs.Speedy.Core.Models.Calculation;
using CustomCADs.Speedy.Core.Models.Calculation.Recipient;
using CustomCADs.Speedy.Core.Models.Shipment.Content;
using CustomCADs.Speedy.Core.Models.Shipment.Payment;
using CustomCADs.Speedy.Core.Models.Shipment.Recipient;
using CustomCADs.Speedy.Core.Models.Shipment.Sender;
using CustomCADs.Speedy.Core.Models.Shipment.Service;
using CustomCADs.Speedy.Core.Models.Shipment.Service.AdditionalServices;
using CustomCADs.Speedy.Core.Services.Shipment;
using CustomCADs.Speedy.Core.Contracts.Calculation;
using CustomCADs.Speedy.Core.Contracts.Services;

namespace CustomCADs.Speedy.Core.Services.Calculation;

using static Constants;

internal static class Mapper
{
	internal static CalculationContentDto ToCalculation(this ShipmentContentModel model)
		=> new(
			ParcelsCount: model.ParcelsCount,
			TotalWeight: model.TotalWeight,
			Documents: model.Documents,
			Palletized: model.Palletized,
			Parcels: [.. model.Parcels?.Select(p => p.ToDto()) ?? []]
		);

	internal static CalculationServiceDto ToCalculation(this ShipmentServiceModel model)
		=> new(
			ServiceIds: [model.ServiceId],
			PickupDate: model.PickupDate?.ToString(DateTimeFormat),
			AutoAdjustPickupDate: model.AutoAdjustPickupDate,
			AdditionalServices: model.AdditionalServices?.ToDto(),
			DeferredDays: model.DeferredDays,
			SaturdayDelivery: model.SaturdayDelivery
		);

	internal static CalculationSenderDto ToCalculation(this ShipmentSenderModel model, CalculationAddressLocationModel? location = null)
		=> new(
			AddressLocation: location?.ToDto(),
			ClientId: model.ClientId,
			PrivatePerson: model.PrivatePerson,
			DropoffOfficeId: model.DropoffOfficeId,
			DropoffGeoPUDOId: model.DropoffGeoPUDOId
		);

	internal static CalculationRecipientDto ToCalculation(this ShipmentRecipientModel model, CalculationAddressLocationModel? location = null)
		=> new(
			AddressLocation: location?.ToDto(),
			ClientId: model.ClientId,
			PrivatePerson: model.PrivatePerson,
			PickupOfficeId: model.PickupOfficeId,
			PickupGeoPUDOId: model.PickupGeoPUDOId
		);

	internal static CalculateModel ToModel(this CalculationResultDto dto, CourierServiceModel[] services)
		=> new(
			Service: services.Single(s => s.Id == dto.ServiceId).NameEn,
			AdditionalServices: dto.AdditionalServices?.ToModel(),
			Price: dto.Price.ToModel(),
			PickupDate: dto.PickupDate.ParseDate(),
			DeliveryDeadline: dto.DeliveryDeadline.ParseDateTime()
		);

	internal static ShipmentParcelDto ToParcelDto(this double weight)
		=> new(
			Weight: weight,
			Id: null,
			SeqNo: null,
			PackageUniqueNumber: null,
			Ref1: null,
			Ref2: null,
			Size: null,
			PickupExternalCarrierParcelNumber: null,
			DeliveryExternalCarrierParcelNumber: null
		);
}
