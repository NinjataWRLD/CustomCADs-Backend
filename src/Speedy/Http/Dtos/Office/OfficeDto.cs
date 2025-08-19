namespace CustomCADs.Speedy.Http.Dtos.Office;

using ShipmentContent.ShipmentParcel;
using ShipmentSenderAndRecipient.ShipmentAddress;

/// <param name="Id">
///     Office id.
///     <br />
///     Required: Yes.
/// </param>
/// <param name="Name">
///     Office name (local language).
///     <br />
///     Required: Yes.
/// </param>
/// <param name="NameEn">
///     Office name (English).
///     <br />
///     Required: Yes.
/// </param>
/// <param name="SiteId">
///     Site id for the office.
///     <br />
///     Required: Yes.
/// </param>
/// <param name="Address">
///     Office address.
///     <br />
///     Required: Yes. See AddressDto for details.
/// </param>
/// <param name="WorkinTimeFrom">
///     Working time start (standard day).
///     <br />
///     Required: Yes.
/// </param>
/// <param name="WorkinTimeTo">
///     Working time end (standard day).
///     <br />
///     Required: Yes.
/// </param>
/// <param name="WorkinTimeHalfFrom">
///     Working time start (half-working day).
///     <br />
///     Required: Yes.
/// </param>
/// <param name="WorkinTimeHalfTo">
///     Working time end (half-working day).
///     <br />
///     Required: Yes.
/// </param>
/// <param name="WorkinTimeDayOffFrom">
///     Working time start (day off).
///     <br />
///     Required: Yes.
/// </param>
/// <param name="WorkinTimeDayOffTo">
///     Working time end (day off).
///     <br />
///     Required: Yes.
/// </param>
/// <param name="SameDayDepartureCutoff">
///     Cutoff time for same day departure (standard day).
///     <br />
///     Required: Yes.
/// </param>
/// <param name="SameDayDepartureCutoffHalf">
///     Cutoff time for same day departure (half-working day).
///     <br />
///     Required: Yes.
/// </param>
/// <param name="SameDayDepartureCutoffDayOff">
///     Cutoff time for same day departure (day off).
///     <br />
///     Required: Yes.
/// </param>
/// <param name="MaxParcelDimensions">
///     Maximum parcel dimensions allowed at the office.
///     <br />
///     Required: Yes. See ShipmentParcelSizeDto for details.
/// </param>
/// <param name="MaxParcelWeight">
///     Maximum parcel weight allowed at the office.
///     <br />
///     Required: Yes.
/// </param>
/// <param name="Type">
///     Office type.
///     <br />
///     Required: Yes.
/// </param>
/// <param name="NearbyOfficeId">
///     Nearby office id.
///     <br />
///     Required: Yes.
/// </param>
/// <param name="WorkingTimeSchedule">
///     Working time schedule for the office.
///     <br />
///     Required: Yes. See OfficeWorkingTimeScheduleDto for details.
/// </param>
/// <param name="PalletOffice">
///     Flag indicating if office is a pallet office.
///     <br />
///     Required: Yes.
/// </param>
/// <param name="CardPaymentAllowed">
///     Flag indicating if card payment is allowed.
///     <br />
///     Required: Yes.
/// </param>
/// <param name="CashPaymentAllowed">
///     Flag indicating if cash payment is allowed.
///     <br />
///     Required: Yes.
/// </param>
/// <param name="ValidFrom">
///     Valid from date for the office.
///     <br />
///     Required: Yes.
/// </param>
/// <param name="ValidTo">
///     Valid to date for the office.
///     <br />
///     Required: Yes.
/// </param>
/// <param name="CargoTypesAllowed">
///     Allowed cargo types for the office.
///     <br />
///     Required: Yes.
/// </param>
/// <param name="PickUpAllowed">
///     Flag indicating if pickup is allowed at the office.
///     <br />
///     Required: Yes.
/// </param>
/// <param name="DropOffAllowed">
///     Flag indicating if drop-off is allowed at the office.
///     <br />
///     Required: Yes.
/// </param>
internal record OfficeDto(
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
