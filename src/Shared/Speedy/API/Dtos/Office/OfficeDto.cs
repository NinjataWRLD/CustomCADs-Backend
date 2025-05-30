namespace CustomCADs.Shared.Speedy.API.Dtos.Office;

using ShipmentContent.ShipmentParcel;
using ShipmentSenderAndRecipient.ShipmentAddress;
public record OfficeDto(
	int Id,
	string Name,
	string NameEn,
	long SiteId,
	AddressDto Address,
	string WorkinTimeFrom,
	string WorkinTimeTo,
	string WorkinTimeHalfFrom,
	string WorkinTimeHalfTo,
	string WorkinTimeDayOffFrom,
	string WorkinTimeDayOffTo,
	string SameDayDepartureCutoff,
	string SameDayDepartureCutoffHalf,
	string SameDayDepartureCutoffDayOff,
	ShipmentParcelSizeDto MaxParcelDimensions,
	double MaxParcelWeight,
	OfficeType Type,
	int NearbyOfficeId,
	OfficeWorkingTimeScheduleDto[] WorkingTimeSchedule,
	bool PalletOffice,
	bool CardPaymentAllowed,
	bool CashPaymentAllowed,
	string ValidFrom,
	string ValidTo,
	CargoType[] CargoTypesAllowed,
	bool PickUpAllowed,
	bool DropOffAllowed
);
