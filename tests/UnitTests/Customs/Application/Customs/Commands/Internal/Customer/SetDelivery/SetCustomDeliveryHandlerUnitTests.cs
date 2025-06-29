﻿using CustomCADs.Customs.Application.Customs.Commands.Internal.Customers.SetDelivery;
using CustomCADs.Customs.Domain.Repositories;
using CustomCADs.Customs.Domain.Repositories.Reads;
using CustomCADs.Shared.Core.Common.Exceptions.Application;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.UnitTests.Customs.Application.Customs.Commands.Internal.Customer.SetDelivery;

public class SetCustomDeliveryHandlerUnitTests : CustomsBaseUnitTests
{
	private readonly SetCustomDeliveryHandler handler;
	private readonly Mock<ICustomReads> reads = new();
	private readonly Mock<IUnitOfWork> uow = new();

	private static readonly CustomId id = CustomId.New();
	private static readonly bool value = true;
	private static readonly AccountId buyerId = AccountId.New();
	private readonly Custom custom = CreateCustom(forDelivery: !value);

	public SetCustomDeliveryHandlerUnitTests()
	{
		handler = new(reads.Object, uow.Object);

		reads.Setup(x => x.SingleByIdAsync(id, true, ct))
			.ReturnsAsync(custom);
	}

	[Fact]
	public async Task Handle_ShouldQueryDatabase()
	{
		// Arrange
		SetCustomDeliveryCommand command = new(
			Id: id,
			Value: value,
			BuyerId: buyerId
		);

		// Act
		await handler.Handle(command, ct);

		// Assert
		reads.Verify(x => x.SingleByIdAsync(id, true, ct), Times.Once);
	}

	[Fact]
	public async Task Handle_ShouldPersistToyDatabase()
	{
		// Arrange
		SetCustomDeliveryCommand command = new(
			Id: id,
			Value: value,
			BuyerId: buyerId
		);

		// Act
		await handler.Handle(command, ct);

		// Assert
		uow.Verify(x => x.SaveChangesAsync(ct), Times.Once);
	}

	[Fact]
	public async Task Handle_ShouldPopulateProperties()
	{
		// Arrange
		SetCustomDeliveryCommand command = new(
			Id: id,
			Value: value,
			BuyerId: buyerId
		);

		// Act
		await handler.Handle(command, ct);

		// Assert
		Assert.Equal(value, custom.ForDelivery);
	}

	[Fact]
	public async Task Handle_ShouldThrowException_WhenCustomNotFound()
	{
		// Arrange
		reads.Setup(x => x.SingleByIdAsync(id, true, ct))
			.ReturnsAsync(null as Custom);

		SetCustomDeliveryCommand command = new(
			Id: id,
			Value: value,
			BuyerId: buyerId
		);

		// Assert
		await Assert.ThrowsAsync<CustomNotFoundException<Custom>>(
			// Act
			async () => await handler.Handle(command, ct)
		);
	}
}
