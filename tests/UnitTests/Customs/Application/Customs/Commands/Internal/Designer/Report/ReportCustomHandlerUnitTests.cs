using CustomCADs.Customs.Application.Customs.Commands.Internal.Designer.Report;
using CustomCADs.Customs.Domain.Customs.Enums;
using CustomCADs.Customs.Domain.Repositories;
using CustomCADs.Customs.Domain.Repositories.Reads;
using CustomCADs.Shared.Core.Common.Exceptions.Application;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.UnitTests.Customs.Application.Customs.Commands.Internal.Designer.Report;

using static CustomsData;

public class ReportCustomHandlerUnitTests : CustomsBaseUnitTests
{
    private readonly ReportCustomHandler handler;
    private readonly Mock<ICustomReads> reads = new();
    private readonly Mock<IUnitOfWork> uow = new();

    private static readonly CustomId id = ValidId1;
    private static readonly AccountId designerId = ValidDesignerId1;
    private static readonly AccountId wrongDesignerId = ValidDesignerId2;
    private readonly Custom custom = CreateCustom();

    public ReportCustomHandlerUnitTests()
    {
        handler = new(reads.Object, uow.Object);

        custom.Accept(designerId);
        reads.Setup(x => x.SingleByIdAsync(id, true, ct))
            .ReturnsAsync(custom);
    }

    [Fact]
    public async Task Handle_ShouldQueryDatabase()
    {
        // Arrange
        ReportCustomCommand command = new(
            Id: id,
            DesignerId: designerId
        );

        // Act
        await handler.Handle(command, ct);

        // Assert
        reads.Verify(x => x.SingleByIdAsync(id, true, ct), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldPersistToDatabase()
    {
        // Arrange
        ReportCustomCommand command = new(
            Id: id,
            DesignerId: designerId
        );

        // Act
        await handler.Handle(command, ct);

        // Assert
        uow.Verify(x => x.SaveChangesAsync(ct), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldPopulateProperly()
    {
        // Arrange
        ReportCustomCommand command = new(
            Id: id,
            DesignerId: designerId
        );

        // Act
        await handler.Handle(command, ct);

        // Assert
        Assert.Multiple(
            () => Assert.Equal(designerId, custom.AcceptedCustom?.DesignerId),
            () => Assert.Equal(CustomStatus.Reported, custom.CustomStatus)
        );
    }

    [Fact]
    public async Task Handle_ShouldThrowException_WhenUnauthorizedAccess()
    {
        // Arrange
        ReportCustomCommand command = new(
            Id: id,
            DesignerId: wrongDesignerId
        );

        // Assert
        await Assert.ThrowsAsync<CustomAuthorizationException<Custom>>(async () =>
        {
            // Act
            await handler.Handle(command, ct);
        });
    }

    [Fact]
    public async Task Handle_ShouldThrowException_WhenCustomNotFound()
    {
        // Arrange
        reads.Setup(x => x.SingleByIdAsync(id, true, ct))
            .ReturnsAsync(null as Custom);

        ReportCustomCommand command = new(
            Id: id,
            DesignerId: designerId
        );

        // Assert
        await Assert.ThrowsAsync<CustomNotFoundException<Custom>>(async () =>
        {
            // Act
            await handler.Handle(command, ct);
        });
    }
}
