using CustomCADs.Shared.Core.Common.Events;

namespace CustomCADs.Orders.Domain.Orders.DomainEvents;

public record OrderDeletedDomainEvent(
    OrderId Id,
    string ImageKey
) : BaseDomainEvent;
