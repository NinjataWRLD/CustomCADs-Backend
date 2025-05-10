using CustomCADs.Categories.Application.Categories.Commands.Internal.Delete;
using CustomCADs.Categories.Domain.Categories.Events;
using CustomCADs.Categories.Domain.Repositories;
using CustomCADs.Categories.Domain.Repositories.Reads;
using CustomCADs.Shared.Abstractions.Events;
using CustomCADs.Shared.Core.Common.Exceptions.Application;

namespace CustomCADs.UnitTests.Categories.Application.Categories.Commands.Internal.Delete;

using static CategoriesData;

public class DeleteCategoryHandlerUnitTests : CategoriesBaseUnitTests
{
    private readonly DeleteCategoryHandler handler;
    private readonly Mock<IEventRaiser> raiser = new();
    private readonly Mock<IUnitOfWork> uow = new();
    private readonly Mock<IWrites<Category>> writes = new();
    private readonly Mock<ICategoryReads> reads = new();

    public DeleteCategoryHandlerUnitTests()
    {
        handler = new(reads.Object, writes.Object, uow.Object, raiser.Object);

        reads.Setup(v => v.SingleByIdAsync(ValidId, true, ct))
            .ReturnsAsync(CreateCategory(ValidId, ValidName1, ValidDescription1));
    }

    [Fact]
    public async Task Handler_ShouldQueryDatabase()
    {
        // Arrange
        DeleteCategoryCommand command = new(ValidId);

        // Act
        await handler.Handle(command, ct);

        // Assert
        reads.Verify(v => v.SingleByIdAsync(ValidId, true, ct), Times.Once());
    }

    [Fact]
    public async Task Handler_ShouldPersistToDatabase_WhenCategoryFound()
    {
        // Arrange
        DeleteCategoryCommand command = new(ValidId);

        // Act
        await handler.Handle(command, ct);

        // Assert
        writes.Verify(v => v.Remove(
            It.Is<Category>(x => x.Id == ValidId)
        ), Times.Once());
        uow.Verify(v => v.SaveChangesAsync(ct), Times.Once());
    }

    [Fact]
    public async Task Handler_ShouldRaiseEvents()
    {
        // Arrange
        DeleteCategoryCommand command = new(ValidId);

        // Act
        await handler.Handle(command, ct);

        // Assert
        raiser.Verify(v => v.RaiseDomainEventAsync(
            It.Is<CategoryDeletedDomainEvent>(x => x.Id == ValidId)
        ), Times.Once());
    }

    [Fact]
    public async Task Handle_ShouldThrowException_WhenCategoryNotFound()
    {
        // Arrange
        reads.Setup(v => v.SingleByIdAsync(ValidId, true, ct)).ReturnsAsync(null as Category);
        DeleteCategoryCommand command = new(ValidId);

        // Assert
        await Assert.ThrowsAsync<CustomNotFoundException<Category>>(async () =>
        {
            // Act
            await handler.Handle(command, ct);
        });
    }
}
