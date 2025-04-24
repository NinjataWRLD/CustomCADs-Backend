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

    private static readonly CustomId id = ValidId1;
    private static readonly AccountId buyerId = ValidBuyerId1;
    private static readonly AccountId wrongBuyerId = ValidBuyerId2;
    private readonly Custom custom = CreateCustom(
        buyerId: buyerId
    );

    public PurchaseCustomHandlerUnitTests()
    {
        handler = new(reads.Object, uow.Object, sender.Object, payment.Object);

        custom.Accept(ValidDesignerId1);
        custom.Begin();
        custom.Finish(ValidCadId1, ValidPrice1);

        reads.Setup(x => x.SingleByIdAsync(id, false, ct))
            .ReturnsAsync(custom);

        sender.Setup(x => x.SendQueryAsync(
            It.Is<GetCadExistsByIdQuery>(x => x.Id == ValidCadId1),
            ct
        )).ReturnsAsync(true);
    }

    [Fact]
    public async Task Handle_ShouldQueryDatabase()
    {
        // Arrange
        PurchaseCustomCommand command = new(id, string.Empty, buyerId);

        // Act
        await handler.Handle(command, ct);

        // Assert
        reads.Verify(x => x.SingleByIdAsync(id, false, ct), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldSendRequests()
    {
        // Arrange
        PurchaseCustomCommand command = new(id, string.Empty, buyerId);

        // Act
        await handler.Handle(command, ct);

        // Assert
        sender.Verify(x => x.SendQueryAsync(
            It.Is<GetUsernameByIdQuery>(x => x.Id == buyerId || x.Id == ValidDesignerId1),
            ct
        ), Times.Exactly(2));
    }

    [Fact]
    public async Task Handle_ShouldCallPayment()
    {
        // Arrange
        PurchaseCustomCommand command = new(id, string.Empty, buyerId);

        // Act
        await handler.Handle(command, ct);

        // Assert
        payment.Verify(x => x.InitializePayment(
            It.Is<string>(x => string.IsNullOrEmpty(x)),
            It.Is<decimal>(x => x == ValidPrice1),
            It.Is<string>(x => x.Contains(custom.Name)),
            ct
        ), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnProperly()
    {
        // Arrange
        const string expected = "Payment Status Message";
        payment.Setup(x => x.InitializePayment(
            It.Is<string>(x => string.IsNullOrEmpty(x)),
            It.Is<decimal>(x => x == ValidPrice1),
            It.Is<string>(x => x.Contains(custom.Name)),
            ct
        )).ReturnsAsync(expected);
        PurchaseCustomCommand command = new(id, string.Empty, buyerId);

        // Act
        string actual = await handler.Handle(command, ct);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public async Task Handle_ShouldThrowException_WhenUnauthorizedAccess()
    {
        // Arrange
        PurchaseCustomCommand command = new(id, string.Empty, wrongBuyerId);

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
        var custom = CreateCustom(buyerId: buyerId);
        reads.Setup(x => x.SingleByIdAsync(id, false, ct))
            .ReturnsAsync(custom);
        PurchaseCustomCommand command = new(id, string.Empty, buyerId);

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
        var custom = CreateCustom(buyerId: buyerId);
        custom.Accept(ValidDesignerId1);
        reads.Setup(x => x.SingleByIdAsync(id, false, ct))
            .ReturnsAsync(custom);
        PurchaseCustomCommand command = new(id, string.Empty, buyerId);

        // Assert
        await Assert.ThrowsAsync<CustomException>(async () =>
        {
            // Act
            await handler.Handle(command, ct);
        });
    }

    [Fact]
    public async Task Handle_ShouldThrowException_WhenForDelivery()
    {
        // Arrange
        var custom = CreateCustom(buyerId: buyerId, forDelivery: true);
        reads.Setup(x => x.SingleByIdAsync(id, false, ct))
            .ReturnsAsync(custom);
        PurchaseCustomCommand command = new(id, string.Empty, buyerId);

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
        PurchaseCustomCommand command = new(id, string.Empty, buyerId);

        // Assert
        await Assert.ThrowsAsync<CustomNotFoundException<Custom>>(async () =>
        {
            // Act
            await handler.Handle(command, ct);
        });
    }
}
