using CustomCADs.Gallery.Application.Carts.Queries.GetAll;
using CustomCADs.Gallery.Application.Carts.Queries.GetById;
using CustomCADs.Gallery.Domain.Carts.Entities;
using CustomCADs.Gallery.Endpoints.Carts.Get.All;
using CustomCADs.Gallery.Endpoints.Carts.Get.Single;
using CustomCADs.Gallery.Endpoints.Carts.Recent;
using CustomCADs.Shared.Core.Common.Dtos;
using CustomCADs.Shared.Core.Common.ValueObjects;

namespace CustomCADs.Gallery.Endpoints.Carts;

using static Constants;

public static class Mapper
{
    public static GetCartsDto ToGetCartsDto(this GetAllCartsDto cart)
        => new(
            Id: cart.Id.Value,
            Total: cart.Total,
            PurchaseDate: cart.PurchaseDate.ToString(DateFormatString),
            ItemsCount: cart.ItemsCount
        );

    public static RecentCartsResponse ToRecentCartsResponse(this GetAllCartsDto cart)
        => new(
            Id: cart.Id.Value,
            PurchaseDate: cart.PurchaseDate.ToString(DateFormatString)
        );

    public static GetCartResponse ToGetCartResponse(this GetCartByIdDto cart)
        => new(
            Id: cart.Id.Value,
            Total: cart.Total,
            PurchaseDate: cart.PurchaseDate.ToString(DateFormatString),
            BuyerId: cart.BuyerId.Value,
            Items: [.. cart.Items.Select(o => o.ToCartItemDto())]
        );

    public static CartItemDto ToCartItemDto(this CartItem item)
        => new(
            Id: item.Id.Value,
            Quantity: item.Quantity,
            DeliveryType: item.DeliveryType.ToString(),
            Price: item.Price.ToMoneyDto(),
            PurchaseDate: item.PurchaseDate.ToString(DateFormatString),
            ProductId: item.ProductId.Value,
            CartId: item.CartId.Value,
            CadId: item.CadId is null ? null : item.CadId.Value!.Value,
            ShipmentId: item.ShipmentId is null ? null : item.ShipmentId.Value!.Value,
            Cost: item.Cost.ToMoneyDto()
        );

    public static MoneyDto ToMoneyDto(this Money dto)
        => new(
            Amount: dto.Amount,
            Currency: dto.Currency,
            Precision: dto.Precision,
            Symbol: dto.Symbol
        );
}
