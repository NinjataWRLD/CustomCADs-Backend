using CustomCADs.Speedy.Http.Dtos.Block;
using CustomCADs.Speedy.Http.Dtos.Complex;
using CustomCADs.Speedy.Http.Dtos.Country;
using CustomCADs.Speedy.Http.Dtos.Office;
using CustomCADs.Speedy.Http.Dtos.PointOfInterest;
using CustomCADs.Speedy.Http.Dtos.Site;
using CustomCADs.Speedy.Http.Dtos.SpecialDeliveryRequirements;
using CustomCADs.Speedy.Http.Dtos.State;
using CustomCADs.Speedy.Http.Dtos.Street;
using CustomCADs.Speedy.Core.Services;
using CustomCADs.Speedy.Core.Models.Shipment.Content;
using CustomCADs.Speedy.Core.Contracts.Location;

namespace CustomCADs.Speedy.Core.Services.Location;

internal static class Mapper
{
	internal static BlockModel ToModel(this BlockDto dto)
		=> new(
			SiteId: dto.SiteId,
			Name: dto.Name,
			NameEn: dto.NameEn
		);

	internal static ComplexModel ToModel(this ComplexDto dto)
		=> new(
			Id: dto.Id,
			SiteId: dto.SiteId,
			ActualId: dto.ActualId,
			Type: dto.Type,
			TypeEn: dto.TypeEn,
			Name: dto.Name,
			NameEn: dto.NameEn,
			ActualType: dto.ActualType,
			ActualTypeEn: dto.ActualTypeEn,
			ActualName: dto.ActualName,
			ActualNameEn: dto.ActualNameEn
		);

	internal static CountryModel ToModel(this CountryDto dto)
		=> new(
			Id: dto.Id,
			Name: dto.Name,
			NameEn: dto.NameEn,
			IsoAlpha2: dto.IsoAlpha2,
			IsoAlpha3: dto.IsoAlpha3,
			PostCodeFormats: dto.PostCodeFormats,
			RequireState: dto.RequireState,
			AddressType: dto.AddressType,
			CurrencyCode: dto.CurrencyCode,
			DefaultOfficeId: dto.DefaultOfficeId,
			SiteNomen: dto.SiteNomen,
			StreetTypes: [.. dto.StreetTypes.Select(t => (t.Name, t.NameEn))],
			ComplexTypes: [.. dto.ComplexTypes.Select(t => (t.Name, t.NameEn))]
		);

	internal static PointOfInterestModel ToModel(this PointOfInterestDto dto)
		=> new(
			Id: dto.Id,
			SiteId: dto.SiteId,
			Name: dto.Name,
			NameEn: dto.NameEn,
			Type: dto.Type,
			TypeEn: dto.TypeEn,
			Address: dto.Address,
			AddressEn: dto.AddressEn,
			X: dto.X,
			Y: dto.Y
		);

	internal static OfficeModel ToModel(this OfficeDto dto)
		=> new(
			Id: dto.Id,
			Name: dto.Name,
			NameEn: dto.NameEn,
			SiteId: dto.SiteId,
			Address: dto.Address.ToModel(),
			WorkinTimeFrom: dto.WorkinTimeFrom,
			WorkinTimeTo: dto.WorkinTimeTo,
			WorkinTimeHalfFrom: dto.WorkinTimeHalfFrom,
			WorkinTimeHalfTo: dto.WorkinTimeHalfTo,
			WorkinTimeDayOffFrom: dto.WorkinTimeDayOffFrom,
			WorkinTimeDayOffTo: dto.WorkinTimeDayOffTo,
			SameDayDepartureCutoff: dto.SameDayDepartureCutoff,
			SameDayDepartureCutoffHalf: dto.SameDayDepartureCutoffHalf,
			SameDayDepartureCutoffDayOff: dto.SameDayDepartureCutoffDayOff,
			MaxParcelDimensions: dto.MaxParcelDimensions.ToModel(),
			MaxParcelWeight: dto.MaxParcelWeight,
			Type: dto.Type,
			NearbyOfficeId: dto.NearbyOfficeId,
			WorkingTimeSchedule: [.. dto.WorkingTimeSchedule.Select(s => s.ToModel())],
			PalletOffice: dto.PalletOffice,
			CardPaymentAllowed: dto.CardPaymentAllowed,
			CashPaymentAllowed: dto.CashPaymentAllowed,
			ValidFrom: dto.ValidFrom,
			ValidTo: dto.ValidTo,
			CargoTypesAllowed: dto.CargoTypesAllowed,
			PickUpAllowed: dto.PickUpAllowed,
			DropOffAllowed: dto.DropOffAllowed
		);

	internal static OfficeModel ToModel(this OfficeResultDto dto)
		=> new(
			Id: dto.Id,
			Name: dto.Name,
			NameEn: dto.NameEn,
			SiteId: dto.SiteId,
			Address: dto.Address.ToModel(),
			WorkinTimeFrom: dto.WorkinTimeFrom,
			WorkinTimeTo: dto.WorkinTimeTo,
			WorkinTimeHalfFrom: dto.WorkinTimeHalfFrom,
			WorkinTimeHalfTo: dto.WorkinTimeHalfTo,
			WorkinTimeDayOffFrom: dto.WorkinTimeDayOffFrom,
			WorkinTimeDayOffTo: dto.WorkinTimeDayOffTo,
			SameDayDepartureCutoff: dto.SameDayDepartureCutoff,
			SameDayDepartureCutoffHalf: dto.SameDayDepartureCutoffHalf,
			SameDayDepartureCutoffDayOff: dto.SameDayDepartureCutoffDayOff,
			MaxParcelDimensions: dto.MaxParcelDimensions.ToModel(),
			MaxParcelWeight: dto.MaxParcelWeight,
			Type: dto.Type,
			NearbyOfficeId: dto.NearbyOfficeId,
			WorkingTimeSchedule: [.. dto.WorkingTimeSchedule.Select(s => s.ToModel())],
			PalletOffice: dto.PalletOffice,
			CardPaymentAllowed: dto.CardPaymentAllowed,
			CashPaymentAllowed: dto.CashPaymentAllowed,
			ValidFrom: dto.ValidFrom,
			ValidTo: dto.ValidTo,
			CargoTypesAllowed: dto.CargoTypesAllowed,
			PickUpAllowed: dto.PickUpAllowed,
			DropOffAllowed: dto.DropOffAllowed
		);

	internal static OfficeWorkingTimeScheduleModel ToModel(this OfficeWorkingTimeScheduleDto dto)
		=> new(
			Date: DateOnly.Parse(dto.Date),
			WorkingTimeFrom: TimeOnly.Parse(dto.WorkingTimeFrom),
			WorkingTimeTo: TimeOnly.Parse(dto.WorkingTimeTo),
			SameDayDepartureCutoff: dto.SameDayDepartureCutoff,
			StandardSchedule: dto.StandardSchedule
		);

	internal static SiteModel ToModel(this SiteDto dto)
		=> new(
			Id: dto.Id,
			CountryId: dto.CountryId,
			MainSiteId: dto.MainSiteId,
			Type: dto.Type,
			TypeEn: dto.TypeEn,
			Name: dto.Name,
			NameEn: dto.NameEn,
			Municipality: dto.Municipality,
			MunicipalityEn: dto.MunicipalityEn,
			Region: dto.Region,
			RegionEn: dto.RegionEn,
			PostCode: dto.PostCode,
			AddressNomenclature: dto.AddressNomenclature,
			X: dto.X,
			Y: dto.Y,
			ServingDays: dto.ServingDays,
			ServingOfficeid: dto.ServingOfficeid,
			ServingHubOfficeId: dto.ServingHubOfficeId
		);

	internal static StateModel ToModel(this StateDto dto)
		=> new(
			Id: dto.Id,
			Name: dto.Name,
			NameEn: dto.NameEn,
			StateAlpha: dto.StateAlpha,
			CountryId: dto.CountryId
		);
	internal static StreetModel ToModel(this StreetDto dto)
		=> new(
			Id: dto.Id,
			SiteId: dto.SiteId,
			Type: dto.Type,
			TypeEn: dto.TypeEn,
			Name: dto.Name,
			NameEn: dto.NameEn,
			ActualId: dto.ActualId,
			ActualType: dto.ActualType,
			ActualTypeEn: dto.ActualTypeEn,
			ActualName: dto.ActualName,
			ActualNameEn: dto.ActualNameEn
		);
}
