﻿using CustomCADs.Shared.Core.Domain;

namespace CustomCADs.Catalog.Domain.Categories.DomainEvents;

public record CategoryCreatedDomainEvent(
    Category Category
) : DomainEvent;