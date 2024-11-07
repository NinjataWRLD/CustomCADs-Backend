namespace CustomCADs.Shared.Core.Events.Products;

public record ProductDeletedEvent(Guid Id) : IEvent;
