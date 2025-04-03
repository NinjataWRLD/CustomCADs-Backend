namespace CustomCADs.Customs.Application.Customs.Queries.Internal.Client.Count;

public record CountCustomsDto(
    int Pending,
    int Accepted,
    int Begun,
    int Finished,
    int Completed,
    int Reported
);