using CustomCADs.Customs.Application.Customs.Commands.Internal.Client.Create;
using CustomCADs.Customs.Domain.Repositories;
using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.Core.Common.Exceptions.Application;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.UseCases.Accounts.Queries;
using CustomCADs.UnitTests.Customs.Application.Customs.Commands.Internal.Client.Create.Data;

namespace CustomCADs.UnitTests.Customs.Application.Customs.Commands.Internal.Client.Create;

using static CustomsData;

public class CreateCustomHandlerUnitTests : CustomsBaseUnitTests
{
    private readonly Mock<IWrites<Custom>> writes = new();
    private readonly Mock<IUnitOfWork> uow = new();
    private readonly Mock<IRequestSender> sender = new();

    public CreateCustomHandlerUnitTests()
    {
        sender.Setup(x => x.SendQueryAsync(It.IsAny<GetAccountExistsByIdQuery>(), ct))
            .ReturnsAsync(true);
    }

    [Theory]
    [ClassData(typeof(CreateCustomValidData))]
    public async Task Handle_ShouldPersistToDatabase(string name, string description, bool fordelivery, AccountId buyerId)
    {
        // Arrange
        CreateCustomCommand command = new(
            Name: name,
            Description: description,
            ForDelivery: fordelivery,
            BuyerId: buyerId
        );
        CreateCustomHandler handler = new(writes.Object, uow.Object, sender.Object);

        // Act
        await handler.Handle(command, ct);

        // Assert
        writes.Verify(x => x.AddAsync(
            It.Is<Custom>(x =>
            x.Name == name &&
            x.Description == description &&
            x.ForDelivery == fordelivery &&
            x.BuyerId == buyerId
        ), ct), Times.Once);
        uow.Verify(x => x.SaveChangesAsync(ct), Times.Once);
    }

    [Theory]
    [ClassData(typeof(CreateCustomValidData))]
    public async Task Handle_ShouldSendRequests(string name, string description, bool fordelivery, AccountId buyerId)
    {
        // Arrange
        CreateCustomCommand command = new(
            Name: name,
            Description: description,
            ForDelivery: fordelivery,
            BuyerId: buyerId
        );
        CreateCustomHandler handler = new(writes.Object, uow.Object, sender.Object);

        // Act
        await handler.Handle(command, ct);

        // Assert
        sender.Verify(x => x.SendQueryAsync(
            It.Is<GetAccountExistsByIdQuery>(x => x.Id == buyerId)
        , ct), Times.Once);
        uow.Verify(x => x.SaveChangesAsync(ct), Times.Once);
    }

    [Theory]
    [ClassData(typeof(CreateCustomValidData))]
    public async Task Handle_ShouldThrowException_WhenBuyerNotFound(string name, string description, bool fordelivery, AccountId buyerId)
    {
        // Arrange
        sender.Setup(x => x.SendQueryAsync(It.Is<GetAccountExistsByIdQuery>(x => x.Id == buyerId), ct))
            .ReturnsAsync(false);

        CreateCustomCommand command = new(
            Name: name,
            Description: description,
            ForDelivery: fordelivery,
            BuyerId: buyerId
        );
        CreateCustomHandler handler = new(writes.Object, uow.Object, sender.Object);

        // Assert
        await Assert.ThrowsAsync<CustomNotFoundException<Custom>>(async () =>
        {
            // Act
            await handler.Handle(command, ct);
        });
    }
}
