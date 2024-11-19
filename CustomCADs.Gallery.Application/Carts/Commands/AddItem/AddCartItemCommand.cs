using CustomCADs.Gallery.Domain.Carts.Enums;
using CustomCADs.Shared.Core.Domain.ValueObjects;
using CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Catalog;
using CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Gallery;

namespace CustomCADs.Gallery.Application.Carts.Commands.AddItem;

public record AddCartItemCommand(
    CartId Id,
    DeliveryType DeliveryType,
    Money Price,
    int Quantity,
    ProductId ProductId
) : ICommand;
