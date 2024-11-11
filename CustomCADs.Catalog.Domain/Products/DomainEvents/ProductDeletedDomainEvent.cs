﻿using CustomCADs.Shared.Core.Domain.ValueObjects.Ids;
using CustomCADs.Shared.Core.Events;

namespace CustomCADs.Catalog.Domain.Products.DomainEvents;

public record ProductDeletedDomainEvent(
    ProductId Id
) : BaseDomainEvent;
