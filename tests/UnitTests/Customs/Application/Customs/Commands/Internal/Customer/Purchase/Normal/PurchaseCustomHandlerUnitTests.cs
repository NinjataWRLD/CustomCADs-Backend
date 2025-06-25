using CustomCADs.Customs.Application.Customs.Commands.Internal.Customers.Purchase.Normal;
using CustomCADs.Customs.Domain.Repositories;
using CustomCADs.Customs.Domain.Repositories.Reads;
using CustomCADs.Shared.Abstractions.Payment;
using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.Core.Common.Exceptions.Application;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.UseCases.Accounts.Queries;
using CustomCADs.Shared.UseCases.Cads.Queries;

namespace CustomCADs.UnitTests.Customs.Application.Customs.Commands.Internal.Customer.Purchase.Normal;

using static CustomsData;

public class PurchaseCustomHandlerUnitTests : CustomsBaseUnitTests
{
	private readonly PurchaseCustomHandler handler;
	private readonly Mock<ICustomReads> reads = new();
	private readonly Mock<IUnitOfWork> uow = new();
	private readonly Mock<IRequestSender> sender = new();
	private readonly Mock<IPaymentService> payment = new();

	private static readonly AccountId buyerId = AccountId.New();
	private const string PaymentMethodId = "payment-method-id";
	private readonly Custom custom = CreateCustom(
		buyerId: ValidBuyerId
	);

	public PurchaseCustomHandlerUnitTests()
	{
		handler = new(reads.Object, uow.Object, sender.Object, payment.Object);

		custom.Accept(ValidDesignerId);
		custom.Begin();
		custom.Finish(ValidCadId, ValidPrice);

		reads.Setup(x => x.SingleByIdAsync(ValidId, false, ct))
			.ReturnsAsync(custom);

		sender.Setup(x => x.SendQueryAsync(
			It.Is<GetCadExistsByIdQuery>(x => x.Id == ValidCadId),
			ct
		)).ReturnsAsync(true);
	}

	[Fact]
	public async Task Handle_ShouldQueryDatabase()
	{
		// Arrange
		PurchaseCustomCommand command = new(ValidId, PaymentMethodId, ValidBuyerId);

		// Act
		await handler.Handle(command, ct);

		// Assert
		reads.Verify(x => x.SingleByIdAsync(ValidId, false, ct), Times.Once);
	}

	[Fact]
	public async Task Handle_ShouldSendRequests()
	{
		// Arrange
		PurchaseCustomCommand command = new(ValidId, PaymentMethodId, ValidBuyerId);

		// Act
		await handler.Handle(command, ct);

		// Assert
		sender.Verify(x => x.SendQueryAsync(
			It.Is<GetUsernameByIdQuery>(x => x.Id == ValidBuyerId || x.Id == ValidDesignerId),
			ct
		), Times.Exactly(2));
	}

	[Fact]
	public async Task Handle_ShouldCallPayment()
	{
		// Arrange
		PurchaseCustomCommand command = new(ValidId, PaymentMethodId, ValidBuyerId);

		// Act
		await handler.Handle(command, ct);

		// Assert
		payment.Verify(x => x.InitializeCustomPayment(
			It.Is<string>(x => x == PaymentMethodId),
			It.Is<AccountId>(x => x == custom.BuyerId),
			It.Is<CustomId>(x => x == custom.Id),
			It.Is<decimal>(x => x == ValidPrice),
			It.Is<string>(x => x.Contains(custom.Name)),
			ct
		), Times.Once);
	}

	[Fact]
	public async Task Handle_ShouldReturnResult()
	{
		// Arrange
		PaymentDto expected = new(string.Empty, Message: "Payment Status Message");
		payment.Setup(x => x.InitializeCustomPayment(
			It.Is<string>(x => x == PaymentMethodId),
			It.Is<AccountId>(x => x == custom.BuyerId),
			It.Is<CustomId>(x => x == custom.Id),
			It.Is<decimal>(x => x == ValidPrice),
			It.Is<string>(x => x.Contains(custom.Name)),
			ct
		)).ReturnsAsync(expected);
		PurchaseCustomCommand command = new(ValidId, PaymentMethodId, ValidBuyerId);

		// Act
		PaymentDto actual = await handler.Handle(command, ct);

		// Assert
		Assert.Equal(expected, actual);
	}

	[Fact]
	public async Task Handle_ShouldThrowException_WhenUnauthorizedAccess()
	{
		// Arrange
		PurchaseCustomCommand command = new(ValidId, string.Empty, buyerId);

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
		var custom = CreateCustom(buyerId: ValidBuyerId);
		reads.Setup(x => x.SingleByIdAsync(ValidId, false, ct))
			.ReturnsAsync(custom);
		PurchaseCustomCommand command = new(ValidId, PaymentMethodId, ValidBuyerId);

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
		var custom = CreateCustom(buyerId: ValidBuyerId);
		custom.Accept(ValidDesignerId);
		reads.Setup(x => x.SingleByIdAsync(ValidId, false, ct))
			.ReturnsAsync(custom);
		PurchaseCustomCommand command = new(ValidId, PaymentMethodId, ValidBuyerId);

		// Assert
		await Assert.ThrowsAsync<CustomException>(
			// Act
			async () => await handler.Handle(command, ct)
		);
	}

	[Fact]
	public async Task Handle_ShouldThrowException_WhenForDelivery()
	{
		// Arrange
		var custom = CreateCustom(buyerId: ValidBuyerId, forDelivery: true);
		reads.Setup(x => x.SingleByIdAsync(ValidId, false, ct))
			.ReturnsAsync(custom);
		PurchaseCustomCommand command = new(ValidId, PaymentMethodId, ValidBuyerId);

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
		PurchaseCustomCommand command = new(ValidId, PaymentMethodId, ValidBuyerId);

		// Assert
		await Assert.ThrowsAsync<CustomNotFoundException<Custom>>(
			// Act
			async () => await handler.Handle(command, ct)
		);
	}
}
