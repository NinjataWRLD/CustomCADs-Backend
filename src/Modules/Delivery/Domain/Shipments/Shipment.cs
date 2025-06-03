using CustomCADs.Delivery.Domain.Shipments.ValueObjects;
using CustomCADs.Shared.Core.Bases.Entities;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.Delivery.Domain.Shipments;

public class Shipment : BaseAggregateRoot
{
	private Shipment() { }
	private Shipment(Address address, string referenceId, AccountId buyerId)
	{
		RequestedAt = DateTimeOffset.UtcNow;
		Address = address;
		ReferenceId = referenceId;
		BuyerId = buyerId;
	}

	public ShipmentId Id { get; private set; }
	public string ReferenceId { get; private set; } = string.Empty;
	public DateTimeOffset RequestedAt { get; private set; }
	public Address Address { get; private set; } = new();
	public AccountId BuyerId { get; private set; }

	public static Shipment Create(
		Address address,
		string referenceId,
		AccountId buyerId
	) => new Shipment(address, referenceId, buyerId)
		.ValidateCountry()
		.ValidateCity();

	public static Shipment CreateWithId(
		ShipmentId id,
		Address address,
		string referenceId,
		AccountId buyerId
	) => new Shipment(address, referenceId, buyerId)
	{
		Id = id
	}
	.ValidateCountry()
	.ValidateCity();
}
