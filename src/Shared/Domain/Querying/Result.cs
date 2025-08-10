namespace CustomCADs.Shared.Domain.Querying;

public record Result<TItem>(
	int Count,
	ICollection<TItem> Items
);
