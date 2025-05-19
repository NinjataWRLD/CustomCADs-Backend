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
	private readonly Mock<ICustomReads> reads = new();
	private readonly Mock<IUnitOfWork> uow = new();
	private readonly Mock<IRequestSender> sender = new();
	private readonly Mock<IPaymentService> payment = new();
	private readonly Mock<IEventRaiser> raiser = new();

	private static readonly CustomId id = ValidId1;
	private static readonly AccountId buyerId = ValidBuyerId1;
	private static readonly AccountId wrongBuyerId = ValidBuyerId2;
	private static readonly AddressDto address = new("Bulgaria", "Burgas");
	private static readonly ContactDto contact = new(null, null);
	private readonly Custom custom = CreateCustom(
		buyerId: buyerId,
		forDelivery: true
	);

	public PurchaseCustomWithDeliveryHandlerUnitTests()
	{
		custom.Accept(ValidDesignerId1);
		custom.Begin();
		custom.Finish(ValidCadId1, ValidPrice1);

		reads.Setup(x => x.SingleByIdAsync(id, false, ct))
			.ReturnsAsync(custom);

		sender.Setup(x => x.SendQueryAsync(It.IsAny<GetCustomizationCostByIdQuery>(), ct))
			.ReturnsAsync(0m);

		sender.Setup(x => x.SendQueryAsync(It.IsAny<GetCadExistsByIdQuery>(), ct))
			.ReturnsAsync(true);

		sender.Setup(x => x.SendQueryAsync(It.IsAny<GetCustomizationExistsByIdQuery>(), ct))
			.ReturnsAsync(true);
	}

	[Fact]
	public async Task Handle_ShouldQueryDatabase()
	{
		// Arrange
		PurchaseCustomWithDeliveryCommand command = new(
			Id: id,
			Count: default,
			CustomizationId: default,
			PaymentMethodId: string.Empty,
			ShipmentService: string.Empty,
			BuyerId: buyerId,
			Address: address,
			Contact: contact
		);
		PurchaseCustomWithDeliveryHandler handler = new(reads.Object, uow.Object, sender.Object, payment.Object, raiser.Object);

		// Act
		await handler.Handle(command, ct);

		// Assert
		reads.Verify(x => x.SingleByIdAsync(id, false, ct), Times.Once);
	}

	[Fact]
	public async Task Handle_ShouldSendRequests()
	{
		// Arrange
		PurchaseCustomWithDeliveryCommand command = new(
			Id: id,
			Count: default,
			CustomizationId: default,
			PaymentMethodId: string.Empty,
			ShipmentService: string.Empty,
			BuyerId: buyerId,
			Address: address,
			Contact: contact
		);
		PurchaseCustomWithDeliveryHandler handler = new(reads.Object, uow.Object, sender.Object, payment.Object, raiser.Object);

		// Act
		await handler.Handle(command, ct);

		// Assert
		sender.Verify(x => x.SendQueryAsync(
			It.IsAny<GetUsernameByIdQuery>()
		, ct), Times.Exactly(2));
		sender.Verify(x => x.SendQueryAsync(
			It.IsAny<GetCustomizationCostByIdQuery>()
		, ct), Times.Once);
		sender.Verify(x => x.SendQueryAsync(
			It.IsAny<GetCustomizationWeightByIdQuery>()
		, ct), Times.Once);
	}

	[Fact]
	public async Task Handle_ShouldCallPayment()
	{
		// Arrange
		PurchaseCustomWithDeliveryCommand command = new(
			Id: id,
			Count: default,
			CustomizationId: default,
			PaymentMethodId: string.Empty,
			ShipmentService: string.Empty,
			BuyerId: buyerId,
			Address: address,
			Contact: contact
		);
		PurchaseCustomWithDeliveryHandler handler = new(reads.Object, uow.Object, sender.Object, payment.Object, raiser.Object);

		// Act
		await handler.Handle(command, ct);

		// Assert
		payment.Verify(x => x.InitializePayment(
			It.IsAny<string>(),
			It.IsAny<decimal>(),
			It.IsAny<string>(),
			ct
		), Times.Once);
	}

	[Fact]
	public async Task Handle_ShouldRaiseEvents()
	{
		// Arrange
		PurchaseCustomWithDeliveryCommand command = new(
			Id: id,
			Count: default,
			CustomizationId: default,
			PaymentMethodId: string.Empty,
			ShipmentService: string.Empty,
			BuyerId: buyerId,
			Address: address,
			Contact: contact
		);
		PurchaseCustomWithDeliveryHandler handler = new(reads.Object, uow.Object, sender.Object, payment.Object, raiser.Object);

		// Act
		await handler.Handle(command, ct);

		// Assert
		raiser.Verify(x => x.RaiseDomainEventAsync(
			It.IsAny<CustomDeliveryRequestedDomainEvent>()
		), Times.Once);
	}

	[Fact]
	public async Task Handle_ShouldReturnProperly()
	{
		// Arrange
		PaymentDto expected = new(string.Empty, Message: "Payment Status Message");
		payment.Setup(x => x.InitializePayment(
			It.IsAny<string>(),
			It.IsAny<decimal>(),
			It.IsAny<string>(),
			ct
		)).ReturnsAsync(expected);


		PurchaseCustomWithDeliveryCommand command = new(
			Id: id,
			Count: default,
			CustomizationId: default,
			PaymentMethodId: string.Empty,
			ShipmentService: string.Empty,
			BuyerId: buyerId,
			Address: address,
			Contact: contact
		);
		PurchaseCustomWithDeliveryHandler handler = new(reads.Object, uow.Object, sender.Object, payment.Object, raiser.Object);

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
			Id: id,
			Count: default,
			CustomizationId: default,
			PaymentMethodId: string.Empty,
			ShipmentService: string.Empty,
			BuyerId: wrongBuyerId,
			Address: address,
			Contact: contact
		);
		PurchaseCustomWithDeliveryHandler handler = new(reads.Object, uow.Object, sender.Object, payment.Object, raiser.Object);

		// Assert
		await Assert.ThrowsAsync<CustomAuthorizationException<Custom>>(async () =>
		{
			// Act
			await handler.Handle(command, ct);
		});
	}

	[Fact]
	public async Task Handle_ShouldThrowException_WhenNotAccepted()
	{
		// Arrange
		var custom = CreateCustom(buyerId: buyerId, forDelivery: true);
		reads.Setup(x => x.SingleByIdAsync(id, false, ct))
			.ReturnsAsync(custom);

		PurchaseCustomWithDeliveryCommand command = new(
			Id: id,
			Count: default,
			CustomizationId: default,
			PaymentMethodId: string.Empty,
			ShipmentService: string.Empty,
			BuyerId: buyerId,
			Address: address,
			Contact: contact
		);
		PurchaseCustomWithDeliveryHandler handler = new(reads.Object, uow.Object, sender.Object, payment.Object, raiser.Object);

		// Assert
		await Assert.ThrowsAsync<CustomException>(async () =>
		{
			// Act
			await handler.Handle(command, ct);
		});
	}

	[Fact]
	public async Task Handle_ShouldThrowException_WhenNotFinished()
	{
		// Arrange
		var custom = CreateCustom(buyerId: buyerId, forDelivery: true);
		custom.Accept(ValidDesignerId1);
		reads.Setup(x => x.SingleByIdAsync(id, false, ct))
			.ReturnsAsync(custom);

		PurchaseCustomWithDeliveryCommand command = new(
			Id: id,
			Count: default,
			CustomizationId: default,
			PaymentMethodId: string.Empty,
			ShipmentService: string.Empty,
			BuyerId: buyerId,
			Address: address,
			Contact: contact
		);
		PurchaseCustomWithDeliveryHandler handler = new(reads.Object, uow.Object, sender.Object, payment.Object, raiser.Object);

		// Assert
		await Assert.ThrowsAsync<CustomException>(async () =>
		{
			// Act
			await handler.Handle(command, ct);
		});
	}

	[Fact]
	public async Task Handle_ShouldThrowException_WhenNoForDelivery()
	{
		// Arrange
		reads.Setup(x => x.SingleByIdAsync(id, false, ct))
			.ReturnsAsync(CreateCustom(buyerId: buyerId, forDelivery: false));

		PurchaseCustomWithDeliveryCommand command = new(
			Id: id,
			Count: default,
			CustomizationId: default,
			PaymentMethodId: string.Empty,
			ShipmentService: string.Empty,
			BuyerId: buyerId,
			Address: address,
			Contact: contact
		);
		PurchaseCustomWithDeliveryHandler handler = new(reads.Object, uow.Object, sender.Object, payment.Object, raiser.Object);

		// Assert
		await Assert.ThrowsAsync<CustomException>(async () =>
		{
			// Act
			await handler.Handle(command, ct);
		});
	}

	[Fact]
	public async Task Handle_ShouldThrowException_WhenCustomNotFound()
	{
		// Arrange
		reads.Setup(x => x.SingleByIdAsync(id, false, ct))
			.ReturnsAsync(null as Custom);

		PurchaseCustomWithDeliveryCommand command = new(
			Id: id,
			Count: default,
			CustomizationId: default,
			PaymentMethodId: string.Empty,
			ShipmentService: string.Empty,
			BuyerId: buyerId,
			Address: address,
			Contact: contact
		);
		PurchaseCustomWithDeliveryHandler handler = new(reads.Object, uow.Object, sender.Object, payment.Object, raiser.Object);

		// Assert
		await Assert.ThrowsAsync<CustomNotFoundException<Custom>>(async () =>
		{
			// Act
			await handler.Handle(command, ct);
		});
	}
}
