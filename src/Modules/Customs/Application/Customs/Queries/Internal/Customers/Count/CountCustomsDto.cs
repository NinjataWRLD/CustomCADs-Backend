namespace CustomCADs.Customs.Application.Customs.Queries.Internal.Customers.Count;

public record CountCustomsDto(
	int Pending,
	int Accepted,
	int Begun,
	int Finished,
	int Completed,
	int Reported
);
