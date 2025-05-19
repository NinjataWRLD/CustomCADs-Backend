namespace CustomCADs.Shared.Speedy.Services.Location.Office;

public record OfficeWorkingTimeScheduleModel(
	DateOnly Date,
	TimeOnly WorkingTimeFrom,
	TimeOnly WorkingTimeTo,
	string SameDayDepartureCutoff,
	bool StandardSchedule
);
