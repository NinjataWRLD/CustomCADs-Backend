using CustomCADs.Shared.Domain.Bases.Entities;
using CustomCADs.Shared.Domain.Exceptions;
using CustomCADs.Shared.Domain.TypedIds.Accounts;
using CustomCADs.Shared.Domain.TypedIds.Catalog;
using CustomCADs.Shared.Domain.TypedIds.Printing;

namespace CustomCADs.Carts.Domain.ActiveCarts;

public class ActiveCartItem : BaseAggregateRoot
{
	private ActiveCartItem() { }
	private ActiveCartItem(
		ProductId productId,
		AccountId buyerId,
		bool forDelivery,
		CustomizationId? customizationId) : this()
	{
		ProductId = productId;
		BuyerId = buyerId;
		ForDelivery = forDelivery;
		AddedAt = DateTimeOffset.UtcNow;
		CustomizationId = customizationId;
	}

	public AccountId BuyerId { get; }
	public ProductId ProductId { get; }
	public int Quantity { get; private set; } = 1;
	public bool ForDelivery { get; private set; }
	public DateTimeOffset AddedAt { get; private set; }
	public CustomizationId? CustomizationId { get; private set; }

	public static ActiveCartItem Create(ProductId productId, AccountId buyerId)
		=> new(
			productId: productId,
			buyerId: buyerId,
			forDelivery: false,
			customizationId: null
		);

	public static ActiveCartItem Create(ProductId productId, AccountId buyerId, CustomizationId customizationId)
		=> new(
			productId: productId,
			buyerId: buyerId,
			forDelivery: true,
			customizationId: customizationId
		);

	public ActiveCartItem IncreaseQuantity(int amount)
	{
		if (!ForDelivery)
		{
			throw CustomValidationException<ActiveCartItem>.Custom("Cannot increase quantity of an Active Cart Item not for delivery");
		}

		Quantity += amount;
		this.ValidateQuantity();

		return this;
	}

	public ActiveCartItem DecreaseQuantity(int amount)
	{
		if (!ForDelivery)
		{
			throw CustomValidationException<ActiveCartItem>.Custom("Cannot decrease quantity of an Active Cart Item not for delivery");
		}

		Quantity -= amount;
		this.ValidateQuantity();

		return this;
	}

	public ActiveCartItem SetForDelivery(CustomizationId customizationId)
	{
		ForDelivery = true;
		CustomizationId = customizationId;

		return this;
	}

	public ActiveCartItem SetNoDelivery()
	{
		ForDelivery = false;
		CustomizationId = null;
		Quantity = 1;

		return this;
	}
}
