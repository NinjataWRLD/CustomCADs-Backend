namespace CustomCADs.Speedy.Http.Dtos.Office;

internal record OfficeWorkingTimeScheduleDto(
	string Date,
	string WorkingTimeFrom,
	string WorkingTimeTo,
	string SameDayDepartureCutoff,
	bool StandardSchedule
);
