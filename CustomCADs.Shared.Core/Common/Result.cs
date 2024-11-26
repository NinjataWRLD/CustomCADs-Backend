namespace CustomCADs.Shared.Core.Common;

public record Result<TItem>(
    int Count,
    ICollection<TItem> Items
);
