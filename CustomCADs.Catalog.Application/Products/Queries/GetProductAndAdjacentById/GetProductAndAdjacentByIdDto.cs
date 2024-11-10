﻿using CustomCADs.Shared.Core.Domain.ValueObjects;

namespace CustomCADs.Catalog.Application.Products.Queries.GetProductAndAdjacentById;

public record GetProductAndAdjacentByIdDto(Guid? PrevId, GetProductAndAdjacentByIdItem Current, Guid? NextId);

public record GetProductAndAdjacentByIdItem(Guid Id, Cad Cad);
