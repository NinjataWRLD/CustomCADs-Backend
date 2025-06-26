using CustomCADs.Customs.Application.Customs.Commands.Internal.Customers.Purchase.WithDelivery;
using CustomCADs.Customs.Domain.Customs.Events;
using CustomCADs.Customs.Domain.Repositories;
using CustomCADs.Customs.Domain.Repositories.Reads;
using CustomCADs.Shared.Abstractions.Events;
using CustomCADs.Shared.Abstractions.Payment;
using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.Core.Common.Dtos;
using CustomCADs.Shared.Core.Common.Exceptions.Application;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.UseCases.Accounts.Queries;
using CustomCADs.Shared.UseCases.Cads.Queries;
using CustomCADs.Shared.UseCases.Customizations.Queries;

namespace CustomCADs.UnitTests.Customs.Application.Customs.Commands.Internal.Customer.Purchase.WithDelivery;

using static CustomsData;

public class PurchaseCustomWithDeliveryHandlerUnitTests : CustomsBaseUnitTests
{
	private readonly PurchaseCustomWithDeliveryHandler handler;
	private readonly Mock<ICustomReads> reads = new();
	private readonly Mock<IUnitOfWork> uow = new();
	private readonly Mock<IRequestSender> sender = new();
	private readonly Mock<IPaymentService> payment = new();
	private readonly Mock<IEventRaiser> raiser = new();

	private static readonly AccountId buyerId = AccountId.New();
	private static readonly AddressDto address = new("Bulgaria", "Burgas", "Slivnitsa");
	private static readonly ContactDto contact = new(null, null);
	private readonly Custom custom = CreateCustomWithId(
		id: ValidId,
		buyerId: ValidBuyerId,
		forDelivery: true
	);

	public PurchaseCustomWithDeliveryHandlerUnitTests()
	{
		handler = new(reads.Object, uow.Object, sender.Object, payment.Object, raiser.Object);

		custom.Accept(ValidDesignerId);
		custom.Begin();
		custom.Finish(ValidCadId, ValidPrice);

		reads.Setup(x => x.SingleByIdAsync(ValidId, false, ct))
			.ReturnsAsync(custom);

		sender.Setup(x => x.SendQueryAsync(
			It.Is<GetCustomizationCostByIdQuery>(x => x.Id == ValidCustomizationId),
			ct
		)).ReturnsAsync(0m);

		sender.Setup(x => x.SendQueryAsync(
			It.Is<GetCadExistsByIdQuery>(x => x.Id == ValidCadId),
			ct
		)).ReturnsAsync(true);

		sender.Setup(x => x.SendQueryAsync(
			It.Is<GetCustomizationExistsByIdQuery>(x => x.Id == ValidCustomizationId),
			ct
		)).ReturnsAsync(true);
	}

	[Fact]
	public async Task Handle_ShouldQueryDatabase()
	{
		// Arrange
		PurchaseCustomWithDeliveryCommand command = new(
			Id: ValidId,
			Count: 1,
			CustomizationId: ValidCustomizationId,
			PaymentMethodId: string.Empty,
			ShipmentService: string.Empty,
			BuyerId: ValidBuyerId,
			Address: address,
			Contact: contact
		);

		// Act
		await handler.Handle(command, ct);

		// Assert
		reads.Verify(x => x.SingleByIdAsync(ValidId, false, ct), Times.Once);
	}

	[Fact]
	public async Task Handle_ShouldSendRequests()
	{
		// Arrange
		PurchaseCustomWithDeliveryCommand command = new(
			Id: ValidId,
			Count: 1,
			CustomizationId: ValidCustomizationId,
			PaymentMethodId: string.Empty,
			ShipmentService: string.Empty,
			BuyerId: ValidBuyerId,
			Address: address,
			Contact: contact
		);

		// Act
		await handler.Handle(command, ct);

		// Assert
		sender.Verify(x => x.SendQueryAsync(
			It.Is<GetUsernameByIdQuery>(x => x.Id == ValidBuyerId || x.Id == ValidDesignerId)
		, ct), Times.Exactly(2));
		sender.Verify(x => x.SendQueryAsync(
			It.Is<GetCustomizationCostByIdQuery>(x => x.Id == ValidCustomizationId)
		, ct), Times.Once);
		sender.Verify(x => x.SendQueryAsync(
			It.Is<GetCustomizationWeightByIdQuery>(x => x.Id == ValidCustomizationId)
		, ct), Times.Once);
	}

	[Fact]
	public async Task Handle_ShouldCallPayment()
	{
		// Arrange
		PurchaseCustomWithDeliveryCommand command = new(
			Id: ValidId,
			Count: 1,
			CustomizationId: ValidCustomizationId,
			PaymentMethodId: string.Empty,
			ShipmentService: string.Empty,
			BuyerId: ValidBuyerId,
			Address: address,
			Contact: contact
		);

		// Act
		await handler.Handle(command, ct);

		// Assert
		payment.Verify(x => x.InitializeCustomPayment(
			It.Is<string>(x => string.IsNullOrEmpty(x)),
			It.Is<AccountId>(x => x == ValidBuyerId),
			It.Is<CustomId>(x => x == ValidId),
			It.Is<decimal>(x => x == ValidPrice),
			It.Is<string>(x => x.Contains(custom.Name)),
			ct
		), Times.Once);
	}

	[Fact]
	public async Task Handle_ShouldRaiseEvents()
	{
		// Arrange
		PurchaseCustomWithDeliveryCommand command = new(
			Id: ValidId,
			Count: 1,
			CustomizationId: ValidCustomizationId,
			PaymentMethodId: string.Empty,
			ShipmentService: string.Empty,
			BuyerId: ValidBuyerId,
			Address: address,
			Contact: contact
		);

		// Act
		await handler.Handle(command, ct);

		// Assert
		raiser.Verify(x => x.RaiseDomainEventAsync(
			It.Is<CustomDeliveryRequestedDomainEvent>(x => x.Id == custom.Id)
		), Times.Once);
	}

	[Fact]
	public async Task Handle_ShouldReturnResult()
	{
		// Arrange
		PaymentDto expected = new(string.Empty, Message: "Payment Status Message");
		payment.Setup(x => x.InitializeCustomPayment(
			It.Is<string>(x => string.IsNullOrEmpty(x)),
			It.Is<AccountId>(x => x == ValidBuyerId),
			It.Is<CustomId>(x => x == ValidId),
			It.Is<decimal>(x => x == ValidPrice),
			It.Is<string>(x => x.Contains(custom.Name)),
			ct
		)).ReturnsAsync(expected);

		PurchaseCustomWithDeliveryCommand command = new(
			Id: ValidId,
			Count: 1,
			CustomizationId: ValidCustomizationId,
			PaymentMethodId: string.Empty,
			ShipmentService: string.Empty,
			BuyerId: ValidBuyerId,
			Address: address,
			Contact: contact
		);

		// Act
		PaymentDto actual = await handler.Handle(command, ct);

		// Assert
		Assert.Equal(expected, actual);
	}

	[Fact]
	public async Task Handle_ShouldThrowException_WhenUnauthorizedAccess()
	{
		// Arrange
		PurchaseCustomWithDeliveryCommand command = new(
			Id: ValidId,
			Count: 1,
			CustomizationId: ValidCustomizationId,
			PaymentMethodId: string.Empty,
			ShipmentService: string.Empty,
			BuyerId: buyerId,
			Address: address,
			Contact: contact
		);

		// Assert
		await Assert.ThrowsAsync<CustomAuthorizationException<Custom>>(
			// Act
			async () => await handler.Handle(command, ct)
		);
	}

	[Fact]
	public async Task Handle_ShouldThrowException_WhenNotAccepted()
	{
		// Arrange
		var custom = CreateCustom(buyerId: ValidBuyerId, forDelivery: true);
		reads.Setup(x => x.SingleByIdAsync(ValidId, false, ct))
			.ReturnsAsync(custom);

		PurchaseCustomWithDeliveryCommand command = new(
			Id: ValidId,
			Count: 1,
			CustomizationId: ValidCustomizationId,
			PaymentMethodId: string.Empty,
			ShipmentService: string.Empty,
			BuyerId: ValidBuyerId,
			Address: address,
			Contact: contact
		);

		// Assert
		await Assert.ThrowsAsync<CustomException>(
			// Act
			async () => await handler.Handle(command, ct)
		);
	}

	[Fact]
	public async Task Handle_ShouldThrowException_WhenNotFinished()
	{
		// Arrange
		var custom = CreateCustom(buyerId: ValidBuyerId, forDelivery: true);
		custom.Accept(ValidDesignerId);
		reads.Setup(x => x.SingleByIdAsync(ValidId, false, ct))
			.ReturnsAsync(custom);

		PurchaseCustomWithDeliveryCommand command = new(
			Id: ValidId,
			Count: 1,
			CustomizationId: ValidCustomizationId,
			PaymentMethodId: string.Empty,
			ShipmentService: string.Empty,
			BuyerId: ValidBuyerId,
			Address: address,
			Contact: contact
		);

		// Assert
		await Assert.ThrowsAsync<CustomException>(
			// Act
			async () => await handler.Handle(command, ct)
		);
	}

	[Fact]
	public async Task Handle_ShouldThrowException_WhenNoForDelivery()
	{
		// Arrange
		reads.Setup(x => x.SingleByIdAsync(ValidId, false, ct))
			.ReturnsAsync(CreateCustom(buyerId: ValidBuyerId, forDelivery: false));

		PurchaseCustomWithDeliveryCommand command = new(
			Id: ValidId,
			Count: 1,
			CustomizationId: ValidCustomizationId,
			PaymentMethodId: string.Empty,
			ShipmentService: string.Empty,
			BuyerId: ValidBuyerId,
			Address: address,
			Contact: contact
		);

		// Assert
		await Assert.ThrowsAsync<CustomException>(
			// Act
			async () => await handler.Handle(command, ct)
		);
	}

	[Fact]
	public async Task Handle_ShouldThrowException_WhenCustomNotFound()
	{
		// Arrange
		reads.Setup(x => x.SingleByIdAsync(ValidId, false, ct))
			.ReturnsAsync(null as Custom);

		PurchaseCustomWithDeliveryCommand command = new(
			Id: ValidId,
			Count: 1,
			CustomizationId: ValidCustomizationId,
			PaymentMethodId: string.Empty,
			ShipmentService: string.Empty,
			BuyerId: ValidBuyerId,
			Address: address,
			Contact: contact
		);

		// Assert
		await Assert.ThrowsAsync<CustomNotFoundException<Custom>>(
			// Act
			async () => await handler.Handle(command, ct)
		);
	}
}
