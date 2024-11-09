﻿using CustomCADs.Shared.Core.Domain;
using CustomCADs.Shared.Core.Dtos;

namespace CustomCADs.Catalog.Domain.DomainEvents.Products;

public record ProductCreatedEvent(
    Guid Id,
    string Name,
    string Description,
    int CategoryId,
    decimal Cost,
    Guid CreatorId,
    string Status,
    FileDto Image,
    FileDto Cad
) : DomainEvent;
