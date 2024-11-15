namespace CustomCADs.Shared.Speedy.Dtos.Office;

public record OfficeWorkingTimeScheduleDto(
    string Date,
    string WorkingTimeFrom,
    string WorkingTimeTo,
    string SameDayDepartureCutoff,
    bool StandardSchedule
);