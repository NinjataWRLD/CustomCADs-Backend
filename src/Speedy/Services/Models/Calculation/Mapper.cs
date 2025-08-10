using CustomCADs.Speedy.API.Dtos.CalculationAddressLocation;
using CustomCADs.Speedy.API.Dtos.CalculationContent;
using CustomCADs.Speedy.API.Dtos.CalculationRecipient;
using CustomCADs.Speedy.API.Dtos.CalculationSender;
using CustomCADs.Speedy.API.Dtos.CalculationService;
using CustomCADs.Speedy.Services;
using CustomCADs.Speedy.Services.Models.Calculation;
using CustomCADs.Speedy.Services.Models.Calculation.Content;
using CustomCADs.Speedy.Services.Models.Calculation.Recipient;
using CustomCADs.Speedy.Services.Models.Calculation.Sender;
using CustomCADs.Speedy.Services.Models.Calculation.Service;
using CustomCADs.Speedy.Services.Models.Shipment.Content;
using CustomCADs.Speedy.Services.Models.Shipment.Service.AdditionalServices;

namespace CustomCADs.Speedy.Services.Models.Calculation;

using static Constants;

internal static class Mapper
{
	internal static CalculationRecipientDto ToDto(this CalculationRecipientModel model)
		=> new(
			AddressLocation: model.AddressLocation?.ToDto(),
			ClientId: model.ClientId,
			PrivatePerson: model.PrivatePerson,
			PickupOfficeId: model.PickupOfficeId,
			PickupGeoPUDOId: model.PickupGeoPUDOIf
		);

	internal static CalculationAddressLocationDto ToDto(this CalculationAddressLocationModel model)
		=> new(
			CountryId: model.CountryId,
			StateId: model.StateId,
			SiteId: model.SiteId,
			SiteType: model.SiteType,
			SiteName: model.SiteName,
			PostCode: model.PostCode
		);

	internal static CalculationServiceDto ToDto(this CalculationServiceModel model)
		=> new(
			ServiceIds: model.ServiceIds,
			PickupDate: model.PickupDate?.ToString(DateFormat),
			AutoAdjustPickupDate: model.AutoAdjustPickupDate,
			AdditionalServices: model.AdditionalServices?.ToDto(),
			DeferredDays: model.DeferredDays,
			SaturdayDelivery: model.SaturdayDelivery
		);

	internal static CalculationContentDto ToDto(this CalculationContentModel model)
		=> new(
			ParcelsCount: model.ParcelsCount,
			TotalWeight: model.TotalWeight,
			Documents: model.Documents,
			Palletized: model.Palletized,
			Parcels: [.. model.Parcels?.Select(p => p.ToDto()) ?? []]
		);

	internal static CalculationSenderDto ToDto(this CalculationSenderModel model)
		=> new(
			AddressLocation: model.AddressLocation?.ToDto(),
			ClientId: model.ClientId,
			PrivatePerson: model.PrivatePerson,
			DropoffOfficeId: model.DropoffOfficeId,
			DropoffGeoPUDOId: model.DropoffGeoPUDOId
		);
}
