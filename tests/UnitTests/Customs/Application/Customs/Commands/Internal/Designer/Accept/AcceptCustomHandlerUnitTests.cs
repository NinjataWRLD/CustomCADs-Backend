using CustomCADs.Customs.Application.Customs.Commands.Internal.Designer.Accept;
using CustomCADs.Customs.Domain.Customs.Enums;
using CustomCADs.Customs.Domain.Repositories;
using CustomCADs.Customs.Domain.Repositories.Reads;
using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.Core.Common.Exceptions.Application;
using CustomCADs.Shared.UseCases.Accounts.Queries;

namespace CustomCADs.UnitTests.Customs.Application.Customs.Commands.Internal.Designer.Accept;

using static CustomsData;

public class AcceptCustomHandlerUnitTests : CustomsBaseUnitTests
{
    private readonly AcceptCustomHandler handler;
    private readonly Mock<ICustomReads> reads = new();
    private readonly Mock<IUnitOfWork> uow = new();
    private readonly Mock<IRequestSender> sender = new();

    private readonly Custom custom = CreateCustom();

    public AcceptCustomHandlerUnitTests()
    {
        handler = new(reads.Object, uow.Object, sender.Object);

        reads.Setup(x => x.SingleByIdAsync(ValidId, true, ct))
            .ReturnsAsync(custom);

        sender.Setup(x => x.SendQueryAsync(
            It.Is<GetAccountExistsByIdQuery>(x => x.Id == ValidDesignerId),
            ct
        )).ReturnsAsync(true);
    }

    [Fact]
    public async Task Handle_ShouldQueryDatabase()
    {
        // Arrange
        AcceptCustomCommand command = new(
            Id: ValidId,
            DesignerId: ValidDesignerId
        );

        // Act
        await handler.Handle(command, ct);

        // Assert
        reads.Verify(x => x.SingleByIdAsync(ValidId, true, ct), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldPersistToDatabase()
    {
        // Arrange
        AcceptCustomCommand command = new(
            Id: ValidId,
            DesignerId: ValidDesignerId
        );

        // Act
        await handler.Handle(command, ct);

        // Assert
        uow.Verify(x => x.SaveChangesAsync(ct), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldSendRequests()
    {
        // Arrange
        AcceptCustomCommand command = new(
            Id: ValidId,
            DesignerId: ValidDesignerId
        );

        // Act
        await handler.Handle(command, ct);

        // Assert
        sender.Verify(x => x.SendQueryAsync(
            It.Is<GetAccountExistsByIdQuery>(x => x.Id == ValidDesignerId),
            ct
        ), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldPopulateProperly()
    {
        // Arrange
        AcceptCustomCommand command = new(
            Id: ValidId,
            DesignerId: ValidDesignerId
        );

        // Act
        await handler.Handle(command, ct);

        // Assert
        Assert.Multiple(
            () => Assert.Equal(ValidDesignerId, custom.AcceptedCustom?.DesignerId),
            () => Assert.Equal(CustomStatus.Accepted, custom.CustomStatus)
        );
    }

    [Fact]
    public async Task Handle_ShouldThrowException_WhenDesignerNotFound()
    {
        // Arrange
        sender.Setup(x => x.SendQueryAsync(
            It.Is<GetAccountExistsByIdQuery>(x => x.Id == ValidDesignerId),
            ct
        )).ReturnsAsync(false);

        AcceptCustomCommand command = new(
            Id: ValidId,
            DesignerId: ValidDesignerId
        );

        // Assert
        await Assert.ThrowsAsync<CustomNotFoundException<Custom>>(async () =>
        {
            // Act
            await handler.Handle(command, ct);
        });
    }

    [Fact]
    public async Task Handle_ShouldThrowException_WhenCustomNotFound()
    {
        // Arrange
        reads.Setup(x => x.SingleByIdAsync(ValidId, true, ct))
            .ReturnsAsync(null as Custom);

        AcceptCustomCommand command = new(
            Id: ValidId,
            DesignerId: ValidDesignerId
        );

        // Assert
        await Assert.ThrowsAsync<CustomNotFoundException<Custom>>(async () =>
        {
            // Act
            await handler.Handle(command, ct);
        });
    }
}
