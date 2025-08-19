using CustomCADs.Speedy.Core.Models;

namespace CustomCADs.Speedy.Core.Contracts.Location;

public record OfficeModel(
	int Id,
	string Name,
	string NameEn,
	long SiteId,
	AddressModel Address,
	string WorkinTimeFrom,
	string WorkinTimeTo,
	string WorkinTimeHalfFrom,
	string WorkinTimeHalfTo,
	string WorkinTimeDayOffFrom,
	string WorkinTimeDayOffTo,
	string SameDayDepartureCutoff,
	string SameDayDepartureCutoffHalf,
	string SameDayDepartureCutoffDayOff,
	ShipmentParcelSizeModel MaxParcelDimensions,
	double MaxParcelWeight,
	OfficeType Type,
	int NearbyOfficeId,
	OfficeWorkingTimeScheduleModel[] WorkingTimeSchedule,
	bool PalletOffice,
	bool CardPaymentAllowed,
	bool CashPaymentAllowed,
	string ValidFrom,
	string ValidTo,
	CargoType[] CargoTypesAllowed,
	bool PickUpAllowed,
	bool DropOffAllowed
);
