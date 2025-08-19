namespace CustomCADs.Speedy.Http.Dtos.Office;

/// <param name="Date">
///     Date for the working time schedule.
///     <br />
///     Required: Yes.
/// </param>
/// <param name="WorkingTimeFrom">
///     Working time start for the schedule.
///     <br />
///     Required: Yes.
/// </param>
/// <param name="WorkingTimeTo">
///     Working time end for the schedule.
///     <br />
///     Required: Yes.
/// </param>
/// <param name="SameDayDepartureCutoff">
///     Cutoff time for same day departure for the schedule.
///     <br />
///     Required: Yes.
/// </param>
/// <param name="StandardSchedule">
///     Flag indicating if this is a standard schedule.
///     <br />
///     Required: Yes.
/// </param>
internal record OfficeWorkingTimeScheduleDto(
	string Date,
	string WorkingTimeFrom,
	string WorkingTimeTo,
	string SameDayDepartureCutoff,
	bool StandardSchedule
);
